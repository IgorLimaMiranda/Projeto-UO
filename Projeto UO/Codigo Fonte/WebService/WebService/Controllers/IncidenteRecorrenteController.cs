using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroIncidente")]
    public class IncidenteRecorrenteController : ApiController {
        private IncidenteRecorrenteRepository incidenteRecorrenteRepository;
        public IncidenteRecorrenteController() {
            incidenteRecorrenteRepository = new IncidenteRecorrenteRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarIncidenteRecorrente(IncidenteRecorrente incidenteRecorrente) {
            try {
                incidenteRecorrenteRepository.Add(incidenteRecorrente);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<IncidenteRecorrente> ListarIncidentesRecorrentes() {
            try {
                return incidenteRecorrenteRepository.GetAll();
            }
            catch (Exception e) {
                return null;
            }
            
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public IncidenteRecorrente BuscarIncidenteRecorrente(int id) {
            try {
                return incidenteRecorrenteRepository.Get(i => i.Id.Equals(id)).First();
            }
            catch (Exception e) {
                return null;
            }

        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarIncidenteRecorrente(IncidenteRecorrente incidenteRecorrente) {
            try {
                incidenteRecorrenteRepository.Update(e => e.Id.Equals(incidenteRecorrente.Id), incidenteRecorrente);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirIncidenteRecorrente(int id) {
            try {
                incidenteRecorrenteRepository.Delete(e => e.Id.Equals(id));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
