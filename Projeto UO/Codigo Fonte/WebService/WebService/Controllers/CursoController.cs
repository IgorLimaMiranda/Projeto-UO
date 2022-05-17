using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroCurso")]
    public class CursoController : ApiController {
        private CursoRepository cursoRepository;
        public CursoController() {
            cursoRepository = new CursoRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarEquipamento(Curso curso) {
            try {
                cursoRepository.Add(curso);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Curso> ListarEquipamentos() {
            try {
                return cursoRepository.GetAll();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("BuscarPorId/{id}")]
        public Curso BuscarCurso(int id) {
            try {
                return cursoRepository.Get(c => c.Id.Equals(id)).First();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool AlterarEquipamento(Curso curso) {
            try {
                cursoRepository.Update(e => e.Id.Equals(curso.Id), curso);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("DELETE")]
        [Route("Excluir/{id}")]
        public bool ExcluirEquipamento(int id) {
            try {
                cursoRepository.Delete(e => e.Id.Equals(id));
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
