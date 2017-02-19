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
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.PhotoManagement
{
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
                    var thumbnail = await thumbnailTask;
                    //Member mit dieser Emailadresse ermitteln
                    var member = await this._memberManagementClientFactory.CreateMemberManagementClient().GetMember(photo.Email);
                    if (member == null) throw new Exception("Member not found");

                    var photoId= Guid.NewGuid();
                    var photoActorId = ActorId.CreateRandom();
                    //await photoActorState.AddAsync(tx, photoId.ToString(), photoActorId);
                    await this._photoManagementStates.AddPhotoIdActorMapping(tx,photoId,photoActorId);

                    //Dateiname ermitteln
                    var filename = photo.FileName;
                    filename = System.IO.Path.GetFileName(filename);
                    
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

                    await this._photoManagementStates.AddPhotoActorToMemberId(tx, member.Id, photoActorId);
                    await tx.CommitAsync();

                    ServiceEventSource.Current.ServiceMessage(this.Context,
                                "Photo hinzugefügt von {0}, Photo Id: {1} ", photo.Email, result.Id);

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

        public async Task<List<PhotoMemberInfo>> GetListOfPhotos()
        {
            using (var tx = _photoManagementStates.CreateTransaction())
            {
                var memberManagementClient = this._memberManagementClientFactory.CreateMemberManagementClient();
                var tuples = await this._photoManagementStates.GetPhotos(tx);
                var photoList = new List<PhotoMemberInfo>();
                foreach (var tuple in tuples)
                {
                    try
                    {
                        var memberId = tuple.Item1;
                        var imageActorId = tuple.Item2;
                        var member = await memberManagementClient.GetMemberOnMemberId(memberId);
                        if (member == null) throw new Exception("Member not found");


                        try
                        {
                            var client = _photoActorClientFactory.CreateClient(imageActorId);
                            var photo = await client.GetPhoto(CancellationToken.None);
                            photoList.Add(new PhotoMemberInfo()
                            {
                                FileName = photo.Filename,
                                PhotoId = photo.Id,
                                Title = photo.Title,
                                Email = member.Email
                            });
                        }
                        catch (Exception ex)
                        {
                            ServiceEventSource.Current.ServiceMessage(this.Context,
                                "GetListOfPhotos: Actor: {0}: Error: {1}", imageActorId, ex.Message);
                        }
                    }
                    catch (Exception ex2)
                    {
                        ServiceEventSource.Current.ServiceMessage(this.Context,
                                "GetListOfPhotos: Error: {1}", ex2.Message);
                    }
                }
                return photoList;
            }
        }

        public async Task DeletePhoto(Guid photoId)
        {
            using (var tx = _photoManagementStates.CreateTransaction())
            {
                var imageActorId = await this.GetPhotoActorId(photoId, tx);
                if (imageActorId.HasValue)
                {
                    var client = _photoActorClientFactory.CreateClient(imageActorId.Value);
                    await client.Delete(CancellationToken.None);
                    await this._photoManagementStates.RemoveActor(photoId, imageActorId.Value, tx);
                }
            }
        }

        public async Task BackupPhotos()
        {
            var proxy = new PhotoActorServiceProxy().Create(0);
            await proxy.BackupActorsAsync();
        }

        public async Task Restore()
        {
            try
            {
                // Create a unique operation id for the command below
                Guid operationId = Guid.NewGuid();

                // Note: Use the appropriate overload for your configuration
                FabricClient fabricClient = new FabricClient();

                // The name of the target service
                Uri targetServiceName = new Uri("fabric:/PhotoAward/PhotoActorService");

                // The id of the target partition inside the target service
                /*Guid targetPartitionId = this.Partition.PartitionInfo.Id;

                PartitionSelector partitionSelector = PartitionSelector.PartitionIdOf(targetServiceName,
                    targetPartitionId);*/
                PartitionSelector partitionSelector = PartitionSelector.RandomOf(targetServiceName);

                // Start the command.  Retry OperationCanceledException and all FabricTransientException's.  Note when StartPartitionDataLossAsync completes
                // successfully it only means the Fault Injection and Analysis Service has saved the intent to perform this work.  It does not say anything about the progress
                // of the command.
                while (true)
                {
                    try
                    {
                        await fabricClient.TestManager.StartPartitionDataLossAsync(operationId, partitionSelector,
                            DataLossMode.FullDataLoss).ConfigureAwait(false);
                        break;
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (FabricTransientException)
                    {
                    }

                    await Task.Delay(TimeSpan.FromSeconds(1.0d)).ConfigureAwait(false);
                }

                PartitionDataLossProgress progress = null;

                // Poll the progress using GetPartitionDataLossProgressAsync until it is either Completed or Faulted.  In this example, we're assuming
                // the command won't be cancelled.        

                while (true)
                {
                    try
                    {
                        progress =
                            await fabricClient.TestManager.GetPartitionDataLossProgressAsync(operationId)
                                .ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        continue;
                    }
                    catch (FabricTransientException)
                    {
                        continue;
                    }

                    if (progress.State == TestCommandProgressState.Completed)
                    {
                        Console.WriteLine("Command '{0}' completed successfully", operationId);

                        // In a terminal state .Result.SelectedPartition.PartitionId will have the chosen partition
                        Console.WriteLine("  Printing selected partition='{0}'",
                            progress.Result.SelectedPartition.PartitionId);
                        break;
                    }
                    else if (progress.State == TestCommandProgressState.Faulted)
                    {
                        // If State is Faulted, the progress object's Result property's Exception property will have the reason why.
                        Console.WriteLine("Command '{0}' failed with '{1}'", operationId, progress.Result.Exception);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Command '{0}' is currently Running", operationId);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(5.0d)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ;
            }
        }

    }
}
