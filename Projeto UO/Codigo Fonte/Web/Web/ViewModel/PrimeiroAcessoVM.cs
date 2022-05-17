using Backend.Enum;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Helper;

namespace Web.ViewModel {
    public class PrimeiroAcessoVM : PermissaoVM {
        public Usuario Usuario { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        public PrimeiroAcessoVM() {
            Usuario = new Usuario();
        }

        public IEnumerable<SelectListItem> ListarPerguntaUsuario() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumPerguntaUsuario>());
        }
    }
}