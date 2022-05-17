using System.ComponentModel;

namespace Backend.Enum {
    public enum EnumStatusChamado {
        [Description("Aberta")]
        ABERTA = 1,

        [Description("Em Atendimento")]
        EM_ATENDIMENTO = 2,

        [Description("Concluída")]
        CONCLUIDA = 3,

        [Description("Cancelada")]
        CANCELADA = 4,

        [Description("Excluída")]
        EXCLUIDA = 5,

        [Description("Reaberta")]
        REABERTA = 6
    }
}
