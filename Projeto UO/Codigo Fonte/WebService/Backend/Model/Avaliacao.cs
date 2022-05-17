using Backend.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_Avaliacao")]
     public class Avaliacao
    {
        [Column ("AV_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column ("DO_CPF"), MaxLength(14)]
        public string Cpf { get; set; }
        [ForeignKey ("Cpf")]
        public Docente Docente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}")]
        [Column("AV_DATA")]
        public DateTime Data { get; set; }

        [Column("AV_TIPO_DOCENTE"),MaxLength(20)]
        public string Tipo { get; set; }

        [Column("AV_AVALIADOR"),MaxLength(100)]
        public string Avaliador { get; set; }

        [Column("AV_APOIADOR"),MaxLength(100)]
        public string Apoiador{ get; set; }

        [Column("AV_NOTA_FINAL")]
        public int NotaFinal { get; set; }
       
    }
}
