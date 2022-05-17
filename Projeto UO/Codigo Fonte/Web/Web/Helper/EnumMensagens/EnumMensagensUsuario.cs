using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensUsuario {
        [Description("Este Usuário já existe!")]
        USUARIO_EXISTENTE
    }
}