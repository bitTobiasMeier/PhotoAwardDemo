using System;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace PhotoAward.PhotoDb.Interfaces
{
    public class PhotoDbClientFactory : IPhotoDbClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/PhotoDb");
                                                          
        public IPhotoDbService CreatePhotoDbClient()
        {
            return ServiceProxy.Create<IPhotoDbService>(ServiceUrl);
        }


    }
}