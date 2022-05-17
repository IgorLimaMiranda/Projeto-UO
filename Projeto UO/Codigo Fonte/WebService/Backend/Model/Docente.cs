using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    [Table("TBL_DOCENTE")]
	public class Docente
	{
        [Column("DO_CPF"), Key, MaxLength(14)]
		public string Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Usuario Usuario { get; set; }
        [Column("DO_NOME_COMPLETO"), MaxLength(100)]
        public string Nome_Completo { get; set; }
        [Column("DO_RG"),MaxLength(10)]
        public string Rg { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("DO_DATA_NASCIMENTO")]
        public DateTime Data_Nascimento {get; set;}
        [Column("DO_TELEFONE_FIXO")]
        public string Telefone_Fixo { get; set; }
        [Column("DO_CELULAR")]
        public string Celular { get; set; }
        [Column("DO_CELULAR_2")]
        public string Celular2 { get; set; }
        [Column("DO_EMAIL"), MaxLength(100)]
        public string Email { get; set; }
        [Column("DO_FOTO"), MaxLength(500)]
        public string Foto { get; set; }
        [Column("DO_DISP_VIAJAR")]
        public bool Disp_Viajar{ get; set; }
        [Column("DO_CEP"), MaxLength(15)]
        public string Cep { get; set; }
        [Column("DO_ENDERECO"), MaxLength(100)]
        public string Endereco { get; set; }
        [Column("DO_CIDADE")]
        public string Cidade { get; set; }
        [Column("DO_UF")]
        public string UF { get; set; }
        [Column("DO_DISP_HORARIO")]
        public string Disp_Horario { get; set; }

        //Mover o de baixo
        [Column("DO_NOME_INSTITUICAO"), MaxLength(50)]
        public string Nome_Instituicao { get; set; }
        [Column("DO_REFERENCIA"),MaxLength(100)]
        public string Referencia { get; set; }
        [Column("DO_CAMPO_EXERCIDO"), MaxLength(120)]
        public string Campo_Exercicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("DO_DATA_INICIO_ULTIMA_EXPERIENCIA")]
        public DateTime Data_Ultima_Experiencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("DO_DATA_FIM_ULTIMA_EXPERIENCIA")]
        public DateTime Data_Fim_Ultima_Experiencia { get; set; }
        [Column("DO_CONTATO_EMPRESA")]
        public string Contato_Empresa { get; set; }


	}
}
