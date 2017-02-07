using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using PhotoAward.MemberActor.Interfaces;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.MemberManagement
{


    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class MemberManagement : StatefulService, IMemberManagement
    {
        public MemberManagement(StatefulServiceContext context)
            : base(context)
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

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

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
                        throw new Exception("Mitglied mit dieser Emailadresse exisitiert bereits.");
                    }
                    member.Id = Guid.NewGuid();
                    var memberActorId = ActorId.CreateRandom();
                    await members.AddAsync(tx, member.Email, memberActorId);
                    //await SheepConnectionFactory.GetMember(sheepActorId).SetLocation(timestamp, location.Latitude, location.Longitude);

                    var result = await MemberClientFactory.GetMember(memberActorId)
                        .SetMemberAsync(member, CancellationToken.None);
                    member.EntryDate = result.EntryDate;
                    member.LastUpdate = result.LastUpdate;

                    // Update service with new Actor
                    await members.AddOrUpdateAsync(tx, member.Email, memberActorId, (key, actorId) => memberActorId);
                    await tx.CommitAsync();
                    return member;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<MemberDto> GetMember(string email)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var members = await StateManager.GetOrAddAsync<IReliableDictionary<string, ActorId>>("members");
                //Gibt es bereits ein Mitglied mit dieser Emailadresse ?
                var existendMemberActorId = members.TryGetValueAsync(tx, email);
                if (!existendMemberActorId.Result.HasValue)
                {
                    throw new Exception("Mitglied mit dieser Emailadresse exisitiert nicht.");
                }

                var result = await MemberClientFactory.GetMember(existendMemberActorId.Result.Value).GetMember(CancellationToken.None);

                return new MemberDto() {Email = result.Email, EntryDate = result.EntryDate,FirstName=result.FirstName, LastUpdate = result.LastUpdate,Surname = result.Surname, Id = result.Id};
            }
        }

    }
}

