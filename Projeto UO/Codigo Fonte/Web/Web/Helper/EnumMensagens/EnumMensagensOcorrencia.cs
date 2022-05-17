using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensOcorrencia {
        [Description("Ocorrência já cadastrada!")]
        OCORRENCIA_EXISTENTE,

        [Description("Status alterado com sucesso!")]
        SUCESSO_STATUS,

        [Description("Nenhum campo preenchido. Retornando todas as Ocorrências.")]
        ATENCAO_NENHUM_CAMPO_PREENCHIDO
    }
}