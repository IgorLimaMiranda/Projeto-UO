using Backend.Enum;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper;
using Web.Helper.EnumMensagens;
using Web.Helper.EnumWeb;
using Web.ViewModel;

namespace Web.Controllers {
    /// <summary>
    /// Ensalamento possui duas controllers
    /// <para>EnsalamentoController: Executa o que todos podem ver (além de métodos compartilhados com CadastroEnsalamento): GETs de Ensalamento</para>
    /// <para>CadastroEnsalamentoController: Executa o que apenas os administradores podem usar: Adicionar, editar e excluir.</para>
    /// </summary>
    public class EnsalamentoController : PermissaoController {
        protected PermissaoVM _permissaoVM;
        protected EnsalamentoVM _ensalamentoVM;
        protected ApiEnsalamento _apiEnsalamento;
        protected ApiSala _apiSala;
        protected ApiTurma _apiTurma;

        public EnsalamentoController() {
            _ensalamentoVM = new EnsalamentoVM();
            _apiEnsalamento = new ApiEnsalamento();
            _apiSala = new ApiSala();
            _apiTurma = new ApiTurma();
        }

        // GET: Ensalamento
        [ActionFilter]
        public ActionResult Index() {
            if (TempData["pesquisa"] != null) {
                _ensalamentoVM = TempData["pesquisa"] as EnsalamentoVM;
                if (TempData["mensagemEnsalamento"] != null) {
                    var toastr = TempData["mensagemEnsalamento"] as List<string>;
                    if (toastr[1].Equals((EnumTipoToastr.WARNING).ToString()))
                        return View(_ensalamentoVM).Mensagem(toastr[0], EnumTipoToastr.WARNING);
                }
                return View(_ensalamentoVM);
            }
            if ((Session["listaConflitos"] != null) && ((Session["listaConflitos"] as EnsalamentoVM).Conflitos)) {
                _ensalamentoVM = Session["listaConflitos"] as EnsalamentoVM;
                (Session["listaConflitos"] as EnsalamentoVM).Conflitos = false;
                return View(_ensalamentoVM).Mensagem(EnumMensagensEnsalamento.ERRO_CADASTRAR_ENSALAMENTO.GetDescription(), EnumTipoToastr.ERROR);
            }
            else if (Session["listaConflitos"] != null) {
                Session["listaConflitos"] = null;
            }
            PopularEnsalamentos(_ensalamentoVM);
            PopularListas(_ensalamentoVM);
            if (TempData["mensagemEnsalamento"] != null) {
                var toastr = TempData["mensagemEnsalamento"] as List<string>;
                if (toastr[1].Equals((EnumTipoToastr.SUCCESS).ToString()))
                    return View(_ensalamentoVM).Mensagem(toastr[0], EnumTipoToastr.SUCCESS);
                else
                    return View(_ensalamentoVM).Mensagem(toastr[0], EnumTipoToastr.ERROR);
            }
            return View(_ensalamentoVM);
        }

