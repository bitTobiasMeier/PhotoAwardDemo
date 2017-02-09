using System;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public class CommentUploadData
    {
        public string Comment { get; set; }
        public string Email { get; set; }
        public Guid PhotoId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}