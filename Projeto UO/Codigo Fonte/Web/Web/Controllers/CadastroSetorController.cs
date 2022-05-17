using Backend.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Backend.Enum;
using Web.Api;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class CadastroSetorController : PermissaoController {
        private ApiSetor _apiSetor;
        private CadastroSetorVM _cadastroSetorVM;

        public CadastroSetorController() {
            _apiSetor = new ApiSetor();
            _cadastroSetorVM = new CadastroSetorVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroSetorVM);
            return View(_cadastroSetorVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSetorVM.Setores = ListarSetores();
            _cadastroSetorVM = AtribuirFiltros(_cadastroSetorVM) as CadastroSetorVM;
            return PartialView(_cadastroSetorVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Setor")] CadastroSetorVM cadastroSetorVM) {
            bool response = await _apiSetor.CadastrarSetor(cadastroSetorVM.Setor);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSetorVM);
                return View("Index", _cadastroSetorVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Editar
        public ActionResult EditarSetor(Setor setor) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSetorVM.Setor = setor;
            _cadastroSetorVM = AtribuirFiltros(_cadastroSetorVM) as CadastroSetorVM;
            return PartialView(_cadastroSetorVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar([Bind(Include = "Setor")]CadastroSetorVM cadastroSetorVM) {
            bool response = await _apiSetor.AlterarSetor(cadastroSetorVM.Setor);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSetorVM);
                return View("Index", _cadastroSetorVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirSetor(Setor setor) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSetorVM.Setor = setor;
            _cadastroSetorVM = AtribuirFiltros(_cadastroSetorVM) as CadastroSetorVM;
            return PartialView(_cadastroSetorVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir(CadastroSetorVM cadastroSetorVM) {
            bool response = await _apiSetor.ExcluirSetor(cadastroSetorVM.Setor.Id);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSetorVM);
                return View("Index", _cadastroSetorVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        public List<Setor> ListarSetores() {
            return _apiSetor.ListarSetores().Result.ToList();
        }

        public void PopularListas(CadastroSetorVM cadastroSetorVM) {
            _cadastroSetorVM = cadastroSetorVM;
            _cadastroSetorVM = AtribuirFiltros(_cadastroSetorVM) as CadastroSetorVM;
            _cadastroSetorVM.Setores = ListarSetores();
        }
    }
}