using System;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using PhotoAward.MemberActor.Interfaces;

namespace PhotoAward.MemberManagement.Interfaces
{
    public class MemberManagementClientFactory : IMemberManagementClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/MemberManagement");

        public async Task<IMemberManagement> CreateMemberManagementClientAsync()
        {
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                var int64RangePartitionInformation = partitions.First().PartitionInformation as Int64RangePartitionInformation;
                return ServiceProxy.Create<IMemberManagement>(ServiceUrl, new ServicePartitionKey(int64RangePartitionInformation.LowKey));
            }
        }

        public async Task TakeFullBackUpAsync(string nameOfBackupSet)
        {
            //var proxy = this.CreateMemberManagementClient();
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    var intpartions = partition.PartitionInformation as Int64RangePartitionInformation;
                    if (intpartions == null) throw new Exception("Unexpected partition type");
                    var proxy = ServiceProxy.Create<IMemberManagement>(ServiceUrl, new ServicePartitionKey(intpartions.LowKey));
                    await proxy.BackupServiceAsync(nameOfBackupSet);
                }
            }
            await MemberActorClientFactory.TakeFullBackUpAsync(nameOfBackupSet);
           
        }

        public async Task RestoreBackupAsync(string nameOfBackupSet)
        {
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    var proxy = await this.CreateMemberManagementClientAsync();
                    await proxy.RestoreServiceAsync(nameOfBackupSet);
                }
            }
            await MemberActorClientFactory.RestoreFullBackUpAsync(nameOfBackupSet);
        }

    }
}