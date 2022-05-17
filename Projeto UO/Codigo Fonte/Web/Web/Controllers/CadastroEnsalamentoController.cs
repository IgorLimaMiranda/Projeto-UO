using Backend.Enum;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Api;
using Web.Helper.EnumMensagens;
using Web.ViewModel;

namespace Web.Controllers {
    /// <summary>
    /// Ensalamento possui duas controllers
    /// <para>EnsalamentoController: Executa o que todos podem ver (além de métodos compartilhados com CadastroEnsalamento): GETs de Ensalamento</para>
    /// <para>CadastroEnsalamentoController: Executa o que apenas os administradores podem usar: Adicionar, editar e excluir</para>
    /// </summary>
    public class CadastroEnsalamentoController : EnsalamentoController {
        ApiHistoricoEnsalamento _apiHistorico;
        HistoricoEnsalamento _historico;

        public CadastroEnsalamentoController() {
            _apiHistorico = new ApiHistoricoEnsalamento();
            _historico = new HistoricoEnsalamento();
        }

        //GET: Cadastrar
        public ActionResult Cadastrar() {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_ensalamentoVM);
            if (Session["listaConflitos"] != null) {
                _ensalamentoVM = Session["listaConflitos"] as EnsalamentoVM;
                Session["listaConflitos"] = null;
            }
            return PartialView(_ensalamentoVM);
        }

        [HttpPost]
        [ActionFilter]
        public async Task<ActionResult> CadastrarEnsalamento([Bind(Include = "Ensalamento, Periodicidade, ChaveTurma, IntervaloEnsalamento")] EnsalamentoVM ensalamentoVM) {
            List<string> mensagem;
            bool response;
            var ChaveTurma = ensalamentoVM.ChaveTurma.Split('/');
            var IntervaloEnsalamento = ensalamentoVM.IntervaloEnsalamento.Split('-');
            ensalamentoVM.Ensalamento.IdTurma = Convert.ToInt32(ChaveTurma[0]);
            ensalamentoVM.Ensalamento.AnoTurma = Convert.ToInt32(ChaveTurma[1]);
            ensalamentoVM.Ensalamento.Data = DateTime.Parse(IntervaloEnsalamento[0]);
            ensalamentoVM.Ensalamento.DataFim = DateTime.Parse(IntervaloEnsalamento[1]);
            //Lista de novos ensalamentos para adicionar
            _ensalamentoVM.Ensalamentos = DesmembrarEnsalamento(ensalamentoVM.Ensalamento, ensalamentoVM.Periodicidade);
            if ((_ensalamentoVM.Ensalamentos != null) && _ensalamentoVM.Ensalamentos.Count() == 0) {
                mensagem = new List<string>();
                mensagem.Add(EnumMensagensEnsalamento.ERRO_PERIODICIDADE.GetDescription());
                mensagem.Add((EnumTipoToastr.ERROR).ToString());
                TempData["mensagemEnsalamento"] = mensagem;
                return RedirectToAction("Index", "Ensalamento");
            }
            List<Ensalamento> conflitos = _apiEnsalamento.VerificarEnsalamento(_ensalamentoVM.Ensalamentos).Result;
            if ((conflitos != null) && conflitos.Count > 0) {
                ensalamentoVM.PeriodicidadeTabela = UnificarEnsalamento(ensalamentoVM.Ensalamentos, true) as List<List<bool>>;
                PopularListas(ensalamentoVM);
                ensalamentoVM.NomeTabela = "Lista de Conflitos";
                ensalamentoVM.EstiloNomeTabela = "color:red";
                _ensalamentoVM.IsEnsalamentos = _ensalamentoVM.Ensalamentos != null ? true : false;
                ensalamentoVM.Conflitos = true;
                var ensalamentos = _apiEnsalamento.ListarEnsalamentos().Result as List<Ensalamento>;
                //periodicidade retorna count 2 quando tem que retornar count 1
                var periodicidade = _apiEnsalamento.ListarEnsalamentos(true).Result as List<List<bool>>;
                ensalamentoVM.PeriodicidadeTabela = new List<List<bool>>();
                foreach (var itemRetorno in conflitos) {
                    int i = 0;
                    foreach (var ensalBanco in ensalamentos) {
                        if (ensalBanco.Id == itemRetorno.Id) {
                            ensalamentoVM.Ensalamentos.Add(itemRetorno);
                            ensalamentoVM.PeriodicidadeTabela.Add(periodicidade[i]);
                        }
                        i++;
                    }
                }

                Session["listaConflitos"] = ensalamentoVM;
                return RedirectToAction("Index", "Ensalamento");
            }
            else {
                response = await _apiEnsalamento.CadastrarEnsalamento(_ensalamentoVM.Ensalamentos);
            }
            if (response)
                await AdicionarHistorico(EnumAcaoHistoricoEnsalamento.ADICIONADO.GetDescription(), _ensalamentoVM.Ensalamentos.First());

            mensagem = new List<string>();
            if (response) {
                mensagem.Add(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription());
                mensagem.Add((EnumTipoToastr.SUCCESS).ToString());
            }
            else {
                mensagem.Add(EnumMensagensCrud.ERRO_CADASTRO.GetDescription());
                mensagem.Add((EnumTipoToastr.ERROR).ToString());
            }
            TempData["mensagemEnsalamento"] = mensagem;
            return RedirectToAction("Index", "Ensalamento");
        }

