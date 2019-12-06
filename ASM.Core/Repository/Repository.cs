using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ASM.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IIdentity
    {
        #region Fields 

        private readonly List<T> _entities;

        #endregion

        #region Properties 

        private List<T> Entities
        {
            get { return _entities; }
        }

        #endregion

        #region Methods 

        #region Constructor 

        public Repository(List<T> entities)
        {
            _entities = entities;
        }

        #endregion

        /// <summary>
        /// Retrieve every entity
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return Entities;
        }

        /// <summary>
        /// Retrieve the first entity that matches the specified id, null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return GetAll().FirstOrDefault(entity => entity.Id == id);
        }

        /// <summary>
        /// Retrieve every entity that matches the specified id's
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<T> GetById(IEnumerable<int> idList)
        {
            return Find(entity => idList.Contains(entity.Id));
        }

        /// <summary>
        /// Retrieve every entity based on the specified expression filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Find(Expression<Func<T, bool>> filter)
        {
            return GetAll().AsQueryable<T>().Where(filter).ToList();
        }

        /// <summary>
        /// Add entity to context associated with repository
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Add entities to context associated with repository
        /// </summary>
        /// <param name="entities"></param>
        public void Add(List<T> entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// Delete from context associated with repository
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        /// <summary>
        /// Delete from context associated with repository
        /// </summary>
        /// <param name="entities"></param>
        public void Remove(List<T> entities)
        {
            entities.ForEach(Remove);
        }

        #endregion
    }
}