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
    public class CadastroRecursoController : PermissaoController {
        private ApiEquipamento _apiEquipamento;
        private CadastroRecursoVM _cadastroRecursoVM;

        public CadastroRecursoController() {
            _apiEquipamento = new ApiEquipamento();
            _cadastroRecursoVM = new CadastroRecursoVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroRecursoVM);
            return View(_cadastroRecursoVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroRecursoVM.Equipamentos = ListarEquipamentos();
            _cadastroRecursoVM = AtribuirFiltros(_cadastroRecursoVM) as CadastroRecursoVM;
            return PartialView(_cadastroRecursoVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Equipamento")] CadastroRecursoVM recursoVM) {
            //recursoVM.Equipamento.Descricao = $"RECURSO {recursoVM.Equipamento.Descricao}";
            bool response = await _apiEquipamento.CadastrarEquipamento(recursoVM.Equipamento);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(recursoVM);
                return View("Index", _cadastroRecursoVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Editar
        public ActionResult EditarRecurso(Equipamento recurso) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroRecursoVM.Equipamento = recurso;
            _cadastroRecursoVM = AtribuirFiltros(_cadastroRecursoVM) as CadastroRecursoVM;
            return PartialView(_cadastroRecursoVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar([Bind(Include = "Equipamento")]CadastroRecursoVM recursoVM) {
            bool response = await _apiEquipamento.AlterarEquipamento(recursoVM.Equipamento);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(recursoVM);
                return View("Index", _cadastroRecursoVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirRecurso(Equipamento recurso) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroRecursoVM.Equipamento = recurso;
            _cadastroRecursoVM = AtribuirFiltros(_cadastroRecursoVM) as CadastroRecursoVM;
            return PartialView(_cadastroRecursoVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir(CadastroRecursoVM recursoVM) {
            bool response = await _apiEquipamento.ExcluirEquipamento(recursoVM.Equipamento.Id);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(recursoVM);
                return View("Index", _cadastroRecursoVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        public List<Equipamento> ListarEquipamentos() {
            return _apiEquipamento.ListarEquipamentos().Result.ToList();
        }

        public void PopularListas(CadastroRecursoVM cadastroRecursoVM) {
            _cadastroRecursoVM = cadastroRecursoVM;
            _cadastroRecursoVM = AtribuirFiltros(_cadastroRecursoVM) as CadastroRecursoVM;
            _cadastroRecursoVM.Equipamentos = ListarEquipamentos();
        }
    }
}