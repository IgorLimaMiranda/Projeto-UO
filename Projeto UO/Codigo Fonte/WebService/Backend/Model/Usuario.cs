using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_USUARIO")]
    public class Usuario
    {
        [Column("DO_CPF"), Key, MaxLength(14)]
        public string Cpf { get; set; }

        [Column("US_NOME"), MaxLength(100)]
        public string Nome { get; set; }

        [Column("US_EMAIL"), MaxLength(150)]
        public string Email { get; set; }

        [Column("US_SENHA"), MaxLength(16)]
        public string Senha { get; set; }

        [Column("US_PERGUNTA"), MaxLength(100)]
        public string Pergunta { get; set; }

        [Column("US_RESPOSTA"), MaxLength(50)]
        public string Resposta { get; set; }

        [Column("US_PRIMEIRO_ACESSO")]
        public bool PrimeiroAcesso { get; set; }

        [Column("US_IMAGEM_USUARIO"), MaxLength(1024)]
        public string ImagemUsuario { get; set; }

        [Column("US_EIXO_EDUCACIONAL"), MaxLength(50)]
        public string EixoEducacional { get; set; }

        [Column("US_TIPO_USUARIO"), MaxLength(20)]
        public string TipoUsuario { get; set; }

        public Usuario()
        {
            ImagemUsuario = "user.png";
        }
    }
}
