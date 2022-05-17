using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_ANEXAR_DOCUMENTO")]
    public class AnexarDocumento
    {
        [Column("AD_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DO_CPF", Order = 1), Key, MaxLength(14), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }

        [ForeignKey("Cpf")]
        public Docente Docente { get; set; }

        [Column("AD_ARQUIVO"), MaxLength(500)]
        public string Arquivo { get; set; }
    }
}
