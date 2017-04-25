using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PhotoAward.MemberActor.Interfaces;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.MemberActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class MemberActor : Actor, IMemberActor
    {
        [DataContract]
        private class MemberData
        {
            //ToDO: objekte immutable machen
            [DataMember]
            public string FirstName { get; set; }
            [DataMember]
            public string Surname { get; set; }
            [DataMember]
            public string Email { get; set; }
            [DataMember]
            public DateTime? EntryDate { get; set; }
            [DataMember]
            public DateTime? LastUpdate { get; set; }
            [DataMember]
            public Guid Id { get; set; }
            [DataMember]
            public byte[] PasswordSalt { get; set; }
            [DataMember]
            public byte[] PasswordHash { get; set; }
        }

        private const string DataKey = "memberData";

        /// <summary>
        /// Initializes a new instance of MemberActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public MemberActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        
        public async Task<InternalMemberDto> GetMember(CancellationToken cancellationToken)
        {
            var data =  await this.StateManager.GetStateAsync<MemberData>(DataKey, cancellationToken);
            return new InternalMemberDto()
            {
              Email = data.Email,
              FirstName = data.FirstName,
              Surname = data.Surname,
              LastUpdate = data.LastUpdate,
              EntryDate = data.EntryDate,
              Id = data.Id,
              PasswordHash = data.PasswordHash,
              PasswordSalt = data.PasswordSalt
            }; 
        }

        public async Task UpdatePassword(byte [] newPasswordHash, byte [] salt,  CancellationToken cancellationToken)
        {
            var datahelper = await this.StateManager.TryGetStateAsync<MemberData>(DataKey, cancellationToken);
            MemberData data;
            if (!datahelper.HasValue)
            {
                throw new Exception("Data of user not found!");
            }
            else
            {
                data = datahelper.Value;
            }
            data.LastUpdate = DateTime.Now;
            data.PasswordHash = newPasswordHash;
            data.PasswordSalt = salt;

            await this.StateManager.SetStateAsync<MemberData>(DataKey, data, cancellationToken);
        }

        public async Task<InternalMemberDto> SetMemberAsync(InternalMemberDto member, CancellationToken cancellationToken)
        {
            //Gibt es bereits Daten dieses Actors

            var datahelper = await this.StateManager.TryGetStateAsync<MemberData>(DataKey, cancellationToken);
            MemberData data;
            if (!datahelper.HasValue)
            {
                data = new MemberData() {EntryDate = DateTime.Now};
            }
            else
            {
                data = datahelper.Value;
            }
            data.Email = member.Email;
            data.LastUpdate = DateTime.Now;
            data.Surname = member.Surname;
            data.FirstName = member.FirstName;
            data.Id = member.Id;
            data.EntryDate = DateTime.Now;
            data.LastUpdate = DateTime.Now;
            data.PasswordHash = member.PasswordHash;
            data.PasswordSalt = member.PasswordSalt;
            
            await this.StateManager.SetStateAsync<MemberData>(DataKey, data, cancellationToken);
            //Fehler: Diese Daten werden nicht übernommen
            member.EntryDate = data.EntryDate;
            member.LastUpdate = data.LastUpdate;
            
            return member;
        }
    }
}
