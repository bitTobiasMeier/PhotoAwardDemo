using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.Platform.Controller
{
    [RoutePrefix("api/Photo")]
    public class PhotoManagementController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public async Task<PhotoManagementData> Add(PhotoUploadData uploadData)
        {
            var client = PhotoManagementClientFactory.CreatePhotoClient();
            var result = await client.AddPhoto(uploadData);
            return result;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<PhotoManagementData> Get(Guid id)
        {
            var client = PhotoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhoto(id);
            return result;
        }

        [HttpGet]
        [Route("GetThumbnailsOfMember/{email}")]
        public async Task<List<PhotoManagementData>> GetImagesOfMember(string email)
        {
            var client = PhotoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotos(email);
            return result;
        }


    }

   
}