using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.PhotoActors.Interfaces
{
    public interface IBackupPhotoActorService : IService
    {
        Task BackupActorsAsync();
    }
}