using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Fabric;
using System.Fabric.Description;
using System.Threading;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.PhotoDb.Interfaces;
using PhotoAward.ReliableServices.Core;

namespace PhotoAward.PhotoActors
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // This line registers an Actor Service to host your actor class with the Service Fabric runtime.
                // The contents of your ServiceManifest.xml and ApplicationManifest.xml files
                // are automatically populated when you build this project.
                // For more information, see https://aka.ms/servicefabricactorsplatform

                //IAnalyzeRepository analyzeRepository = new AnalyzeRepository();
                IPhotoDbService photoDbService = new PhotoDbClientFactory().CreatePhotoDbClient();

                
                IAnalyzeRepository CreateAnalyzeRepository(StatefulServiceContext context)
                {
                    var parameters = context.CodePackageActivationContext.GetConfigurationPackageObject("Config").Settings
                        .Sections["PhotoActorConfigSection"].Parameters;
                    return new AnalyzeRepository(parameters["OcpApimSubscriptionKey"].Value,parameters["CognitiveServiceUri"].Value);
                }
                

                ActorRuntime.RegisterActorAsync<PhotoActor>(
                   (context, actorType) =>
                   {
                       var backupStore = new FileStoreCreator().CreateFileStore(context);
                       return new PhotoActorService(context, actorType, backupStore, PhotoActorEventSource.Current,
                            (service, id) => new PhotoActor(service, id, CreateAnalyzeRepository(context),
                                photoDbService));
                   }).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                PhotoActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
