using System.ComponentModel;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensLogin {
        [Description("A senha está errada!")]
        SENHA_ERRADA,

        [Description("Usuário não existe!")]
        USUARIO_NAO_EXISTE,

        [Description("Cpf é inválido!")]
        CPF_INVALIDO
    }
}