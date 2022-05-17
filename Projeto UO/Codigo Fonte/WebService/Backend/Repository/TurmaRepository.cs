using Backend.Context;
using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class TurmaRepository : Repository<Turma> {
        ContextModel ctx = new ContextModel();

        public override IQueryable<Turma> Get(Func<Turma, bool> predicate) {
            return GetAll().OrderByDescending(predicate).AsQueryable();
        }
        
        public override IQueryable<Turma> GetAll() {
            return ctx.Set<Turma>().OrderByDescending(t => t.Ano).OrderBy(t => t.Id);
        }

        public List<Turma> GetTurmasLinq() {
            var turma = from t in ctx.Turma
                        .Include("Curso")
                        orderby t.Ano descending, t.Curso.Descricao ascending
                        select t;

            if(turma.Count() > 0)
                return turma.ToList();

            return null;
        }

        public Turma GetTurmaComCursoLinq(int id, int ano) {
            var turma = from t in ctx.Turma
                        .Include("Curso")
                        where t.Id == id && t.Ano == ano
                        select t;

            if(turma.Any())
                return turma.ToList()[0];

            return null;
        }
    }
}
