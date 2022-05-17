using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Backend.Enum;
using Web.Helper.EnumWeb;

namespace WebService.Controllers {
    [RoutePrefix("CadastroChamado")]
    public class ChamadoController : ApiController {
        private ChamadoRepository chamadoRepository;
        private AtendimentoRepository atendimentoRepository;

        public ChamadoController() {
            chamadoRepository = new ChamadoRepository();
            atendimentoRepository = new AtendimentoRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarChamado(Chamado chamado) {
            try {
                chamadoRepository.Add(chamado);
                atendimentoRepository.Add(new Atendimento {
                    Detalhes = EnumDetalhesAtendimentoOcorrencia.ABERTURA.GetDescription(),
                    DataAtendimento = chamado.HoraData,
                    Status = EnumStatusChamado.ABERTA.GetDescription(),
                    CpfTecnico = chamado.CpfUsuario,
                    IdChamado = chamado.IdChamado
                });
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Listar/{apenasMeusChamados}")]
        public IEnumerable<Chamado> ListarChamados(Usuario usuario, bool apenasMeusChamados) {
            try {
                List<Chamado> chamados = chamadoRepository.GetAll(usuario, apenasMeusChamados);
                return chamados;
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public Chamado BuscarChamado(int id) {
            try {
                return chamadoRepository.Get(id);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarChamado(Chamado chamado) {
            try {
                chamadoRepository.Update(c => c.IdChamado == chamado.IdChamado, chamado);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("ListarChamadosDashboard")]
        public IEnumerable<Chamado> ListarChamadosDashboard(Usuario usuario) {
            try {
                return chamadoRepository.GetAllPorUsuarioAbertos(usuario);
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}