using Backend.Model;
using System.Collections.Generic;

namespace Web.ViewModel {
    public class CadastroRecursoVM : PermissaoVM { 
        public Equipamento Equipamento { get; set; }
        public List<Equipamento> Equipamentos { get; set; }
        
        public CadastroRecursoVM() {
            Equipamento = new Equipamento();
            Equipamentos = new List<Equipamento>();
        }
    }
}