namespace PhotoAward.PhotoManagement.Interfaces
{
    public interface IPhotoManagementClientFactory
    {
        IPhotoManagement CreatePhotoClient();
        IPhotoComments CreateCommentsClient();
    }
}