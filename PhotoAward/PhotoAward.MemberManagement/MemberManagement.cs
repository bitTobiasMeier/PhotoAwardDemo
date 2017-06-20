using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using PhotoAward.MemberActor.Interfaces;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.ReliableServices.Core;

namespace PhotoAward.MemberManagement
{


    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class MemberManagement : BackupRestoreStatefulService, IMemberManagement
    {
        public MemberManagement(StatefulServiceContext context,IFileStore fileStore, IServiceEventSource serviceEventSource)
            : base(context,fileStore, serviceEventSource)
        {
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(
                    this.CreateServiceRemotingListener
                )
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.
            
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        /// <summary>
        /// Für jedes Mitglied wir ein Actor erzeugt. Als Identifier wird die Emailadresse verwendet. Um eine Möglichkeit zu schaffen
        /// um alle Actors zu ermitteln, führen wir zusätzlich ein Dictionary mit allen Akteuren.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<MemberDto> AddMember(MemberDto member)
        {
            try
            {
                using (var tx = StateManager.CreateTransaction())
                {
                    var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");
                    //Gibt es bereits ein Mitglied mit dieser Emailadresse ?
                    var existendMemberActorId = members.TryGetValueAsync(tx, member.Email);
                    if (existendMemberActorId.Result.HasValue)
                    {
                        throw new Exception("Mitglied mit dieser Emailadresse existiert bereits.");
                    }
                    member.Id = Guid.NewGuid();
                    var memberActorId = new ActorId(member.Email);
                    await members.AddAsync(tx, member.Email, memberActorId);
                    
                    var memberActor = MemberActorClientFactory.CreateMemberActorClient(memberActorId);
                    var internalMember = CreateInternalMemberAndHash(member);

                    var result = await memberActor.SetMemberAsync(internalMember, CancellationToken.None);
                    member.EntryDate = result.EntryDate;
                    member.LastUpdate = result.LastUpdate;

                    // Update service with new Actor
                    await members.AddOrUpdateAsync(tx, member.Email, memberActorId, (key, actorId) => memberActorId);
                    await tx.CommitAsync();
                    ServiceEventSource.Current.ServiceMessage(this.Context,
                                "Mitglied hinzugefügt: {0}, Id: {1} ", member.Email, result.Id);
                    return member;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private static InternalMemberDto CreateInternalMemberAndHash(MemberDto member)
        {
            var internalMember = new InternalMemberDto()
            {
                Email = member.Email,
                EntryDate = member.EntryDate,
                FirstName = member.FirstName,
                LastUpdate = member.LastUpdate,
                Id = member.Id,
                Surname = member.Surname
            };
            var passwordHash = PasswordHashCalculator.ComputeHash(member.Password);
            internalMember.PasswordHash = passwordHash.Hash;
            internalMember.PasswordSalt = passwordHash.Salt;
            return internalMember;
        }


        public async Task<MemberDto> GetMember(string email)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");
                //Gibt es bereits ein Mitglied mit dieser Emailadresse ?
                var existendMemberActorId = await members.TryGetValueAsync(tx, email);
                if (!existendMemberActorId.HasValue)
                {
                    throw new Exception("Mitglied mit dieser Emailadresse existiert nicht.");
                }
               
                var result = await MemberActorClientFactory.CreateMemberActorClient(existendMemberActorId.Value).GetMemberAsync(CancellationToken.None);

                return new MemberDto() {Email = result.Email, EntryDate = result.EntryDate,FirstName=result.FirstName, LastUpdate = result.LastUpdate,Surname = result.Surname, Id = result.Id};
            }
        }

        public async Task<MemberDto> LoginMember(string email, string password)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");
                //Gibt es bereits ein Mitglied mit dieser Emailadresse ?
                var existendMemberActorId = await members.TryGetValueAsync(tx, email);
                if (!existendMemberActorId.HasValue)
                {
                    throw new Exception("Mitglied mit dieser Emailadresse existiert nicht.");
                }
           
                var result = await MemberActorClientFactory.CreateMemberActorClient(existendMemberActorId.Value).GetMemberAsync(CancellationToken.None);

                //Password-Validation
                if (!PasswordHashCalculator.VerifyPassword(password, result.PasswordSalt, result.PasswordHash))
                    throw new Exception("The combination of username and password is invalid");

                return new MemberDto() { Email = result.Email, EntryDate = result.EntryDate, FirstName = result.FirstName, LastUpdate = result.LastUpdate, Surname = result.Surname, Id = result.Id };
            }
        }

        public async Task<MemberDto> GetMemberOnMemberId(Guid memberId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");

                var asyncEnumerable = await members.CreateEnumerableAsync(tx);
                using (var asyncEnumerator = asyncEnumerable.GetAsyncEnumerator())
                {
                    while (await asyncEnumerator.MoveNextAsync(CancellationToken.None))
                    {
                        var actorId = asyncEnumerator.Current.Value;
                        var member = await MemberActorClientFactory.CreateMemberActorClient(actorId).GetMemberAsync(CancellationToken.None);
                        if (member !=null && member.Id == memberId) return new MemberDto()
                        {
                            Id = member.Id,
                            Email = member.Email,
                            EntryDate = member.EntryDate,
                            FirstName = member.FirstName,
                            LastUpdate = member.LastUpdate,
                            Surname = member.Surname
                        };
                    }
                }
                return null;
            }
        }

        public async Task<MemberDto> ChangePassword(ChangePasswordDto dto)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");
                //Gibt es bereits ein Mitglied mit dieser Emailadresse ?
                var existendMemberActorId = members.TryGetValueAsync(tx, dto.Email);
                if (!existendMemberActorId.Result.HasValue)
                {
                    throw new Exception("Mitglied mit dieser Emailadresse existiert nicht.");
                }

                var actorFactory = MemberActorClientFactory.CreateMemberActorClient(existendMemberActorId.Result.Value);

                var result = await actorFactory.GetMemberAsync(CancellationToken.None);

                //Password-Validation
                if (!PasswordHashCalculator.VerifyPassword(dto.OldPassword, result.PasswordSalt, result.PasswordHash))
                    throw new Exception("The combination of username and password is invalid");

                var passwordHash = PasswordHashCalculator.ComputeHash(dto.NewPassword);

                await actorFactory.UpdatePasswordAsync(passwordHash.Hash, passwordHash.Salt, CancellationToken.None);

                return new MemberDto() { Email = result.Email, EntryDate = result.EntryDate, FirstName = result.FirstName, LastUpdate = result.LastUpdate, Surname = result.Surname, Id = result.Id };
            }
        }

        public async Task<List<MemberName>> GetNamesOfMembersAsync(List<Guid?> authorIds)
        {
            var tasks = new List<Task<MemberDto>>();
            foreach (var authorId in authorIds )
            {
                 if (authorId == null) continue;
                var result = this.GetMemberOnMemberId(authorId.Value);
                tasks.Add(result);
            }
            var results = new List<MemberName>();
            foreach (var task in tasks)
            {
                var result = await task;
                results.Add(new MemberName() {Id = result.Id, Name = result.FirstName +" " + result.Surname});
            }
            return results;
        }

    
    }
}

