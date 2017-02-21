using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json.Bson;
using PhotoAward.PhotoActors.Interfaces;
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
        private const string checkMemberReminderName = "keinMitglied";

        /// <summary>
        /// Initializes a new instance of PhotoActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public PhotoActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override async Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            IActorReminder reminderRegistration = await this.RegisterReminderAsync(
                checkMemberReminderName,
                null,
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(15));
            
        }

        public async Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
        {
            if (reminderName.Equals(checkMemberReminderName))
            {
                var thumbnailClient = new ThumbnailClientFactory().CreateThumbnailClient();
                var dir =  System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
                var filename = System.IO.Path.Combine(dir,"PackageRoot","Data", "bitLogo.gif");
                var imgdata = System.IO.File.ReadAllBytes(filename);
                var thumbnail = await thumbnailClient.GetThumbnail(imgdata);
                var photo = await GetPhoto(CancellationToken.None);
                photo.ThumbnailBytes = thumbnail;
                photo.Title = "War nur 5 Minuten sichtbar";
                photo.Filename = "Logo";
                await this.SetPhoto(photo, CancellationToken.None);
                System.Console.WriteLine("Bild wurde ersetzt!");
                var reminder = this.GetReminder(checkMemberReminderName);
                await this.UnregisterReminderAsync(reminder);
            }
        }

        public async Task<PhotoInfo> SetPhoto(PhotoInfo photo, CancellationToken cancellationToken)
        {
            var datahelper = await this.StateManager.TryGetStateAsync<PhotoData>(DataKey, cancellationToken);
            PhotoData data;
            if (!datahelper.HasValue)
            {
                data = new PhotoData() { Comments = { }, Id = Guid.NewGuid()};
            }
            else
            {
                data = datahelper.Value;
            }
            data.UploadDate = DateTime.Now;
            data.ThumbnailAsByte = photo.ThumbnailBytes;
            data.FileName = photo.Filename;
            data.Title = photo.Title;
            
            await this.StateManager.SetStateAsync(DataKey, data, cancellationToken);
            photo.UploadDate = data.UploadDate;
            return photo;
        }

        public async Task<PhotoInfo> GetPhoto(CancellationToken cancellationToken)
        {
            var data = await GetPhotoData(cancellationToken);
            return new PhotoInfo()
            {
                Id = data.Id,
                Filename= data.FileName,
                ThumbnailBytes = data.ThumbnailAsByte,
                UploadDate = data.UploadDate,
                Title = data.Title
            };
        }

        private async Task<PhotoData> GetPhotoData(CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            return data;
        }

        public async Task<CommentInfo> AddComment(CommentInfo comment, CancellationToken cancellationToken)
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

        public async Task<List<CommentInfo>> GetComments(CancellationToken cancellationToken)
        {
            var photo = await this.GetPhotoData(cancellationToken);
            
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

        public async Task Delete(CancellationToken cancellationToken)
        {
            var svc = (IActorService) this.ActorService;
            await svc.DeleteActorAsync(this.Id, cancellationToken);
        }

        

    }



    
}
