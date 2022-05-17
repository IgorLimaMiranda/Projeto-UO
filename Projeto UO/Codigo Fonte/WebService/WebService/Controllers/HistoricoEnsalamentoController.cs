using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("HistoricoEnsalamento")]
    public class HistoricoEnsalamentoController : ApiController{
        private HistoricoEnsalamentoRepository historicoEnsalamentoRepository;
        public HistoricoEnsalamentoController() {
            historicoEnsalamentoRepository = new HistoricoEnsalamentoRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarHistoricoEnsalamento(HistoricoEnsalamento historicoEnsalamento) {
            try {
                historicoEnsalamentoRepository.Add(historicoEnsalamento);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<HistoricoEnsalamento> ListarHistoricoEnsalamentos() {
            try {
                return historicoEnsalamentoRepository.GetAll();
            } catch (Exception e) {
                return new List<HistoricoEnsalamento>();
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarOcupantes")]
        public List<string> ListarOcupantes() {
            try {
                return historicoEnsalamentoRepository.RetornarOcupantesDistinct();
            } catch (Exception e) {
                return new List<string>();
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarUsuarios")]
        public List<string> ListarUsuarios() {
            try {
                return historicoEnsalamentoRepository.RetornarUsuariosDistinct();
            }
            catch(Exception e) {
                return new List<string>();
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarTurmas")]
        public List<string> ListarTurmas() {
            try {
                return historicoEnsalamentoRepository.RetornarTurmasDistinct();
            }
            catch(Exception e) {
                return new List<string>();
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarSalas")]
        public List<string> ListarSalas() {
            try {
                return historicoEnsalamentoRepository.RetornarSalasDistinct();
            }
            catch(Exception e) {
                return new List<string>();
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarAcoes")]
        public List<string> ListarAcoes() {
            try {
                return historicoEnsalamentoRepository.RetornarAcoesDistinct();
            }
            catch(Exception e) {
                return new List<string>();
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarHistoricoEnsalamento(HistoricoEnsalamento historicoEnsalamento) {
            try {
                historicoEnsalamentoRepository.Update(e => e.Id == historicoEnsalamento.Id, historicoEnsalamento);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Excluir")]
        public bool ExcluirHistoricoEnsalamento(HistoricoEnsalamento historicoEnsalamento) {
            try {
                historicoEnsalamentoRepository.Delete(e => e.Id == historicoEnsalamento.Id);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}