using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumMensagens {
    public enum EnumMensagensRecuperarSenha {
        [Description("Cpf inválido!")]
        CPF_INVALIDO,

        [Description("Dados incorretos!")]
        DADOS_INCORRETOS,

        [Description("Faça o primeiro acesso ou entre em contato com o administrador.")]
        PRIMEIRO_ACESSO
    }
}