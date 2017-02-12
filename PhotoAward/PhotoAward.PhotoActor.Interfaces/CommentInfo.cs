using System;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActors.Interfaces
{
    public class CommentInfo
    {
        public string Comment { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PhotoId { get; set; }
        public DateTime CommentDate { get; set; }
        public Guid? Id { get; set; }
    }
}