using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.ReliableServices.Core
{
    public interface IBackupRestoreActorService : IService
    {
        Task BackupActorsAsync(string nameofbackupset);
        Task RestoreActorsAsync(string nameOfBackupSet);
    }
}