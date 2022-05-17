using System.ComponentModel;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensEditarPerfil {
        [Description("Senha com menos de 8 caracteres!")]
        ERRO_SENHA,

        [Description("Senha atual inválida. Tente novamente.")]
        ERRO_SENHA_INVALIDA
    }
}