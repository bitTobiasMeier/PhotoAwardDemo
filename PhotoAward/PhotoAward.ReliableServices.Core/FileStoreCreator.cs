using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAward.ReliableServices.Core
{
    public class FileStoreCreator : IFileStoreCreator
    {
        public IFileStore CreateFileStore(StatefulServiceContext context)
        {
            var filestoretype = context.GetFileStoreType();
            switch (filestoretype)
            {
                    case FileStoreType.Local:
                        return CreateLocalFileStore(context);
                    case FileStoreType.AzureBlobStorage:
                        return CreateAzureBlobStorageStore(context);
                default:
                        return null;
            }
        }

        private IFileStore CreateAzureBlobStorageStore(StatefulServiceContext context)
        {
            var backupRestoreConfiguration = new AzureBackupRestoreConfiguration(context);
            var partitionName = context.GetCurrentPartitionInfos().Result.InternalName;
            var backupStore = new AzureBlobStorageStore(backupRestoreConfiguration, 
                context.GetSystemServiceName(),
                partitionName,context.CodePackageActivationContext.TempDirectory);
            return backupStore;
        }

        private LocalFileStore CreateLocalFileStore(StatefulServiceContext context)
        {
            var backupRestoreConfiguration = new BackupRestoreConfiguration(context);
            var partitionName = context.GetCurrentPartitionInfos().Result.InternalName;
            var backupStore = new LocalFileStore(backupRestoreConfiguration, partitionName);
            return backupStore;
        }
    }

    public enum FileStoreType
    {
        NotDefined= 0,
        Local = 1,
        AzureBlobStorage= 2
    }
}
