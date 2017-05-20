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
        Task<PhotoInfo> SetPhotoAsync(PhotoInfo photo, CancellationToken cancellationToken);

        
        Task<PhotoInfo> GetPhotoAsync(CancellationToken cancellationToken);

        Task<CommentInfo> AddCommentAsync(CommentInfo comment,CancellationToken cancellationToken);

        Task<List<CommentInfo>> GetCommentsAsync(CancellationToken cancellationToken);

        Task DeleteAsync(CancellationToken cancellationToken);

    }
}
