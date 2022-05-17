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
    public class CadastroTurmaController : PermissaoController {
        private ApiTurma _apiTurma;
        private ApiCurso _apiCurso;
        private CadastroTurmaVM _cadastroTurmaVM;

        public CadastroTurmaController() {
            _apiTurma = new ApiTurma();
            _apiCurso = new ApiCurso();
            _cadastroTurmaVM = new CadastroTurmaVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroTurmaVM);
            return View(_cadastroTurmaVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_cadastroTurmaVM);
            return PartialView(_cadastroTurmaVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Turma")] CadastroTurmaVM cadastroTurmaVM) {
            bool response = await _apiTurma.CadastrarTurma(cadastroTurmaVM.Turma);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroTurmaVM);
                return View("Index", _cadastroTurmaVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Alterar
        public ActionResult EditarTurma(Turma turma) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroTurmaVM.Cursos = ListarCursos();
            _cadastroTurmaVM.Turma = _apiTurma.BuscarTurma(turma).Result;
            _cadastroTurmaVM = AtribuirFiltros(_cadastroTurmaVM) as CadastroTurmaVM;
            return PartialView(_cadastroTurmaVM);
        }

        // POST: Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Alterar([Bind(Include = "Turma")] CadastroTurmaVM cadastroTurmaVM) {
            bool response = await _apiTurma.AlterarTurma(cadastroTurmaVM.Turma);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroTurmaVM);
                return View("Index", _cadastroTurmaVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirTurma(Turma turma) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroTurmaVM.Turma = _apiTurma.BuscarTurma(turma).Result;
            _cadastroTurmaVM = AtribuirFiltros(_cadastroTurmaVM) as CadastroTurmaVM;
            return PartialView(_cadastroTurmaVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir(CadastroTurmaVM cadastroTurmaVM) {
            bool response = await _apiTurma.ExcluirTurma(cadastroTurmaVM.Turma);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroTurmaVM);
                return View("Index", _cadastroTurmaVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        public List<Turma> ListarTurmas() {
            return _apiTurma.ListarTurmas().Result.ToList();
        }

        public List<Curso> ListarCursos() {
            return _apiCurso.ListarCursos().Result.ToList();
        }

        public void PopularListas(CadastroTurmaVM cadastroTurmaVM) {
            _cadastroTurmaVM = cadastroTurmaVM;
            _cadastroTurmaVM = AtribuirFiltros(cadastroTurmaVM) as CadastroTurmaVM;
            _cadastroTurmaVM.Turmas = ListarTurmas();
            _cadastroTurmaVM.Cursos = ListarCursos();
        }
    }
}