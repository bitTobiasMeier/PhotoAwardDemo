using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;


namespace PhotoAward.MemberActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IMemberActor : IActor
    {
        Task<InternalMemberDto> GetMember(CancellationToken cancellationToken);

        Task<InternalMemberDto> SetMemberAsync(InternalMemberDto member, CancellationToken cancellationToken);

        Task UpdatePassword(byte[] newPasswordHash, byte[] salt, CancellationToken cancellationToken);
    }
}
