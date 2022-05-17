using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    public class Sala : Setor {
        [Column("SE_NUMERO")]
        public int? Numero { get; set; }

        [Column("SE_TIPO"), MaxLength(50)]
        public string Tipo { get; set; }

        [Column("SE_DESCRICAO"), MaxLength(200)]
        public string Descricao { get; set; }

        [Column("SE_CAPACIDADE")]
        public int? Capacidade { get; set; }

        [Column("SE_DATA_ALTERACAO")]
        public DateTime? DataAlteracao { get; set; }

        public Sala() {
            DataAlteracao = DateTime.Now;
        }
    }
}
