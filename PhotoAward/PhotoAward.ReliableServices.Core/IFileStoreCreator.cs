using System.Fabric;

namespace PhotoAward.ReliableServices.Core
{
    public interface IFileStoreCreator
    {
        IFileStore CreateFileStore(StatefulServiceContext context);
    }
}