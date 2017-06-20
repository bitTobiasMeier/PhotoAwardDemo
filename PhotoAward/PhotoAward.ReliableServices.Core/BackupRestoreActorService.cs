using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.ReliableServices.Core
{
    public abstract class BackupRestoreActorService : ActorService, IBackupRestoreActorService
    {
	    private readonly IServiceEventSource _serviceEventSource;
	    private readonly IFileStore _fileStore;
		
		protected BackupRestoreActorService(StatefulServiceContext context,
			ActorTypeInformation actorTypeInfo,
            IFileStore fileStore,
            IServiceEventSource serviceEventSource, Func<ActorService, ActorId, ActorBase> actorFactory = null,
			Func<ActorBase, IActorStateProvider, IActorStateManager> stateManagerFactory = null,
			IActorStateProvider stateProvider = null,
			ActorServiceSettings settings = null)
			: base(context, actorTypeInfo, actorFactory, stateManagerFactory, stateProvider, settings)
		{
		    this._serviceEventSource = serviceEventSource;
		    this._fileStore = fileStore;
		}

        public IFileStore FileStore
        {
            get { return _fileStore; }
        }


        public async  Task BackupActorsAsync(string nameofbackupset)
        {
            this._serviceEventSource.Message($"BackupRestoreActorService: Backing up actor partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri}.");
            await this.BackupAsync(new BackupDescription((info, token) => this._fileStore.PerformBackupAsync(info, token,nameofbackupset)));
            this._serviceEventSource.Message($"BackupRestoreActorService: Backup completed for actor partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri}.");
        }

        

	    public async Task RestoreActorsAsync(string nameOfBackupSet)
	    {
            this._fileStore.WriteRestoreInformation(nameOfBackupSet);
	        var partitionSelector = PartitionSelector.PartitionIdOf(this.Context.ServiceName, this.Context.PartitionId);

            var operationId = Guid.NewGuid();
	        await new FabricClient(FabricClientRole.Admin).TestManager.StartPartitionDataLossAsync(operationId, partitionSelector, DataLossMode.FullDataLoss);
	    }

	    protected override async Task<bool> OnDataLossAsync(RestoreContext restoreCtx, CancellationToken cancellationToken)
	    {

	        var backupsourcedir = this._fileStore.GetRestoreDirectory();
	        this._serviceEventSource.Message($"BackupRestBackupRestoreActorServiceoreService: Restoring backup of actor partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri} from {backupsourcedir}");
            await restoreCtx.RestoreAsync(new RestoreDescription(backupsourcedir, RestorePolicy.Force),cancellationToken);
	        this._serviceEventSource.Message($"BackupRestoreActorService: Backup of actor partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri} from {backupsourcedir} has been restored!");
            return true;
	    }

	    

	   
	    


    }
}
