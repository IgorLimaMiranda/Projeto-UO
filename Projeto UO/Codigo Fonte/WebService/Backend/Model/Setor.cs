using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    //Essa classe faz referência à 'Local', nomenclatura escolhida para as views de 'Setor' e 'Sala'
    [Table("TBL_SETOR")]
    public class Setor {
        [Column("SE_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("SE_NOME"), MaxLength(40)]
        public string Nome { get; set; }

        [Column("SE_ANDAR"), MaxLength(50)]
        public string Andar { get; set; }
    }
}
