using System;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors.Query;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace PhotoAward.MemberActor.Interfaces
{
    public static class MemberActorClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/MemberActorService");

        public static IMemberActor CreateMemberActorClient(ActorId actorId)
        {
            var actor = ActorProxy.Create<IMemberActor>(actorId, ServiceUrl);
            return actor;
        }

        public static IMemberActorService CreateMemberActorServiceClient(long partitionKey)
        {
            var actorService = ActorServiceProxy.Create<IMemberActorService>(ServiceUrl, partitionKey);
            return actorService;
        }

        public static async Task TakeFullBackUpAsync(string nameofbackupset)
        {
            MemberActorEventSource.Current.Message("MemberActor: TakeFullBackUpAsync started");
            
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    if (partition.PartitionInformation.Kind != ServicePartitionKind.Int64Range)
                    {
                        throw new Exception("Unexpected Partition Kind");
                    }

                    var actorpartition = partition.PartitionInformation as Int64RangePartitionInformation;
                    if (actorpartition == null) throw new Exception("Unexpected partition type");

                    MemberActorEventSource.Current.Message("MemberActor: TakeFullBackUpAsync: Partition: {0}-{1} ; ID: {2}",
                        actorpartition.LowKey, actorpartition.HighKey, actorpartition.Id);

                    var actorServiceProxy = ActorServiceProxy.Create<IMemberActorService>(ServiceUrl, actorpartition.LowKey) ;
                    await actorServiceProxy.BackupActorsAsync(nameofbackupset);
                }
            }
            /*var actorService = ActorServiceProxy.Create<IMemberActorService>(ServiceUrl, -1) as IMemberActorService;
            //await ActorProxy.Create<IMemberActor>(ActorId.CreateRandom()).BackupAsync();
            await actorService.BackupActorsAsync("test");*/
            MemberActorEventSource.Current.Message("MemberActor: TakeFullBackUpAsync completed.");
        }


        public static async Task RestoreFullBackUpAsync(string nameOfBackupSet)
        {
            MemberActorEventSource.Current.Message("MemberActor: RestoreFullBackUpAsync started");

            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    if (partition.PartitionInformation.Kind != ServicePartitionKind.Int64Range)
                    {
                        throw new Exception("Unexpected Partition Kind");
                    }

                    var actorpartition = partition.PartitionInformation as Int64RangePartitionInformation;
                    if (actorpartition == null) throw new Exception("Unexpected partition type");



                    MemberActorEventSource.Current.Message("MemberActor: RestoreFullBackUpAsync: Partition: {0}-{1} ; ID: {2}",
                        actorpartition.LowKey, actorpartition.HighKey, actorpartition.Id);

                    var actorServiceProxy = ActorServiceProxy.Create<IMemberActorService>(ServiceUrl, -1) as IMemberActorService;
                    await actorServiceProxy.RestoreActorsAsync(nameOfBackupSet);
                }
            }
            //var actorService = ActorServiceProxy.Create<IMemberActorService>(ServiceUrl, -1) as IMemberActorService;
            //await ActorProxy.Create<IMemberActor>(ActorId.CreateRandom()).BackupAsync();
            //await actorService.BackupActorsAsync("test");
            MemberActorEventSource.Current.Message("MemberActor: RestoreFullBackUpAsync completed.");
        }

        public static async Task LogAllActorsAsync()
        {
            var actorServiceProxy = ActorServiceProxy.Create(ServiceUrl, -1);
            ContinuationToken continuationToken = null;

            do
            {
                var queryResult = actorServiceProxy.GetActorsAsync(continuationToken, CancellationToken.None)
                    .GetAwaiter().GetResult();
                foreach (var item in queryResult.Items)
                {
                    var actorId = item.ActorId;
                    var actorProxy = MemberActorClientFactory.CreateMemberActorClient(actorId);
                    var entry = await actorProxy.GetMemberAsync(CancellationToken.None);
                    MemberActorEventSource.Current.Message("Log Member: {0}", entry.FirstName);
                }
                continuationToken = queryResult.ContinuationToken;
            } while (continuationToken != null);

        }
    }
}