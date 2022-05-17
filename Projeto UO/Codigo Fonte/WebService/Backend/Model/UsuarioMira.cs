using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model {
    public class UsuarioMira {
        public string NomeAluno { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public string CPF { get; set; }
        public int Sexo { get; set; }
        public int CodigoPessoaFisica { get; set; }
    }
}
