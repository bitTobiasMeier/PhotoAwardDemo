using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

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

    }
}