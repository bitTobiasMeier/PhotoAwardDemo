using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActor
{
    [DataContract]
    internal class PhotoComment
    {
        public Guid ? Id { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }

        


    }
}