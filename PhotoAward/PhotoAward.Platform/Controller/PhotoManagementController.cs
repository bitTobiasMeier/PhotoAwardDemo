using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.ServiceFabric.Data.Collections.Preview;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        public async Task<List<PhotoManagementData>> GetThumbnailsOfMember(string email)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotos(email);
            return result;
        }

        [HttpGet]
        [Route("GetImagesOfMember")]
        public async Task<IList<PhotoMemberInfo>> GetImagesOfMember()
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetListOfPhotos();
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


        [HttpPost]
        [Route("UploadPhoto")]
        public async Task<HttpResponseMessage> UploadPhoto()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
            IEnumerable<string> values;
            var foundEmail = this.Request.Headers.TryGetValues("email", out values);
            if (!foundEmail)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            var email = values.FirstOrDefault();
            var foundTitle = this.Request.Headers.TryGetValues("title", out values);
            var title = foundTitle ? values.FirstOrDefault() : "";
            var foundFilename = this.Request.Headers.TryGetValues("filename", out values);
            var filename = foundFilename ? values.FirstOrDefault() : "";

            var client = this._photoManagementClientFactory.CreatePhotoClient();


            foreach (var stream in filesReadToProvider.Contents)
            {
                var fileBytes = await stream.ReadAsByteArrayAsync();
                var uploadData = new PhotoUploadData()
                {
                    Data = fileBytes,
                    Email = email,
                    FileName = filename,
                    Title = title
                };
                var result = await client.AddPhoto(uploadData);
            }

            var task = this.Request.CreateResponse(HttpStatusCode.OK);
            return task;
        }


    }
}