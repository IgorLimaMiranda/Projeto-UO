using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensSala {
        [Description("A sala já existe!")]
        SALA_EXISTENTE,

        [Description("Recurso(s) cadastrado(s) com sucesso!")]
        RECURSO_SALA
    }
}