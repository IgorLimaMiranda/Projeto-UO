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
        [Column("DO_CPF"), Key,MaxLength(17),DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Column("DO_TELEFONE_FIXO"),MaxLength(15)]
        public string Telefone_Fixo { get; set; }

        [Column("DO_CELULAR"),MaxLength(15)]
        public string Celular { get; set; }

        [Column("DO_CELULAR_2"), MaxLength(15)]
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
        public string Endereço { get; set; }

        [Column("DO_CIDADE"),MaxLength(16)]
        public string Cidade { get; set; }

        [Column("DO_UF"), MaxLength(2)]
        public string UF { get; set; }

        [Column("DO_DISP_HORARIO"),MaxLength(20)]
        public string Disp_Horario { get; set; }
        

	}
}
