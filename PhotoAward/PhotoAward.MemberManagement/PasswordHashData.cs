namespace PhotoAward.MemberManagement
{
    public class PasswordHashData
    {
        public byte[] Hash { get; set; }
        public byte [] Salt { get; set; }
    }
}