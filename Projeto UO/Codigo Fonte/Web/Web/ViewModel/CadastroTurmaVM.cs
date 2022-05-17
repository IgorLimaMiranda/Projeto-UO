using Backend.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.ViewModel {
    public class CadastroTurmaVM : PermissaoVM {
        public Turma Turma { get; set; }
        public List<Turma> Turmas { get; set; }
        public List<Curso> Cursos { get; set; }

        public CadastroTurmaVM() {
            Turma = new Turma();
            Turmas = new List<Turma>();
            Cursos = new List<Curso>();
            Turma.Ano = DateTime.Now.Year;
        }

        public IEnumerable<SelectListItem> ListarCursos() {
            return AtribuirItensListaCursos(Cursos);
        }

        private List<SelectListItem> AtribuirItensListaCursos(IEnumerable<Curso> cursos) {
            var selectList = new List<SelectListItem>();
            foreach (var curso in cursos) {
                selectList.Add(new SelectListItem {
                    Value = curso.Id.ToString(),
                    Text = curso.Descricao
                });
            }
            return selectList;
        }
    }
}