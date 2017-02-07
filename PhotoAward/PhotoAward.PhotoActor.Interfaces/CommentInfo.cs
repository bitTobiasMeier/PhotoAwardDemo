using System;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActor.Interfaces
{
    public class CommentInfo
    {
        public Guid? Id { get; set; }
        public ActorId ActorId { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
    }
}