using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroEquipamentoSetor")]
    public class EquipamentoSetorController : ApiController {
        private EquipamentoSetorRepository equipamentoSetorRepository;
        public EquipamentoSetorController() {
            equipamentoSetorRepository = new EquipamentoSetorRepository();
        }
         
        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarEquipamento(EquipamentoSetor equipamentoSetor) {
            try {
                equipamentoSetorRepository.Add(equipamentoSetor);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("CadastrarEquipamentosSetor")]
        public bool CadastrarEquipamentosSetor(List<EquipamentoSetor> equipamentosSetor) {
            try {
                equipamentoSetorRepository.CadastrarEquipamentosSetor(equipamentosSetor);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<EquipamentoSetor> ListarEquipamentos() {
            try {
                return equipamentoSetorRepository.GetAll();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarEquipamentosSala/{id}")]
        public IEnumerable<EquipamentoSetor> ListarEquipamentosSala(int id) {
            try {
                return equipamentoSetorRepository.ListarEquipamentosSala(id);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarEquipamento(EquipamentoSetor equipamentoSetor) {
            try {
                equipamentoSetorRepository.Update(e => e.IdEquipamento.Equals(equipamentoSetor.IdEquipamento) && e.IdSetor.Equals(equipamentoSetor.IdSetor), equipamentoSetor);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Excluir")]
        public bool ExcluirEquipamento(EquipamentoSetor equipamentoSetor) {
            try {
                equipamentoSetorRepository.Delete(e => e.IdEquipamento.Equals(equipamentoSetor.IdEquipamento) && e.IdSetor.Equals(equipamentoSetor.IdSetor));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("ExcluirEquipamentosSetor/{idSetor}")]
        public bool ExcluirEquipamentosSetor(int idSetor) {
            try {
                equipamentoSetorRepository.Delete(e => e.IdSetor.Equals(idSetor));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
