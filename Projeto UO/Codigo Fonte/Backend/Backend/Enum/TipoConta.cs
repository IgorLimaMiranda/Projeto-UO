using System.ComponentModel;

namespace Backend.Model
{
    public enum TipoConta
    {
        [Description("Conta-Corrente")]
        CONTA_CORRENTE = 1,

        [Description("Conta-Poupança")]
        CONTA_POUPANCA = 2,

        [Description("Conta-Salário")]
        CONTA_SALARIO = 3,

    }
}
