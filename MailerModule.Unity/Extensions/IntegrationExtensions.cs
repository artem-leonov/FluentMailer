using Microsoft.Practices.Unity;

namespace MailerModule.Unity.Extensions
{
    public static class IntegrationExtensions
    {
        public static void RegisterMailerModuleDependencies(this IUnityContainer container)
        {
            UnityConfig.Configure(container);
        }
    }
}