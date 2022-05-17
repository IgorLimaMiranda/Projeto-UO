using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroTurma")]
    public class TurmaController : ApiController {
        private TurmaRepository turmaRepository;

        public TurmaController() {
            turmaRepository = new TurmaRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarTurma(Turma turma) {
            try {
                turmaRepository.Add(turma);
                return true;
            }
            catch(Exception e) {
                return false;
            }
            
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Turma> ListarTurmas() {
            try {
                return turmaRepository.GetTurmasLinq();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}/{ano}")]
        public Turma BuscarTurma(int id, int ano) {
            try {
                return turmaRepository.GetTurmaComCursoLinq(id, ano);
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarTurma(Turma turma) {
            try {
                turmaRepository.Update(t => t.Id.Equals(turma.Id) && t.Ano.Equals(turma.Ano), turma);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Excluir")]
        public bool ExcluirTurma(Turma turma) {
            try {
                turmaRepository.Delete(t => t.Id.Equals(turma.Id) && t.Ano.Equals(turma.Ano));
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}
