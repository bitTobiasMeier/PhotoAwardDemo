using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.PhotoManagement
{
    public interface IPhotoManagementStates
    {
        Task AddPhotoIdActorMapping(ITransaction tx,Guid photoId, ActorId photoActorId);
        Task AddPhotoToMember(ITransaction tx, Guid memberId, ActorId photoActorId);
        Task<ConditionalValue<ActorId>> GetPhotoActorId(ITransaction tx, Guid photoId);
        ITransaction CreateTransaction();
        Task<List<ActorId>> GetPhotoActorIdListOfMember(ITransaction tx, Guid memberId);
    }
}