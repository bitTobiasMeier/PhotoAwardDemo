using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using PhotoAward.ReliableServices.Core;

namespace PhotoAward.PhotoActors.Interfaces
{
    public interface IPhotoActorService : IBackupRestoreActorService
    {
     
    }
}