using System;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Client;

namespace PhotoAward.PhotoActors.Interfaces
{
    public static class PhotoActorServiceProxy 
    {
        public static IPhotoActorService Create(long partitionKey)
        {
            var actor = ActorServiceProxy.Create<IPhotoActorService>(PhotoActorClientFactory.ServiceUrl, partitionKey);
            return actor;
        }

        public static async Task TakeFullBackup(string nameOfBackupset)
        {
            PhotoActorEventSource.Current.Message($"Backup of service {PhotoActorClientFactory.ServiceUrl} started");
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(PhotoActorClientFactory.ServiceUrl);
                foreach (var partition in partitions)
                {
                    if (partition.PartitionInformation.Kind != ServicePartitionKind.Int64Range)
                    {
                        throw new Exception("Unexpected Partition Kind");
                    }

                    var actorpartition = partition.PartitionInformation as Int64RangePartitionInformation;
                    if (actorpartition == null) throw new Exception("Unexpected partition type");

                    PhotoActorEventSource.Current.Message($"Backing up partition {actorpartition.LowKey}-{actorpartition.HighKey} of service {PhotoActorClientFactory.ServiceUrl} started");
                    var actorServiceProxy = ActorServiceProxy.Create<IPhotoActorService>(PhotoActorClientFactory.ServiceUrl,actorpartition.LowKey);
                    await actorServiceProxy.BackupActorsAsync(nameOfBackupset);
                }
            }
            PhotoActorEventSource.Current.Message($"Backup of service {PhotoActorClientFactory.ServiceUrl} completed.");
        }

        public static async Task RestoreFullBackup(string nameOfBackupset)
        {
            PhotoActorEventSource.Current.Message($"Method RestoreFullBackup of service {PhotoActorClientFactory.ServiceUrl} started");
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(PhotoActorClientFactory.ServiceUrl);
                foreach (var partition in partitions)
                {
                    if (partition.PartitionInformation.Kind != ServicePartitionKind.Int64Range)
                    {
                        throw new Exception("Unexpected Partition Kind");
                    }

                    var actorpartition = partition.PartitionInformation as Int64RangePartitionInformation;
                    if (actorpartition == null) throw new Exception("Unexpected partition type");

                    PhotoActorEventSource.Current.Message($"Restoring  partition {actorpartition.LowKey}-{actorpartition.HighKey} of service {PhotoActorClientFactory.ServiceUrl} started");
                    var actorServiceProxy = ActorServiceProxy.Create<IPhotoActorService>(PhotoActorClientFactory.ServiceUrl, actorpartition.LowKey);
                    await actorServiceProxy.RestoreActorsAsync(nameOfBackupset);
                }
            }
            PhotoActorEventSource.Current.Message($"Method RestoreFullBackup of service {PhotoActorClientFactory.ServiceUrl} completed");
        }
    }
}