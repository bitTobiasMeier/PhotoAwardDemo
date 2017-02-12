using System;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public class PhotoMemberInfo
    {
        public string Email { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public Guid ? PhotoId { get; set; }
    }
}