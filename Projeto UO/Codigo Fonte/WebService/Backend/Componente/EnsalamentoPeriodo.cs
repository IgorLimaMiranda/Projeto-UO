using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Componente {
    public class EnsalamentoPeriodo {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Matutino { get; set; }
        public bool Vespertino { get; set; }
        public bool Noturno { get; set; }

        public EnsalamentoPeriodo() {
            Matutino = false;
            Vespertino = false;
            Noturno = false;
        }
    }
}
