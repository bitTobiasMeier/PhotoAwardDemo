using System.Web.Http;
using Microsoft.Practices.Unity;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;
using Unity.WebApi;

namespace PhotoAward.AdministrationPlatform.App_Start
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