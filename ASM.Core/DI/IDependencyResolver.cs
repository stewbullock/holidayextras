using System;
using System.Collections.Generic;

namespace ASM.Core.Dependency
{
    public interface IDependencyResolver : IDisposable
    {
        T Get<T>();
        T Get<T>(string name);
        object Get(Type serviceType);
        IEnumerable<object> GetAll(Type serviceType);
    }
}