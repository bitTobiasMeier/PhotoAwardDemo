using System;
using System.Collections.Generic;
using System.Fabric.Description;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.ReliableServices.Core
{
    public interface IBackupStore
    {
        long BackupFrequencyInSeconds { get; }

        Task ArchiveBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken);

        Task<string> Restore(CancellationToken cancellationToken);

        Task DeleteBackupsAsync(CancellationToken cancellationToken);
    }

    public class LocalBackupManager : IBackupStore
    {
        private readonly string _partitionArchiveFolder;
        private readonly string _partitionTempDirectory;
        private readonly long _backupFrequencyInSeconds;
        private readonly int _maxBackupsToKeep;

        private readonly IServiceEventSource _serviceEventSource;

        public LocalBackupManager(long backupFrequencyInSeconds, string partitionId,
            string tempDirectory, string backupArchivalPath, IServiceEventSource serviceEventSource)
        {
            _serviceEventSource = serviceEventSource;

            this._backupFrequencyInSeconds = backupFrequencyInSeconds;
            this._maxBackupsToKeep = 10;

            this._partitionArchiveFolder = Path.Combine(backupArchivalPath, "Backups", partitionId);
            this._partitionTempDirectory = Path.Combine(tempDirectory, partitionId);
        }


        long IBackupStore.BackupFrequencyInSeconds => this._backupFrequencyInSeconds;

        public Task ArchiveBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken)
        {
            string fullArchiveDirectory = Path.Combine(
                this._partitionArchiveFolder,
                string.Format(CultureInfo.CurrentCulture, "{0}", Guid.NewGuid().ToString("N")));

            DirectoryInfo dirInfo = new DirectoryInfo(fullArchiveDirectory);
            dirInfo.Create();

            string fullArchivePath = Path.Combine(fullArchiveDirectory, "Backup.zip");

            ZipFile.CreateFromDirectory(backupInfo.Directory, fullArchivePath, CompressionLevel.Fastest, false);

            DirectoryInfo backupDirectory = new DirectoryInfo(backupInfo.Directory);
            backupDirectory.Delete(true);

            return Task.FromResult(true);
        }

        public Task<string> Restore(CancellationToken cancellationToken)
        {
            
            this._serviceEventSource.Message("Restoring backup to temp source:{0} destination:{1}", this._partitionArchiveFolder, this._partitionTempDirectory);

            DirectoryInfo dirInfo = new DirectoryInfo(this._partitionArchiveFolder);

            string backupZip = dirInfo.GetDirectories().OrderByDescending(x => x.LastWriteTime).First().FullName;

            string zipPath = Path.Combine(backupZip, "Backup.zip");

            this._serviceEventSource.Message("latest zip backup is {0}", zipPath);

            DirectoryInfo directoryInfo = new DirectoryInfo(this._partitionTempDirectory);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }

            directoryInfo.Create();

            ZipFile.ExtractToDirectory(zipPath, this._partitionTempDirectory);

            _serviceEventSource.Message("Zip backup {0} extracted to {1}", zipPath, this._partitionTempDirectory);

            return Task.FromResult<string>(this._partitionTempDirectory);
        }

        public async Task DeleteBackupsAsync(CancellationToken cancellationToken)
        {
            await Task.Run(
                () =>
                {
                    _serviceEventSource.Message("deleting old backups");

                    if (!Directory.Exists(this._partitionArchiveFolder))
                    {
                        return;
                    }

                    var dirInfo = new DirectoryInfo(this._partitionArchiveFolder);

                    var oldBackups = dirInfo.GetDirectories().OrderByDescending(x => x.LastWriteTime).Skip(this._maxBackupsToKeep);

                    foreach (var backup in oldBackups)
                    {
                        _serviceEventSource.Message("Deleting old backup {0}", backup.FullName);
                        backup.Delete(true);
                        _serviceEventSource.Message("Old backup {0} has been deleted", backup.FullName);
                    }
                },
                cancellationToken);
        }
    }
}