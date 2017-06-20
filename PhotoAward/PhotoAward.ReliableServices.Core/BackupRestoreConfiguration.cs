using System.Fabric;

namespace PhotoAward.ReliableServices.Core
{
    public class BackupRestoreConfiguration
    {
        private readonly string _servicebackupdirectory;
        public BackupRestoreConfiguration(StatefulServiceContext context)
        {
            this._servicebackupdirectory = context.GetServicebackupDirectory();
        }

        public string Servicebackupdirectory
        {
            get { return _servicebackupdirectory; }
        }
    }
}