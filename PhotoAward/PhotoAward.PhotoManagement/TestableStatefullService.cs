using System.Fabric;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Runtime;

namespace PhotoAward.PhotoManagement
{
    public class TestableStatefullService : StatefulService
    {
        public TestableStatefullService(StatefulServiceContext serviceContext) : base(serviceContext)
        {
        }

        public TestableStatefullService(StatefulServiceContext serviceContext, IReliableStateManagerReplica reliableStateManagerReplica) : base(serviceContext, reliableStateManagerReplica)
        {
            
        }

        //StateManager verstecken, so dass immer der Wrapper verwendet wird.
        protected new object StateManager => null;
    }
}