using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhotoAward.PhotoActors
{
    [DataContract]
    internal class PhotoData
    {
        //ToDO: objekte immutable machen
        [DataMember]
        public byte[] ThumbnailAsByte { get; set; }
        [DataMember]
        public string FileName { get; set; }

        public List<PhotoComment> Comments
        {
            get { return _comments; }
        }

        [DataMember]
        private List<PhotoComment> _comments = new List<PhotoComment>();
        [DataMember]
        public DateTime UploadDate { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string OwnerEmail { get; set; }
    }
}