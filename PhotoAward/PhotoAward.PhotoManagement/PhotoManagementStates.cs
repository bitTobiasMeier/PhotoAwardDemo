using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace PhotoAward.PhotoManagement
{
    public class PhotoManagementStates : IPhotoManagementStates
    {
        private readonly IReliableStateManagerReplica _stateManager;
        private const string PhotoactorsDictName = "photoActors";
        private const string PhotoActorMemberDictName = "photoActorMemberDictionary";

        public PhotoManagementStates(IReliableStateManagerReplica stateManager)
        {
            _stateManager = stateManager;
        }

        public ITransaction CreateTransaction()
        {
            return this._stateManager.CreateTransaction();
        }

        public async Task AddPhotoIdActorMapping(ITransaction tx,Guid photoId, ActorId photoActorId)
        {
            var photoActorDictionary = await this._stateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                (PhotoactorsDictName);
            await photoActorDictionary.AddAsync(tx, photoId.ToString(), photoActorId);
        }

        public async Task AddPhotoToMember(ITransaction tx, Guid memberId, ActorId photoActorId)
        {
            var photoMemberActorDictionary = await _stateManager.GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                PhotoActorMemberDictName);
            var conditionalActorList = await photoMemberActorDictionary.TryGetValueAsync(tx, memberId.ToString());
            var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
            actorList.Add(photoActorId);
            await photoMemberActorDictionary.SetAsync(tx, memberId.ToString(), actorList);
        }

        public async Task<ConditionalValue<ActorId>> GetPhotoActorId(ITransaction tx,Guid photoId)
        {
            var photoActorState = await _stateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                (PhotoactorsDictName);
            
            var actorId = await photoActorState.TryGetValueAsync(tx, photoId.ToString());
            return actorId;
        }

        public async Task<List<ActorId>>  GetPhotoActorIdListOfMember(ITransaction tx, Guid memberId)
        {
            var photoMemberActorDictionary = await this._stateManager
                    .GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
            var conditionalActorList = await photoMemberActorDictionary.TryGetValueAsync(tx, memberId.ToString());
            var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
            return actorList;
        }
    }
}