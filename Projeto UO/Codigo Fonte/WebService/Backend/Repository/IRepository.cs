using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{

        public interface IRepository<TEntity> where TEntity : class
        {
            IQueryable<TEntity> GetAll();
            IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
            TEntity Find(params object[] obj);
            void Update(Func<TEntity, bool> predicatge, TEntity obj);
            void SaveAll();
            void Add(TEntity obj);
            void Delete(Func<TEntity, bool> predicate);
        }
    
}
