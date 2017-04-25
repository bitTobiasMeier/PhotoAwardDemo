using System;

namespace PhotoAward.MemberActor.Interfaces
{
    public class InternalMemberDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Guid Id { get; set; }
        
        public byte[] PasswordHash { get; set; }
        public byte [] PasswordSalt { get; set; }
    }
}