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
    [Table("TBL_DADOS_BANCARIOS")]
    public class DadosBancarios
	{
        [Column("DB_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DO_CPF", Order = 1), Key,MaxLength(17), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
        [ForeignKey("Cpf")]
        public Docente Docente { get; set; }

        [Column("DB_NUMERO_DA_AGENCIA")]
        public int Numero_Da_Agencia { get; set; }

        [Column("DB_TIPO_DE_CONTA"),MaxLength(50)]
        public string Tipo_De_Conta { get; set; }

        [Column("DB_NUMERO_DA_CONTA"),MaxLength(50)]
        public string Numero_Da_Conta { get; set; }

        [Column("DB_BANCO"),MaxLength(50)]
        public string Banco { get; set; }







    }
}
