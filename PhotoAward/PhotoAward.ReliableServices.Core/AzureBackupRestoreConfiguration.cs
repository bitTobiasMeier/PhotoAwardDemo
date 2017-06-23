using System.Fabric;

namespace PhotoAward.ReliableServices.Core
{
    public class AzureBackupRestoreConfiguration
    {
        private readonly string _backupAccountName;
   
        private readonly string _backupStorageConnectionString ;
        public AzureBackupRestoreConfiguration(StatefulServiceContext context)
        {
            this._backupStorageConnectionString = context.GetBackupStorageConnectionString();
            
        }


        public string BackupStorageConnectionString
        {
            get { return _backupStorageConnectionString; }
        }
    }
}