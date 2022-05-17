using System.ComponentModel;

namespace Backend.Model
{
    public enum TipoCandidato
    {
        [Description("Horista")]
        HORISTA = 1,

        [Description("Prestador")]
        PRESTADOR = 2,

        [Description("Candidato")]
        CANDIDATO = 3,
    }
}
