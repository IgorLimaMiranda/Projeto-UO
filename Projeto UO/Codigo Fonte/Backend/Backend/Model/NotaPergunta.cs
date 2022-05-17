using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_NOTA_PERGUNTA")]
    public class NotaPergunta
    {
      
            [Column("NT_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int NtId { get; set; }

            [Column("AV_ID", Order = 1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int AvId { get; set; }
            [ForeignKey("AvId")]
            public Avaliacao Avaliacao { get; set; }

            [Column("NT_NOTA")]
            public int Nota { get; set; }

            [Column("PE_ID"),]
            public int PeId { get; set; }
            [ForeignKey("PeId")]
            public Pergunta Pergunta { get; set; }
                      



        
    }
}
