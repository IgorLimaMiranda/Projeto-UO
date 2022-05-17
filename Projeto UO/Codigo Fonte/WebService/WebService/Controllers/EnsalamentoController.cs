using Backend.Componente;
using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Backend.Enum;

namespace WebService.Controllers {
    [RoutePrefix("Ensalamento")]
    public class EnsalamentoController : ApiController {
        private Ensalamento _ensalamento;
        private EnsalamentoRepository _ensalamentoRepository;
        private List<bool> _periodicidade;
        public EnsalamentoController() {
            _ensalamento = new Ensalamento();
            _ensalamentoRepository = new EnsalamentoRepository();
            _periodicidade = new List<bool>();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarEnsalamento(List<Ensalamento> ensalamentos) {
            List<Ensalamento> adicionados = new List<Ensalamento>();
            try {
                int id = _ensalamentoRepository.GetLastId() + 1;
                foreach (var ensalamento in ensalamentos) {
                    ensalamento.Id = id;
                    _ensalamentoRepository.Add(ensalamento);
                    adicionados.Add(ensalamento);
                }
                ensalamentos = null;
                return true;
            }
            catch (Exception e) {
                if (adicionados.Any())
                    _ensalamentoRepository.Delete(ensal => ensal.Id == adicionados.First().Id);
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Verificar")]
        public List<Ensalamento> VerificarEnsalamento(List<Ensalamento> ensalamentos) {
            try {
                List<Ensalamento> retorno = _ensalamentoRepository.VerificarConflito(ensalamentos);
                return retorno;
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar/{periodicidade}")]
        public object ListarEnsalamentos(bool periodicidade) {
            try {
                var resultado = _ensalamentoRepository.ListarEnsalamentoPeriodicidade(periodicidade); ;
                return resultado;
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Route("BuscarEnsalamentoDesmembrado")]
        public List<Ensalamento> BuscarEnsalamentoDesmembrado(Ensalamento ensalamento) {
            try {
                return _ensalamentoRepository.GetEnsalamentoDesmemebrado(ensalamento);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Route("AtribuirIncludes")]
        public List<Ensalamento> AtribuirIncludes(List<Ensalamento> ensalamentos) {
            try {
                return _ensalamentoRepository.AtribuirIncludes(ensalamentos);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirEnsalamento(int id) {
            try {
                _ensalamentoRepository.Delete(e => e.Id.Equals(id));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("GetAllEnsalamentoDia")]
        public List<EnsalamentoPeriodo> GetAllEnsalamentoDia(Ensalamento ensalamento) {
            try {
                return _ensalamentoRepository.GetAllEnsalamentoDia(ensalamento.Data);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Route("GetEnsalamentosDiario")]
        public List<Ensalamento> GetEnsalamentosDiario(Ensalamento ensalamento) {
            try {
                return _ensalamentoRepository.GetEnsalamentosDiario(ensalamento.Data, ensalamento.Periodo);
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}