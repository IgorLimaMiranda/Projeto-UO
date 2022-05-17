using Backend.Model;
using System;
using System.Web.Mvc;
using Web.Api;
using Web.ViewModel;

namespace Web.Controllers {
    public class DashboardController : PermissaoController
    {
        private DashboardVM _dashboardVM;

        private ApiChamado _apiChamado;
        private ApiEnsalamento _apiEnsalamento;

        // GET: Dashboard
        //[ActionFilter]
        public ActionResult Index() {
            _dashboardVM = new DashboardVM();
            //_dashboardVM = AtribuirFiltros(_dashboardVM) as DashboardVM;
            _apiChamado = new ApiChamado();
            _apiEnsalamento = new ApiEnsalamento();
            //Usuario usuario = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            //_dashboardVM.UltimosChamados = _apiChamado.ListarChamadosDashboard(usuario).Result;
            _dashboardVM.EnsalamentosDia = _apiEnsalamento.GetAllEnsalementoDia(DateTime.MinValue).Result;
            return View(_dashboardVM);
        }

        [HttpPost]
        public ActionResult MudarMapa(DateTime data)
        {
            _apiEnsalamento = new ApiEnsalamento();
            _dashboardVM = new DashboardVM();
            _dashboardVM.EnsalamentosDia = _apiEnsalamento.GetAllEnsalementoDia(data).Result;
            return Json(_dashboardVM.EnsalamentosDia, JsonRequestBehavior.AllowGet);
        }

    }

}