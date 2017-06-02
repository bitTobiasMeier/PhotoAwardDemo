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
    public class BackupPhotoActorService : ActorService, IBackupPhotoActorService
    {
        public BackupPhotoActorService(StatefulServiceContext context, ActorTypeInformation actorTypeInfo, Func<ActorService, ActorId, ActorBase> actorFactory = null, Func<ActorBase, IActorStateProvider, IActorStateManager> stateManagerFactory = null, IActorStateProvider stateProvider = null, ActorServiceSettings settings = null) : base(context, actorTypeInfo, actorFactory, stateManagerFactory, stateProvider, settings)
        {
             
        }

        
        public Task BackupActorsAsync()
        {
            return this.BackupAsync(new BackupDescription(PerformBackupAsync));
        }

        private async Task<bool> PerformBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken)
        {
                var backupDescription = new BackupDescription(BackupOption.Full, this.PerformBackupCallbackAsync);
                await this.BackupAsync(backupDescription, TimeSpan.FromHours(1), cancellationToken);
                return true;
        }

        private async Task<bool> PerformBackupCallbackAsync(BackupInfo backupInfo, CancellationToken cancellationToken)
        {
            var backupMngr = CreateBackupManager();
            await backupMngr.ArchiveBackupAsync(backupInfo, cancellationToken);
            return true;
        }

        private LocalBackupManager CreateBackupManager()
        {
            var partitionId = this.Context.PartitionId.ToString("N");
            var backupMngr = new LocalBackupManager(120, partitionId, @"c:\temp\Backups\Temp", @"c:\temp\Backups\Temp",
                ActorEventSource.Current);
            return backupMngr;
        }

        protected override async Task<bool> OnDataLossAsync(RestoreContext restoreCtx, CancellationToken cancellationToken)
        {
            var backupMngr = CreateBackupManager();
            var backupFolder = await backupMngr.Restore(cancellationToken);
            var restoreDescription = new RestoreDescription(backupFolder, RestorePolicy.Force);
            await restoreCtx.RestoreAsync(restoreDescription, cancellationToken);
            
            var tempRestoreDirectory = new DirectoryInfo(backupFolder);
            tempRestoreDirectory.Delete(true);
            return true;
        }

        /*
        public async Task GetActivePhotoActors(long partitionKey)
        {
            IActorService actorServiceProxy = ActorServiceProxy.Create(
                new Uri("fabric:/MyApp/MyService"), partitionKey);

            ContinuationToken continuationToken = null;
            List<ActorInformation> activeActors = new List<ActorInformation>();

            do
            {
               
                PagedResult<ActorInformation> page = await actorServiceProxy.GetActorsAsync(continuationToken, CancellationToken.None);

                activeActors.AddRange(page.Items.Where(x => x.IsActive));

                continuationToken = page.ContinuationToken;
            }
            while (continuationToken != null);
        }*/
    }
}