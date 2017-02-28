using Delegate.UI.IoC.Registry;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Delegate.UI.IoC
{
    public static class Bootstrap
    {
        private static IContainer _theContainer;

        private static IServiceLocator _theServiceLocator;
        public static IContainer TheContainer => _theContainer ?? (_theContainer = CreateContainer());

        public static IServiceLocator TheServiceLocator => _theServiceLocator ??
                                                           (_theServiceLocator = CreateServiceLocator());

        private static IServiceLocator CreateServiceLocator()
        {
            return new StructureMapServiceLocator(TheContainer);
        }

        private static IContainer CreateContainer()
        {
            return new Container(DelegateRegistryBuilder.GetStandardRegistry());
        }
    }
}