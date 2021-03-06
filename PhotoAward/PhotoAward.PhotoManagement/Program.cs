﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Runtime;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.PhotoDb.Interfaces;
using PhotoAward.ReliableServices.Core;
using PhotoAward.ThumbnailService.Interfaces;

namespace PhotoAward.PhotoManagement
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
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                //
                ServiceRuntime.RegisterServiceAsync("PhotoManagementType",
                    delegate(StatefulServiceContext context)
                    {
                        var stateMngr = new ReliableStateManager(context);
                        var backupStore = new FileStoreCreator().CreateFileStore(context);
                        return new PhotoManagement(context, new PhotoManagementStates(stateMngr), stateMngr,
                            new MemberManagementClientFactory(), 
                            new PhotoActorClientFactory(), 
                            new ThumbnailClientFactory(),
                            new PhotoDbClientFactory(),
                            backupStore,
                            ServiceEventSource.Current);
                    }).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(PhotoManagement).Name);

                // Prevents this host process from terminating so services keep running.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
