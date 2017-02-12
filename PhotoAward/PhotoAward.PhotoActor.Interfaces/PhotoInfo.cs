using System;
using System.Collections.Generic;

namespace PhotoAward.PhotoActors.Interfaces
{
    public class PhotoInfo
    {
        public byte [] ThumbnailBytes { get; set; }
        public string Filename { get; set; }
        public DateTime ? UploadDate { get; set; }
        public string Title { get; set; }

        public Guid ? Id { get; set; }
        
    }

}