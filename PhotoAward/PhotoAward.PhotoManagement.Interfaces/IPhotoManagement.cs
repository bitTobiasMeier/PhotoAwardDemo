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

        Task<PhotoManagementData> GetPhoto(Guid id);
        Task<List<PhotoManagementData>> GetPhotos(string email);
    }

    public interface IPhotoComments : IService
    {
    }

    public class PhotoUploadData
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }

    public class PhotoManagementData
    {
        public string FileName { get; set; }
        public byte [] ThumbnailBytes { get; set; }
        public string Title { get; set; }

        public Guid ? Id { get; set; }
    }
}
