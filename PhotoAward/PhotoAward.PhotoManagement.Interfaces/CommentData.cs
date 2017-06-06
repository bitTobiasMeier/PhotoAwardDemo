using System;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public class CommentData
    {
        public string Comment { get; set; }
        public Guid ? AuthorId { get; set; }
        public string Authorname { get; set; }
        public Guid PhotoId { get; set; }
        public DateTime CommentDate { get; set; }
        public Guid? Id { get; set; }
    }
}