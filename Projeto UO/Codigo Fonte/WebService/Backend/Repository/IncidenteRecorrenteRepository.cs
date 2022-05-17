using Backend.Model;
using Backend.Repository.Base;
using System.Linq;

namespace Backend.Repository {
    public class IncidenteRecorrenteRepository : Repository<IncidenteRecorrente> {
        public override IQueryable<IncidenteRecorrente> GetAll() {
            return ctx.Set<IncidenteRecorrente>().OrderBy(i => i.Descricao);
        }
    }
}
