using ASM.Core.Dependency;

namespace ASM.HolidayExtra.CompositionRoot
{
    public class DependencyResolver
    {
        #region Methods 

        public static IDependencyResolver Create()
        {
            return DependencyResolverFactory.Create(ModuleResolver.Create());
        }

        #endregion
    }
}