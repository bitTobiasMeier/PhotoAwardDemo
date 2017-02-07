using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActor
{
    [DataContract]
    internal class PhotoComment
    {
        public Guid ? Id { get; set; }
        public ActorId ActorId { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }

        


    }
}