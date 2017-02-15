using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace PhotoAward.PhotoManagement
{
    public class PhotoManagementStates : IPhotoManagementStates
    {
        private readonly IReliableStateManagerReplica _stateManager;
        private const string PhotoIdToPhotoActorDictionary = "photoActors";
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
                (PhotoIdToPhotoActorDictionary);
            await photoActorDictionary.AddAsync(tx, photoId.ToString(), photoActorId);
        }

        public async Task AddPhotoActorToMemberId(ITransaction tx, Guid memberId, ActorId photoActorId)
        {
            var memberIdPhotoActorDictionary = await _stateManager.GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                PhotoActorMemberDictName);
            var conditionalActorList = await memberIdPhotoActorDictionary.TryGetValueAsync(tx, memberId.ToString());
            var actorList = !conditionalActorList.HasValue ? new List<ActorId>() : conditionalActorList.Value;
            actorList.Add(photoActorId);
            await memberIdPhotoActorDictionary.SetAsync(tx, memberId.ToString(), actorList);
        }

        public async Task<ConditionalValue<ActorId>> GetPhotoActorId(ITransaction tx,Guid photoId)
        {
            var photoActorState = await _stateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                (PhotoIdToPhotoActorDictionary);
            
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

        public async Task<IList<Tuple<Guid,ActorId>>> GetPhotos(ITransaction tx)
        {
            var list = new List<Tuple<Guid, ActorId>>();
            var photoMemberActorDictionary = await this._stateManager
                    .GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
            var asyncEnumerable = await photoMemberActorDictionary.CreateEnumerableAsync(tx);
            using (IAsyncEnumerator<KeyValuePair<string, List<ActorId>>> asyncEnumerator = asyncEnumerable.GetAsyncEnumerator())
            {
                while (await asyncEnumerator.MoveNextAsync(CancellationToken.None))
                {
                    var key = new Guid( asyncEnumerator.Current.Key);
                    var actorlist = asyncEnumerator.Current.Value;
                    foreach (var actorId in actorlist)
                    {
                        list.Add(new Tuple<Guid, ActorId>(key, actorId));
                    }
                    
                }
            }
            return list;
        }

        public async Task RemoveActor(Guid photoId, ActorId imageActorId, ITransaction tx)
        {
            var photoActorDictionary = await this._stateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>
                (PhotoIdToPhotoActorDictionary);

            
            var photoMemberActorDictionary = await this._stateManager
                    .GetOrAddAsync<IReliableDictionary<string, List<ActorId>>>(
                        PhotoActorMemberDictName);
            var asyncEnumerable = await photoMemberActorDictionary.CreateEnumerableAsync(tx);
            using (IAsyncEnumerator<KeyValuePair<string, List<ActorId>>> asyncEnumerator = asyncEnumerable.GetAsyncEnumerator())
            {
                while (await asyncEnumerator.MoveNextAsync(CancellationToken.None))
                {
                    var key = new Guid(asyncEnumerator.Current.Key);
                    var actorlist = asyncEnumerator.Current.Value;
                    var found = false;
                    foreach (var actorId in actorlist)
                    {
                        if (actorId == imageActorId)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        actorlist.Remove(imageActorId);
                        await photoMemberActorDictionary.AddAsync(tx, key.ToString(), actorlist);
                    }
                }
            }

            
            

            await photoActorDictionary.TryRemoveAsync(tx, photoId.ToString());
        }
    }
}