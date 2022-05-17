using System.ComponentModel;

namespace Web.Helper {
    public enum PermissaoHelper {

        [Description("Cadastro")]
        CADASTRO,

        [Description("Ensalamento")]
        ENSALAMENTO,

        [Description("Gerenciar")]
        GERENCIAR,

        [Description("display: normal")]
        MOSTRAR,

        [Description("Ocorrencia")]
        OCORRENCIA,

        [Description("display: none")]
        OCULTAR,

        [Description("Pesquisar")]
        PESQUISAR
    }
}