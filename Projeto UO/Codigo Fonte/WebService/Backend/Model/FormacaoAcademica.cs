using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_FORMACAO_ACADEMICA")]
    public class FormacaoAcademica
    {
        [Column("FA_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Column("DO_CPF", Order = 1), Key,MaxLength(14),DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Docente Docente{ get; set; }

        [Column("FA_NOME_DO_CURSO"), MaxLength(40)]
        public string Nome_Do_Curso { get; set; }

        [Column("FA_INSTITUICAO_ENSINO"),MaxLength(30)]
        public string Instituicao_De_Ensino { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("FA_DATA_INICIO")]
        public DateTime Data_Inicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("FA_DATA_FIM")]
        public DateTime Data_Fim { get; set; }

        [Column("FA_SITUACAO_CURSO"),MaxLength(15)]
		public string Situa_Curso { get; set; }






	}
}
