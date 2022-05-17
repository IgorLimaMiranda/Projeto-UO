using Backend.Enum;
using Backend.Model;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class LoginController : PermissaoController {
        private ApiUsuario _apiUsuario {
            get; set;
        }
        private ApiPermissao _apiPermissao {
            get; set;
        }
        private Usuario _usuario {
            get; set;
        }
        private PermissaoVM _permissaoVM {
            get; set;
        }

        public LoginController() {
            _apiUsuario = new ApiUsuario();
            _apiPermissao = new ApiPermissao();
            _usuario = new Usuario();
            _permissaoVM = new PermissaoVM();
        }

        // GET: Login
        public ActionResult Index(Usuario usuario) {
            if (usuario != null)
                _usuario = usuario;

            if ((Session["permissao"] != null) && (Session["permissao"] as PermissaoVM).PerdeuSession == true) {
                return View(_usuario).Mensagem("Sua sessão expirou! Para continuar, faça login novamente.", EnumTipoToastr.INFO);
            }


            return View(_usuario);
        }

        [HttpPost, ActionName("Index")]
        public async Task<ActionResult> RealizarLogin([Bind(Include = "Cpf, Senha")] Usuario usuario) {
            if (UsuarioHelper.ValidarCpf(usuario.Cpf)) {
                Usuario response = await _apiUsuario.Login(usuario);
                if (response != null) {
                    if (response.Senha.Equals(usuario.Senha)) {

                        AtribuirPermissoes(response);

                        if (response.PrimeiroAcesso) {
                            return RedirectToAction("Index", "PrimeiroAcesso");
                        }
                        return RedirectToAction("Index", "Dashboard");
                    }

                    response.Senha = string.Empty;
                    return View("Index", response).Mensagem(EnumMensagensLogin.SENHA_ERRADA.GetDescription(), EnumTipoToastr.ERROR);
                }

                usuario.Senha = string.Empty;
                return View("Index", usuario).Mensagem(EnumMensagensLogin.USUARIO_NAO_EXISTE.GetDescription(), EnumTipoToastr.ERROR);
            }

            usuario.Senha = string.Empty;
            return View("Index", usuario).Mensagem(EnumMensagensLogin.CPF_INVALIDO.GetDescription(), EnumTipoToastr.ERROR);
        }

        public ActionResult Sair() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SairSistema() {
            //Caso deslogue ou tenha perdido a Sessão
            if (Session["permissao"] != null) {
                _permissaoVM = new PermissaoVM();
                _permissaoVM.Controller = (Session["permissao"] as PermissaoVM).Controller;
                Session["permissao"] = _permissaoVM;
            }
            return RedirectToAction("Index", "Login");
        }
    }
}