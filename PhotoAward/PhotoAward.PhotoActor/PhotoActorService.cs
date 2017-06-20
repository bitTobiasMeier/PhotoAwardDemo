using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors.Query;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.ReliableServices.Core;

namespace PhotoAward.PhotoActors
{
    public class PhotoActorService : BackupRestoreActorService, IPhotoActorService
    {
        public PhotoActorService(StatefulServiceContext context, ActorTypeInformation actorTypeInfo, IFileStore fileStore, IServiceEventSource serviceEventSource, Func<ActorService, ActorId, ActorBase> actorFactory = null, Func<ActorBase, IActorStateProvider, IActorStateManager> stateManagerFactory = null,
            IActorStateProvider stateProvider = null, ActorServiceSettings settings = null) 
            : base(context, actorTypeInfo, fileStore, serviceEventSource, actorFactory, stateManagerFactory, stateProvider, settings)
        {
             
        }

        
        

       
    }
}