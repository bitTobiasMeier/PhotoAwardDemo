using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace PhotoAward.MemberManagement.Interfaces
{
    public class MemberManagementClientFactory : IMemberManagementClientFactory
    {
        private static readonly Uri ServiceUrl = new Uri("fabric:/PhotoAward/MemberManagement");

        public IMemberManagement CreateMemberManagementClient()
        {
            return ServiceProxy.Create<IMemberManagement>(ServiceUrl, new ServicePartitionKey(0));
        }
        
    }
}