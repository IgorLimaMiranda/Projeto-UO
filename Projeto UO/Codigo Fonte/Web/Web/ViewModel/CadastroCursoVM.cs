using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.ViewModel {
    public class CadastroCursoVM : PermissaoVM {
        public Curso Curso { get; set; }
        public List<Curso> Cursos { get; set; }

        public CadastroCursoVM() {
            Curso = new Curso();
            Cursos = new List<Curso>();
        }
    }
}