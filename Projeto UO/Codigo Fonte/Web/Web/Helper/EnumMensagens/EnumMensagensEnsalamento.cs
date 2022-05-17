using System.ComponentModel;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensEnsalamento {
        [Description("Erro ao tentar cadastrar! Confira a lista de conflitos na tabela.")]
        ERRO_CADASTRAR_ENSALAMENTO,

        [Description("Não foi possivel realizar a exclusão!")]
        ERRO_EXCLUIR_ENSALAMENTO,

        [Description("Erro ao realizar a operação! O intervalo e a periodicidade não geraram nenhuma data!")]
        ERRO_PERIODICIDADE,

        [Description("Nenhum campo preenchido. Retornando todos os Ensalamentos")]
        ATENCAO_NENHUM_CAMPO_PREENCHIDO
    }
}