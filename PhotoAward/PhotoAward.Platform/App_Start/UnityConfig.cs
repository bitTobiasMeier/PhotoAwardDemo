using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.Platform
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            container.RegisterType<IMemberManagementClientFactory, MemberManagementClientFactory>();
            container.RegisterType<IPhotoManagementClientFactory, PhotoManagementClientFactory>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}