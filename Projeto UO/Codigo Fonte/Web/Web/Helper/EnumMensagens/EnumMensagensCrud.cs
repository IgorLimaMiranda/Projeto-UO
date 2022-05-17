using System.ComponentModel;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensCrud {
        [Description("Cadastro efetuado com sucesso!")]
        SUCESSO_CADASTRO,

        [Description("Edição efetuada com sucesso!")]
        SUCESSO_EDICAO,

        [Description("Excluído com sucesso!")]
        SUCESSO_EXCLUSAO,

        [Description("Não foi possível realizar a exclusão, pois você não pode deletar a si mesmo")]
        ERRO_AUTO_EXCLUSAO,

        [Description("Não foi possível realizar o cadastro")]
        ERRO_CADASTRO,

        [Description("Não foi possível realizar a edição")]
        ERRO_EDICAO,

        [Description("Não foi possível realizar a exclusão, pois esse registro está vinculado a outros cadastros")]
        ERRO_EXCLUSAO,

        [Description("Não foi possível realizar a alteração, pois é preciso que tenha ao menos 2 administradores no sistema")]
        ERRO_ALTERAR_MINIMO_ADMINISTRADORES,

        [Description("Não foi possível realizar a exclusão, pois é preciso que tenha ao menos 2 administradores no sistema")]
        ERRO_DELETAR_MINIMO_ADMINISTRADORES
    }
}