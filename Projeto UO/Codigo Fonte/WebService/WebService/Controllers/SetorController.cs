using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroSetor")]
    public class SetorController : ApiController {
        private SetorRepository setorRepository;

        public SetorController() {
            setorRepository = new SetorRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarSetor(Setor setor) {
            try {
                setorRepository.Add(setor);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Setor> ListarSetores() {
            try {
                return setorRepository.ListarSetores();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarSetorSala")]
        public IEnumerable<Setor> ListarSetoresSalas() {
            try {
                return setorRepository.GetAll();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public Setor BuscarSetor(int id) {
            try {
                return setorRepository.Get(s => s.Id.Equals(id)).First();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarSetor(Setor setor) {
            try {
                setorRepository.Update(t => t.Id.Equals(setor.Id), setor);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirSetor(int id) {
            try {
                setorRepository.Delete(t => t.Id.Equals(id));
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}
