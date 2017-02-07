using System;
using System.Collections.Generic;
using System.Drawing;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
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
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class PhotoManagement : StatefulService,IPhotoComments, IPhotoManagement
    {
        private const string PhotoactorsDictName = "photoActors";
        private const string PhotoActorMemberDictName = "photoActorMemberDictionary";

        public PhotoManagement(StatefulServiceContext context)
            : base(context)
        { }

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

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public async Task<PhotoManagementData> AddPhoto(PhotoUploadData photo)
        {
            try
            {
                using (var tx = StateManager.CreateTransaction())
                {
                    var ms = new MemoryStream(photo.Data);
                    var bmp = new Bitmap(ms);
                    var thumbnailTask =  ThumbnailCreator.GetThumbnail(bmp);
                    //Member mit dieser Emailadresse ermitteln
                    var member = MemberManagementClientFactory.CreateMemberManagementClient().GetMember(photo.Email);
                    if (member?.Result == null) throw new Exception("Member not found");


                    //Gibt es bereits einen Actor für dieses Photo. Wenn ja, Exception ...
                    var photoActorState = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                        (PhotoactorsDictName);
                    
                    //PhotoActorMemberDictName
                
                    var photoId= Guid.NewGuid();
                    var photoActorId = ActorId.CreateRandom();
                    await photoActorState.AddAsync(tx, photoId.ToString(), photoActorId);

                    //Pfad herausschreiben
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
                    var client = PhotoActorClientFactory.CreateClient(photoActorId);
                    var result = await client.SetPhoto(data, CancellationToken.None);
                  

                    //Photo einem Member zuordnen
                    
                    var photoMemberActorDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
                    var conditionalActorList = await photoMemberActorDictionary.TryGetValueAsync(tx, member.Result.Id.ToString());
                    var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
                    actorList.Add(photoActorId);
                    await photoMemberActorDictionary.SetAsync(tx, member.Result.Id.ToString(), actorList);
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
                using (var tx = StateManager.CreateTransaction())
                {
                    //Gibt es bereits einen Actor für dieses Photo. Wenn ja, Exception ...
                    var photoActorState = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                        (PhotoactorsDictName);
            
                    //PhotoActorMemberDictName
                    var actorId = await photoActorState.TryGetValueAsync(tx, id.ToString());
                    if (!actorId.HasValue)
                    {
                        throw  new Exception("Actor for image not found!");
                    }
                    var client = PhotoActorClientFactory.CreateClient(actorId.Value);
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

        public async Task<List<PhotoManagementData>> GetPhotos(string email)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                //Member mit dieser Emailadresse ermitteln
                var member = MemberManagementClientFactory.CreateMemberManagementClient().GetMember(email);
                if (member?.Result == null) throw new Exception("Member not found");

                var photoMemberActorDictionary = await StateManager
                    .GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
                var conditionalActorList = await photoMemberActorDictionary.TryGetValueAsync(tx, member.Result.Id.ToString());
                var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
                var photoList = new List<PhotoManagementData>();
                //Gibt es bereits einen Actor für dieses Photo. Wenn ja, Exception ...
                foreach (ActorId imageActorId in actorList.AsParallel())
                {
                    var client = PhotoActorClientFactory.CreateClient(imageActorId);
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
    }
}
