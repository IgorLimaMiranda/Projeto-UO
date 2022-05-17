using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_TURMA")]
    public class Turma {
        [Column("TU_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [Column("TU_ANO", Order = 1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ano { get; set; }

        [Column("CU_ID")]
        public int IdCurso { get; set; }

        [ForeignKey("IdCurso")]
        public Curso Curso { get; set; }
    }
}
