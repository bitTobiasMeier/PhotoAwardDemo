using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhotoAward.PhotoActor
{
    [DataContract]
    internal class PhotoData
    {
        public byte[] ThumbnailAsByte { get; set; }
        public string FileName { get; set; }

        public List<PhotoComment> Comments
        {
            get { return _comments; }
        }

        [DataMember]
        private List<PhotoComment> _comments = new List<PhotoComment>();

        public DateTime UploadDate { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
    }
}