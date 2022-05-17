using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroAtendimento")]
    public class AtendimentoController : ApiController {
        private AtendimentoRepository atendimentoRepository;
        public AtendimentoController() {
            atendimentoRepository = new AtendimentoRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarAtendimento(Atendimento atendimento) {
            try {
                atendimentoRepository.Add(atendimento);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Atendimento> ListarAtendimentos() {
            try {
                return atendimentoRepository.GetAll();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarAtendimentosDeChamado/{id}")]
        public List<Atendimento> ListarAtendimentosDeChamado(int id) {
            try {
                return atendimentoRepository.GetAllAtendimentosDeChamado(id);
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public Atendimento BuscarAtendimento(int id) {
            try {
                return atendimentoRepository.Get(a => a.Id.Equals(id)).First();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarAtendimento(Atendimento atendimento) {
            try {
                atendimentoRepository.Update(e => e.Id.Equals(atendimento.Id), atendimento);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirAtendimento(int id) {
            try {
                atendimentoRepository.Delete(e => e.Id.Equals(id));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}