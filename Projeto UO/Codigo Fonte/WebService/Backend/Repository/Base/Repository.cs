using Backend.Context;
using Backend.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Base {
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class {
        public ContextModel ctx;

        public Repository() {
            ctx = new ContextModel();
        }
        public virtual void Add(TEntity obj) {
            ctx = new ContextModel();
            ctx.Set<TEntity>().Add(obj);
            SaveAll();
        }

        public void Delete(Func<TEntity, bool> predicate) {
            ctx = new ContextModel();
            ctx.Set<TEntity>().Where(predicate).ToList().ForEach(del => ctx.Set<TEntity>().Remove(del));
            SaveAll();
        }

        public void Dispose() {
            ctx.Dispose();
        }

        public TEntity Find(params object[] obj) {
            ctx = new ContextModel();
            return ctx.Set<TEntity>().Find(obj);
        }

        public virtual IQueryable<TEntity> Get(Func<TEntity, bool> predicate) {
            return GetAll().Where(predicate).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll() {
            return ctx.Set<TEntity>();
        }

        public void SaveAll() {
            ctx.SaveChanges();
        }

        public void Update(Func<TEntity, bool> predicate, TEntity obj) {
            ctx = new ContextModel();
            var atualizar = ctx.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
            atualizar = obj;
            ctx.Set<TEntity>().Attach(obj);
            ctx.Entry(atualizar).State = EntityState.Modified;
            ctx.Entry(obj).State = EntityState.Modified;
            SaveAll();
        }
    }
}
