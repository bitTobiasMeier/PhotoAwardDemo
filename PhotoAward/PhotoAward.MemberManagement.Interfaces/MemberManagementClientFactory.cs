using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace PhotoAward.MemberManagement.Interfaces
{
    public static class MemberManagementClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/MemberManagement");

        public static IMemberManagement CreateMemberManagementClient()
        {
            return ServiceProxy.Create<IMemberManagement>(ServiceUrl, new ServicePartitionKey(0));
        }
        
    }
}