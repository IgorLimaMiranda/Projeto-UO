using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    //Essa classe faz referência à 'Recurso', nomenclatura escolhida para a view
    [Table("TBL_EQUIPAMENTO")]
    public class Equipamento {
        [Column("EQ_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("EQ_DESCRICAO"), MaxLength(100)]
        public string Descricao { get; set; }
    }
}
