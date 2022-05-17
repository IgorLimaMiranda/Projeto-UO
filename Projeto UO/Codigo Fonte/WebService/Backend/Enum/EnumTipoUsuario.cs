using System.ComponentModel;

namespace Backend.Enum {
    public enum EnumTipoUsuario {
        [Description("Administrativo")]
        ADMINISTRATIVO = 1,

        [Description("Aluno(a)")]
        ALUNO = 2,

        [Description("Coordenador(a)")]
        COORDENADOR = 3,

        [Description("Gerente")]
        GERENTE = 4,

        [Description("Professor(a)")]
        PROFESSOR = 5
    }
}