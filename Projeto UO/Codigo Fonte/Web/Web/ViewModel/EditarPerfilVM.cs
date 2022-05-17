using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;

namespace Web.ViewModel {
    public class EditarPerfilVM : PermissaoVM {
        public string SenhaAtual { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Senha { get; set; }
        public Usuario Usuario { get; set; }

        public EditarPerfilVM() {
            Usuario = new Usuario();
        }

        public IEnumerable<SelectListItem> ListarPerguntaUsuario() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumPerguntaUsuario>());
        }
    }
}