using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.ReliableServices.Core
{
    public interface IBackupRestoreStatefulService: IService
    {
        Task BackupServiceAsync(string nameofbackupset);
        Task RestoreServiceAsync(string nameOfBackupSet);
    }
}