using Backend.Componente;
using Backend.Model;
using System.Collections.Generic;

namespace Web.ViewModel {
    public class DashboardVM : PermissaoVM
    {
        public List<Chamado> UltimosChamados { get; set; }
        public List<EnsalamentoPeriodo> EnsalamentosDia { get; set; }

        public DashboardVM() {
            UltimosChamados = new List<Chamado>();
            EnsalamentosDia = new List<EnsalamentoPeriodo>();
        }
    }
}