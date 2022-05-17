using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_HISTORICO_ENSALAMENTO")]
    public class HistoricoEnsalamento {
        [Column("HE_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column("HE_DATA_INICIO")]
        public DateTime DataInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column("HE_DATA_FIM")]
        public DateTime DataFim { get; set; }

        [Column("HE_PERIODICIDADE"), MaxLength(30)]
        public string Periodicidade { get; set; }

        [Column("HE_SALA"), MaxLength(100)]
        public string Sala { get; set; }

        [Column("HE_TURNO"), MaxLength(100)]
        public string Turno { get; set; }

        [Column("HE_TURMA"), MaxLength(100)]
        public string Turma { get; set; }

        [Column("HE_USUARIO"), MaxLength(100)]
        public string Usuario { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - HH:mm}")]
        [Column("HE_DATA_HORA")]
        public DateTime DataHora { get; set; }

        [Column("HE_ACAO"), MaxLength(100)]
        public string Acao { get; set; }

        [Column("HE_OCUPANTE"), MaxLength(100)]
        public string Ocupante { get; set; }

        public HistoricoEnsalamento() {
            DataHora = DateTime.Now;
        }
    }
}
