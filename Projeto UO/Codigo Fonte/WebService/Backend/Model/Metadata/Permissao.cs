using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Metadata {
    [Table("mdt.TBL_PERMISSAO")]
    public class Permissao {

        [Column("PE_ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("PE_TIPO_USUARIO"), MaxLength(20)]
        public string TipoUsuario { get; set; }

        [Column("PE_CONTROLLER"), MaxLength(50)]
        public string Controller { get; set; }
    }
}
