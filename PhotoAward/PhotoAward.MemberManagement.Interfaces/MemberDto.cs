using System;

namespace PhotoAward.MemberManagement.Interfaces
{
    public class MemberDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public DateTime ? EntryDate { get; set; }
        public DateTime ? LastUpdate { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}