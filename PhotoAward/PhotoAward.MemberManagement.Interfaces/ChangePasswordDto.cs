namespace PhotoAward.MemberManagement.Interfaces
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
    }
}