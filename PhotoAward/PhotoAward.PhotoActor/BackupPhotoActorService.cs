using System;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;
using PhotoAward.PhotoActors.Interfaces;

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
            try
            {
                //TODo: Backup/Restore vervollständigen. Aktuell nicht vollständig
                var dir = @"c:\temp\BackupDir";
                if (!System.IO.Directory.Exists(""))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                string[] files = System.IO.Directory.GetFiles(backupInfo.Directory);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    var fileName = System.IO.Path.GetFileName(s);
                    var destFile = System.IO.Path.Combine(dir, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }


                // store the contents of backupInfo.Directory
                return true;
            }
            finally
            {
                //Directory.Delete(backupInfo.Directory, recursive: true);
            }
        }

        protected override async Task<bool> OnDataLossAsync(RestoreContext restoreCtx, CancellationToken cancellationToken)
        {
            //ToDo: Restore-Logik korrigieren
            //var backupFolder = await this.externalBackupStore.DownloadLastBackupAsync(cancellationToken);
            var backupFolder = @"c:\temp\BackupDir\";

            var restoreDescription = new RestoreDescription(backupFolder);

            try
            {
                await restoreCtx.RestoreAsync(restoreDescription, cancellationToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            

            return true;

        }
    }
}