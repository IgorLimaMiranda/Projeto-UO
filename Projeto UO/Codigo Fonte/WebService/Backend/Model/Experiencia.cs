using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_EXPERIENCIA")]
    public class Experiencia
    {
        [Column("EX_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DO_CPF", Order = 1), Key, MaxLength(14), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Docente Docente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]

        [Column("EX_DATA_INICIO_ULTIMA_EXPERIENCIA")]
        public string DataInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]

        [Column("EX_DATA_FIM_ULTIMA_EXPERIENCIA")]
        public string DataFim { get; set; }

        [Column("EX_CAMPO_EXERCIDO"),MaxLength(120)]
        public string CampoExercido { get; set; }

        [Column("EX_REFERENCIA"),MaxLength(100)]
        public string Referencia { get; set; }

        [Column("EX_CONTATO_EMPRESA"),MaxLength(15)]
        public string ContatoEmpresa { get; set; }

        [Column("EX_NOME_INSTITUICAO"),MaxLength(50)]
        public string NomeInstituicao { get; set; }
    }
}
