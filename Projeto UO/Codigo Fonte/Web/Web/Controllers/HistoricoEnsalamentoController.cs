using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.ViewModel;


namespace Web.Controllers {
    public class HistoricoEnsalamentoController : PermissaoController {
        private HistoricoEnsalamentoVM _historicoEnsalamentoVM;
        private ApiSala _ApiSala;
        private ApiTurma _ApiTurma;
        private ApiUsuario _ApiUsuario;
        private ApiHistoricoEnsalamento _ApiHistoricoEnsalamento;

        public HistoricoEnsalamentoController() {
            _ApiSala = new ApiSala();
            _ApiTurma = new ApiTurma();
            _ApiUsuario = new ApiUsuario();
            _ApiHistoricoEnsalamento = new ApiHistoricoEnsalamento();
            _historicoEnsalamentoVM = new HistoricoEnsalamentoVM();
        }

        //GET: Index
        [ActionFilter]
        public ActionResult Index() {
            popularHistoricos(_historicoEnsalamentoVM);
            PopularListas(_historicoEnsalamentoVM);
            return View(_historicoEnsalamentoVM);
        }
        
        public ActionResult VisualizarHistoricoEnsalamento(HistoricoEnsalamento historico) {
            if(VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _historicoEnsalamentoVM.HistoricoEnsalamento = historico;
            _historicoEnsalamentoVM = AtribuirFiltros(_historicoEnsalamentoVM) as HistoricoEnsalamentoVM;
            return PartialView(_historicoEnsalamentoVM);
        }

        [ActionFilter]
        public async Task<ActionResult> Pesquisar([Bind(Include = "HistoricoEnsalamento,IntervaloHistorico")]HistoricoEnsalamentoVM historicoVM) {
            string[] IntervaloHistorico = { string.Empty, string.Empty };
            if (historicoVM.HistoricoEnsalamento.Acao != null || historicoVM.HistoricoEnsalamento.Ocupante != null || historicoVM.HistoricoEnsalamento.Sala != null || historicoVM.HistoricoEnsalamento.Turma != null || historicoVM.HistoricoEnsalamento.Turno != null || historicoVM.HistoricoEnsalamento.Usuario != null || historicoVM.IntervaloHistorico != null) {
                if (historicoVM.IntervaloHistorico != null) {
                    IntervaloHistorico = historicoVM.IntervaloHistorico.Split('-');
                    historicoVM.HistoricoEnsalamento.DataInicio = DateTime.Parse(IntervaloHistorico[0]);
                    historicoVM.HistoricoEnsalamento.DataFim = DateTime.Parse(IntervaloHistorico[1]);
                }

                historicoVM.Historicos = await _ApiHistoricoEnsalamento.ListarHistoricos() as List<HistoricoEnsalamento>;
                HistoricoEnsalamento historicoPesquisa = historicoVM.HistoricoEnsalamento;
                List<HistoricoEnsalamento> retornoHistoricos = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista1 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista2 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista3 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista4 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista5 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista6 = new List<HistoricoEnsalamento>();
                List<HistoricoEnsalamento> Lista7 = new List<HistoricoEnsalamento>();
                int acertos;
                foreach (var historico in historicoVM.Historicos) {
                    acertos = 0;
                    if ((historico.DataInicio != DateTime.MinValue && historico.DataFim != DateTime.MinValue) && VerificarDatas(historicoPesquisa.DataInicio, historicoPesquisa.DataFim, historico.DataInicio, historico.DataFim))
                        acertos++;
                    if ((historico.Sala != null) && (historicoPesquisa.Sala != null) && historicoPesquisa.Sala.Equals(historico.Sala))
                        acertos++;
                    if ((historico.Turma != null) && (historicoPesquisa.Turma != null) && historicoPesquisa.Turma.Equals(historico.Turma))
                        acertos++;
                    if ((historico.Turno != null) && (historicoPesquisa.Turno != null) && historicoPesquisa.Turno.Equals(historico.Turno))
                        acertos++;
                    if ((historico.Acao != null) && (historicoPesquisa.Acao != null) && historicoPesquisa.Acao.Equals(historico.Acao))
                        acertos++;
                    if ((historico.Usuario != null) && (historicoPesquisa.Usuario != null) && historicoPesquisa.Usuario.Equals(historico.Usuario))
                        acertos++;
                    if ((historico.Ocupante != null) && (historicoPesquisa.Ocupante != null) && historicoPesquisa.Ocupante.Equals(historico.Ocupante))
                        acertos++;

                    switch (acertos) {
                        case 1:
                            Lista1.Add(historico);
                            break;
                        case 2:
                            Lista2.Add(historico);
                            break;
                        case 3:
                            Lista3.Add(historico);
                            break;
                        case 4:
                            Lista4.Add(historico);
                            break;
                        case 5:
                            Lista5.Add(historico);
                            break;
                        case 6:
                            Lista5.Add(historico);
                            break;
                        case 7:
                            Lista5.Add(historico);
                            break;
                    }
                }
                //if(historicoVM.TipoDePesquisa.Equals(EnumPesquisa.ACERTOS.GetDescription())) {
                if ((Lista7 != null) && Lista7.Count() > 0)
                    retornoHistoricos = Lista7;
                else if ((Lista6 != null) && Lista6.Count() > 0)
                    retornoHistoricos = Lista6;
                else if ((Lista5 != null) && Lista5.Count() > 0)
                    retornoHistoricos = Lista5;
                else if ((Lista4 != null) && Lista4.Count() > 0)
                    retornoHistoricos = Lista4;
                else if ((Lista3 != null) && Lista3.Count() > 0)
                    retornoHistoricos = Lista3;
                else if ((Lista2 != null) && Lista2.Count() > 0)
                    retornoHistoricos = Lista2;
                else if ((Lista1 != null) && Lista1.Count() > 0)
                    retornoHistoricos = Lista1;
                //}
                //else {

                //    if((Lista5 != null) && Lista5.Count() > 0)
                //        retornoEnsalamentos.AddRange(Lista5);
                //    if((Lista4 != null) && Lista4.Count() > 0)
                //        retornoEnsalamentos.AddRange(Lista4);
                //    if((Lista3 != null) && Lista3.Count() > 0)
                //        retornoEnsalamentos.AddRange(Lista3);
                //    if((Lista2 != null) && Lista2.Count() > 0)
                //        retornoEnsalamentos.AddRange(Lista2);
                //    if((Lista1 != null) && Lista1.Count() > 0)
                //        retornoEnsalamentos.AddRange(Lista1);
                //}
                PopularListas(_historicoEnsalamentoVM);
                var historicos = _ApiHistoricoEnsalamento.ListarHistoricos().Result;
                foreach (var itemRetorno in retornoHistoricos) {
                    foreach (var itemBanco in historicos) {
                        if (itemBanco.Id == itemRetorno.Id) {
                            _historicoEnsalamentoVM.Historicos.Add(itemRetorno);
                        }
                    }
                }
                _historicoEnsalamentoVM.Pesquisa = string.Empty;
                return View("Index", _historicoEnsalamentoVM);
            }
            return RedirectToAction("Index");
        }

        public List<String> ListarSalas() {
            return _ApiHistoricoEnsalamento.ListarSalas().Result.ToList();
        }

        public List<String> ListarTurmas() {
            return _ApiHistoricoEnsalamento.ListarTurmas().Result.ToList();
        }

        public List<String> ListarUsuarios() {
            return _ApiHistoricoEnsalamento.ListarUsuarios().Result.ToList();
        }

        public List<String> ListarOcupantes() {
            return _ApiHistoricoEnsalamento.ListarOcupantes().Result.ToList();
        }

        public List<HistoricoEnsalamento> ListarHistoricos() {
            return _ApiHistoricoEnsalamento.ListarHistoricos().Result;
        }

        //GET: VisualizarHistoricoEnsalamento
        //[ActionFilter]
        //public ActionResult VisualizarHistoricoEnsalamento()
        //{
        //    return PartialView();
        //}
        public void popularHistoricos(HistoricoEnsalamentoVM historicoVM) {
            _historicoEnsalamentoVM.Historicos = ListarHistoricos() as List<HistoricoEnsalamento>;
        }

        public void PopularListas(HistoricoEnsalamentoVM historicoEnsalamentoVM) {
            _historicoEnsalamentoVM = historicoEnsalamentoVM;
            _historicoEnsalamentoVM.ComboSalas = ListarSalas();
            _historicoEnsalamentoVM.ComboTurmas = ListarTurmas();
            _historicoEnsalamentoVM.ComboOcupantes = ListarOcupantes();
            _historicoEnsalamentoVM.ComboUsuarios = ListarUsuarios();
            _historicoEnsalamentoVM = AtribuirFiltros(_historicoEnsalamentoVM) as HistoricoEnsalamentoVM;
        }

        public bool VerificarDatas(DateTime pesquisaInicio, DateTime pesquisaFim, DateTime inicioAtual, DateTime fimAtual) {
            for(DateTime dia = pesquisaInicio; dia <= pesquisaFim; dia = dia.AddDays(1)) {
                if(dia != DateTime.MinValue)
                    if((inicioAtual == dia || fimAtual == dia) || (dia > inicioAtual && dia < fimAtual))
                        return true;
            }
            return false;
        }
    }
}