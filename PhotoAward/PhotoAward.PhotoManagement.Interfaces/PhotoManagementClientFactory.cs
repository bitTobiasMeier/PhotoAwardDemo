using System;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using PhotoAward.PhotoActors.Interfaces;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public  class PhotoManagementClientFactory : IPhotoManagementClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/PhotoManagement");

        public  IPhotoManagement CreatePhotoClient()
        {
            return ServiceProxy.Create<IPhotoManagement>(ServiceUrl, new ServicePartitionKey(0));
        }

        public  IPhotoComments CreateCommentsClient()
        {
            return ServiceProxy.Create<IPhotoComments>(ServiceUrl, new ServicePartitionKey(0));
        }


        public async Task TakeFullBackUpAsync(string nameOfBackupset)
        {
            var proxy = this.CreatePhotoClient();
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    await proxy.BackupServiceAsync(nameOfBackupset);
                }
            }
            await PhotoActorServiceProxy.TakeFullBackup(nameOfBackupset);

        }

        public async Task RestoreFullBackup(string nameOfBackupset)
        {
            var proxy = this.CreatePhotoClient();
            using (var client = new FabricClient())
            {
                var partitions = await client.QueryManager.GetPartitionListAsync(ServiceUrl);
                foreach (var partition in partitions)
                {
                    await proxy.RestoreServiceAsync("test");
                }
            }
            await PhotoActorServiceProxy.RestoreFullBackup(nameOfBackupset);
        }

    }
    
}