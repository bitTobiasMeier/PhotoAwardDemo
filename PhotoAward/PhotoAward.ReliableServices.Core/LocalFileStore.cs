using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.ReliableServices.Core
{
    public class LocalFileStore : IFileStore
    {
        private readonly BackupRestoreConfiguration _backupRestoreConfiguration;
        private readonly string _partitionName;

        public LocalFileStore(BackupRestoreConfiguration backupRestoreConfiguration, string partitionName)
        {
            _backupRestoreConfiguration = backupRestoreConfiguration;
            _partitionName = partitionName;
        }
        public async Task<bool> PerformBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken, string backupName)
        {
            try
            {
                await Task.Run(() =>
                {
                    var dir = this._backupRestoreConfiguration.Servicebackupdirectory;
                    var target = Path.Combine(dir, backupName, this._partitionName);
                    if (Directory.Exists(target))
                    {
                        Directory.Delete(target, recursive: true);
                    }
                    //Backupdaten kopieren
                    new DirectoryInfo(backupInfo.Directory).Copy(target);
                },cancellationToken);
                
                return true;
            }
            finally
            {
                Directory.Delete(backupInfo.Directory, recursive: true);
            }
        }

        public void WriteRestoreInformation(string nameOfBackupSet)
        {
            var restoreDir = this._backupRestoreConfiguration.Servicebackupdirectory; 
            File.WriteAllText(Path.Combine(restoreDir, this._partitionName + ".restoreinfo"), nameOfBackupSet);
        }

        public string GetRestoreDirectory()
        {
            var restoreDir = this._backupRestoreConfiguration.Servicebackupdirectory;
            var nameofbackupset = File.ReadAllText(Path.Combine(restoreDir, this._partitionName + ".restoreinfo"));
            var backupsourcedir = Path.Combine(restoreDir, nameofbackupset, this._partitionName);
            return backupsourcedir;
        }
    }
}
