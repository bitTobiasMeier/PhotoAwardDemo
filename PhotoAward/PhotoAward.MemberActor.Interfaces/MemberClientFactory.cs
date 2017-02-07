using System;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace PhotoAward.MemberActor.Interfaces
{
    public static class MemberClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/MemberActorService");

        public static IMemberActor GetMember(ActorId actorId)
        {
            var actor = ActorProxy.Create<IMemberActor>(actorId, ServiceUrl);
            return actor;
        }
    }
}