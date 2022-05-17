using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensIncidenteRecorrente {
        [Description("Esse incidente já existe!")]
        INCIDENTE_EXISTENTE
    }
}