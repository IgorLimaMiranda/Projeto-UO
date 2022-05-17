using Backend.Enum;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;

using Web.Helper.EnumWeb;

namespace Web.ViewModel {
    public class HistoricoEnsalamentoVM : PermissaoVM {

        public HistoricoEnsalamento HistoricoEnsalamento { get; set; }
        public List<HistoricoEnsalamento> Historicos { get; set; }
        public string IntervaloHistorico { get; set; }
        public List<string> ComboSalas { get; set; }
        public List<string> ComboTurmas { get; set; }
        //public string Turma { get; set; }
        public List<string> ComboUsuarios{ get; set; }
        public List<string> ComboOcupantes { get; set; }
        public List<string> ComboAcao { get; set; }
        public string Pesquisa { get;  set; }

        //public Ensalamento Ensalamento { get; set; }
        //public string Ocupante { get; set; }

        public HistoricoEnsalamentoVM() {
            HistoricoEnsalamento = new HistoricoEnsalamento();
            Historicos = new List<HistoricoEnsalamento>();
            ComboSalas = new List<string>();
            ComboTurmas = new List<string>();
            ComboUsuarios = new List<string>();
            ComboOcupantes = new List<string>();

            Pesquisa = PermissaoHelper.OCULTAR.GetDescription();
        }

        public IEnumerable<SelectListItem> ListarSalas() {
            return AtribuirItensListaSalas(ComboSalas);
        }

        public IEnumerable<SelectListItem> ListarAcao() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumAcaoHistoricoEnsalamento>());
        }

        private List<SelectListItem> AtribuirItensListaSalas(IEnumerable<string> salas) {
            var selectList = new List<SelectListItem>();
            foreach (var sala in salas) {
                selectList.Add(new SelectListItem {
                    Value = sala,
                    Text = sala
                });
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> ListarTurmas() {
            return AtribuirItensListaTurmas(ComboTurmas);
        }

        private List<SelectListItem> AtribuirItensListaTurmas(IEnumerable<string> turmas) {
            var selectList = new List<SelectListItem>();
            foreach (var turma in turmas) {
                selectList.Add(new SelectListItem {
                    Value = turma,
                    Text = turma
                });
            }
            return selectList;
        }



        public IEnumerable<SelectListItem> ListarUsuarios() {
            return AtribuirItensListaUsuarios(ComboUsuarios);
        }

        private List<SelectListItem> AtribuirItensListaUsuarios(IEnumerable<string> usuarios) {
            var selectList = new List<SelectListItem>();
            foreach (var usuario in usuarios) {
                selectList.Add(new SelectListItem {
                    Value = usuario,
                    Text = usuario
                });
            }
            return selectList;
        }


        public IEnumerable<SelectListItem> ListarOcupantes() {
            return AtribuirItensListaOcupantes(ComboOcupantes);
        }

        private List<SelectListItem> AtribuirItensListaOcupantes(IEnumerable<string> ocupantes) {
            var selectList = new List<SelectListItem>();
            foreach (var ocupante in ocupantes) {
                selectList.Add(new SelectListItem {
                    Value = ocupante,
                    Text = ocupante
                });
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> ListarTurno() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumPeriodoEnsalamento>());
        }
    }
}