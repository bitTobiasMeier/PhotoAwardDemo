﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PhotoAward.PhotoActor.Interfaces;

namespace PhotoAward.PhotoActor
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
    internal class PhotoActor : Actor, IPhotoActor
    {
        private const string DataKey = "memberData";

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
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
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
            

            //var data = new MemberData();
            await this.StateManager.AddOrUpdateStateAsync(DataKey, data, (key, value) => data, cancellationToken);
            photo.UploadDate = data.UploadDate;
            return photo;
        }

        public async Task<PhotoInfo> GetPhoto(CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            return new PhotoInfo()
            {
                Id = data.Id,
                Filename= data.FileName,
                ThumbnailBytes = data.ThumbnailAsByte,
                UploadDate = data.UploadDate,
                Title = data.Title
            };
        }

        public async Task<CommentInfo> AddComment(CommentInfo comment, CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            if (comment.Id != null) throw new Exception("Kommentar exisitiert bereits");
            var photoComment = new PhotoComment()
            {
                ActorId = comment.ActorId,
                Comment = comment.Comment,
                CommentDate = comment.CommentDate,
                Id = Guid.NewGuid()
            };
            data.Comments.Add(photoComment);
            comment.Id = photoComment.Id;
            return comment;
        }

        public async Task<List<CommentInfo>> GetComments(CancellationToken cancellationToken)
        {
            var data = await this.StateManager.GetStateAsync<PhotoData>(DataKey, cancellationToken);
            return
                data.Comments.OrderByDescending(c=>c.CommentDate).Select(
                    c =>
                        new CommentInfo()
                        {
                            ActorId = c.ActorId,
                            Comment = c.Comment,
                            Id = c.Id,
                            CommentDate = c.CommentDate
                        }).ToList();
        }
    }
}