using System.ComponentModel;

namespace Web.Helper.EnumMensagens {
    public enum EnumTipoToastr {
        [Description("error")]
        ERROR,

        [Description("success")]
        SUCCESS,

        [Description("info")]
        INFO,

        [Description("warning")]
        WARNING
    }
}