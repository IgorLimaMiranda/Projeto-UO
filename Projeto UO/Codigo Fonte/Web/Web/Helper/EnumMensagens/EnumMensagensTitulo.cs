using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensTitulo {
        [Description("Desculpe!")]
        DESCULPE,

        [Description("Notificação")]
        NOTIFICACAO,

        [Description("Bem-sucedido!")]
        BEM_SUCEDIDO,

        [Description("Atenção!")]
        ATENCAO
    }
}