using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_CURSO")]
    public class Curso {
        [Column("CU_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CU_DESCRICAO"), MaxLength(100)]
        public string Descricao { get; set; }

        [Column("CU_SIGLA"), MaxLength(50)]
        public string Sigla { get; set; }
    }
}
