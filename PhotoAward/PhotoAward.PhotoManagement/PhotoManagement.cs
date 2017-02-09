using System;
using System.Collections.Generic;
using System.Drawing;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoActor.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.PhotoManagement
{
    public class TestableStatefullService : StatefulService
    {
        public TestableStatefullService(StatefulServiceContext serviceContext) : base(serviceContext)
        {
        }

        public TestableStatefullService(StatefulServiceContext serviceContext, IReliableStateManagerReplica reliableStateManagerReplica) : base(serviceContext, reliableStateManagerReplica)
        {
            
        }

        //StateManager verstecken, so dass immer der Wrapper verwendet wird.
        protected new object StateManager => null;
    }

    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    public sealed class PhotoManagement : TestableStatefullService, IPhotoComments, IPhotoManagement
    {
        private readonly IPhotoManagementStates _photoManagementStates;
        private readonly IMemberManagementClientFactory _memberManagementClientFactory;
        private readonly IPhotoActorClientFactory _photoActorClientFactory;
        private readonly IThumbnailCreator _thumbnailCreator;
        
        private const string PhotoActorMemberDictName = "photoActorMemberDictionary";

        public PhotoManagement(StatefulServiceContext context, IPhotoManagementStates photoManagementStates, IReliableStateManagerReplica stateManager, 
            IMemberManagementClientFactory memberManagementClientFactory, IPhotoActorClientFactory photoActorClientFactory, IThumbnailCreator thumbnailCreator)
            : base(context, stateManager)
        {
            _photoManagementStates = photoManagementStates;
            _memberManagementClientFactory = memberManagementClientFactory;
            _photoActorClientFactory = photoActorClientFactory;
            _thumbnailCreator = thumbnailCreator;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
             {
                new ServiceReplicaListener(
                    this.CreateServiceRemotingListener
                )
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                /*
                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }*/

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public async Task<PhotoManagementData> AddPhoto(PhotoUploadData photo)
        {
            try
            {
                using (var tx = this._photoManagementStates.CreateTransaction())
                {
                    var thumbnailTask = this._thumbnailCreator.GetThumbnail(photo.Data);
                    //Member mit dieser Emailadresse ermitteln
                    var member = this._memberManagementClientFactory.CreateMemberManagementClient().GetMember(photo.Email);
                    if (member?.Result == null) throw new Exception("Member not found");

                    var photoId= Guid.NewGuid();
                    var photoActorId = ActorId.CreateRandom();
                    //await photoActorState.AddAsync(tx, photoId.ToString(), photoActorId);
                    await this._photoManagementStates.AddPhotoIdActorMapping(tx,photoId,photoActorId);

                    //Dateiname ermitteln
                    var filename = photo.FileName;
                    filename = System.IO.Path.GetFileName(filename);
                    var thumbnail = await thumbnailTask;
                    //Bild hochladen
                    var data = new PhotoInfo()
                    {
                        Filename = filename,
                        //ToDo: Create Thumbnail asynchron
                        ThumbnailBytes = thumbnail,
                        Title = photo.Title,
                        Id = photoId
                    };
                    var client = this._photoActorClientFactory.CreateClient(photoActorId);
                    var result = await client.SetPhoto(data, CancellationToken.None);


                    //Photo einem Member zuordnen

                    /*var photoMemberActorDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
                    var conditionalActorList = await photoMemberActorDictionary.TryGetValueAsync(tx, member.Result.Id.ToString());
                    var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
                    actorList.Add(photoActorId);
                    await photoMemberActorDictionary.SetAsync(tx, member.Result.Id.ToString(), actorList);*/


                    await this._photoManagementStates.AddPhotoToMember(tx, member.Result.Id, photoActorId);

                    await tx.CommitAsync();

                    return new PhotoManagementData()
                    {
                        FileName = photo.FileName,
                        Id = result.Id,
                        Title = result.Title,
                        ThumbnailBytes = result.ThumbnailBytes
                    };

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PhotoManagementData> GetPhoto(Guid id)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    var actorId = await GetPhotoActorId(id, tx);
                    var client = _photoActorClientFactory.CreateClient(actorId.Value);
                    var result = await client.GetPhoto(CancellationToken.None);
                    return new PhotoManagementData()
                    {
                        FileName = result.Filename,
                        Id = result.Id,
                        Title = result.Title,
                        ThumbnailBytes = result.ThumbnailBytes
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private async Task<ConditionalValue<ActorId>> GetPhotoActorId(Guid id, ITransaction tx)
        {
            //Actor für dieses Photo
            var actorId = await this._photoManagementStates.GetPhotoActorId(tx, id);
            if (!actorId.HasValue)
            {
                throw new Exception("Actor for image not found!");
            }
            return actorId;
        }

        public async Task<List<PhotoManagementData>> GetPhotos(string email)
        {
            using (var tx = _photoManagementStates.CreateTransaction())
            {
                //Member mit dieser Emailadresse ermitteln
                var member = this._memberManagementClientFactory.CreateMemberManagementClient().GetMember(email);
                if (member?.Result == null) throw new Exception("Member not found");

                var actorList =  await this._photoManagementStates.GetPhotoActorIdListOfMember(tx, member.Result.Id);
                var photoList = new List<PhotoManagementData>();
                //Gibt es bereits einen Actor für dieses Photo. Wenn ja, Exception ...
                foreach (ActorId imageActorId in actorList.AsParallel())
                {
                    var client = _photoActorClientFactory.CreateClient(imageActorId);
                    var result = await client.GetPhoto(CancellationToken.None);
                    photoList.Add(new PhotoManagementData()
                    {
                        FileName = result.Filename,
                        Id = result.Id,
                        Title = result.Title,
                        ThumbnailBytes = result.ThumbnailBytes
                    });
                }
                return photoList;

            }
        }

        public async Task<List<CommentData>> GetComments(Guid photoId)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    //photoActor ermitteln
                    var actorId = await GetPhotoActorId(photoId, tx);
                    var client = _photoActorClientFactory.CreateClient(actorId.Value);
                    var infos = await client.GetComments( CancellationToken.None);
                    return infos.Select(i => new CommentData()
                    {
                        AuthorId = i.AuthorId,
                        Id = i.Id,
                        PhotoId = i.PhotoId,
                        Comment = i.Comment,
                        CommentDate = i.CommentDate
                    }).ToList();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<CommentData> AddComment(CommentUploadData comment)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    
                    //Member mit dieser Emailadresse ermitteln
                    var memberdto = this._memberManagementClientFactory.CreateMemberManagementClient().GetMember(comment.Email);
                    if (memberdto?.Result == null) throw new Exception("Member not found");
                    var member = memberdto.Result;

                    //photoActor ermitteln
                    var actorId = await GetPhotoActorId(comment.PhotoId, tx);
                    var client = _photoActorClientFactory.CreateClient(actorId.Value);
                    var ci = await client.AddComment(new CommentInfo()
                    {
                        AuthorId = member.Id,
                        PhotoId = comment.PhotoId,
                        Comment = comment.Comment,
                        CommentDate = comment.CreateDate
                    }, CancellationToken.None);

                    await tx.CommitAsync();

                    return new CommentData()
                     {
                         AuthorId = member.Id,
                         PhotoId = comment.PhotoId,
                         CommentDate = comment.CreateDate,
                         Comment = ci.Comment,
                         Id = ci.Id
                     };
                  
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