        //GET: Excluir
        public ActionResult ExcluirEnsalamento(Ensalamento ensalamento) {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            _ensalamentoVM.Ensalamento = ensalamento;
            PopularEnsalamentos(_ensalamentoVM);
            PopularListas(_ensalamentoVM);
            return PartialView(_ensalamentoVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir([Bind(Include = "Ensalamento")] EnsalamentoVM ensalamentoVM) {
            bool response = await AdicionarHistorico(EnumAcaoHistoricoEnsalamento.EXCLUIDO.GetDescription(), ensalamentoVM.Ensalamento), deletar = false;
            if (response) {
                deletar = await _apiEnsalamento.ExcluirEnsalamento(ensalamentoVM.Ensalamento.Id);
            }
            PopularEnsalamentos(_ensalamentoVM);
            PopularListas(_ensalamentoVM);
            List<string> mensagem = new List<string>();
            if (deletar) {
                mensagem.Add(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription());
                mensagem.Add((EnumTipoToastr.SUCCESS).ToString());
            }
            else {
                mensagem.Add(EnumMensagensEnsalamento.ERRO_EXCLUIR_ENSALAMENTO.GetDescription());
                mensagem.Add((EnumTipoToastr.ERROR).ToString());
            }
            TempData["mensagemEnsalamento"] = mensagem;
            return RedirectToAction("Index", "Ensalamento");
        }

        //GET: Editar Ensalamento
        public ActionResult EditarEnsalamento(Ensalamento ensalamento) {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            var banco = _apiEnsalamento.AtribuirIncludes(null, ensalamento).Result;
            if ((banco != null) && banco.Any())
                _ensalamentoVM.Ensalamento = banco[0];
            List<Ensalamento> ensalamentos = _apiEnsalamento.ListarEnsalamentos().Result as List<Ensalamento>;
            List<List<bool>> periodicidade = _apiEnsalamento.ListarEnsalamentos(true).Result as List<List<bool>>;
            int i = 0;
            foreach (var itemBanco in ensalamentos) {
                if (itemBanco.Id == _ensalamentoVM.Ensalamento.Id) {
                    _ensalamentoVM.Periodicidade = periodicidade[i];
                    break;
                }
                i++;
            }
            PopularListas(_ensalamentoVM);
            _ensalamentoVM.IntervaloEnsalamento = $"{_ensalamentoVM.Ensalamento.Data.ToString("dd/MM/yyyy")} - {_ensalamentoVM.Ensalamento.DataFim.ToString("dd/MM/yyyy")}";
            _ensalamentoVM.ChaveTurma = $"{_ensalamentoVM.Ensalamento.IdTurma}/{_ensalamentoVM.Ensalamento.AnoTurma}";
            if ((_ensalamentoVM.Ensalamentos != null) && _ensalamentoVM.Ensalamentos.Count() > 0)
                _ensalamentoVM.IsEnsalamentos = true;
            return PartialView(_ensalamentoVM);
        }

        [HttpPost]
        public async Task<ActionResult> Validar(List<List<string>> ensalamentos, int idOriginal) {
            List<List<string>> retorno = new List<List<string>>();
            retorno = new List<List<string>>();
            if (ensalamentos == null || ensalamentos.Count == 0) {
                retorno.First().Add("false");
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            List<Ensalamento> novosEnsalamentos = await AtribuirEnsalamentosPorString(ensalamentos) as List<Ensalamento>;
            List<List<bool>> novasPeriodicidades = await AtribuirEnsalamentosPorString(ensalamentos, true) as List<List<bool>>;
            for (int i = 0; i < novosEnsalamentos.Count(); i++) {
                var item = DesmembrarEnsalamento(novosEnsalamentos[i], novasPeriodicidades[i]);
                retorno.Add(new List<string>(){
                    $"{item.First().Data.ToString("dd/MM/yyyy")} - {item.Last().Data.ToString("dd/MM/yyyy")}",
                ListarTurnos()[novosEnsalamentos[i].Periodo - 1],
                $"{AtribuirStringPeriodicidade(item, new List<List<bool>> { novasPeriodicidades[i] }).First()}",
                $"{ensalamentos[i][3]}",
                $"{ensalamentos[i][4]}",
                $"{ensalamentos[i][5]}"});
            }
            List<Ensalamento> desmembrados = new List<Ensalamento>();
            for (int i = 0; i < novosEnsalamentos.Count; ++i)
                desmembrados.AddRange(DesmembrarEnsalamento(novosEnsalamentos[i], novasPeriodicidades[i]));
            List<Ensalamento> parciaisOrdenados = desmembrados.OrderBy(ordenar => ordenar.Data).ToList();
            List<Ensalamento> originais = await _apiEnsalamento.BuscarEnsalamentoDesmembrado(new Ensalamento { Id = idOriginal });
            List<bool> periodicidadeOriginais = AtribuirPeriodicidade(originais);
            List<List<Ensalamento>> originalDesmembrado = new List<List<Ensalamento>>();
            originalDesmembrado.Add(new List<Ensalamento>());
            foreach (Ensalamento ensalamentoAtual in originais) {
                if (!(parciaisOrdenados.Exists(procurar => procurar.Data == ensalamentoAtual.Data))) {
                    originalDesmembrado.Last().Add(ensalamentoAtual);
                }
                else if (originalDesmembrado.Last().Any())
                    originalDesmembrado.Add(new List<Ensalamento>());
            }
            foreach (List<Ensalamento> lista in originalDesmembrado) {
                if (lista.Any()) {
                    retorno.Add(new List<string>(){ $"{lista.First().Data.ToString("dd/MM/yyyy")} - {lista.Last().Data.ToString("dd/MM/yyyy")}",
                ListarTurnos()[lista.First().Periodo - 1],
                $"{AtribuirStringPeriodicidade(originais, new List<List<bool>> { AtribuirPeriodicidade(lista) }).First()}",
                $"{lista.First().Sala.Numero}",
                $"{lista.First().IdTurma}/{lista.First().AnoTurma}",
                lista.First().Ocupante});
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(List<List<string>> ensalamentos, int idOriginal) {
            bool IsEnsalamentoAdicionado = false, IsEnsalamentoDeletado = false, IsHistoricoAdicionado = false;
            if (ensalamentos != null) {
                if (ensalamentos.Count() > 0 && ((await _apiEnsalamento.ListarEnsalamentos(false)) as List<Ensalamento>).Count() > 0) {
                    List<Ensalamento> ensalamentosParciais = await AtribuirEnsalamentosPorString(ensalamentos, false) as List<Ensalamento>;
                    List<List<bool>> periodicidadesParciais = await AtribuirEnsalamentosPorString(ensalamentos, true) as List<List<bool>>;

                    Ensalamento ensalamento = new Ensalamento { Id = idOriginal };
                    ensalamento = _apiEnsalamento.BuscarEnsalamentoDesmembrado(ensalamento).Result.First();
                    IsHistoricoAdicionado = await AdicionarHistorico(EnumAcaoHistoricoEnsalamento.EDITADO.GetDescription(), ensalamento);
                    if(IsHistoricoAdicionado)
                        IsEnsalamentoDeletado = await _apiEnsalamento.ExcluirEnsalamento(idOriginal);
                    if (IsEnsalamentoDeletado) {
                        for (int i = 0; i < ensalamentosParciais.Count(); i++) {
                            List<Ensalamento> ensalamentoAtual = DesmembrarEnsalamento(ensalamentosParciais[i], periodicidadesParciais[i]);
                            foreach (var item in ensalamentoAtual) {
                                item.DataFim = ensalamentoAtual.Last().Data;
                            }
                            IsEnsalamentoAdicionado = await _apiEnsalamento.CadastrarEnsalamento(ensalamentoAtual);
                            ensalamentoAtual = null;
                        }
                    }
                }
            }
            if (IsEnsalamentoAdicionado) {
                List<string> mensagem = new List<string>();
                mensagem.Add(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription());
                mensagem.Add((EnumTipoToastr.SUCCESS).ToString());
                TempData["mensagemEnsalamento"] = mensagem;
            }
            return Json(IsEnsalamentoAdicionado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> VerificarParcial(Ensalamento ensalamento, string IntervaloEnsalamento, List<bool> Periodicidade, string ChaveTurma, int IdOriginal, List<List<string>> parciais) {
            if ((ensalamento != null && ensalamento.IdSala != 0) && IntervaloEnsalamento != null && Periodicidade != null && ChaveTurma != null) {
                string[] chaveTurma = { string.Empty, string.Empty }, intervaloEnsalamento = { string.Empty, string.Empty };
                string retorno = "";
                chaveTurma = ChaveTurma.Split('/');
                intervaloEnsalamento = IntervaloEnsalamento.Split('-');
                ensalamento.IdTurma = Convert.ToInt32(chaveTurma[0]);
                ensalamento.AnoTurma = Convert.ToInt32(chaveTurma[1]);
                ensalamento.Data = DateTime.Parse(intervaloEnsalamento[0]);
                ensalamento.DataFim = DateTime.Parse(intervaloEnsalamento[1]);
                List<Ensalamento> ensalamentos = new List<Ensalamento>();
                List<Ensalamento> ensalamentosParciais = new List<Ensalamento>();
                if (parciais != null && parciais.Any()) {
                    ensalamentosParciais = await AtribuirEnsalamentosPorString(parciais, false) as List<Ensalamento>;
                    List<List<bool>> periodicidadesParciais = await AtribuirEnsalamentosPorString(parciais, true) as List<List<bool>>;
                    for (int i = 0; i < ensalamentosParciais.Count(); i++) {
                        ensalamentos.AddRange(DesmembrarEnsalamento(ensalamentosParciais[i], periodicidadesParciais[i]));
                    }
                }
                List<Ensalamento> novoEnsalamento = DesmembrarEnsalamento(ensalamento, Periodicidade);
                ensalamentos.AddRange(novoEnsalamento);
                List<Ensalamento> conflitos = _apiEnsalamento.VerificarEnsalamento(novoEnsalamento).Result;

                foreach (var conflito in conflitos) {
                    if (conflito.Id != IdOriginal) {
                        if (retorno.Equals(""))
                            retorno = "Conflitos com:\n";
                        retorno += $"Intervalo: {conflito.Data.ToString("dd/MM/yyyy")} - {conflito.DataFim.Date.ToString("dd/MM/yyyy")} | Turno: {ListarTurnos()[(conflito.Periodo - 1)]} | Ocupante: {conflito.Ocupante}\n";
                    }
                }
                //verifica o que está na tabela de edição
                List<List<Ensalamento>> parciaisJaExistentesDesmembrados = new List<List<Ensalamento>>();
                if (parciais != null && parciais.Any()) {
                    List<Ensalamento> parciaisJaExistentes = await AtribuirEnsalamentosPorString(parciais, false) as List<Ensalamento>;
                    List<List<bool>> periodicidadesJaExistentes = await AtribuirEnsalamentosPorString(parciais, true) as List<List<bool>>;
                    for (int i = 0; i < parciaisJaExistentes.Count(); i++) {
                        parciaisJaExistentesDesmembrados.Add(new List<Ensalamento>());
                        parciaisJaExistentesDesmembrados[i] = new List<Ensalamento>();
                        parciaisJaExistentesDesmembrados[i].AddRange(DesmembrarEnsalamento(parciaisJaExistentes[i], periodicidadesJaExistentes[i]));
                    }
                }
                Ensalamento ultimoEnsalamentoNoRetorno = new Ensalamento();

                //i = Ensalamento
                //j = Dias do ensalamento
                for (int i = 0; i < parciaisJaExistentesDesmembrados.Count(); i++) {
                    for (int j = 0; j < parciaisJaExistentesDesmembrados[i].Count(); j++) {
                        if (novoEnsalamento.Exists(procurar => procurar.Data == parciaisJaExistentesDesmembrados[i][j].Data)) {
                            if (parciaisJaExistentesDesmembrados[i].First().Periodo == novoEnsalamento.First().Periodo) {
                                if (retorno.Equals(""))
                                    retorno = "Conflitos com:\n";
                                retorno += $"Intervalo: {parciaisJaExistentesDesmembrados[i].First().Data.ToString("dd/MM/yyyy")} - {parciaisJaExistentesDesmembrados[i].First().DataFim.Date.ToString("dd/MM/yyyy")} | Turno: {ListarTurnos()[(parciaisJaExistentesDesmembrados[i].First().Periodo) - 1]} | Ocupante: {parciaisJaExistentesDesmembrados[i].First().Ocupante} - Nesta mesma tabela!\n";
                                break;
                            }
                        }
                    }
                }

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        [HttpPost]
        public async Task<ActionResult> ValidarEnsalamento(List<List<string>> parciais) {
            string retorno = string.Empty;
            List<Ensalamento> ensalamentos = await AtribuirEnsalamentosPorString(parciais) as List<Ensalamento>;
            List<List<bool>> periodicidades = await AtribuirEnsalamentosPorString(parciais, true) as List<List<bool>>;
            List<Ensalamento> ensalamentosDesmembrados = new List<Ensalamento>();
            for (int i = 0; i < ensalamentos.Count(); i++)
                ensalamentosDesmembrados.AddRange(DesmembrarEnsalamento(ensalamentos[i], periodicidades[i]));


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Atribui a periodicidade de uma lista DE UM MESMO ensalamento
        /// </summary>
        List<bool> AtribuirPeriodicidade(List<Ensalamento> ensalamentos) {
            List<bool> periodicidade = new List<bool> { false, false, false, false, false, false, false };
            int i = 0;
            foreach (var ensalamento in ensalamentos) {
                periodicidade[(int)(ensalamento.Data.DayOfWeek)] = true;
                if (i == 6)
                    break;
            }
            return periodicidade;
        }

        async Task<object> AtribuirEnsalamentosPorString(List<List<string>> ensalamentos, bool periodicidade = false) {
            List<List<bool>> periodicidadesParciais = new List<List<bool>>();
            List<Ensalamento> ensalamentosParciais = new List<Ensalamento>();
            for (int i = 0; i < ensalamentos.Count(); i++) {
                int turno = 0;
                for (int j = 0; j < 3; j++) {
                    if (ListarTurnos()[j].Equals(ensalamentos[i][1])) {
                        turno = (j + 1);
                        break;
                    }
                }
                periodicidadesParciais.Add(AtribuirPeriodicidadePorString(ensalamentos[i][2]));
                ensalamentosParciais.Add(new Ensalamento {
                    Data = Convert.ToDateTime(ensalamentos[i][0].Split('-')[0]),
                    DataFim = Convert.ToDateTime(ensalamentos[i][0].Split('-')[1]),
                    Periodo = turno,
                    IdSala = (await _apiSala.BuscarSala(0, Convert.ToInt32(ensalamentos[i][3]))).Id,
                    IdTurma = Convert.ToInt32(ensalamentos[i][4].Split('/')[0]),
                    AnoTurma = Convert.ToInt32(ensalamentos[i][4].Split('/')[1]),
                    Ocupante = ensalamentos[i][5] != "" ? ensalamentos[i][5] : null
                });
            }
            if (periodicidade)
                return periodicidadesParciais;
            return ensalamentosParciais;
        }

        List<bool> AtribuirPeriodicidadePorString(string entrada) {
            List<bool> periodicidade = new List<bool> { false, false, false, false, false, false, false };
            if (entrada.Contains("Dom"))
                periodicidade[0] = true;
            if (entrada.Contains("Seg"))
                periodicidade[1] = true;
            if (entrada.Contains("Ter"))
                periodicidade[2] = true;
            if (entrada.Contains("Qua"))
                periodicidade[3] = true;
            if (entrada.Contains("Qui"))
                periodicidade[4] = true;
            if (entrada.Contains("Sex"))
                periodicidade[5] = true;
            if (entrada.Contains("Sab"))
                periodicidade[6] = true;
            return periodicidade;
        }

        //Pega um Emsalamento e sua periodicidade e o transforma em uma lista
        List<Ensalamento> DesmembrarEnsalamento(Ensalamento ensalamento, List<bool> periodicidade) {
            List<Ensalamento> listaRetorno = new List<Ensalamento>();
            for (DateTime dia = ensalamento.Data; dia <= ensalamento.DataFim; dia = dia.AddDays(1)) {
                if (periodicidade[((int)(dia.DayOfWeek))]) {
                    listaRetorno.Add(new Ensalamento {
                        AnoTurma = ensalamento.AnoTurma,
                        Data = dia,
                        DataFim = ensalamento.DataFim,
                        IdSala = ensalamento.IdSala,
                        IdTurma = ensalamento.IdTurma,
                        Ocupante = ensalamento.Ocupante,
                        Periodo = ensalamento.Periodo
                    });
                }
            }
            return listaRetorno;
        }

        async Task<bool> AdicionarHistorico(string acao, Ensalamento ensalamento) {
            EnsalamentoVM ensalamentoVM = new EnsalamentoVM();
            ensalamentoVM.Ensalamentos.Add(ensalamento);
            ensalamentoVM.Ensalamento = (_apiEnsalamento.AtribuirIncludes(ensalamentoVM.Ensalamentos).Result).First();
            ensalamentoVM.Ensalamentos = _apiEnsalamento.BuscarEnsalamentoDesmembrado(ensalamentoVM.Ensalamento).Result;
            ensalamentoVM.PeriodicidadeTabela = (UnificarEnsalamento(ensalamentoVM.Ensalamentos, true) as List<List<bool>>);
            _historico.Acao = acao;
            _historico.DataInicio = ensalamentoVM.Ensalamento.Data;
            _historico.DataFim = ensalamentoVM.Ensalamento.DataFim;
            _historico.DataHora = DateTime.Now;
            _historico.Periodicidade = AtribuirStringPeriodicidade(ensalamentoVM.Ensalamentos, ensalamentoVM.PeriodicidadeTabela)[0];
            if (ensalamentoVM.Ensalamento.Ocupante != null)
                _historico.Ocupante = ensalamentoVM.Ensalamento.Ocupante;
            _historico.Usuario = (Session["permissao"] as PermissaoVM).UsuarioLogado.Nome;
            _historico.Turno = ListarTurnos()[ensalamentoVM.Ensalamento.Periodo - 1];
            _historico.Turma = $"{ensalamentoVM.Ensalamento.Turma.Curso.Descricao} {ensalamentoVM.Ensalamento.Turma.Id} / {ensalamentoVM.Ensalamento.Turma.Ano}";
            _historico.Sala = $"{ensalamentoVM.Ensalamento.Sala.Numero.ToString()} | {ensalamentoVM.Ensalamento.Sala.Andar} | {ensalamentoVM.Ensalamento.Sala.Descricao} | {ensalamentoVM.Ensalamento.Sala.Tipo}";
            return await _apiHistorico.CadastrarHistoricoEnsalamento(_historico);
        }
    }
}