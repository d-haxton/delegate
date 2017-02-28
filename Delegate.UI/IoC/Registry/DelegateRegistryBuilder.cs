namespace Delegate.UI.IoC.Registry
{
    public class DelegateRegistryBuilder
    {
        public static StructureMap.Registry GetStandardRegistry()
        {
            var registry = new StructureMap.Registry();
            registry.IncludeRegistry<DelegateRegistry>();
            return registry;
        }
    }
}