﻿using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ServiceFabric.Actors.Runtime;

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

                IAnalyzeRepository analyzeRepository = new AnalyzeRepository();
                

                ActorRuntime.RegisterActorAsync<PhotoActor>(
                   (context, actorType) => new BackupPhotoActorService(context, actorType,
                   (service, id) => new PhotoActor(service,id, analyzeRepository))).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
