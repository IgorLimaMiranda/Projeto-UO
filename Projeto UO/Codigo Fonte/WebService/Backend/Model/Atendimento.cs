using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_ATENDIMENTO")]
    public class Atendimento {
        [Column("AT_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("AT_DETALHES"), MaxLength(350)]
        public string Detalhes { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Column("AT_DATA_ATENDIMENTO")]
        public DateTime DataAtendimento { get; set; }

        [Column("AT_STATUS"), MaxLength(20)]
        public string Status { get; set; }

        [Column("DO_CPF_TECNICO"), MaxLength(14)]
        public string CpfTecnico { get; set; }

        [ForeignKey("CpfTecnico")]
        public Usuario Usuario { get; set; }

        [Column("CH_ID")]
        public int IdChamado { get; set; }

        [ForeignKey("IdChamado")]
        public Chamado Chamado { get; set; }
    }
}
