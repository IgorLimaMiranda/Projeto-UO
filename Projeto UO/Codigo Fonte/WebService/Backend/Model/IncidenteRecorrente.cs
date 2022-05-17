using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_Incidente_RECORRENTE")]
    public class IncidenteRecorrente {
        [Column("IR_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("IR_DESCRICAO"), MaxLength(250)]
        public string Descricao { get; set; }
    }
}
