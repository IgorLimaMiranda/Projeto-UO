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
    public class OcorrenciaController : PermissaoController {
        private ApiChamado _apiChamado;
        private ApiAtendimento _apiAtendimento;
        private ApiIncidenteRecorrente _apiIncidenteRecorrente;
        private ApiSetor _apiSetor;
        private ApiUsuario _apiUsuario;
        private OcorrenciaVM _ocorrenciaVM;

        public OcorrenciaController() {
            _apiChamado = new ApiChamado();
            _apiAtendimento = new ApiAtendimento();
            _apiIncidenteRecorrente = new ApiIncidenteRecorrente();
            _apiSetor = new ApiSetor();
            _apiUsuario = new ApiUsuario();
            _ocorrenciaVM = new OcorrenciaVM();
        }

        // GET: Ocorrência
        [ActionFilter]
        public ActionResult Index() {
            if (TempData["pesquisa"] != null) {
                _ocorrenciaVM = TempData["pesquisa"] as OcorrenciaVM;
                if (TempData["mensagemOcorrencia"] != null) {
                    var toastr = TempData["mensagemOcorrencia"] as List<string>;
                    if (toastr[1].Equals((EnumTipoToastr.WARNING).ToString()))
                        return View(_ocorrenciaVM).Mensagem(toastr[0], EnumTipoToastr.WARNING);
                }
                return View(_ocorrenciaVM);
            }
            PopularListas(_ocorrenciaVM);
            return View(_ocorrenciaVM);
        }

        // GET: Cadastrar
        public ActionResult Cadastrar() {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_ocorrenciaVM);
            return PartialView(_ocorrenciaVM);
        }

        // POST: Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Chamado")] OcorrenciaVM ocorrenciaVM) {
            ocorrenciaVM.Chamado.CpfUsuario = (Session["permissao"] as PermissaoVM).UsuarioLogado.Cpf;
            ocorrenciaVM.Chamado.Imagem = ocorrenciaVM.Chamado.Imagem ?? EnumOcorrencia.URI_IMAGEM_PADRAO.GetDescription();
            bool response = await _apiChamado.CadastrarChamado(ocorrenciaVM.Chamado);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_CADASTRO.GetDescription(), EnumTipoToastr.SUCCESS);
            }
            else {
                PopularListas(ocorrenciaVM);
                return View("Index", _ocorrenciaVM).Mensagem(EnumMensagensCrud.ERRO_CADASTRO.GetDescription(), EnumTipoToastr.ERROR);
            }
        }

        // GET: Editar
        public ActionResult EditarOcorrencia(Chamado chamado) {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_ocorrenciaVM);
            _ocorrenciaVM.Chamado = _apiChamado.BuscarChamado(chamado.IdChamado).Result;
            _ocorrenciaVM.Atendimento = ListarAtendimentosDeChamado(_ocorrenciaVM.Chamado.IdChamado).First();
            _ocorrenciaVM.Atendimento.Detalhes = string.Empty;
            _ocorrenciaVM.PopularRadioButtonsStatus(_ocorrenciaVM.Atendimento);
            PopularDesabilitarCamposEdicao();
            return PartialView(_ocorrenciaVM);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Editar(OcorrenciaVM ocorrenciaVM) {
            // ReSharper disable once PossibleNullReferenceException
            Usuario usuarioAtual = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            Atendimento ultimoAtendimento = ListarAtendimentosDeChamado(ocorrenciaVM.Chamado.IdChamado).First();

            ocorrenciaVM.Chamado.Imagem = ocorrenciaVM.Chamado.Imagem ?? EnumOcorrencia.URI_IMAGEM_PADRAO.GetDescription();

            bool isAdministrativo = usuarioAtual.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription());
            bool isCoordenador = usuarioAtual.TipoUsuario.Equals(EnumTipoUsuario.COORDENADOR.GetDescription());

            // TODO ARRUMAR AQUI PARA VERIFICAR SE O ADMINISTRADOR ESTA DANDO UPDATE NA OCORRENCIA DELE QUE ESTÁ ABERTA
            bool response = false;
            if (isAdministrativo || isCoordenador) {
                if (!ocorrenciaVM.Atendimento.Detalhes.IsNullOrEmpty()) {
                    Atendimento atendimento = new Atendimento {
                        Detalhes = ocorrenciaVM.Atendimento.Detalhes,
                        DataAtendimento = DateTime.Now,
                        Status = ocorrenciaVM.StatusSelecionado,
                        CpfTecnico = usuarioAtual.Cpf,
                        IdChamado = ocorrenciaVM.Chamado.IdChamado
                    };

                    response = await _apiAtendimento.CadastrarAtendimento(atendimento);
                    if (!ocorrenciaVM.DesabilitarCamposEdicao.IsNullOrEmpty()) {
                        if (response) {
                            return RedirectToAction("Index").Mensagem(EnumMensagensOcorrencia.SUCESSO_STATUS.GetDescription(), EnumTipoToastr.SUCCESS);
                        }

                        PopularListas(ocorrenciaVM);
                        return View("Index", _ocorrenciaVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
                    }
                }
            }

            response = await _apiChamado.AlterarChamado(ocorrenciaVM.Chamado);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EDICAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }

            PopularListas(ocorrenciaVM);
            return View("Index", _ocorrenciaVM).Mensagem(EnumMensagensCrud.ERRO_EDICAO.GetDescription(), EnumTipoToastr.ERROR);
        }

        // GET: Excluir
        public ActionResult ExcluirOcorrencia(Chamado chamado) {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_ocorrenciaVM);
            _ocorrenciaVM.Chamado = _apiChamado.BuscarChamado(chamado.IdChamado).Result;
            return PartialView(_ocorrenciaVM);
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionFilter]
        public async Task<ActionResult> Excluir([Bind(Include = "Chamado")] OcorrenciaVM ocorrenciaVM) {
            bool response = await _apiAtendimento.CadastrarAtendimento(new Atendimento {
                Detalhes = EnumDetalhesAtendimentoOcorrencia.EXCLUSAO.GetDescription(),
                DataAtendimento = DateTime.Now,
                Status = EnumStatusChamado.EXCLUIDA.GetDescription(),
                CpfTecnico = (Session["permissao"] as PermissaoVM).UsuarioLogado.Cpf,
                IdChamado = ocorrenciaVM.Chamado.IdChamado
            });
            PopularListas(ocorrenciaVM);
            if (response) {
                return RedirectToAction("Index").Mensagem(EnumMensagensCrud.SUCESSO_EXCLUSAO.GetDescription(), EnumTipoToastr.SUCCESS);
            }

            return View("Index", _ocorrenciaVM).Mensagem(EnumMensagensCrud.ERRO_EXCLUSAO.GetDescription(), EnumTipoToastr.ERROR);
        }

        // GET: VisualizarOcorrencia
        public async Task<ActionResult> VisualizarOcorrencia(Chamado chamado) {
            if (VerificarSession())
                return PartialView("~/Views/Shared/Error.cshtml");
            PopularListas(_ocorrenciaVM);
            _ocorrenciaVM.Chamado = await _apiChamado.BuscarChamado(chamado.IdChamado);
            _ocorrenciaVM.Atendimentos = ListarAtendimentosDeChamado(chamado.IdChamado);
            _ocorrenciaVM.IconesAtendimentos = OcorrenciaHelper.DefinirIconesAtendimentos(_ocorrenciaVM.Atendimentos);
            return PartialView(_ocorrenciaVM);
        }

        // GET: PesquisarOcorrencia
        [ActionFilter]
        public ActionResult Pesquisar([Bind(Include = "IntervaloOcorrencia, Atendimento, Chamado, TipoDePesquisa")] OcorrenciaVM ocorrenciaVM) {
            if ((ocorrenciaVM.IntervaloOcorrencia == null) && (ocorrenciaVM.Atendimento.Status == null) && (ocorrenciaVM.Chamado.IdSetor == null) && (ocorrenciaVM.Chamado.CpfUsuario == null)) {
                PopularListas(_ocorrenciaVM);
                //PopularEnsalamentos(_ensalamentoVM);
                _ocorrenciaVM.IsPesquisa = true;
                TempData["pesquisa"] = _ocorrenciaVM;
                List<string> mensagem = new List<string>();
                mensagem.Add(EnumMensagensOcorrencia.ATENCAO_NENHUM_CAMPO_PREENCHIDO.GetDescription());
                mensagem.Add((EnumTipoToastr.WARNING).ToString());
                TempData["mensagemOcorrencia"] = mensagem;
                return RedirectToAction("Index");
            }
            string[] intervaloOcorrencia = { string.Empty, string.Empty };
            DateTime inicioPesquisa = DateTime.MinValue, fimPesquisa = DateTime.MinValue;
            if (ocorrenciaVM.IntervaloOcorrencia != null) {
                intervaloOcorrencia = ocorrenciaVM.IntervaloOcorrencia.Split('-');
                inicioPesquisa = Convert.ToDateTime(intervaloOcorrencia[0]);
                fimPesquisa = Convert.ToDateTime(intervaloOcorrencia[1]);
            }
            PopularListas(_ocorrenciaVM);
            var chamadosBanco = ListarChamados();
            List<Chamado> retornoChamado = new List<Chamado>();
            List<Chamado> Lista1 = new List<Chamado>();
            List<Chamado> Lista2 = new List<Chamado>();
            List<Chamado> Lista3 = new List<Chamado>();
            List<Chamado> Lista4 = new List<Chamado>();
            int acertos;
            foreach (var chamado in chamadosBanco) {
                acertos = 0;
                if ((ocorrenciaVM.Atendimento.Status != null) && ocorrenciaVM.Atendimento.Status.Equals(_ocorrenciaVM.StatusChamado[chamado.IdChamado]))
                    acertos++;
                if ((ocorrenciaVM.Chamado.IdSetor != null) && ocorrenciaVM.Chamado.IdSetor.Equals(chamado.IdSetor))
                    acertos++;
                if ((ocorrenciaVM.Chamado.CpfUsuario != null) && ocorrenciaVM.Chamado.CpfUsuario.Equals(chamado.CpfUsuario))
                    acertos++;
                if (inicioPesquisa != DateTime.MinValue && fimPesquisa != DateTime.MinValue && ocorrenciaVM.Chamado.HoraData.Date != DateTime.MinValue)
                    if (VerificarDatas(inicioPesquisa, fimPesquisa, ocorrenciaVM.Chamado.HoraData.Date))
                        acertos++;

                switch (acertos) {
                    case 1:
                        Lista1.Add(chamado);
                        break;
                    case 2:
                        Lista2.Add(chamado);
                        break;
                    case 3:
                        Lista3.Add(chamado);
                        break;
                    case 4:
                        Lista4.Add(chamado);
                        break;
                }
            }
            if (ocorrenciaVM.TipoDePesquisa.Equals(EnumPesquisa.ACERTOS.GetDescription())) {
                if ((Lista4 != null) && Lista4.Count() > 0)
                    retornoChamado = Lista4;
                else if ((Lista3 != null) && Lista3.Count() > 0)
                    retornoChamado = Lista3;
                else if ((Lista2 != null) && Lista2.Count() > 0)
                    retornoChamado = Lista2;
                else if ((Lista1 != null) && Lista1.Count() > 0)
                    retornoChamado = Lista1;
            }
            else {
                if ((Lista4 != null) && Lista4.Count() > 0)
                    retornoChamado.AddRange(Lista4);
                if ((Lista3 != null) && Lista3.Count() > 0)
                    retornoChamado.AddRange(Lista3);
                if ((Lista2 != null) && Lista2.Count() > 0)
                    retornoChamado.AddRange(Lista2);
                if ((Lista1 != null) && Lista1.Count() > 0)
                    retornoChamado.AddRange(Lista1);
            }
            _ocorrenciaVM.Chamados = retornoChamado;
            _ocorrenciaVM.IsPesquisa = true;
            TempData["pesquisa"] = _ocorrenciaVM;
            return RedirectToAction("Index");
        }

        public List<Chamado> ListarChamados() {
            Usuario usuarioAtual = (Session["permissao"] as PermissaoVM).UsuarioLogado;
            return _apiChamado.ListarChamado(usuarioAtual).Result.ToList();
        }

        public List<Atendimento> ListarAtendimentosDeChamado(int id) {
            return _apiAtendimento.ListarAtendimentosDeChamado(id).Result;
        }

        public List<IncidenteRecorrente> ListarIncidentesRecorrentes() {
            return _apiIncidenteRecorrente.ListarIncidenteRecorrente().Result.ToList();
        }

        public List<Setor> ListarSetores() {
            return _apiSetor.ListarSetoresSalas().Result.ToList();
        }

        public List<Usuario> ListarUsuarios() {
            return _apiUsuario.ListarUsuarios().Result.ToList();
        }

        public void PopularListas(OcorrenciaVM ocorrenciaVM) {
            _ocorrenciaVM = ocorrenciaVM;
            _ocorrenciaVM = AtribuirFiltros(_ocorrenciaVM) as OcorrenciaVM;
            _ocorrenciaVM.Chamados = ListarChamados();
            _ocorrenciaVM.IncidentesRecorrentes = ListarIncidentesRecorrentes();
            _ocorrenciaVM.Setores = ListarSetores();
            _ocorrenciaVM.Usuarios = ListarUsuarios();
            if (!_ocorrenciaVM.Chamados.IsNullOrEmpty()) {
                ConfigurarStatusEPermissoesDeChamado();
            }
        }

        public void ConfigurarStatusEPermissoesDeChamado() {
            _ocorrenciaVM.StatusChamado = new Dictionary<int, string>();
            _ocorrenciaVM.PermissaoBotaoExcluir = new Dictionary<int, string>();
            _ocorrenciaVM.PermissaoBotaoEditar = new Dictionary<int, string>();
            foreach (Chamado chamado in _ocorrenciaVM.Chamados) {
                List<Atendimento> atendimentos = ListarAtendimentosDeChamado(chamado.IdChamado);
                Atendimento atendimento = atendimentos.First();
                Usuario usuarioLogado = _ocorrenciaVM.UsuarioLogado;

                _ocorrenciaVM.StatusChamado.Add(chamado.IdChamado, atendimento.Status);

                if (chamado.CpfUsuario.Equals(usuarioLogado.Cpf)) {
                    ConfigurarOcorrenciasDoUsuarioAtual(chamado.IdChamado, atendimento, usuarioLogado);
                }
                else {
                    ConfigurarPermissaoAcaoEditar(chamado.IdChamado, atendimento, usuarioLogado.TipoUsuario);
                    ConfigurarPermissaoAcaoExcluir(chamado.IdChamado, atendimento);
                }

                _ocorrenciaVM.ConfigurarLayoutPermissoesBotoes(chamado.IdChamado);
            }
        }

        private void ConfigurarPermissaoAcaoEditar(int idChamado, Atendimento atendimento, string tipoUsuario) {
            if (atendimento.Status.Equals(EnumStatusChamado.EXCLUIDA.GetDescription()) || atendimento.Status.Equals(EnumStatusChamado.CANCELADA.GetDescription()) || tipoUsuario.Equals(EnumTipoUsuario.GERENTE.GetDescription())) {
                if (!_ocorrenciaVM.PermissaoBotaoEditar.ContainsKey(idChamado))
                    _ocorrenciaVM.PermissaoBotaoEditar.Add(idChamado, EnumOcorrencia.DISABLED.GetDescription());
                else
                    _ocorrenciaVM.PermissaoBotaoEditar[idChamado] = EnumOcorrencia.DISABLED.GetDescription();
            }
            else {
                if (!_ocorrenciaVM.PermissaoBotaoEditar.ContainsKey(idChamado))
                    _ocorrenciaVM.PermissaoBotaoEditar.Add(idChamado, string.Empty);
                else
                    _ocorrenciaVM.PermissaoBotaoEditar[idChamado] = string.Empty;
            }
        }

        private void ConfigurarPermissaoAcaoExcluir(int idChamado, Atendimento atendimento) {
            if (!_ocorrenciaVM.PermissaoBotaoExcluir.ContainsKey(idChamado))
                _ocorrenciaVM.PermissaoBotaoExcluir.Add(idChamado, EnumOcorrencia.DISABLED.GetDescription());
            else
                _ocorrenciaVM.PermissaoBotaoExcluir[idChamado] = EnumOcorrencia.DISABLED.GetDescription();
        }

        private void ConfigurarOcorrenciasDoUsuarioAtual(int idChamado, Atendimento atendimento, Usuario usuarioLogado) {
            bool existeKeyPermissaoBotaoEditar = _ocorrenciaVM.PermissaoBotaoEditar.ContainsKey(idChamado);
            bool existeKeyPermissaoBotaoExcluir = _ocorrenciaVM.PermissaoBotaoExcluir.ContainsKey(idChamado);

            bool isAdministrativo = usuarioLogado.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription());
            bool isCoordenador = usuarioLogado.TipoUsuario.Equals(EnumTipoUsuario.COORDENADOR.GetDescription());
            bool isStatusAberta = atendimento.Status.Equals(EnumStatusChamado.ABERTA.GetDescription());
            bool isStatusExcluida = atendimento.Status.Equals(EnumStatusChamado.EXCLUIDA.GetDescription());
            bool isStatusCancelada = atendimento.Status.Equals(EnumStatusChamado.CANCELADA.GetDescription());

            if (isStatusAberta) {
                if (!existeKeyPermissaoBotaoEditar)
                    _ocorrenciaVM.PermissaoBotaoEditar.Add(idChamado, string.Empty);
                else
                    _ocorrenciaVM.PermissaoBotaoEditar[idChamado] = string.Empty;

                if (!existeKeyPermissaoBotaoExcluir)
                    _ocorrenciaVM.PermissaoBotaoExcluir.Add(idChamado, string.Empty);
                else
                    _ocorrenciaVM.PermissaoBotaoExcluir[idChamado] = string.Empty;
            }
            else {
                if (!existeKeyPermissaoBotaoEditar)
                    _ocorrenciaVM.PermissaoBotaoEditar.Add(idChamado, EnumOcorrencia.DISABLED.GetDescription());
                else
                    _ocorrenciaVM.PermissaoBotaoEditar[idChamado] = EnumOcorrencia.DISABLED.GetDescription();

                if (!existeKeyPermissaoBotaoExcluir)
                    _ocorrenciaVM.PermissaoBotaoExcluir.Add(idChamado, EnumOcorrencia.DISABLED.GetDescription());
                else
                    _ocorrenciaVM.PermissaoBotaoExcluir[idChamado] = EnumOcorrencia.DISABLED.GetDescription();
            }

            if ((isAdministrativo || isCoordenador) && !isStatusExcluida && !isStatusCancelada) {
                existeKeyPermissaoBotaoEditar = _ocorrenciaVM.PermissaoBotaoEditar.ContainsKey(idChamado);
                if (!existeKeyPermissaoBotaoEditar)
                    _ocorrenciaVM.PermissaoBotaoEditar.Add(idChamado, string.Empty);
                else
                    _ocorrenciaVM.PermissaoBotaoEditar[idChamado] = string.Empty;
            }
        }

        public void PopularDesabilitarCamposEdicao() {
            Usuario usuarioAtual = _ocorrenciaVM.UsuarioLogado;
            bool isAdministrativo = usuarioAtual.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription());
            bool isCoordenador = usuarioAtual.TipoUsuario.Equals(EnumTipoUsuario.COORDENADOR.GetDescription());
            bool isMinhaOcorrencia = _ocorrenciaVM.Chamado.CpfUsuario.Equals(usuarioAtual.Cpf);
            bool isStatusAberta = _ocorrenciaVM.Atendimento.Status.Equals(EnumStatusChamado.ABERTA.GetDescription());

            if (isMinhaOcorrencia && isStatusAberta) {
                _ocorrenciaVM.DesabilitarCamposEdicao = string.Empty;
            }
            else {
                _ocorrenciaVM.DesabilitarCamposEdicao = EnumOcorrencia.DISABLED.GetDescription();
            }
        }

        private bool VerificarDatas(DateTime pesquisaInicio, DateTime pesquisaFim, DateTime chamado) {
            if (pesquisaInicio == DateTime.MinValue || pesquisaFim == DateTime.MinValue)
                return false;
            for (DateTime dia = pesquisaInicio; dia <= pesquisaFim; dia = dia.AddDays(1)) {
                if (chamado == dia)
                    return true;
            }
            return false;
        }
    }
}