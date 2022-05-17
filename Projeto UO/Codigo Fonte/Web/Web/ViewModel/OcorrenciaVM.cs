using Backend.Enum;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;
using Web.Helper.EnumWeb;

namespace Web.ViewModel {
    public class OcorrenciaVM : PermissaoVM {
        public Chamado Chamado { get; set; }
        public List<Chamado> Chamados { get; set; }
        public Atendimento Atendimento { get; set; }
        public List<Atendimento> Atendimentos { get; set; }
        public List<IncidenteRecorrente> IncidentesRecorrentes { get; set; }
        public List<Setor> Setores { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public Dictionary<int, string> StatusChamado { get; set; }
        public Dictionary<int, string> PermissaoBotaoEditar { get; set; }
        public Dictionary<int, string> PermissaoBotaoExcluir { get; set; }
        public Dictionary<int, string> LayoutPermissaoBotaoEditar { get; set; }
        public Dictionary<int, string> LayoutPermissaoBotaoExcluir { get; set; }
        public List<string> LabelStatus { get; set; }
        public List<string> IconesAtendimentos { get; set; }
        public String DesabilitarCamposEdicao { get; set; }
        public String StatusSelecionado { get; set; }
        public Dictionary<string, string> LayoutStatus { get; set; }
        public string IntervaloOcorrencia { get; set; }
        public string TipoDePesquisa { get; set; }
        public bool IsPesquisa { get; set; }

        public OcorrenciaVM() {
            Chamado = new Chamado();
            Chamados = new List<Chamado>();
            Atendimento = new Atendimento();
            Atendimentos = new List<Atendimento>();
            IncidentesRecorrentes = new List<IncidenteRecorrente>();
            Setores = new List<Setor>();
            Usuarios = new List<Usuario>();
            StatusChamado = new Dictionary<int, string>();
            PermissaoBotaoEditar = new Dictionary<int, string>();
            PermissaoBotaoExcluir = new Dictionary<int, string>();
            LayoutPermissaoBotaoEditar = new Dictionary<int, string>();
            LayoutPermissaoBotaoExcluir = new Dictionary<int, string>();
            LabelStatus = new List<string>();
            IconesAtendimentos = new List<string>();
            LayoutStatus = new Dictionary<string, string>();
            IsPesquisa = false;
        }

        public IEnumerable<SelectListItem> ListarStatus() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumStatusChamado>());
        }

        public IEnumerable<SelectListItem> ListarIncidentesRecorrentes() {
            return AtribuirItensListaIncidentesRecorrentes(IncidentesRecorrentes);
        }

