using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public interface IPhotoManagement : IService
    {
        Task<PhotoManagementData> AddPhoto(PhotoUploadData photo);

        Task DeletePhoto(Guid photoId);

        Task<PhotoManagementData> GetPhoto(Guid id);
        Task<List<PhotoManagementData>> GetPhotos(string email);
        Task<List<PhotoMemberInfo>> GetListOfPhotos();

        Task BackupPhotos ();
    }
}
