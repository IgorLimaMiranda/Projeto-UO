using Backend.Componente;
using Backend.Context;
using Backend.Enum;
using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Backend.Repository {
    public class EnsalamentoRepository : Repository<Ensalamento> {
        private Ensalamento _ensalamento;
        private List<Ensalamento> _ensalamentos;

        public List<Turma> GetAllTurma() {
            var turmas = from t in ctx.Turma
                         .Include("Curso")
                         orderby t.Id ascending, t.Ano ascending
                         select t;
            if (turmas == null)
                return null;
            else
                return turmas.ToList();
        }

        public List<Ensalamento> GetAllLinq() {
            ctx = new ContextModel();
            GetAllTurma();
            var ensalamentos = from e in ctx.Ensalamento
                               .Include("Turma")
                               .Include("Sala")
                               orderby e.Id descending, e.Data ascending
                               select e;
            if (ensalamentos.ToList().Count > 0)
                return ensalamentos.ToList();
            else
                return null;
        }

        public List<Ensalamento> GetEnsalamentoDesmemebrado(Ensalamento ensalamento) {
            ctx = new ContextModel();
            GetAllTurma();
            var db = ctx.Ensalamento;
            var ensalamentos = from e in db
                               .Include("Turma")
                               .Include("Sala")
                               where e.Id == ensalamento.Id
                               orderby e.Id descending, e.Data ascending
                               select e;
            return ensalamentos.ToList();
        }

        public int GetLastId() {
            ctx = new ContextModel();
            int id = 0;
            var ensalamentos = from e in ctx.Ensalamento
                               orderby e.Id
                               select e.Id;
            if (ensalamentos.Any())
                id += ensalamentos.ToList().Last();
            return id;
        }

        public object ListarEnsalamentoPeriodicidade(bool listarPeriodicidade = false) {
            ctx = new ContextModel();
            List<Ensalamento> ensalamentos = GetAllLinq().ToList();

            int ensalamento, proxEnsalamento, dia;
            if (!listarPeriodicidade) {
                //ListarEnsalamento
                var lista = new List<Ensalamento>();
                if (ensalamentos.Count == 0)
                    return null;
                else if (ensalamentos.Count == 1) {
                    lista.Add(ensalamentos[0]);
                    return lista;
                }
                for (ensalamento = 0; ensalamento < ensalamentos.Count(); ensalamento++) {
                    //Verifica na lista de retorno:
                    //Se está vazia ou
                    //Se o último adicionado é diferente do ensalamento atual: [ensalamento]
                    if ((lista.Count == 0) || !(lista.Last().Id.Equals(ensalamentos[ensalamento].Id))) {
                        lista.Add(ensalamentos[ensalamento]);
                        //Verifica se o ensalamento atual é o mesmo do último da lista que veio do banco
                        if (ensalamentos[ensalamento].Id == ensalamentos.Last().Id)
                            break;
                    }
                }
                return AtribuirIncludes(lista);
            }
            else {
                //Listar Periodicidade
                List<List<bool>> lista = new List<List<bool>>();
                int ultimoEnsalamentoNaPeriodicidade = 0;
                //Se o banco estiver vazio
                if (ensalamentos.Count == 0)
                    return null;
                //Se a lista que veio do banco tiver apenas um índice
                else if (ensalamentos.Count == 1) {
                    lista.Add(new List<bool> { false, false, false, false, false, false, false });
                    lista[0][(int)ensalamentos[0].Data.DayOfWeek] = true;
                    return lista;
                }
                for (ensalamento = 0; ensalamento < ensalamentos.Count(); ensalamento++) {
                    if ((lista.Count == 0) || !ultimoEnsalamentoNaPeriodicidade.Equals(ensalamentos[ensalamento].Id)) {
                        proxEnsalamento = (ensalamento + 1);
                        lista.Add(new List<bool> { false, false, false, false, false, false, false });
                        //Pega os 7 dias desse ensalamento
                        for (dia = 0; dia < 7; dia++) {
                            //Verifica se o dia atual já não excedeu os dias ensalados no Código atual
                            if ((ensalamento + dia) < (ensalamentos.Count()) && ensalamentos[ensalamento].Id.Equals(ensalamentos[ensalamento + dia].Id)) {
                                //Pega o dia da semana deste dia do ensalamento e atribui true no responsável por ele na periodicidade, que é o dia: [ensalamento][dia]
                                lista[lista.Count - 1][(int)ensalamentos[ensalamento + dia].Data.DayOfWeek] = true;
                            }
                            else
                                break;
                            //Verifica se esse ensalamento é o último
                            if (ensalamento.Equals((ensalamentos.Count - 1)))
                                break;
                        }
                        ultimoEnsalamentoNaPeriodicidade = ensalamentos[ensalamento].Id;
                        //Se estiver no último código
                        if (ensalamentos[ensalamento].Id == ensalamentos.Last().Id)
                            break;
                    }
                }
                return lista;
            }
        }

        public List<Ensalamento> VerificarConflito(List<Ensalamento> ensalamentos) {
            ctx = new ContextModel();
            var ensal = from e in ensalamentos
                        orderby e.Data
                        select e;
            ensalamentos = ensal.ToList();
            List<Ensalamento> listaRetorno = new List<Ensalamento>();
            List<Ensalamento> listaBanco = new List<Ensalamento>();
            List<Ensalamento> listaComparar = new List<Ensalamento>();
            listaBanco = GetAllLinq();
            foreach (var item in listaBanco) {
                foreach (var param in ensalamentos)
                    if ((item.IdSala == param.IdSala) && (item.Periodo == param.Periodo)) {
                        if (listaComparar.Count > 0) {
                            if (listaComparar.Last().Id != item.Id)
                                listaComparar.Add(item);
                        }
                        else
                            listaComparar.Add(item);
                    }
            }


            foreach (var lista in listaComparar)
                foreach (var ensalamento in ensalamentos)
                    if (lista.Data.Date.Equals(ensalamento.Data.Date))
                        if (!(listaRetorno.Any()) || (listaRetorno.Any() && !(listaRetorno.Last().Id.Equals(lista.Id))))
                            listaRetorno.Add(lista);
            return listaRetorno;
        }

        public List<Ensalamento> AtribuirIncludes(List<Ensalamento> entradas) {
            ctx = new ContextModel();
            List<Ensalamento> listaRetorno = new List<Ensalamento>();
            List<Ensalamento> listaBanco = new List<Ensalamento>();
            listaBanco = GetAllLinq().ToList();
            foreach (var entrada in entradas) {
                if (entrada.Id != 0) {
                    var ensalamentos = from e in listaBanco
                                       where entrada.Id.Equals(e.Id)
                                       select e;
                    listaRetorno.Add(ensalamentos.First());

                }
                //Caso a entrada tenha o tamanho de apenas 1 índice e o id seja 0
                else if ((entradas != null) && (entrada.Id == 0 && entradas.Count == 1)) {
                    var ensalamentos = from e in listaBanco
                                       where entrada.IdSala.Equals(e.IdSala) && entrada.Periodo.Equals(e.Periodo) && entrada.Data.Equals(e.Data) && entrada.DataFim.Equals(e.DataFim)
                                       select e;
                    listaRetorno.Add(ensalamentos.First());
                    return listaRetorno;
                }
            }
            return listaRetorno;
        }

        ////Verificar se vai realmente precisar desse
        //public override void Add(Ensalamento ensalamento) {
        //    ctx = new ContextModel();
        //    ensalamentos = GetAll().ToList();
        //    if (ensalamentos != null) {
        //        int ultimaId = GetAll().Last().Id++;
        //    }
        //    else {
        //        ctx.Set<Ensalamento>().Add(ensalamento);
        //        SaveAll();
        //    }
        //}

        public List<EnsalamentoPeriodo> GetAllEnsalamentoDia(DateTime data) {
            List<EnsalamentoPeriodo> ensalamentoPeriodo = new List<EnsalamentoPeriodo>();

            var listaSetor = (from s in ctx.Sala where s.Tipo != null select s).ToList();

            foreach (var setor in listaSetor) {
                EnsalamentoPeriodo novo = new EnsalamentoPeriodo();
                novo.Nome = setor.Nome;
                novo.Id = setor.Id;

                if ((from e in ctx.Ensalamento where e.IdSala == novo.Id && e.Periodo == (int)EnumPeriodoEnsalamento.MATUTINO select e).ToList().Where(e => e.Data.Date == data.Date).ToList().Count() > 0)
                    novo.Matutino = true;
                if ((from e in ctx.Ensalamento where e.IdSala == novo.Id && e.Periodo == (int)EnumPeriodoEnsalamento.VESPERTINO select e).ToList().Where(e => e.Data.Date == data.Date).ToList().Count() > 0)
                    novo.Vespertino = true;
                if ((from e in ctx.Ensalamento where e.IdSala == novo.Id && e.Periodo == (int)EnumPeriodoEnsalamento.NOTURNO select e).ToList().Where(e => e.Data.Date == data.Date).ToList().Count() > 0)
                    novo.Noturno = true;

                ensalamentoPeriodo.Add(novo);
            }

            return ensalamentoPeriodo;
        }

        public List<Ensalamento> GetEnsalamentosDiario(DateTime dataEnsalamento, int enumPeriodo) {
            var ensalamentos = from e in ctx.Ensalamento
                               .Include("Sala")
							   .Include("Turma")
							   .Include("Turma.Curso")
                               where e.Periodo == enumPeriodo &&
                               e.Data == dataEnsalamento
                               orderby e.Sala.Numero
                               select e;

            if (ensalamentos.Any()) {
                return ensalamentos.ToList();
            }

            return new List<Ensalamento>();
        }
    }
}
