using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_DISPONIBILIDADE")]
    public class Disponibilidade
	{
        [Column("DI_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("DI_DISPONIBILIDADE"), MaxLength(20)]
		public string Desc_Disponibilidade { get; set; }

        
        
    }
}
