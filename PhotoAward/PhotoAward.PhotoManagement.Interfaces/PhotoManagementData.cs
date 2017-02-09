using System;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public class PhotoManagementData
    {
        public string FileName { get; set; }
        public byte [] ThumbnailBytes { get; set; }
        public string Title { get; set; }

        public Guid ? Id { get; set; }
    }
}