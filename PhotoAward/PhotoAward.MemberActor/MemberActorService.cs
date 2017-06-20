using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Client;
using PhotoAward.MemberActor.Interfaces;
using PhotoAward.ReliableServices.Core;


namespace PhotoAward.MemberActor
{
    public class MemberActorService : BackupRestoreActorService, IMemberActorService
    {
        

        public MemberActorService(StatefulServiceContext context, ActorTypeInformation actorTypeInfo, IFileStore fileStore, IServiceEventSource serviceEventSource, Func<ActorService, ActorId, ActorBase> actorFactory = null, 
            Func<ActorBase, IActorStateProvider, IActorStateManager> stateManagerFactory = null, IActorStateProvider stateProvider = null, ActorServiceSettings settings = null) 
            : base(context, actorTypeInfo,fileStore,  serviceEventSource, actorFactory, stateManagerFactory, stateProvider, settings)
        {
           
        }



        



    }
}