        public List<SelectListItem> ListarTipoDePesquisa() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumPesquisa>());
        }

        private List<SelectListItem> AtribuirItensListaIncidentesRecorrentes(IEnumerable<IncidenteRecorrente> incidentes) {
            var selectList = new List<SelectListItem>();
            foreach (var incidente in incidentes) {
                selectList.Add(new SelectListItem {
                    Value = incidente.Id.ToString(),
                    Text = incidente.Descricao
                });
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> ListarSetores() {
            return AtribuirItensListaSetores(Setores);
        }

        private List<SelectListItem> AtribuirItensListaSetores(IEnumerable<Setor> setores) {
            var selectList = new List<SelectListItem>();
            foreach (var setor in setores) {
                selectList.Add(new SelectListItem {
                    Value = setor.Id.ToString(),
                    Text = setor.Nome
                });
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> ListarUsuarios() {
            return AtribuirItensListaUsuarios(Usuarios);
        }

        private List<SelectListItem> AtribuirItensListaUsuarios(IEnumerable<Usuario> usuarios) {
            var selectList = new List<SelectListItem>();
            foreach (var usuario in usuarios) {
                selectList.Add(new SelectListItem {
                    Value = usuario.Cpf,
                    Text = usuario.Nome
                });
            }
            return selectList;
        }

        public void PopularRadioButtonsStatus(Atendimento atendimento) {
            LayoutStatus.Clear();
            LabelStatus.Clear();
            if (atendimento.Status.Equals(EnumStatusChamado.ABERTA.GetDescription())) {
                LayoutStatus.Add(EnumStatusChamado.EM_ATENDIMENTO.GetDescription(), EnumLayoutStatusChamado.WARNING.GetDescription());
                LayoutStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription(), EnumLayoutStatusChamado.SUCCESS.GetDescription());
                LayoutStatus.Add(EnumStatusChamado.CANCELADA.GetDescription(), EnumLayoutStatusChamado.DANGER.GetDescription());

                LabelStatus.Add(EnumStatusChamado.EM_ATENDIMENTO.GetDescription());
                LabelStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription());
                LabelStatus.Add(EnumStatusChamado.CANCELADA.GetDescription());
            }
            else if (atendimento.Status.Equals(EnumStatusChamado.EM_ATENDIMENTO.GetDescription())) {
                LayoutStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription(), EnumLayoutStatusChamado.SUCCESS.GetDescription());
                LayoutStatus.Add(EnumStatusChamado.CANCELADA.GetDescription(), EnumLayoutStatusChamado.DANGER.GetDescription());

                LabelStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription());
                LabelStatus.Add(EnumStatusChamado.CANCELADA.GetDescription());
            }
            else if (atendimento.Status.Equals(EnumStatusChamado.CONCLUIDA.GetDescription())) {
                LayoutStatus.Add(EnumStatusChamado.REABERTA.GetDescription(), EnumLayoutStatusChamado.INFO.GetDescription());
                LabelStatus.Add(EnumStatusChamado.REABERTA.GetDescription());
            }
            else if (atendimento.Status.Equals(EnumStatusChamado.REABERTA.GetDescription())) {
                LayoutStatus.Add(EnumStatusChamado.EM_ATENDIMENTO.GetDescription(), EnumLayoutStatusChamado.WARNING.GetDescription());
                LayoutStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription(), EnumLayoutStatusChamado.SUCCESS.GetDescription());
                LayoutStatus.Add(EnumStatusChamado.CANCELADA.GetDescription(), EnumLayoutStatusChamado.DANGER.GetDescription());

                LabelStatus.Add(EnumStatusChamado.EM_ATENDIMENTO.GetDescription());
                LabelStatus.Add(EnumStatusChamado.CONCLUIDA.GetDescription());
                LabelStatus.Add(EnumStatusChamado.CANCELADA.GetDescription());
            }
        }

        public void ConfigurarLayoutPermissoesBotoes(int idChamado) {
            bool existeKeyLayoutPermissaoBotaoEditar = LayoutPermissaoBotaoEditar.ContainsKey(idChamado);
            bool existeKeyLayoutPermissaoBotaoExcluir = LayoutPermissaoBotaoExcluir.ContainsKey(idChamado);
            if (PermissaoBotaoEditar[idChamado].Equals(EnumOcorrencia.DISABLED.GetDescription())) {
                if (!existeKeyLayoutPermissaoBotaoEditar) {
                    LayoutPermissaoBotaoEditar.Add(idChamado, EnumOcorrencia.LAYOUT_DISABLED.GetDescription());
                }
                else {
                    LayoutPermissaoBotaoEditar[idChamado] = EnumOcorrencia.LAYOUT_DISABLED.GetDescription();
                }
            }
            else {
                if (!existeKeyLayoutPermissaoBotaoEditar) {
                    LayoutPermissaoBotaoEditar.Add(idChamado, EnumOcorrencia.EDITAR_LAYOUT.GetDescription());
                }
                else {
                    LayoutPermissaoBotaoEditar[idChamado] = EnumOcorrencia.EDITAR_LAYOUT.GetDescription();
                }
            }

            if (PermissaoBotaoExcluir[idChamado].Equals(EnumOcorrencia.DISABLED.GetDescription())) {
                if (!existeKeyLayoutPermissaoBotaoExcluir) {
                    LayoutPermissaoBotaoExcluir.Add(idChamado, EnumOcorrencia.LAYOUT_DISABLED.GetDescription());
                }
                else {
                    LayoutPermissaoBotaoExcluir[idChamado] = EnumOcorrencia.LAYOUT_DISABLED.GetDescription();
                }
            }
            else {
                if (!existeKeyLayoutPermissaoBotaoExcluir) {
                    LayoutPermissaoBotaoExcluir.Add(idChamado, EnumOcorrencia.EXCLUIR_LAYOUT.GetDescription());
                }
                else {
                    LayoutPermissaoBotaoExcluir[idChamado] = EnumOcorrencia.EXCLUIR_LAYOUT.GetDescription();
                }
            }
        }
    }
}