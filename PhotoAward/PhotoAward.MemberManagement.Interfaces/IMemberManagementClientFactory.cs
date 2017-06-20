using System.Threading.Tasks;

namespace PhotoAward.MemberManagement.Interfaces
{
    public interface IMemberManagementClientFactory
    {
        Task<IMemberManagement> CreateMemberManagementClientAsync();
        Task TakeFullBackUpAsync(string nameOfBackupSet);
        Task RestoreBackupAsync(string nameOfBackupSet);
    }
}