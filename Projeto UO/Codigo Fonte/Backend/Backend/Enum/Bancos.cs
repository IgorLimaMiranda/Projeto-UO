using System.ComponentModel;

namespace Backend.Model
{
    public enum Bancos
    {
        [Description("Santander")]
        SANTANDER = 1,

        [Description("Banco do Brasil")]
        BANCO_DO_BRASIL = 2,

        [Description("Itaú")]
        ITAU = 3,

        [Description("Caixa Econômica Federal")]
        CAIXA_ECONOMICA_FEDERAL = 4,

        [Description("Bradesco")]
        BRADESCO = 5,
    }

}
