using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;

namespace ASM.Core.Dependency
{
    public sealed class NinjectDependencyResolver : IDependencyResolver
    {
        #region Fields 

        private readonly IKernel _kernel;
        private bool _isDisposed;

        #endregion

        #region Methods 

        #region Constructor 

        public NinjectDependencyResolver(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(new NinjectSettings { LoadExtensions = false }, modules);
        }

        #endregion

        ~NinjectDependencyResolver()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _kernel?.Dispose();

            _isDisposed = true;

            // Stop garbage collector calling destructor
            GC.SuppressFinalize(this);
        }

        public object Get(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public T Get<T>(string name)
        {
            return _kernel.Get<T>(new Parameter(name, true, true));
        }

        #endregion
    }
}