using FluentMailer.Unity.App_Start;
using Microsoft.Practices.Unity;

namespace FluentMailer.Unity.Extensions
{
    public static class UnityContainerExtensions
    {
        public static void RegisterFluentMailerDependencies(this IUnityContainer container)
        {
            UnityConfig.Configure(container);
        }
    }
}