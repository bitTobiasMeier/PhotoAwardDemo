using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public interface IPhotoComments : IService
    {
        Task<List<CommentData>> GetComments(Guid photoId);
        Task<CommentData> AddComment(CommentUploadData comment);
    }
}