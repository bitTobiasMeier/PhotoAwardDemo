﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using PhotoAward.PhotoDb.Interfaces;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace PhotoAward.PhotoDb
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class PhotoDb : StatelessService, IPhotoDbService
    {
        private readonly IPhotoDbRepository<PhotoDocument> _photoDbRepository;

        public PhotoDb(StatelessServiceContext context, IPhotoDbRepository<PhotoDocument> photoDbRepository)
            : base(context)
        {
            try
            {
                var configurationPackage = Context.CodePackageActivationContext.GetConfigurationPackageObject("Config");
                var databaseParameter = configurationPackage.Settings.Sections["PhotoDbConfigSection"]
                    .Parameters["database"].Value;
                var collectionParameter = configurationPackage.Settings.Sections["PhotoDbConfigSection"]
                    .Parameters["collection"].Value;
                var endpointParameter = configurationPackage.Settings.Sections["PhotoDbConfigSection"]
                    .Parameters["endpoint"].Value;
                var authParameter = configurationPackage.Settings.Sections["PhotoDbConfigSection"].Parameters["authKey"]
                    .Value;
                _photoDbRepository = photoDbRepository;
                _photoDbRepository.Initialize(databaseParameter, collectionParameter, endpointParameter, authParameter);
            }
            catch (Exception ex)
            {
                //ServiceEventSource.Current.ServiceMessage(this.Context, "PhotoDb Service: {0}", ex.Message);
            }
        }

        public async Task AddPhotoAsync(PhotoDocument document)
        {
            await this._photoDbRepository.CreateItemAsync(document);
        }

        public async Task<byte[]> GetPhotoAsync(string id)
        {
            var data  = await this._photoDbRepository.GetItemAsync(id);
            return data?.Image;
        }

        public async Task ReplacePhotoAsync(string id, byte[] photoThumbnailBytes)
        {
            var doc = new PhotoDocument()
            {
                Id = id,
                Image = photoThumbnailBytes
            };
            await this._photoDbRepository.UpdateItemAsync(id, doc);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(this.CreateServiceRemotingListener) };
        }

        
    }
}
