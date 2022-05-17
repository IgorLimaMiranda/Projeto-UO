using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_CONHECIMENTOS")]
     public class Conhecimentos
	{
        [Column("CO_ID" , Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Column("DO_CPF", Order = 1), Key,MaxLength(14), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Docente Docente { get; set; }
        
        [Column("CO_EIXO_DE_CONHECIMENTO"),MaxLength(20)]
        public string Eixo_De_Conhecimento { get; set; }

        [Column("CO_CONHECIMENTO"),MaxLength(50)]
        public string Conhecimento { get; set; }
	}
}
