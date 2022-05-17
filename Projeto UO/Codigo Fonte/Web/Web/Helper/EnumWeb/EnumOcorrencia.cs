using System.ComponentModel;

namespace Web.Helper.EnumWeb {
    public enum EnumOcorrencia {
        [Description("~\\Arquivos\\images\\logo\\senaclogo.png")]
        URI_IMAGEM_PADRAO,
        
        [Description("<i class='fa fa-flag bg-blue'></i>")]
        ICONE_ABERTA,

        [Description("<i class='fa fa-exclamation bg-yellow'></i>")]
        ICONE_EM_ATENDIMENTO,

        [Description("<i class='fa fa-close bg-red'></i>")]
        ICONE_CANCELADA,

        [Description("<i class='fa fa-trash bg-gray'></i>")]
        ICONE_EXCLUIDA,

        [Description("<i class='fa fa-check bg-green'></i>")]
        ICONE_CONCLUIDA,

        [Description("<i class='fa fa-refresh bg-azul-turquesa'></i>")]
        ICONE_REABERTA,

        [Description("disabled")]
        DISABLED,

        [Description("default-2")]
        LAYOUT_DISABLED,

        [Description("primary")]
        EDITAR_LAYOUT,

        [Description("warning")]
        EXCLUIR_LAYOUT
    }
}