using Backend.Model;
using Backend.Model.Metadata;
using System.Data.Entity;

namespace Backend.Context {
    public class ContextModel : DbContext {

        public ContextModel() : base("name=SistemaUOConnection") {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Atendimento> Atendimento { get; set; }
        public virtual DbSet<Chamado> Chamado { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Ensalamento> Ensalamento { get; set; }
        public virtual DbSet<Equipamento> Equipamento { get; set; }
        public virtual DbSet<EquipamentoSetor> EquipamentoSetor { get; set; }
        public virtual DbSet<HistoricoEnsalamento> HistoricoEnsalamento { get; set; }
        public virtual DbSet<IncidenteRecorrente> IncidenteRecorrente { get; set; }
        public virtual DbSet<Permissao> Permissao { get; set; }
        public virtual DbSet<Setor> Setor { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Turma> Turma { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<FormacaoAcademica> FormacaoAcademica { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<HistoricoContrato> HistoricoContrato { get; set; }
        public virtual DbSet<Conhecimentos> Conhecimentos { get; set; }
        public virtual DbSet<DadosBancarios> DadosBancarios { get; set; }
        public virtual DbSet<Disponibilidade> Disponibilidade { get; set; }
        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
        public virtual DbSet<AnexarDocumento> AnexarDocumento { get; set; }
        public virtual DbSet<Pergunta> Pergunta { get; set; }
    }
}
