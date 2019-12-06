using Ninject.Modules;

namespace ASM.Core.Dependency
{
    public class DependencyResolverFactory
    {
        #region Methods 

        public static IDependencyResolver Create(params INinjectModule[] dependencyModules)
        {
            return new NinjectDependencyResolver(dependencyModules);
        }

        #endregion        
    }
}