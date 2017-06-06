using Microsoft.ServiceFabric.Actors.Client;

namespace PhotoAward.PhotoActors.Interfaces
{
    public class PhotoActorServiceProxy 
    {
        public IPhotoActorService Create(long partitionKey)
        {
            var actor = ActorServiceProxy.Create<IPhotoActorService>(PhotoActorClientFactory.ServiceUrl, partitionKey);
            return actor;
        }
    }
}