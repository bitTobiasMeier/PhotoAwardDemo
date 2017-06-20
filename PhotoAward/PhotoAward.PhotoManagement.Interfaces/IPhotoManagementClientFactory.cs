using System.Threading.Tasks;

namespace PhotoAward.PhotoManagement.Interfaces
{
    public interface IPhotoManagementClientFactory
    {
        IPhotoManagement CreatePhotoClient();
        IPhotoComments CreateCommentsClient();
        Task TakeFullBackUpAsync(string nameOfBackupset);
        Task RestoreFullBackup(string nameOfBackupset);


    }
}