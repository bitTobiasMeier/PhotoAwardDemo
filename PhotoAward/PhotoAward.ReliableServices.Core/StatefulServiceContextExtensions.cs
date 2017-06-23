using System;
using System.Collections.ObjectModel;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PhotoAward.ReliableServices.Core
{
    public static class StatefulServiceContextExtensions
    {
        public static async Task<ActorInstanceInformation> GetCurrentPartitionInfos (this StatefulServiceContext context)
        {
            using (var fabricClient = new FabricClient())
            {

                var partition =
                    (await fabricClient.QueryManager.GetPartitionListAsync(context.ServiceName)).FirstOrDefault(
                        d => d.PartitionInformation.Id == context.PartitionId);
                var partitionInformation = partition?.PartitionInformation as Int64RangePartitionInformation;
                if (partitionInformation == null) return null;
                return new ActorInstanceInformation(partitionInformation.LowKey + "-" + partitionInformation.HighKey, partition.PartitionInformation.Id);
            }

        }

        public static string GetSystemServiceName (this StatefulServiceContext context )
        {
            return context.ServiceName.AbsoluteUri.Replace(":", string.Empty).Replace("/", "-");
        }

        public static string GetServicebackupDirectory(this StatefulServiceContext context)
        {
            var dir = GetValueOfServiceBackupRestoreSection(context, "PhotoAwardBackupDirectory");
            string serviceName = context.ServiceName.AbsoluteUri.Replace(":", string.Empty).Replace("/", "-");
            return Path.Combine(dir, serviceName);
        }

        public static string GetBackupStorageConnectionString(this StatefulServiceContext context)
        {
            return GetValueOfServiceBackupRestoreSection(context, "PhotoAwardAzureStorageConnectionString");
        }

        public static FileStoreType  GetFileStoreType(this StatefulServiceContext context)
        {
            var filestoretypeasstr = GetValueOfServiceBackupRestoreSection(context, "PhotoAwardBackupRestoreFileStoreType");
            
            return !Enum.TryParse( filestoretypeasstr, true,out FileStoreType result) ? FileStoreType.NotDefined : result;
        }

        public static string GetPhotoAwardAzureBackupAccountName(this StatefulServiceContext context)
        {
            return GetValueOfServiceBackupRestoreSection(context, "PhotoAwardAzureBackupAccountName");
        }

        public static string GetPhotoAwardAzureBackupAccessKey(this StatefulServiceContext context)
        {
            return GetValueOfServiceBackupRestoreSection(context, "PhotoAwardAzureBackupAccessKey");
        }

        private static string GetValueOfServiceBackupRestoreSection(StatefulServiceContext context, string key)
        {
            var parameters = GetServiceBackupRestoreSectionParameters(context);
            var entry = parameters[key];
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.Value;
        }


        private static KeyedCollection<string, ConfigurationProperty> GetServiceBackupRestoreSectionParameters(StatefulServiceContext context)
        {
            var parameters = context.CodePackageActivationContext.GetConfigurationPackageObject("Config").Settings
                .Sections["ServiceBackupRestore"].Parameters;
            return parameters;
        }
    }
}