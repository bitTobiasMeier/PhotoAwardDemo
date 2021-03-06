﻿using System; 
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json.Bson;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.PhotoDb.Interfaces;
using PhotoAward.ThumbnailService.Interfaces;

namespace PhotoAward.PhotoActors
{
    
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    public class PhotoActor : Actor, IPhotoActor, IRemindable
    {
        private const string DataKey = "memberData";
        private const string CheckMemberReminderName = "keinMitglied";
        private const string CheckPictureAnalysis = "CheckPictureAnalysis";
        private IActorReminder _reminderPictureAnalysis;
        private readonly IAnalyzeRepository _analyzeRepository;
        private readonly IPhotoDbService _photoDbService;

        /// <summary>
        /// Initializes a new instance of PhotoActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        /// <param name="analyzeRepository"></param>
        /// <param name="photoDbService"></param>
        public PhotoActor(ActorService actorService, ActorId actorId, IAnalyzeRepository analyzeRepository, IPhotoDbService photoDbService) : base(actorService, actorId)
        {
            this._analyzeRepository = analyzeRepository;
            _photoDbService = photoDbService;
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override async Task OnActivateAsync()
        {
            PhotoActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization 

            IActorReminder reminderRegistration = await this.RegisterReminderAsync(
                CheckMemberReminderName,
                null,
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(10));

            this._reminderPictureAnalysis  = await this.RegisterReminderAsync(
                CheckPictureAnalysis,
                null,
                TimeSpan.FromMinutes(0.001),
                TimeSpan.FromMinutes(0.75));

        }

        public async Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
        {
            if (reminderName.Equals(CheckMemberReminderName))
            {
                await ReplaceImageAsync();
            }
            if (reminderName.Equals(CheckPictureAnalysis))
            {
                await AnalyzePictureAsync();
                await this.UnregisterReminderAsync(this._reminderPictureAnalysis);
            }
        }

        private async Task ReplaceImageAsync()
        {
            var thumbnailClient = new ThumbnailClientFactory().CreateThumbnailClient();
            var dir = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
            var filename = System.IO.Path.Combine(dir, "PackageRoot", "Data", "bitLogo.gif");
            var imgdata = System.IO.File.ReadAllBytes(filename);
            var thumbnail = await thumbnailClient.GetThumbnailAsync(imgdata);
            var photo = await GetPhotoAsync(CancellationToken.None);
            photo.ThumbnailBytes = thumbnail;
            photo.Title = "War nur 5 Minuten sichtbar";
            photo.Filename = "Logo";
            await this.SetPhotoAsync(photo, CancellationToken.None);
            await this._photoDbService.ReplacePhotoAsync(photo.Id.Value.ToString(), photo.ThumbnailBytes);
            System.Console.WriteLine("Bild wurde ersetzt!");
            var reminder = this.GetReminder(CheckMemberReminderName);
            await this.UnregisterReminderAsync(reminder);
        }

        private async Task AnalyzePictureAsync()
        {
            var photo = await this.GetPhotoDataAsync(CancellationToken.None);
            var description = await this._analyzeRepository.AnalyzeImageAsync(photo.ThumbnailAsByte);
            photo.Description = description;
            await this.StateManager.SetStateAsync(DataKey, photo, CancellationToken.None);
        }


        public async Task<PhotoInfo> SetPhotoAsync(PhotoInfo photo, CancellationToken cancellationToken)
        {
            var datahelper = await this.StateManager.TryGetStateAsync<PhotoData>(DataKey, cancellationToken);
            PhotoData data;
            if (!datahelper.HasValue)
            {
                data = new PhotoData() { Comments = { }, Id = photo.Id.Value};
            }
            else
            {
                data = datahelper.Value;
            }
            data.UploadDate = DateTime.Now;
            data.ThumbnailAsByte = photo.ThumbnailBytes;
            data.FileName = photo.Filename;
            data.Title = photo.Title;
            data.OwnerEmail = photo.OwnerEmail;
            
            await this.StateManager.SetStateAsync(DataKey, data, cancellationToken);
            photo.UploadDate = data.UploadDate;
            return photo;
        }

        public async Task<PhotoInfo> GetPhotoAsync(CancellationToken cancellationToken)
        {
            var data = await GetPhotoDataAsync(cancellationToken);
            return new PhotoInfo()
            {
                Id = data.Id,
                Filename= data.FileName,
                ThumbnailBytes = data.ThumbnailAsByte,
                UploadDate = data.UploadDate,
                Title = data.Title,
                Description = data.Description
            };
        }

        private async Task<PhotoData> GetPhotoDataAsync(CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            return data;
        }

        public async Task<CommentInfo> AddCommentAsync(CommentInfo comment, CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            if (comment.Id != null) throw new Exception("Kommentar exisitiert bereits");
            var photoComment = new PhotoComment()
            {
                AuthorId = comment.AuthorId,
                Comment = comment.Comment,
                CommentDate = comment.CommentDate,
                Id = Guid.NewGuid()
            };
            data.Comments.Add(photoComment);
            comment.Id = photoComment.Id;
            await this.StateManager.SetStateAsync(DataKey, data, cancellationToken);
            return comment;
        }

        public async Task<List<CommentInfo>> GetCommentsAsync(CancellationToken cancellationToken)
        {
            var photo = await this.GetPhotoDataAsync(cancellationToken);
            
            return
                photo.Comments.OrderByDescending(c=>c.CommentDate).Select(
                    c =>
                        new CommentInfo()
                        {
                            AuthorId = c.AuthorId,
                            Comment = c.Comment,
                            Id = c.Id,
                            PhotoId = photo.Id,
                            CommentDate = c.CommentDate
                        }).ToList();
        }

        public async Task DeleteAsync(CancellationToken cancellationToken)
        {
            var svc = (IActorService) this.ActorService;
            await svc.DeleteActorAsync(this.Id, cancellationToken);
        }

        

    }



    
}
