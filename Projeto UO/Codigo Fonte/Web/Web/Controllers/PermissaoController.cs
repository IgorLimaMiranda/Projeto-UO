using Backend.Enum;
using Backend.Model;
using System.Linq;
using System.Web.Mvc;
using Web.Api;
using Web.Helper;
using Web.ViewModel;

namespace Web.Controllers {
    public class PermissaoController : Controller {

        private ApiPermissao _apiPermissao { get; set; }
        private PermissaoVM _permissaoVM { get; set; }

        public PermissaoController() {
            _apiPermissao = new ApiPermissao();
            _permissaoVM = new PermissaoVM();
        }

        public object AtribuirFiltros(PermissaoVM permissaoVM) {
            PermissaoVM session = new PermissaoVM();
            session = Session["permissao"] as PermissaoVM;
            permissaoVM.Acao = session.Acao;
            permissaoVM.MenuAcao = session.MenuAcao;
            permissaoVM.MenuCadastro = session.MenuCadastro;
            permissaoVM.MenuEnsalamento = session.MenuEnsalamento;
            permissaoVM.MenuHome = session.MenuHome;
            permissaoVM.MenuOcorrencia = session.MenuOcorrencia;
            permissaoVM.Permissoes = session.Permissoes;
            permissaoVM.UsuarioLogado = session.UsuarioLogado;

            if (!session.UsuarioLogado.ImagemUsuario.Contains("http")) {
                permissaoVM.UsuarioLogado.ImagemUsuario = $"http://{Configuration.EnderecoWebServer}:{Configuration.PortaWebServer}/{Configuration.CaminhoImagem}/{session.UsuarioLogado.ImagemUsuario}";
            }

            return permissaoVM;
        }

		/// <summary>
		/// Verifica se o usuário continua logado.
		/// <para>Recomendável utilizar no início de cada método com retorno de PartialView</para>
		/// </summary>
		/// <returns></returns>
        protected bool VerificarSession() {
            if ((Session["permissao"] == null) || (Session["permissao"] != null) && !((Session["permissao"] as PermissaoVM).Permissoes.Any()))
                return true;
            else
                return false;
        }

        protected void AtribuirPermissoes(Usuario usuario) {
            if (Session["permissao"] != null)
                _permissaoVM = Session["permissao"] as PermissaoVM;

            _permissaoVM.UsuarioLogado = usuario;
            _permissaoVM.Permissoes = _apiPermissao.ListarPermissao(usuario.TipoUsuario).Result;
            //Se o usuário não for aluno ou professor, a Home dele será Dashboard.
            //Caso contrário, como executado no construtor de PermissaoVM, a home dele será Ocorrencia
            if (!_permissaoVM.UsuarioLogado.TipoUsuario.Equals(EnumTipoUsuario.ALUNO.GetDescription()) &&
				!_permissaoVM.UsuarioLogado.TipoUsuario.Equals(EnumTipoUsuario.PROFESSOR.GetDescription()))
                _permissaoVM.MenuHome = Configuration.Dashboard;
            foreach (var permissao in _permissaoVM.Permissoes) {
                if (permissao.Controller.Contains(PermissaoHelper.CADASTRO.GetDescription())) {
                    _permissaoVM.MenuAcao = PermissaoHelper.GERENCIAR.GetDescription();
                    _permissaoVM.MenuCadastro = PermissaoHelper.MOSTRAR.GetDescription();
                    _permissaoVM.MenuEnsalamento = PermissaoHelper.MOSTRAR.GetDescription();
                    _permissaoVM.MenuOcorrencia = PermissaoHelper.MOSTRAR.GetDescription();
                    break;
                }
                if (permissao.Controller.Contains(PermissaoHelper.ENSALAMENTO.GetDescription()))
                    _permissaoVM.MenuEnsalamento = PermissaoHelper.MOSTRAR.GetDescription();
                if (permissao.Controller.Contains(PermissaoHelper.OCORRENCIA.GetDescription()))
                    _permissaoVM.MenuOcorrencia = PermissaoHelper.MOSTRAR.GetDescription();
            }
            Session["permissao"] = _permissaoVM;
        }
    }
}