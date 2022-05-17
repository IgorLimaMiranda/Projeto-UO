using Backend.Model;
using Backend.Repository.Base;
using System.Linq;

namespace Backend.Repository {
    public class SalaRepository : Repository<Sala> {
        public override IQueryable<Sala> GetAll() {
            return ctx.Set<Sala>().OrderBy(c => c.Numero);
        }
    }
}
