using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.PhotoDb.Interfaces
{
    public interface IPhotoDbService : IService
    {
        Task AddPhoto(PhotoDocument document);
        Task<byte[]> GetPhoto(string id);
    }
}
