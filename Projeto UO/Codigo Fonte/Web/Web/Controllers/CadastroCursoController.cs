using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class CadastroCursoController : PermissaoController {
        private ApiCurso _apiCurso;
        private CadastroCursoVM _cadastroCursoVM;

        public CadastroCursoController() {
            _apiCurso = new ApiCurso();
            _cadastroCursoVM = new CadastroCursoVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroCursoVM);
            return View(_cadastroCursoVM);
        }

        [HttpPost]
        public JsonResult Index(string Prefix) {
            var CursoList = (from C in _cadastroCursoVM.Cursos
                             where C.Descricao.StartsWith(Prefix)
                             select new { C.Descricao });
            return Json(CursoList, JsonRequestBehavior.AllowGet);
        }

        // GET: Index
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroCursoVM.Cursos = ListarCursos();
            _cadastroCursoVM = AtribuirFiltros(_cadastroCursoVM) as CadastroCursoVM;
            return PartialView(_cadastroCursoVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Curso")] CadastroCursoVM cursoVM) {
            bool response = await _apiCurso.CadastrarCurso(cursoVM.Curso);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cursoVM);
                return View("Index", _cadastroCursoVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Editar
        public ActionResult EditarCurso(Curso curso) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroCursoVM.Curso = curso;
            _cadastroCursoVM = AtribuirFiltros(_cadastroCursoVM) as CadastroCursoVM;
            return PartialView(_cadastroCursoVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar([Bind(Include = "Curso")] CadastroCursoVM cursoVM) {
            bool response = await _apiCurso.AlterarCurso(cursoVM.Curso);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                return View("Index", _cadastroCursoVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirCurso(Curso curso) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroCursoVM.Curso = curso;
            _cadastroCursoVM = AtribuirFiltros(_cadastroCursoVM) as CadastroCursoVM;
            return PartialView(_cadastroCursoVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir([Bind(Include = "Curso")] CadastroCursoVM cursoVM) {
            bool response = await _apiCurso.ExcluirCurso(cursoVM.Curso.Id);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cursoVM);
                return View("Index", _cadastroCursoVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        public List<Curso> ListarCursos() {
            return _apiCurso.ListarCursos().Result.ToList();
        }

        public void PopularListas(CadastroCursoVM cadastroCursoVM) {
            _cadastroCursoVM = cadastroCursoVM;
            _cadastroCursoVM = AtribuirFiltros(_cadastroCursoVM) as CadastroCursoVM;
            _cadastroCursoVM.Cursos = ListarCursos();
        }
    }
}