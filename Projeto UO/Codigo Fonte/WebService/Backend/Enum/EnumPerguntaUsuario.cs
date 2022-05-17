using System.ComponentModel;

namespace Backend.Enum {
    public enum EnumPerguntaUsuario {
        [Description("Qual sua comida favorita?")]
        PRIMEIRA_PERGUNTA = 1,

        [Description("País que deseja passar as férias?")]
        SEGUNDA_PERGUNTA = 2,

        [Description("Nome do seu pet?")]
        TERCEIRA_PERGUNTA = 3,

        [Description("Nome da sua avó materna?")]
        QUARTA_PERGUNTA = 4,

        [Description("Qual sua cor favorita?")]
        QUINTA_PERGUNTA = 5,

        [Description("Qual sua música favorita?")]
        SEXTA_PERGUNTA = 6
    }
}
