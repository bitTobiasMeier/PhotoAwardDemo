using Microsoft.ServiceFabric.Actors.Client;

namespace PhotoAward.PhotoActors.Interfaces
{
    public class PhotoActorServiceProxy 
    {
        public IBackupPhotoActorService Create(long partitionKey)
        {
            var actor = ActorServiceProxy.Create<IBackupPhotoActorService>(PhotoActorClientFactory.ServiceUrl, partitionKey);
            return actor;
        }
    }
}