        [ActionFilter]
        public async Task<ActionResult> Pesquisar([Bind(Include = "Ensalamento, ChaveTurma, IntervaloEnsalamento, TipoDePesquisa")] EnsalamentoVM ensalamentoVM) {
            if ((ensalamentoVM.Ensalamento.Ocupante == null) && (ensalamentoVM.Ensalamento.IdSala == 0) && (ensalamentoVM.Ensalamento.Periodo == 0) && (ensalamentoVM.ChaveTurma == null) && (ensalamentoVM.IntervaloEnsalamento == null)) {
                PopularListas(_ensalamentoVM);
                PopularEnsalamentos(_ensalamentoVM);
                _ensalamentoVM.IsPesquisa = true;
                TempData["pesquisa"] = _ensalamentoVM;
                _ensalamentoVM.IsEnsalamentos = _ensalamentoVM.Ensalamentos != null ? true : false;
                List<string> mensagem = new List<string>();
                mensagem.Add(EnumMensagensEnsalamento.ATENCAO_NENHUM_CAMPO_PREENCHIDO.GetDescription());
                mensagem.Add((EnumTipoToastr.WARNING).ToString());
                TempData["mensagemEnsalamento"] = mensagem;
                return RedirectToAction("Index");
            }
            string[] ChaveTurma = { string.Empty, string.Empty }, IntervaloEnsalamento = { string.Empty, string.Empty };
            //Inicio dos splits
            if (ensalamentoVM.ChaveTurma != null) {
                ChaveTurma = ensalamentoVM.ChaveTurma.Split('/');
                ensalamentoVM.Ensalamento.IdTurma = Convert.ToInt32(ChaveTurma[0]);
                ensalamentoVM.Ensalamento.AnoTurma = Convert.ToInt32(ChaveTurma[1]);
            }
            if (ensalamentoVM.IntervaloEnsalamento != null) {
                IntervaloEnsalamento = ensalamentoVM.IntervaloEnsalamento.Split('-');
                ensalamentoVM.Ensalamento.Data = Convert.ToDateTime(IntervaloEnsalamento[0]);
                ensalamentoVM.Ensalamento.DataFim = Convert.ToDateTime(IntervaloEnsalamento[1]);
            }
            //Fim dos splits
            ensalamentoVM.Ensalamentos = await _apiEnsalamento.ListarEnsalamentos() as List<Ensalamento>;
            Ensalamento ensalamentoPesquisa = ensalamentoVM.Ensalamento;
            List<Ensalamento> retornoEnsalamentos = new List<Ensalamento>();
            List<Ensalamento> Lista1 = new List<Ensalamento>();
            List<Ensalamento> Lista2 = new List<Ensalamento>();
            List<Ensalamento> Lista3 = new List<Ensalamento>();
            List<Ensalamento> Lista4 = new List<Ensalamento>();
            List<Ensalamento> Lista5 = new List<Ensalamento>();
            int acertos;
            foreach (var ensalamento in ensalamentoVM.Ensalamentos) {
                acertos = 0;
                if ((ensalamentoPesquisa.Data != DateTime.MinValue && ensalamentoPesquisa.DataFim != DateTime.MinValue) && VerificarDatas(ensalamentoPesquisa.Data, ensalamentoPesquisa.DataFim, ensalamento.Data, ensalamento.DataFim))
                    acertos++;
                if ((ensalamentoPesquisa.Periodo != 0) && ensalamentoPesquisa.Periodo.Equals(ensalamento.Periodo))
                    acertos++;
                if ((ensalamentoPesquisa.IdTurma != 0 && ensalamentoPesquisa.AnoTurma != 0) && (ensalamentoPesquisa.AnoTurma.Equals(ensalamento.AnoTurma) && ensalamentoPesquisa.IdTurma.Equals(ensalamento.IdTurma)))
                    acertos++;
                if ((ensalamentoPesquisa.IdSala != 0) && ensalamentoPesquisa.IdSala.Equals(ensalamento.Sala.Id))
                    acertos++;
                if ((ensalamentoPesquisa.Ocupante != null && ensalamento.Ocupante != null) && ensalamento.Ocupante.Contains(ensalamentoPesquisa.Ocupante))
                    acertos++;

                switch (acertos) {
                    case 1:
                        Lista1.Add(ensalamento);
                        break;
                    case 2:
                        Lista2.Add(ensalamento);
                        break;
                    case 3:
                        Lista3.Add(ensalamento);
                        break;
                    case 4:
                        Lista4.Add(ensalamento);
                        break;
                    case 5:
                        Lista5.Add(ensalamento);
                        break;
                }
            }
            if (ensalamentoVM.TipoDePesquisa.Equals(EnumPesquisa.ACERTOS.GetDescription())) {
                if ((Lista5 != null) && Lista5.Count() > 0)
                    retornoEnsalamentos = Lista5;
                else if ((Lista4 != null) && Lista4.Count() > 0)
                    retornoEnsalamentos = Lista4;
                else if ((Lista3 != null) && Lista3.Count() > 0)
                    retornoEnsalamentos = Lista3;
                else if ((Lista2 != null) && Lista2.Count() > 0)
                    retornoEnsalamentos = Lista2;
                else if ((Lista1 != null) && Lista1.Count() > 0)
                    retornoEnsalamentos = Lista1;
            }
            else {

                if ((Lista5 != null) && Lista5.Count() > 0)
                    retornoEnsalamentos.AddRange(Lista5);
                if ((Lista4 != null) && Lista4.Count() > 0)
                    retornoEnsalamentos.AddRange(Lista4);
                if ((Lista3 != null) && Lista3.Count() > 0)
                    retornoEnsalamentos.AddRange(Lista3);
                if ((Lista2 != null) && Lista2.Count() > 0)
                    retornoEnsalamentos.AddRange(Lista2);
                if ((Lista1 != null) && Lista1.Count() > 0)
                    retornoEnsalamentos.AddRange(Lista1);
            }
            PopularListas(_ensalamentoVM);
            var ensalamentos = _apiEnsalamento.ListarEnsalamentos().Result as List<Ensalamento>;
            var periodicidade = _apiEnsalamento.ListarEnsalamentos(true).Result as List<List<bool>>;
            foreach (var itemRetorno in retornoEnsalamentos) {
                int i = 0;
                foreach (var itemBanco in ensalamentos) {
                    if (itemBanco.Id == itemRetorno.Id) {
                        _ensalamentoVM.Ensalamentos.Add(itemRetorno);
                        _ensalamentoVM.PeriodicidadeTabela.Add(periodicidade[i]);
                    }
                    i++;
                }
            }
            _ensalamentoVM.ColunaOcultaPeriodicidade = AtribuirStringPeriodicidade(_ensalamentoVM.Ensalamentos, _ensalamentoVM.PeriodicidadeTabela);
            _ensalamentoVM.IsPesquisa = true;
            TempData["pesquisa"] = _ensalamentoVM;
            _ensalamentoVM.IsEnsalamentos = _ensalamentoVM.Ensalamentos != null ? true : false;
            return RedirectToAction("Index");
        }

