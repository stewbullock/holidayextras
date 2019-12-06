using ASM.Core.Dependency;
using Ninject.Modules;

namespace ASM.HolidayExtra.CompositionRoot
{
    public class ModuleResolver
    {
        #region Methods 

        public static INinjectModule[] Create()
        {
            return new[]
            {
                DependencyModule.Create(),
                Core.Dependency.DependencyModule.Create()
            };
        }

        #endregion
    }
}