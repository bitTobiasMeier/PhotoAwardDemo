using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.ReliableServices.Core
{
    public interface IFileStore
    {
        Task<bool> PerformBackupAsync(BackupInfo backupInfo, CancellationToken cancellationToken, string backupName);
        void WriteRestoreInformation(string nameOfBackupSet);

        string GetRestoreDirectory();
    }
}