        public object ListarEnsalamentosPeriodicidades(bool periodicidade = false) {
            return _apiEnsalamento.ListarEnsalamentos(periodicidade).Result;
        }

        public List<string> ListarTurnos() {
            List<string> turnos = new List<string>();
            turnos.Add(EnumPeriodoEnsalamento.MATUTINO.GetDescription());
            turnos.Add(EnumPeriodoEnsalamento.VESPERTINO.GetDescription());
            turnos.Add(EnumPeriodoEnsalamento.NOTURNO.GetDescription());
            return turnos;
        }

        public IEnumerable<SelectListItem> ListarToastr() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumTipoToastr>());
        }

        public List<Sala> ListarSalas() {
            return _apiSala.ListarSalas().Result.ToList();
        }

        public List<Turma> ListarTurmas() {
            return _apiTurma.ListarTurmas().Result.ToList();
        }

        public void PopularEnsalamentos(EnsalamentoVM ensalamentoVM) {
            _ensalamentoVM = ensalamentoVM;
            _ensalamentoVM.Ensalamentos = ListarEnsalamentosPeriodicidades() as List<Ensalamento>;
            _ensalamentoVM.PeriodicidadeTabela = ListarEnsalamentosPeriodicidades(true) as List<List<bool>>;
            _ensalamentoVM.ColunaOcultaPeriodicidade = AtribuirStringPeriodicidade(_ensalamentoVM.Ensalamentos, _ensalamentoVM.PeriodicidadeTabela);
            _ensalamentoVM.IsEnsalamentos = _ensalamentoVM.Ensalamentos != null ? true : false;
        }

        public void PopularListas(EnsalamentoVM ensalamentoVM) {
            _ensalamentoVM = ensalamentoVM;
            _ensalamentoVM = AtribuirFiltros(ensalamentoVM) as EnsalamentoVM;
            _ensalamentoVM.Salas = ListarSalas();
            _ensalamentoVM.Turmas = ListarTurmas();
            _ensalamentoVM.Turnos = ListarTurnos();
        }

		/// <summary>
		/// Esse método funciona com uma lista simplificada (1 índice para cada ensalamento diferente) ou com uma lista de um mesmo ensalamento e uma matriz de periodicidade com tamanho [0][6] (count 1 e count 7)
		/// </summary>
		public List<string> AtribuirStringPeriodicidade(List<Ensalamento> ensalamentos, List<List<bool>> periodicidade) {
            
            if (ensalamentos == null || periodicidade == null)
                return null;

            List<string> colunaOculta = new List<string>();
            string indiceColuna = " ";
            int ultimoEnsalamentoNaLista = 0;
            for (int i = 0; i < ensalamentos.Count(); i++) {
                if (ensalamentos.Count == i || periodicidade.Count == i)
                    break;
                if (!ultimoEnsalamentoNaLista.Equals(ensalamentos[i].Id) || ensalamentos[i].Id == 0) {
                    if (periodicidade[i][0])
                        indiceColuna += EnumDiasDaSemana.DOMINGO.GetDescription() + " ";
                    if (periodicidade[i][1])
                        indiceColuna += EnumDiasDaSemana.SEGUNDA.GetDescription() + " ";
                    if (periodicidade[i][2])
                        indiceColuna += EnumDiasDaSemana.TERCA.GetDescription() + " ";
                    if (periodicidade[i][3])
                        indiceColuna += EnumDiasDaSemana.QUARTA.GetDescription() + " ";
                    if (periodicidade[i][4])
                        indiceColuna += EnumDiasDaSemana.QUINTA.GetDescription() + " ";
                    if (periodicidade[i][5])
                        indiceColuna += EnumDiasDaSemana.SEXTA.GetDescription() + " ";
                    if (periodicidade[i][6])
                        indiceColuna += EnumDiasDaSemana.SABADO.GetDescription() + " ";

                    colunaOculta.Add(indiceColuna);
                    ultimoEnsalamentoNaLista = ensalamentos[i].Id;
                    indiceColuna = " ";
                }
            }
            return colunaOculta;
        }

        public bool VerificarDatas(DateTime pesquisaInicio, DateTime pesquisaFim, DateTime inicioAtual, DateTime fimAtual) {
            for (DateTime dia = pesquisaInicio; dia <= pesquisaFim; dia = dia.AddDays(1)) {
                if (dia != DateTime.MinValue)
                    if ((inicioAtual == dia || fimAtual == dia) || (dia > inicioAtual && dia < fimAtual))
                        return true;
            }
            return false;
        }

        protected object UnificarEnsalamento(List<Ensalamento> entradaEnsalamentos, bool periodicidade = false) {
            List<Ensalamento> ensalamentos = entradaEnsalamentos;
            int ensalamento, proxEnsalamento, dia;
            if (!periodicidade) {
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
                return lista;
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
                            if (ensalamentos[ensalamento].Id.Equals(ensalamentos[ensalamento + dia].Id)) {
                                //Pega o dia da semana deste dia do ensalamento e atribui true no responsável por ele na periodicidade, que é o dia: [ensalamento][dia]
                                lista[lista.Count - 1][(int)ensalamentos[ensalamento + dia].Data.DayOfWeek] = true;
                            }
                            else
                                break;
                            //Verifica se esse ensalamento é o último
                            if (proxEnsalamento.Equals((ensalamentos.Count - 1)))
                                break;
                            proxEnsalamento++;
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
    }
}