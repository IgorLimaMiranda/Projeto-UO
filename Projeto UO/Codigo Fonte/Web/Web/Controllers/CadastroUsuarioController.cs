using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class CadastroUsuarioController : PermissaoController {
        private ApiUsuario _apiUsuario;
        private CadastroUsuarioVM _cadastroUsuarioVM;

        public CadastroUsuarioController() {
            _apiUsuario = new ApiUsuario();
            _cadastroUsuarioVM = new CadastroUsuarioVM();
        }

        //GET: Index
        [ActionFilter]
        public ActionResult Index() {
            PopularListas(_cadastroUsuarioVM);
            return View(_cadastroUsuarioVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroUsuarioVM.Usuarios = ListarUsuarios();
            _cadastroUsuarioVM = AtribuirFiltros(_cadastroUsuarioVM) as CadastroUsuarioVM;
            return PartialView(_cadastroUsuarioVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Usuario")] CadastroUsuarioVM cadastroUsuarioVM) {
            if (UsuarioHelper.ValidarCpf(cadastroUsuarioVM.Usuario.Cpf)) {
                cadastroUsuarioVM.Usuario.PrimeiroAcesso = true;
                bool response = await _apiUsuario.CadastrarUsuario(cadastroUsuarioVM.Usuario);

                //TODO Alterar nas views onde hoje está "Model.Professor.EixoEducacional"
                //TODO                             para "Model.Usuario.EixoEducacional"

                if (response) {
                    return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
                }

                PopularListas(cadastroUsuarioVM);
                return View("Index", _cadastroUsuarioVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }

            PopularListas(cadastroUsuarioVM);
            return View("Index", _cadastroUsuarioVM).Mensagem(EnumMensagensLogin.CPF_INVALIDO.GetDescription(), EnumTipoToastr.ERROR);
        }

        // GET: Alterar
        public ActionResult AlterarUsuario(Usuario usuario) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroUsuarioVM.Usuario = usuario;
            _cadastroUsuarioVM = AtribuirFiltros(_cadastroUsuarioVM) as CadastroUsuarioVM;
            return PartialView(_cadastroUsuarioVM);
        }

        // POST: Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Alterar([Bind(Include = "Usuario")] CadastroUsuarioVM cadastroUsuarioVM) {
            Usuario novoUsuario = new Usuario();
            novoUsuario = cadastroUsuarioVM.Usuario;
            cadastroUsuarioVM.Usuario = _apiUsuario.BuscarUsuario(cadastroUsuarioVM.Usuario.Cpf).Result;
            
            if (((!(novoUsuario.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription())) && (cadastroUsuarioVM.Usuario.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription())))
                && (_apiUsuario.ListarUsuariosAdministrativos().Result.Count <= 2))) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.ERRO_ALTERAR_MINIMO_ADMINISTRADORES.GetDescription(), EnumTipoToastr.ERROR);
            }
            if (((!(novoUsuario.TipoUsuario.Equals(EnumTipoUsuario.PROFESSOR.GetDescription())) && (cadastroUsuarioVM.Usuario.TipoUsuario.Equals(EnumTipoUsuario.PROFESSOR.GetDescription()))))) {
                novoUsuario.EixoEducacional = null;
            }

            bool resultado = await _apiUsuario.AlterarUsuario(novoUsuario);

            if ((novoUsuario.Cpf.Equals((Session["permissao"] as PermissaoVM).UsuarioLogado.Cpf)) && resultado) {
                if(!(novoUsuario.TipoUsuario.Equals((Session["permissao"] as PermissaoVM).UsuarioLogado.TipoUsuario)))
                    return RedirectToAction("Index", "Login").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
                PermissaoVM p = Session["permissao"] as PermissaoVM;
                p.UsuarioLogado = novoUsuario;
                Session["permissao"] = p;
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            if (resultado) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(cadastroUsuarioVM);
                return View("Index", _cadastroUsuarioVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Excluir
        public ActionResult ExcluirUsuario(Usuario usuario) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroUsuarioVM.Usuario = usuario;
            _cadastroUsuarioVM = AtribuirFiltros(_cadastroUsuarioVM) as CadastroUsuarioVM;
            return PartialView(_cadastroUsuarioVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir(CadastroUsuarioVM cadastroUsuarioVM) {
            Usuario usuarioLogado = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            bool response;
            cadastroUsuarioVM.Usuario = _apiUsuario.BuscarUsuario(cadastroUsuarioVM.Usuario.Cpf).Result;


            if ((cadastroUsuarioVM.Usuario.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription()) && (_apiUsuario.ListarUsuariosAdministrativos().Result.Count <= 2)))
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.ERRO_DELETAR_MINIMO_ADMINISTRADORES.GetDescription(), EnumTipoToastr.ERROR);

            if (!(usuarioLogado.Cpf.Equals(cadastroUsuarioVM.Usuario.Cpf))) {
                response = await _apiUsuario.ExcluirUsuario(cadastroUsuarioVM.Usuario.Cpf);
                if (response) {
                    return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
                }
                else {
                    PopularListas(cadastroUsuarioVM);
                    return View("Index", _cadastroUsuarioVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
                }
            }
            else
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.ERRO_AUTO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
        }

        // GET: Visualizar
        public ActionResult VisualizarUsuario(Usuario usuario) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _cadastroUsuarioVM.Usuario = usuario;
            _cadastroUsuarioVM = AtribuirFiltros(_cadastroUsuarioVM) as CadastroUsuarioVM;
            return PartialView(_cadastroUsuarioVM);
        }

        public List<Usuario> ListarUsuarios() {
            return _apiUsuario.ListarUsuarios().Result.ToList();
        }

        public void PopularListas(CadastroUsuarioVM cadastroUsuarioVM) {
            _cadastroUsuarioVM = cadastroUsuarioVM;
            _cadastroUsuarioVM = AtribuirFiltros(_cadastroUsuarioVM) as CadastroUsuarioVM;
            _cadastroUsuarioVM.Usuarios = ListarUsuarios();
        }
    }
}