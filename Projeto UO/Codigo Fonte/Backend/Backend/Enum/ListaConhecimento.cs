using System.ComponentModel;

namespace Backend.Enum
{
    public enum ListaConhecimento
    {
        ///----------- Ti--------
        /////
        [Description("Redes")]
        REDES = 1,

        [Description("Hardware")]
        HARDWARE = 2,

        [Description("Manutenção")]
        MANUTENCAO = 3,

        [Description("Banco De Dados")]
        BANCO_DE_DADOS = 4,

        //---------Beleza--------
        //
        [Description("Maquiagem")]
        MAQUIAGEM = 5,

        [Description("Corte")]
        CORTE = 6,

        ///----------SAUDE-------------
        ///
        [Description("Primeiros Socorros")]
        PRIMEIROS_SOCORROS = 7,
        ///----------Audio Visual-------------
        ///
        [Description("Filmagem")]
        Filmagem = 8,

        [Description("Edição De Fotos")]
        Edição_De_Fotos = 9,

        [Description("Edição De Videos")]
        Edição_De_Videos = 10,
    }
}
