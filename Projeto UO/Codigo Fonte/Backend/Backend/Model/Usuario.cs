using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    [Table("TBL_USUARIO")]
    public class Usuario
    {
        [Column("DO_CPF"),Key, MaxLength(17),DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
        
    }
}
