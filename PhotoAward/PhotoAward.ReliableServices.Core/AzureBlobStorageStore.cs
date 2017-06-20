using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PhotoAward.ReliableServices.Core
{
    public class AzureBlobStorageStore : IFileStore
    {
        private readonly AzureBackupRestoreConfiguration _backupRestoreConfiguration;
        private readonly CloudBlobClient _blobClient;
        private readonly string _partitionName;
        private readonly string _systemservicename;
        private readonly string _temporaryDirectory;

        public AzureBlobStorageStore(AzureBackupRestoreConfiguration backupRestoreConfiguration,
            string systemservicename, string partitionName, string temporaryDirectory)
        {
            _backupRestoreConfiguration = backupRestoreConfiguration;
            _systemservicename = systemservicename;
            _partitionName = partitionName;
            _temporaryDirectory = temporaryDirectory;
            var storageCredentials = new StorageCredentials(_backupRestoreConfiguration.BackupAccountName,
                _backupRestoreConfiguration.BackupAccountKey);
            _blobClient = new CloudBlobClient(new Uri(_backupRestoreConfiguration.BlobEndpointAddress),
                storageCredentials);
        }

        public async Task<bool> PerformBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken,
            string backupName)
        {
            var fullArchiveDirectory = Path.Combine(_temporaryDirectory, _systemservicename,  _partitionName, backupName);

            var fullArchiveDirectoryInfo = new DirectoryInfo(fullArchiveDirectory);
            fullArchiveDirectoryInfo.Create();

            var blobName = $"{Guid.NewGuid().ToString("N")}_{backupName}_{_partitionName}_{"Backup.zip"}";
            var fullArchivePath = Path.Combine(fullArchiveDirectory, "Backup.zip");

            ZipFile.CreateFromDirectory(backupInfo.Directory, fullArchivePath, CompressionLevel.Fastest, false);

            var backupDirectory = new DirectoryInfo(backupInfo.Directory);
            backupDirectory.Delete(true);

            var container = GetBlobContainer();
            var blob = container.GetBlockBlobReference(blobName);
            await blob.UploadFromFileAsync(fullArchivePath, CancellationToken.None);

            var tempDirectory = new DirectoryInfo(fullArchiveDirectory);
            tempDirectory.Delete(true);
            return true;
        }

        public void WriteRestoreInformation(string nameOfBackupSet)
        {
            var blobName = $"{Guid.NewGuid().ToString("N")}_{nameOfBackupSet}_{_partitionName}_{"Backup.zip"}";
            var container = GetBlobContainer();
            var lastBackupBlob = container.GetBlockBlobReference(blobName);

            var fullArchiveDirectory = Path.Combine(_temporaryDirectory, "restore", _systemservicename, _partitionName, nameOfBackupSet);

            var fullArchiveDirectoryInfo = new DirectoryInfo(fullArchiveDirectory);
            fullArchiveDirectoryInfo.Create();


            string downloadId = Guid.NewGuid().ToString("N");

            string zipPath = Path.Combine(fullArchiveDirectory, $"{downloadId}_Backup.zip");

            lastBackupBlob.DownloadToFile(zipPath, FileMode.CreateNew);

            string restorePath = Path.Combine(fullArchiveDirectory, downloadId);

            ZipFile.ExtractToDirectory(zipPath, restorePath);

            FileInfo zipInfo = new FileInfo(zipPath);
            zipInfo.Delete();

            var restoreDir = Path.Combine(_temporaryDirectory, "restore", _systemservicename);
            File.WriteAllText(Path.Combine(restoreDir, this._partitionName + ".restoreinfo"), nameOfBackupSet + "\n" + downloadId);
        }

        public string GetRestoreDirectory()
        {
            var restoreDir = Path.Combine(_temporaryDirectory, "restore", _systemservicename);
            var metadata = File.ReadAllText(Path.Combine(restoreDir, this._partitionName + ".restoreinfo"));
            var data = metadata.Split('\n');
            var nameOfBackupSet = data.FirstOrDefault();
            var downloadId = data.Last();
            var restorepath = Path.Combine(_temporaryDirectory, "restore", _systemservicename, _partitionName, nameOfBackupSet,downloadId);
            return restorepath;
        }

        private CloudBlobContainer GetBlobContainer()
        {
            var container =
                _blobClient.GetContainerReference(_systemservicename.ToLowerInvariant() + "_" + _partitionName);
            container.CreateIfNotExists();
            return container;
        }
    }
}