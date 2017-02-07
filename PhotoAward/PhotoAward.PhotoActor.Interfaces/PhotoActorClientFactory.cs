using System;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace PhotoAward.PhotoActor.Interfaces
{
    public static class PhotoActorClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/PhotoActorService");

        public static IPhotoActor CreateClient(ActorId photoActorId)
        {
            var actor = ActorProxy.Create<IPhotoActor>(photoActorId, ServiceUrl);
            return actor;
        }
    }
}