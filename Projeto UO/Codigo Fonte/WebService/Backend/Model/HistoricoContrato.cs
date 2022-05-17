using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_HISTORICO_CONTRATO")]
   public class HistoricoContrato
   {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Column("HC_DATA_INICIO",Order =0), Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataInicio { get; set; }

        [Column("DO_CPF",Order =1),Key, MaxLength(14), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Docente Docente { get; set; }

        [Column("HC_TIPO_CANDIDATO",Order =2),Key, MaxLength(20)]
        public string TipoCandidato  { get; set; }

        [Column("HC_TIPO_CONTRATO"), MaxLength(20)]
        public string TipoContrato { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Column("HC_DATA_FIM")]
        public DateTime DataFinal{ get; set; }
 









   }
}
