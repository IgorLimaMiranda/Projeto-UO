using System.ComponentModel;

namespace Backend.Enum {
    public enum EnumDescricaoSala {

        [Description("Análises Clínicas")]
        ANALISES_CLINICAS = 1,

        [Description("Beleza")]
        BELEZA = 2,

        [Description("Enfermagem")]
        ENFERMAGEM = 3,

        [Description("Fotografia")]
        FOTOGRAFIA = 4,

        [Description("Informática")]
        INFORMATICA = 5,

        [Description("Podologia")]
        PODOLOGIA = 6

    }
}