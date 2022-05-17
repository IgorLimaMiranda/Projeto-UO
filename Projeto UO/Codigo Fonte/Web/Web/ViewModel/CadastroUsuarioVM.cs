using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;

namespace Web.ViewModel {
    public class CadastroUsuarioVM : PermissaoVM {
        public Usuario Usuario { get; set; }
        public List<Usuario> Usuarios { get; set; }

        public CadastroUsuarioVM() {
            Usuario = new Usuario();
            Usuarios = new List<Usuario>();
        }

        public IEnumerable<SelectListItem> ListarTiposUsuario() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumTipoUsuario>());
        }

        public IEnumerable<SelectListItem> ListarEixoEducacionalProfessor() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumEixoEducacional>());
        }
    }
}