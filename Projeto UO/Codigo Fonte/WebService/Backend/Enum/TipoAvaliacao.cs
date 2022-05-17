using System.ComponentModel;

namespace Backend.Model
{
    public enum TipoAvaliacao
    {
        [Description("Mini Aula")]
        MINI_AULA = 1,

        [Description("Entrevista")]
        ENTREVISTA = 2,
    }
}
