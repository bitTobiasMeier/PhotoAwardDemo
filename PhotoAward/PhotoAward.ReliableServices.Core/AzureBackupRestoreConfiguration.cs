using System.Fabric;

namespace PhotoAward.ReliableServices.Core
{
    public class AzureBackupRestoreConfiguration
    {
        private readonly string _backupAccountName;
        private readonly string _backupAccountKey ;
        private readonly string _blobEndpointAddress ;
        public AzureBackupRestoreConfiguration(StatefulServiceContext context)
        {
            this._blobEndpointAddress = context.GetAzureBlobStorageEndpoint();
            this._backupAccountName = context.GetPhotoAwardAzureBackupAccountName();
            this._backupAccountKey = context.GetPhotoAwardAzureBackupAccessKey();
        }

        public string BackupAccountName
        {
            get { return _backupAccountName; }
        }

        public string BackupAccountKey
        {
            get { return _backupAccountKey; }
        }

        public string BlobEndpointAddress
        {
            get { return _blobEndpointAddress; }
        }
    }
}