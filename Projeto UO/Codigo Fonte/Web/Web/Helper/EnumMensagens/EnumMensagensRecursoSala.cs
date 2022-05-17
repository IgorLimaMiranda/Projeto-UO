using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensRecursoSala {
        [Description("Lista de recursos atualizada com sucesso!")]
        SUCESSO_SALVAR,

        [Description("Não foi possível atualizar lista de recursos!")]
        ERRO_SALVAR,

    }
}