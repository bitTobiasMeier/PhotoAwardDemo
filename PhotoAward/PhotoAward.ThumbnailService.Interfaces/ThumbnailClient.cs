using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.ThumbnailService.Interfaces
{
    public interface IThumbnailClientFactory
    {
        IThumbnailService CreateThumbnailClient();

    }

    public interface IThumbnailService : IService
    {
        Task<byte[]> GetThumbnail(byte[] data);
    }

    public class ThumbnailClientFactory : IThumbnailClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/ThumbnailService");

        public IThumbnailService CreateThumbnailClient()
        {
            return ServiceProxy.Create<IThumbnailService>(ServiceUrl);
        }


    }
}