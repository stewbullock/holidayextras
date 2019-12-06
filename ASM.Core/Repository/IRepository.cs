using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ASM.Core.Repository
{
    public interface IRepository { }

    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        List<T> GetById(IEnumerable<int> idList);
        List<T> Find(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Add(List<T> entities);
        void Remove(T entity);
        void Remove(List<T> entities);
    }
}