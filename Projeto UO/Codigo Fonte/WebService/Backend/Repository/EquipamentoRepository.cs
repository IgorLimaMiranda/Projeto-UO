using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Linq;

namespace Backend.Repository {
    public class EquipamentoRepository : Repository<Equipamento> {
        //public override IQueryable<Equipamento> Get(Func<Equipamento, bool> predicate) {
        //    return GetAll().OrderByDescending(predicate).AsQueryable();
        //}

        public override IQueryable<Equipamento> GetAll() {
            return ctx.Set<Equipamento>().OrderBy(c => c.Descricao);
        }
    }
}
