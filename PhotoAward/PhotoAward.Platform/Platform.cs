using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace PhotoAward.Platform
{
    /// <summary>
    /// Gateway-Service der Photo-Award-Demo-Anwendung.
    /// </summary>
    /// <remarks>An instance of this class is created for each service instance by the Service Fabric runtime.</remarks>
    internal sealed class Platform : StatelessService
    {
        public Platform(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var endpoints = Context.CodePackageActivationContext.GetEndpoints()
                            .Where(endpoint => endpoint.Protocol == EndpointProtocol.Http || endpoint.Protocol == EndpointProtocol.Https)
                            .Select(endpoint => endpoint.Name);

            var listeners = endpoints.Select(endpoint => new ServiceInstanceListener(
                serviceContext => new OwinCommunicationListener
                (Startup.ConfigureApp, serviceContext, ServiceEventSource.Current, endpoint), endpoint)
                ).ToList();
            return listeners;
        }

        
    }
}
