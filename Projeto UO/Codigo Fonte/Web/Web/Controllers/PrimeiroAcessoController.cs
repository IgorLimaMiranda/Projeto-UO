using Backend.Model;
using System.Threading.Tasks;
using System.Web.Mvc;
using Backend.Enum;
using Web.Api;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class PrimeiroAcessoController : PermissaoController {
        private ApiUsuario _apiUsuario;
        private PrimeiroAcessoVM _primeiroAcessoVM;

        public PrimeiroAcessoController() {
            _apiUsuario = new ApiUsuario();
            _primeiroAcessoVM = new PrimeiroAcessoVM();
        }

        // GET: Index
        [ActionFilter]
        public ActionResult Index() {
            _primeiroAcessoVM.Usuario = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            return View(_primeiroAcessoVM);
        }

        // POST: Index
        [HttpPost, ActionName("Index")]
        [ActionFilter]
        public async Task<ActionResult> PrimeiroAcesso(PrimeiroAcessoVM primeiroAcessoVM) {
            primeiroAcessoVM.Usuario.PrimeiroAcesso = false;
            primeiroAcessoVM.Usuario.Senha = primeiroAcessoVM.Senha;
            bool response = await _apiUsuario.AlterarUsuario(primeiroAcessoVM.Usuario);
            if (response) {
                AtribuirPermissoes(primeiroAcessoVM.Usuario);
                return RedirectToAction("Index", "Dashboard");
            }
            
            return View("Index", primeiroAcessoVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
        }
    }
}