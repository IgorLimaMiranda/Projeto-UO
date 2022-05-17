using System.ComponentModel;

namespace Backend.Enum {
    public enum EnumPeriodoEnsalamento {
        [Description("Matutino")]
        MATUTINO = 1,

        [Description("Vespertino")]
        VESPERTINO = 2,

        [Description("Noturno")]
        NOTURNO = 3
    }
}
