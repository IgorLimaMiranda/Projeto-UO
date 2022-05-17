using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Model {
    public class AutenticarUsuario {
        public int CodigoPessoaFisica { get; set; }
        public string Token { get; set; }
        public string NomeAluno { get; set; }
        public string Email { get; set; }
    }
}