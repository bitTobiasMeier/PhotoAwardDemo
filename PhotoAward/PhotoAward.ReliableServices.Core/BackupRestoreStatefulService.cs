using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Runtime;

namespace PhotoAward.ReliableServices.Core
{
    public abstract class BackupRestoreStatefulService : StatefulService, IBackupRestoreStatefulService
    {
        private readonly IFileStore _fileStore;
        private readonly IServiceEventSource _serviceEventSource;

        protected BackupRestoreStatefulService(StatefulServiceContext serviceContext, IFileStore fileStore,
            IServiceEventSource serviceEventSource) : base(serviceContext)
        {
            _fileStore = fileStore;
            _serviceEventSource = serviceEventSource;
        }

        protected BackupRestoreStatefulService(StatefulServiceContext serviceContext, IReliableStateManagerReplica reliableStateManagerReplica, IFileStore fileStore,
            IServiceEventSource serviceEventSource) : base(serviceContext, reliableStateManagerReplica)
        {
            _fileStore = fileStore;
            _serviceEventSource = serviceEventSource;
        }

        public async Task BackupServiceAsync(string nameofbackupset)
        {
            this._serviceEventSource.Message($"BackupRestoreStatefulService: Backing up service partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri}.");
            await this.BackupAsync(new BackupDescription((info, token) => this._fileStore.PerformBackupAsync(info, token, nameofbackupset)));
            this._serviceEventSource.Message($"BackupRestoreStatefulService: Backup completed for service partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri}.");
        }



        public async Task RestoreServiceAsync(string nameOfBackupSet)
        {
            this._fileStore.WriteRestoreInformation(nameOfBackupSet);
            var partitionSelector = PartitionSelector.PartitionIdOf(this.Context.ServiceName, this.Context.PartitionId);

            var operationId = Guid.NewGuid();
            await new FabricClient(FabricClientRole.Admin).TestManager.StartPartitionDataLossAsync(operationId, partitionSelector, DataLossMode.FullDataLoss);
        }

        protected override async Task<bool> OnDataLossAsync(RestoreContext restoreCtx, CancellationToken cancellationToken)
        {

            var backupsourcedir = this._fileStore.GetRestoreDirectory();
            this._serviceEventSource.Message($"BackupRestoreStatefulService: Restoring backup of partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri} from {backupsourcedir}");
            await restoreCtx.RestoreAsync(new RestoreDescription(backupsourcedir, RestorePolicy.Force), cancellationToken);
            this._serviceEventSource.Message($"BackupRestoreStatefulService: Backup of partition { Context.PartitionId} of service {Context.ServiceName.AbsoluteUri} from {backupsourcedir} has been restored!");
            return true;
        }
    }
}