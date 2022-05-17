using Backend.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_CHAMADO")]
    public class Chamado {
        [Column("CH_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChamado { get; set; }

        [Column("CH_PATRIMONIO"), MaxLength(150)]
        public string Patrimonio { get; set; }

        [Column("CH_DESCRICAO"), MaxLength(500)]
        public string Descricao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Column("CH_HORA_DATA")]
        public DateTime HoraData { get; set; }

        [Column("CH_IMAGEM"), MaxLength(1024)]
        public string Imagem { get; set; }

        [Column("SE_ID")]
        public int? IdSetor { get; set; }

        [ForeignKey("IdSetor")]
        public Setor Setor { get; set; }

        [Column("IR_ID")]
        public int? IdIncidenteRecorrente { get; set; }

        [ForeignKey("IdIncidenteRecorrente")]
        public IncidenteRecorrente IncidenteRecorrente { get; set; }

        [Column("DO_CPF"), MaxLength(14)]
        public string CpfUsuario { get; set; }

        [ForeignKey("CpfUsuario")]
        public Usuario Usuario { get; set; }

		[NotMapped]
		public List<Atendimento> Atendimentos { get; set; }

		public Chamado() {
            HoraData = DateTime.Now;
        }
    }
}
