using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
    [Table("TBL_ENSALAMENTO")]
    public class Ensalamento {
        [Column("EN_ID")]
        public int Id { get; set; }

        [Column("EN_ID_PARCIAL")]
        public int? IdParcial { get; set; }

        [Column("SE_ID", Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSala { get; set; }

        [ForeignKey("IdSala")]
        public Sala Sala { get; set; }

        [Column("TU_ID")]
        public int IdTurma { get; set; }

        [Column("TU_ANO")]
        public int AnoTurma { get; set; }

        [ForeignKey("IdTurma, AnoTurma")]
        public Turma Turma { get; set; }

        [Column("EN_PERIODO", Order = 1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Periodo { get; set; }

        [Column("EN_DATA", Order = 2), Key, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [Column("EN_DATA_FIM"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFim { get; set; }

        [Column("EN_OCUPANTE"), MaxLength(100)]
        public string Ocupante { get; set; }


        //Criado por Gabriel, mas verificar se continuará assim
        public bool Equals(int id) {
            if (id == Id)
                return true;
            else
                return false;
        }
    }
}
