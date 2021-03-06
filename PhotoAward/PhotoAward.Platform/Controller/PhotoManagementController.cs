﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.Platform.Controller
{
    [Authorize]
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
            var result = await client.AddPhotoAsync(uploadData);
            return result;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(Guid photoId)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            await client.DeletePhotoAsync(photoId);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<PhotoManagementData> Get(Guid id)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotoAsync(id);
            return result;
        }

        [HttpGet]
        [Route("GetImage/{id}")]
        public async Task<byte[]> GetImage(Guid id)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotoDetailAsync(id);
            return result;
        }

        [HttpGet]
        [Route("GetThumbnailsOfMember/{email}")]
        public async Task<List<PhotoManagementData>> GetThumbnailsOfMember(string email)
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetPhotosAsync(email);
            return result;
        }

        [HttpGet]
        [Route("GetImagesOfMember")]
        public async Task<IList<PhotoMemberInfo>> GetImagesOfMember()
        {
            var client = this._photoManagementClientFactory.CreatePhotoClient();
            var result = await client.GetListOfPhotosAsync();
            return result;
        }



        [HttpGet]
        [Route("GetComments/{photoId}")]
        public async Task<List<CommentData>> GetComments(Guid photoId)
        {
            var client = this._photoManagementClientFactory.CreateCommentsClient();
            var result = await client.GetCommentsAsync(photoId);
            return result;
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<CommentData> AddComment(CommentUploadData uploadData)
        {
            var client = this._photoManagementClientFactory.CreateCommentsClient();
            var result = await client.AddCommentAsync(uploadData);
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
                var result = await client.AddPhotoAsync(uploadData);
            }

            var task = this.Request.CreateResponse(HttpStatusCode.OK);
            return task;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Backup")]
        public async Task<string> Backup()
        {
            await this._photoManagementClientFactory.TakeFullBackUpAsync("test");
            return "Backup erfolgt";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Restore")]
        public async Task Restore()
        {
            await this._photoManagementClientFactory.RestoreFullBackup("test");
        }
    }
}