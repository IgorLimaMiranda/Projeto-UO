using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_EQUIPAMENTO_SETOR")]
    public class EquipamentoSetor {
        [Column("EQ_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEquipamento { get; set; }

        [ForeignKey("IdEquipamento")]
        public Equipamento Equipamento { get; set; }

        [Column("SE_ID", Order = 1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSetor { get; set; }

        [ForeignKey("IdSetor")]
        public Setor Setor { get; set; }

        [Column("ES_QUANTIDADE")]
        public int Quantidade { get; set; }
    }
}
