
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DBContext
{
    public class ContextModel:DbContext
    {
        public ContextModel() : base("name=SistemaUOConnection")
        {

            this.Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<FormacaoAcademica>FormacaoAcademica { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
		public virtual DbSet<HistoricoContrato>HistoricoContrato { get; set; }
		public virtual DbSet<Conhecimentos> Conhecimentos { get; set; }
		public virtual DbSet<DadosBancarios> DadosBancarios { get; set; }
		public virtual DbSet<Disponibilidade>Disponibilidade { get; set; }
        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
        public virtual DbSet<AnexarDocumento> AnexarDocumento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<NotaPergunta> NotaPergunta { get; set; }
        public virtual DbSet<Pergunta> Pergunta { get; set; }



    }
}
