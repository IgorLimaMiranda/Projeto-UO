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
    public class CadastroSalaController : PermissaoController {
        private ApiSala _apiSala;
        private ApiEquipamentoSetor _apiEquipamentoSetor;
        private ApiEquipamento _apiEquipamento;
        private CadastroSalaVM _cadastroSalaVM;

        public CadastroSalaController() {
            _apiSala = new ApiSala();
            _apiEquipamentoSetor = new ApiEquipamentoSetor();
            _cadastroSalaVM = new CadastroSalaVM();
            _apiEquipamento = new ApiEquipamento();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroSalaVM);
            return View(_cadastroSalaVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSalaVM.Salas = ListarSalas();
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            return PartialView(_cadastroSalaVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Sala")] CadastroSalaVM cadastroSalaVM) {
            cadastroSalaVM.Sala.Nome = $"SALA {cadastroSalaVM.Sala.Numero}";
            bool response = await _apiSala.CadastrarSala(cadastroSalaVM.Sala);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSalaVM);
                return View("Index", _cadastroSalaVM).Mensagem(EnumMensagensSala.SALA_EXISTENTE.GetDescription(), EnumTipoToastr.ERROR);
            }
        }
        
        // GET: Editar
        public ActionResult EditarSala(Sala sala) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSalaVM.Sala = sala;
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            return PartialView(_cadastroSalaVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar(CadastroSalaVM cadastroSalaVM) {
            cadastroSalaVM.Sala.Nome = $"SALA {cadastroSalaVM.Sala.Numero}";
            bool response = await _apiSala.AlterarSala(cadastroSalaVM.Sala);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSalaVM);
                return View("Index", cadastroSalaVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        //GET: Excluir
        public ActionResult ExcluirSala(Sala sala) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSalaVM.Sala = sala;
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            return PartialView(_cadastroSalaVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir(CadastroSalaVM cadastroSalaVM) {
            bool response = await _apiSala.ExcluirSala(cadastroSalaVM.Sala.Id);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroSalaVM);
                return View("Index", _cadastroSalaVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        //GET: Recurso
        public ActionResult RecursoSala(Sala sala) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSalaVM.EquipamentosSetor = _apiEquipamentoSetor.ListarEquipamentosSala(sala).Result.ToList();
            _cadastroSalaVM.Equipamentos = _apiEquipamento.ListarEquipamentos().Result.ToList();
            _cadastroSalaVM.Sala = sala;
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            return PartialView(_cadastroSalaVM);
        }

        [HttpPost]
        [ActionFilter]
        public async Task<JsonResult> CadastrarEquipamentosSetorAsync(List<EquipamentoSetor> equipamentosSetor) {

            await _apiEquipamentoSetor.ExcluirEquipamentosSetor(equipamentosSetor[0].IdSetor);
            var response = await _apiEquipamentoSetor.CadastrarEquipamentosSetor(equipamentosSetor);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionFilter]
        public async Task<JsonResult> ExcluirEquipamentosSetor(int idSetor) {
            var response = await _apiEquipamentoSetor.ExcluirEquipamentosSetor(idSetor);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //GET: VisualizarSala
        public ActionResult VisualizarSala(Sala sala) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroSalaVM.EquipamentosSetor = _apiEquipamentoSetor.ListarEquipamentosSala(sala).Result.ToList();
            _cadastroSalaVM.Equipamentos = _apiEquipamento.ListarEquipamentos().Result.ToList();
            _cadastroSalaVM.Sala = sala;
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            return PartialView(_cadastroSalaVM);
        }

        public List<Sala> ListarSalas() {
            return _apiSala.ListarSalas().Result.ToList();
        }

        public void PopularListas(CadastroSalaVM cadastroSalaVM) {
            _cadastroSalaVM = cadastroSalaVM;
            _cadastroSalaVM = AtribuirFiltros(_cadastroSalaVM) as CadastroSalaVM;
            _cadastroSalaVM.Salas = ListarSalas();
        }
    }
}