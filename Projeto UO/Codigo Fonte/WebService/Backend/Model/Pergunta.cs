using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{     [Table("TBL_PERGUNTA")]

    public class Pergunta
    {
        [Column("PE_ID"), Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("PE_PERGUNTA"),MaxLength(100)]
        public string Pergunta_Avaliacao { get; set; }

        [Column("PE_TIPO_AVALIACAO"), MaxLength (15)]
        public string Tipo_Avaliacao { get; set; }
    }
}
