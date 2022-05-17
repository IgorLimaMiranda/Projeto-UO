using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroEquipamento")]
    public class EquipamentoController : ApiController {
        private EquipamentoRepository equipamentoRepository;
        public EquipamentoController() {
            equipamentoRepository = new EquipamentoRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarEquipamento(Equipamento equipamento) {
            try {
                equipamentoRepository.Add(equipamento);
                return true;
            } catch(Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Equipamento> ListarEquipamentos() {
            try {
                return equipamentoRepository.GetAll();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public Equipamento BuscarEquipamento(int id) {
            try {
                return equipamentoRepository.Get(e => e.Id.Equals(id)).First();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarEquipamento(Equipamento equipamento) {
            try {
                equipamentoRepository.Update(e => e.Id.Equals(equipamento.Id), equipamento);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Excluir")]
        public bool ExcluirEquipamento(Equipamento equipamento) {
            try {
                equipamentoRepository.Delete(e => e.Id.Equals(equipamento.Id));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
