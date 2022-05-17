using System;
using System.Linq;

namespace Backend.Interface {
    public interface IRepository<TEntity> where TEntity : class {
        IQueryable<TEntity> GetAll();
		IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
		TEntity Find(params object[] obj);
		void Update(Func<TEntity, bool> predicate, TEntity obj);
		void SaveAll();
		void Add(TEntity obj);
		void Delete(Func<TEntity, bool> predicate);
    }
}
