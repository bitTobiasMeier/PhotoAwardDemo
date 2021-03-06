﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using PhotoAward.ReliableServices.Core;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public interface IPhotoManagement : IBackupRestoreStatefulService
    {
        Task<PhotoManagementData> AddPhotoAsync(PhotoUploadData photo);

        Task DeletePhotoAsync(Guid photoId);

        Task<PhotoManagementData> GetPhotoAsync(Guid id);
        Task<byte[]> GetPhotoDetailAsync(Guid id);
        Task<List<PhotoManagementData>> GetPhotosAsync(string email);
        Task<List<PhotoMemberInfo>> GetListOfPhotosAsync();
        
    }
}
