using System;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace PhotoAward.PhotoActors.Interfaces
{
    public interface IPhotoActorClientFactory
    {
        IPhotoActor CreateClient(ActorId photoActorId);
    }

    public  class PhotoActorClientFactory : IPhotoActorClientFactory
    {
        internal static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/PhotoActorService");

        public  IPhotoActor CreateClient(ActorId photoActorId)
        {
            var actor = ActorProxy.Create<IPhotoActor>(photoActorId, ServiceUrl);
            return actor;
        }
    }
}