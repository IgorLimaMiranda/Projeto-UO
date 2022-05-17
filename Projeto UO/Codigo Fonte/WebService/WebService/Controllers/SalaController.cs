using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroSala")]
    public class SalaController : ApiController {
        private SalaRepository salaRepository;

        public SalaController() {
            salaRepository = new SalaRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarSala(Sala sala) {
            try {
                if(!salaRepository.Get(s => s.Numero.Equals(sala.Numero)).Any()) {
                    salaRepository.Add(sala);
                    return true;
                }

                return false;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Sala> ListarSalas() {
            try {
                return salaRepository.GetAll();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}/{numero}")]
        public Sala BuscarSala(int id, int numero = 0) {
            try {
                if (numero != 0)
                    return salaRepository.Get(s => s.Numero.Equals(numero)).First();
                return salaRepository.Get(s => s.Id.Equals(id)).First();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarSala(Sala sala) {
            try {
                salaRepository.Update(t => t.Id.Equals(sala.Id), sala);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirSala(int id) {
            try {
                salaRepository.Delete(t => t.Id.Equals(id));
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}
