using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActors.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IPhotoActor : IActor
    {
        Task<PhotoInfo> SetPhoto(PhotoInfo photo, CancellationToken cancellationToken);

        
        Task<PhotoInfo> GetPhoto(CancellationToken cancellationToken);

        Task<CommentInfo> AddComment(CommentInfo comment,CancellationToken cancellationToken);

        Task<List<CommentInfo>> GetComments(CancellationToken cancellationToken);

    }
}
