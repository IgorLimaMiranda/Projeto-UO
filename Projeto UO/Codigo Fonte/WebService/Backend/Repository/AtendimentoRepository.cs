using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class AtendimentoRepository : Repository<Atendimento> {
        public List<Atendimento> GetAllAtendimentosDeChamado(int idChamado) {
            var atendimentos = (from a in ctx.Atendimento
                                .Include("Usuario")
                                where a.IdChamado.Equals(idChamado)
                                orderby a.DataAtendimento descending
                                select a);

            if (atendimentos.Any())
                return atendimentos.ToList();

            return null;
        }
    }
}
