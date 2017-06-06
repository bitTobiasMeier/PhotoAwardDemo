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
using PhotoAward.ThumbnailService.Interfaces;
using PhotoAward.PhotoDb.Interfaces;

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
        private readonly IThumbnailClientFactory _thumbnailClientFactory;
        private readonly IPhotoDbClientFactory _photoDbClientFactory;

        private const string PhotoActorMemberDictName = "photoActorMemberDictionary";

        public PhotoManagement(StatefulServiceContext context, IPhotoManagementStates photoManagementStates, IReliableStateManagerReplica stateManager, 
            IMemberManagementClientFactory memberManagementClientFactory, IPhotoActorClientFactory photoActorClientFactory, IThumbnailClientFactory thumbnailClientFactory,
            IPhotoDbClientFactory photoDbClientFactory)
            : base(context, stateManager)
        {
            _photoManagementStates = photoManagementStates;
            _memberManagementClientFactory = memberManagementClientFactory;
            _photoActorClientFactory = photoActorClientFactory;
            _thumbnailClientFactory = thumbnailClientFactory;
            _photoDbClientFactory = photoDbClientFactory;
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


                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public async Task<PhotoManagementData> AddPhotoAsync(PhotoUploadData photo)
        {
            try
            {
                using (var tx = this._photoManagementStates.CreateTransaction())
                {
                    var thumbnailClient = this._thumbnailClientFactory.CreateThumbnailClient();
                    var thumbnailTask = thumbnailClient.GetThumbnailAsync(photo.Data);
                   
                    //Member mit dieser Emailadresse ermitteln
                    var member = await this._memberManagementClientFactory.CreateMemberManagementClient().GetMember(photo.Email);
                    if (member == null) throw new Exception("Member not found");
                    var photoId = Guid.NewGuid();
                    var photoDbClient = this._photoDbClientFactory.CreatePhotoDbClient();
                    var doc = new PhotoDocument()
                    {
                        Id = photoId.ToString(),
                        Image = photo.Data
                    };
                    await photoDbClient.AddPhotoAsync(doc);

                    
                    var photoActorId = ActorId.CreateRandom();
                    await this._photoManagementStates.AddPhotoIdActorMapping(tx,photoId,photoActorId);

                    //Dateiname ermitteln
                    var filename = photo.FileName;
                    filename = System.IO.Path.GetFileName(filename);
                    var thumbnail = await thumbnailTask;
                    //Bild hochladen
                    var data = new PhotoInfo()
                    {
                        Filename = filename,
                        ThumbnailBytes = thumbnail,
                        Title = photo.Title,
                        Id = photoId,
                        OwnerEmail = photo.Email
                    };
                    var client = this._photoActorClientFactory.CreateActorClient(photoActorId);
                    var result = await client.SetPhotoAsync(data, CancellationToken.None);


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

        public async Task<PhotoManagementData> GetPhotoAsync(Guid id)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    var actorId = await GetPhotoActorId(id, tx);
                    var client = _photoActorClientFactory.CreateActorClient(actorId.Value);
                    var result = await client.GetPhotoAsync(CancellationToken.None);
                    return new PhotoManagementData()
                    {
                        FileName = result.Filename,
                        Id = result.Id,
                        Title = result.Title,
                        ThumbnailBytes = result.ThumbnailBytes,
                        Description = result.Description
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<byte[]> GetPhotoDetailAsync(Guid id)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    var client = this._photoDbClientFactory.CreatePhotoDbClient();
                    var doc = await client.GetPhotoAsync(id.ToString());
                    return doc;
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

        public async Task<List<PhotoManagementData>> GetPhotosAsync(string email)
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
                    var client = _photoActorClientFactory.CreateActorClient(imageActorId);
                    var result = await client.GetPhotoAsync(CancellationToken.None);
                    photoList.Add(new PhotoManagementData()
                    {
                        FileName = result.Filename,
                        Id = result.Id,
                        Title = result.Title,
                        ThumbnailBytes = result.ThumbnailBytes,
                        Description = result.Description
                    });
                }
                return photoList;

            }
        }

        public async Task<List<CommentData>> GetCommentsAsync(Guid photoId)
        {
            try
            {
                using (var tx = _photoManagementStates.CreateTransaction())
                {
                    //photoActor ermitteln
                    var actorId = await GetPhotoActorId(photoId, tx);
                    var client = _photoActorClientFactory.CreateActorClient(actorId.Value);
                    var infos = await client.GetCommentsAsync( CancellationToken.None);
                    var comments= infos.Select(i => new CommentData()
                    {
                        AuthorId = i.AuthorId,
                        Id = i.Id,
                        PhotoId = i.PhotoId,
                        Comment = i.Comment,
                        CommentDate = i.CommentDate
                    }).ToList();
                    var authorIds = comments.Select(c => c.AuthorId).Distinct().ToList();
                    var authors = await  this._memberManagementClientFactory.CreateMemberManagementClient().GetNamesOfMembersAsync(authorIds);
                    foreach (var comment in comments)
                    {
                        comment.Authorname = authors.Where(a => a.Id == comment.AuthorId).Select(c => c.Name)
                            .FirstOrDefault();
                    }
                    return comments;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        

        public async Task<CommentData> AddCommentAsync(CommentUploadData comment)
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
                    var client = _photoActorClientFactory.CreateActorClient(actorId.Value);
                    var ci = await client.AddCommentAsync(new CommentInfo()
                    {
                        AuthorId = member.Id,
                        PhotoId = comment.PhotoId,
                        Comment = comment.Comment,
                        CommentDate = DateTime.Now
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

        public async Task<List<PhotoMemberInfo>> GetListOfPhotosAsync()
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
                            var client = _photoActorClientFactory.CreateActorClient(imageActorId);
                            var photo = await client.GetPhotoAsync(CancellationToken.None);
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

        public async Task DeletePhotoAsync(Guid photoId)
        {
            using (var tx = _photoManagementStates.CreateTransaction())
            {
                var imageActorId = await this.GetPhotoActorId(photoId, tx);
                if (imageActorId.HasValue)
                {
                    var client = _photoActorClientFactory.CreateActorClient(imageActorId.Value);
                    await client.DeleteAsync(CancellationToken.None);
                    await this._photoManagementStates.RemoveActor(photoId, imageActorId.Value, tx);
                }
            }
        }

        
        

    }
}
