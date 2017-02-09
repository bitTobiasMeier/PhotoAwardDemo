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
        private readonly IPhotoManagementClientFactory _photoManagementClientFactory;

        public PhotoManagementController(IPhotoManagementClientFactory photoManagementClientFactory)
        {
            _photoManagementClientFactory = photoManagementClientFactory;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<PhotoManagementData> Add(PhotoUploadData uploadData)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.AddPhoto(uploadData);
            return result;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<PhotoManagementData> Get(Guid id)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhoto(id);
            return result;
        }

        [HttpGet]
        [Route("GetThumbnailsOfMember/{email}")]
        public async Task<List<PhotoManagementData>> GetImagesOfMember(string email)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotos(email);
            return result;
        }

        [HttpGet]
        [Route("GetComments/{photoId}")]
        public async Task<List<CommentData>> GetComments(Guid photoId)
        {
            var client = this._photoManagementClientFactory.CreateCommentsClient();
            var result = await client.GetComments(photoId);
            return result;
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<CommentData> AddComment(CommentUploadData uploadData)
        {
            var client = this._photoManagementClientFactory.CreateCommentsClient();
            var result = await client.AddComment(uploadData);
            return result;
        }

    }

   
}