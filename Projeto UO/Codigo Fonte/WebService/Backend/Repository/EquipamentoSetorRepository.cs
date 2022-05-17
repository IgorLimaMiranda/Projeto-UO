using Backend.Context;
using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class EquipamentoSetorRepository : Repository<EquipamentoSetor> {

        public List<EquipamentoSetor> ListarEquipamentosSala(int id) {
            ctx = new ContextModel();
            var equipamentosSala = from es in ctx.EquipamentoSetor
                                   .Include("Equipamento")
                                   where es.IdSetor == id
                                   orderby es.Equipamento.Descricao
                                   select es;
            
            return equipamentosSala.ToList();
        }

        public void CadastrarEquipamentosSetor(List<EquipamentoSetor> equipamentosSetor) {
            foreach(var equipamentoSetor in equipamentosSetor) {
                Add(equipamentoSetor);
            }
        }
    }
}
