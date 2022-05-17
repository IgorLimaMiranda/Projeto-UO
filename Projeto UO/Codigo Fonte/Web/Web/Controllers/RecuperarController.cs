using Backend.Enum;
using Backend.Model;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper;
using Web.Helper.EnumMensagens;

namespace Web.Controllers {
    public class RecuperarController : Controller {
        private ApiUsuario _apiUsuario;
        private Usuario _usuario;

        public RecuperarController() {
            _apiUsuario = new ApiUsuario();
            _usuario = new Usuario();
        }

        // GET: RecuperarCpf
        public ActionResult RecuperarCpf() {
            return View(_usuario);
        }

        // POST: RecuperarPergunta
        [HttpPost]
        public async Task<ActionResult> RecuperarCpf(Usuario usuario) {
            if (UsuarioHelper.ValidarCpf(usuario.Cpf)) {
                Usuario response = await _apiUsuario.BuscarUsuario(usuario.Cpf);
                if (response != null) {
                    if (response.PrimeiroAcesso)
                        return View("RecuperarCpf", usuario).Mensagem(EnumMensagensRecuperarSenha.PRIMEIRO_ACESSO.GetDescription(), EnumTipoToastr.INFO);

                    response.Resposta = string.Empty;
                    TempData["usuarioRecuperarSenha"] = response;
                    return RedirectToAction("ExibirSenha");
                }
                
                return View("RecuperarCpf", usuario).Mensagem(EnumMensagensLogin.USUARIO_NAO_EXISTE.GetDescription(), EnumTipoToastr.ERROR);
            }
            
            return View("RecuperarCpf", usuario).Mensagem(EnumMensagensRecuperarSenha.CPF_INVALIDO.GetDescription(), EnumTipoToastr.ERROR);
        }

        // GET: ExibirSenha
        public ActionResult ExibirSenha() {
            _usuario = TempData["usuarioRecuperarSenha"] as Usuario;
            TempData.Keep("usuarioRecuperarSenha");
            return View(_usuario);
        }

        // POST: ExibirSenha
        [HttpPost]
        public async Task<ActionResult> ExibirSenha(Usuario usuario) {
            _usuario = await _apiUsuario.RecuperarSenhaUsuario(usuario);
            if (_usuario != null)
                return View("ExibirSenha", _usuario).Mensagem($"SENHA: {_usuario.Senha}", EnumTipoToastr.INFO, $"Atenção {_usuario.Nome}");

            return View("ExibirSenha", usuario).Mensagem(EnumMensagensRecuperarSenha.DADOS_INCORRETOS.GetDescription(), EnumTipoToastr.ERROR);
        }
    }
}