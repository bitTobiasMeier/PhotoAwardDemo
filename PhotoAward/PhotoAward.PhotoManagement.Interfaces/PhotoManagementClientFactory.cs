using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public static class PhotoManagementClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/PhotoManagement");

        public static IPhotoManagement CreatePhotoClient()
        {
            return ServiceProxy.Create<IPhotoManagement>(ServiceUrl, new ServicePartitionKey(0));
        }

        public static IPhotoComments CreateCommentsClient()
        {
            return ServiceProxy.Create<IPhotoComments>(ServiceUrl, new ServicePartitionKey(0));
        }

    }
}