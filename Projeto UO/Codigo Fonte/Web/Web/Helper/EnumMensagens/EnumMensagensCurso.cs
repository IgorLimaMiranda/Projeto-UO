using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensCurso {
        [Description("Esse curso ou sigla já existe!")]
        CURSO_EXISTENTE
    }
}