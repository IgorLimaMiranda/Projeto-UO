using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Linq;

namespace Backend.Repository {
    public class CursoRepository : Repository<Curso> {
        //public override IQueryable<Curso> Get(Func<Curso, bool> predicate) {
        //    return GetAll().OrderByDescending(predicate).AsQueryable();
        //}

        public override IQueryable<Curso> GetAll() {
            return ctx.Set<Curso>().OrderBy(c => c.Descricao);
        }
    }
}
