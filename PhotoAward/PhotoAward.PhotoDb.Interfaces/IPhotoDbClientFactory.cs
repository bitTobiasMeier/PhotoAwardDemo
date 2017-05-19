namespace PhotoAward.PhotoDb.Interfaces
{
    public interface IPhotoDbClientFactory
    {
        IPhotoDbService CreatePhotoDbClient();
    }
}