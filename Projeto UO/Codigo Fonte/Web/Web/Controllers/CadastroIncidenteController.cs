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
    public class CadastroIncidenteController : PermissaoController {
        private ApiIncidenteRecorrente _apiIncidenteRecorrente;
        private CadastroIncidenteVM _cadastroIncidenteVM;

        public CadastroIncidenteController() {
            _apiIncidenteRecorrente = new ApiIncidenteRecorrente();
            _cadastroIncidenteVM = new CadastroIncidenteVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroIncidenteVM);
            return View(_cadastroIncidenteVM);
        }

        // GET: Cadastrar - Lucas Bolívia
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroIncidenteVM.IncidenteRecorrentes = ListarIncidentesRecorrentes();
            _cadastroIncidenteVM = AtribuirFiltros(_cadastroIncidenteVM) as CadastroIncidenteVM;
            return PartialView(_cadastroIncidenteVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "IncidenteRecorrente")] CadastroIncidenteVM incidenteRecorrenteVM) {
            bool response = await _apiIncidenteRecorrente.CadastrarIncidenteRecorrente(incidenteRecorrenteVM.IncidenteRecorrente);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(incidenteRecorrenteVM);
                return View("Index", _cadastroIncidenteVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Editar
        public ActionResult EditarIncidente(IncidenteRecorrente incidenteRecorrente) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroIncidenteVM.IncidenteRecorrente = incidenteRecorrente;
            _cadastroIncidenteVM = AtribuirFiltros(_cadastroIncidenteVM) as CadastroIncidenteVM;
            return PartialView(_cadastroIncidenteVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar([Bind(Include = "IncidenteRecorrente")] CadastroIncidenteVM incidenteRecorrenteVM) {
            bool response = await _apiIncidenteRecorrente.AlterarIncidenteRecorrente(incidenteRecorrenteVM.IncidenteRecorrente);
            if(response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(incidenteRecorrenteVM);
                return View("Index", _cadastroIncidenteVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirIncidente(IncidenteRecorrente excluirincidenteRecorrente) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroIncidenteVM.IncidenteRecorrente = excluirincidenteRecorrente;
            _cadastroIncidenteVM = AtribuirFiltros(_cadastroIncidenteVM) as CadastroIncidenteVM;
            return PartialView(_cadastroIncidenteVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir([Bind(Include = "IncidenteRecorrente")] CadastroIncidenteVM incidenteRecorrenteVM) {
            bool response = await _apiIncidenteRecorrente.ExcluirIncidenteRecorrente(incidenteRecorrenteVM.IncidenteRecorrente.Id);
            if(response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(incidenteRecorrenteVM);
                return View("Index", _cadastroIncidenteVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        private List<IncidenteRecorrente> ListarIncidentesRecorrentes() {
            return _apiIncidenteRecorrente.ListarIncidenteRecorrente().Result.ToList();
        }

        public void PopularListas(CadastroIncidenteVM cadastroIncidenteVM) {
            _cadastroIncidenteVM = cadastroIncidenteVM;
            _cadastroIncidenteVM = AtribuirFiltros(_cadastroIncidenteVM) as CadastroIncidenteVM;
            _cadastroIncidenteVM.IncidenteRecorrentes = ListarIncidentesRecorrentes();
        }
    }
}
