using FluentMailer.Factories.Interfaces;
using FluentMailer.Interfaces;
using Microsoft.Practices.Unity;

namespace FluentMailer.Unity.Factories
{
    internal class UnityFluentMailerMessageBodyCreatorFactory: IFluentMailerMessageBodyCreatorFactory
    {
        private readonly IUnityContainer _container;

        public UnityFluentMailerMessageBodyCreatorFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IFluentMailerMessageBodyCreator Create()
        {
            return _container.Resolve<IFluentMailerMessageBodyCreator>();
        }
    }
}