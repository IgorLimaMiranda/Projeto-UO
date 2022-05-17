using Backend.Enum;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Api;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    public class EditarPerfilController : PermissaoController {
        ApiUsuario _apiUsuario;
        ApiImagem _apiImagem;
        EditarPerfilVM _editarPerfilVM;
        PermissaoVM _session;

        public EditarPerfilController() {
            _apiUsuario = new ApiUsuario();
            _apiImagem = new ApiImagem();
            _editarPerfilVM = new EditarPerfilVM();
        }

        // GET: EditarPerfil
        [ActionFilter]
        public ActionResult Index() {
            AtribuirCampos();
            return View(_editarPerfilVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar([Bind(Include = "Usuario")] EditarPerfilVM perfilVM) {
            AtribuirCampos();
            _editarPerfilVM.Usuario = _session.UsuarioLogado;
            if (string.IsNullOrEmpty(_editarPerfilVM.Usuario.Pergunta) && !string.IsNullOrEmpty(perfilVM.Usuario.Pergunta))
                _editarPerfilVM.Usuario.Pergunta = perfilVM.Usuario.Pergunta;
            if (string.IsNullOrEmpty(_editarPerfilVM.Usuario.Resposta) && !string.IsNullOrEmpty(perfilVM.Usuario.Resposta))
                _editarPerfilVM.Usuario.Resposta = perfilVM.Usuario.Resposta;
            string[] s = _editarPerfilVM.Usuario.ImagemUsuario.Split('/');
            _editarPerfilVM.Usuario.ImagemUsuario = s[s.Length - 1];
            bool resultado = await _apiUsuario.AlterarUsuario(_editarPerfilVM.Usuario);
            if (resultado) {
                (Session["permissao"] as PermissaoVM).UsuarioLogado.Pergunta = perfilVM.Usuario.Pergunta;
                (Session["permissao"] as PermissaoVM).UsuarioLogado.Resposta = perfilVM.Usuario.Resposta;
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> EditarSenha([Bind(Include = "Usuario, Senha, SenhaAtual")] EditarPerfilVM perfilVM) {
            AtribuirCampos();
            if(perfilVM.SenhaAtual != (Session["permissao"] as PermissaoVM).UsuarioLogado.Senha) {
                return RedirectToAction("Index").Mensagem(EnumMensagensEditarPerfil.ERRO_SENHA_INVALIDA.GetDescription(), EnumTipoToastr.ERROR);
            }
            if (perfilVM.Senha.Length < 8) {
                perfilVM.Senha = "";
                return View("Index", _editarPerfilVM).Mensagem(EnumMensagensEditarPerfil.ERRO_SENHA.GetDescription(), EnumTipoToastr.ERROR);
            }
            else {
                _editarPerfilVM.Usuario = _session.UsuarioLogado;
                if(string.IsNullOrEmpty(_editarPerfilVM.Usuario.Pergunta) && !string.IsNullOrEmpty(perfilVM.Usuario.Pergunta))
                    _editarPerfilVM.Usuario.Pergunta = perfilVM.Usuario.Pergunta;
                if (string.IsNullOrEmpty(_editarPerfilVM.Usuario.Resposta) && !string.IsNullOrEmpty(perfilVM.Usuario.Resposta))
                    _editarPerfilVM.Usuario.Resposta = perfilVM.Usuario.Resposta;
                string[] s = _editarPerfilVM.Usuario.ImagemUsuario.Split('/');
                _editarPerfilVM.Usuario.ImagemUsuario = s[s.Length - 1];
                bool resultado = await _apiUsuario.AlterarUsuario(_editarPerfilVM.Usuario);
                if (resultado) {
                    (Session["permissao"] as PermissaoVM).UsuarioLogado.Senha = perfilVM.Senha;
                    return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
                }
                else {
                    return View("Index", _editarPerfilVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> EnviarImagem() {
            AtribuirCampos();
            string fileName = String.Empty;
            string extension = String.Empty;
            string path = string.Empty;

            foreach (string item in Request.Files) {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                fileName = /*DateTime.Today.ToString("dd.MM.yyyy HH-mm-ss-fff ") +*/ file.FileName;
                string UploadPath = "~/Arquivos/Images/";

                if (file.ContentLength == 0)
                    continue;
                if (file.ContentLength > 0) {
                    path = Path.Combine(HttpContext.Request.MapPath(UploadPath), fileName);
                    extension = Path.GetExtension(file.FileName);

                    file.SaveAs(path);
                }
            }

            Stream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //Cadastra a imagem no servidor. Ela fisicamente.
            _apiImagem.CadastrarImagem(fs, _editarPerfilVM.UsuarioLogado.Cpf + extension);

			/*Atuliza a imagem do usuário do perfil no banco de dados*/
            _editarPerfilVM.UsuarioLogado.ImagemUsuario = $"{_editarPerfilVM.UsuarioLogado.Cpf.Replace(".", "").Replace("-", "")}{extension}";
            bool resultado = await _apiUsuario.AlterarUsuario(_editarPerfilVM.UsuarioLogado);
            if (resultado) {
                (Session["permissao"] as PermissaoVM).UsuarioLogado.ImagemUsuario = _editarPerfilVM.UsuarioLogado.ImagemUsuario;
            }

            System.IO.File.Delete(fileName);

            return Json("Atualizar()");
        }

        private void AtribuirCampos() {
            _session = Session["permissao"] as PermissaoVM;
            _editarPerfilVM = AtribuirFiltros(_editarPerfilVM) as EditarPerfilVM;
            _editarPerfilVM.Usuario = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            string imagemUsuario = (Session["permissao"] as PermissaoVM).UsuarioLogado.ImagemUsuario;
            if (_editarPerfilVM.UsuarioLogado.ImagemUsuario == null) {
                _editarPerfilVM.UsuarioLogado.ImagemUsuario = $"{_apiImagem.Endereco}:{_apiImagem.Porta}/Imagens/Profile/user.png";
            }
        }
    }
}