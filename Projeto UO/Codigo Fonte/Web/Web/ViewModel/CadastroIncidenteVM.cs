using Backend.Model;
using System.Collections.Generic;

namespace Web.ViewModel {
    public class CadastroIncidenteVM : PermissaoVM {
        public IncidenteRecorrente IncidenteRecorrente { get; set; }
        public List<IncidenteRecorrente> IncidenteRecorrentes { get; set; }
        public CadastroIncidenteVM() {
            IncidenteRecorrente = new IncidenteRecorrente();
            IncidenteRecorrentes = new List<IncidenteRecorrente>();
        }
    }
}