using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class HistoricoEnsalamentoRepository : Repository<HistoricoEnsalamento> {
        public override IQueryable<HistoricoEnsalamento> GetAll() {
            return ctx.Set<HistoricoEnsalamento>().OrderByDescending(he => he.DataHora);
        }

        public List<string> RetornarOcupantesDistinct() {
            var ocupantes = (from he in ctx.HistoricoEnsalamento
                             select he.Ocupante).Distinct();
            
            return ocupantes.ToList();
        }

        public List<string> RetornarUsuariosDistinct() {
            var usuarios = (from he in ctx.HistoricoEnsalamento
                             select he.Usuario).Distinct();
            
            return usuarios.ToList();
        }

        public List<string> RetornarTurmasDistinct() {
            var turmas = (from he in ctx.HistoricoEnsalamento
                             select he.Turma).Distinct();

            return turmas.ToList();
        }

        public List<string> RetornarSalasDistinct() {
            var salas = (from he in ctx.HistoricoEnsalamento
                          select he.Sala).Distinct();

            return salas.ToList();
        }

        public List<string> RetornarAcoesDistinct() {
            var acoes = (from he in ctx.HistoricoEnsalamento
                         select he.Acao).Distinct();
            
                return acoes.ToList();
        }

        public List<HistoricoEnsalamento> GetAllhistoricos() {
            var historicos = from h in ctx.HistoricoEnsalamento
                               select h;

                return historicos.ToList();
            
        }
    }
}
