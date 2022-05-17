using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;
using Web.Helper.EnumWeb;

namespace Web.ViewModel {
    public class EnsalamentoVM : PermissaoVM {

        //Publics
        public Ensalamento Ensalamento { get; set; }
        public List<Ensalamento> Ensalamentos { get; set; }
        public List<Sala> Salas { get; set; }
        public List<Turma> Turmas { get; set; }
        public string ChaveTurma { get; set; }
        public string IntervaloEnsalamento { get; set; }
        public string NomeTabela { get; set; }          //Prop de personalização da tabela
        public string EstiloNomeTabela { get; set; }    //Prop de personalização da tabela
        public List<string> ValueTurma { get; set; }
        public List<bool> Periodicidade { set; get; }
        public List<List<bool>> PeriodicidadeTabela { get; set; }
        public List<string> Turnos { get; set; }
        public List<string> PeriodicidadesString { get; set; }
        public List<string> ColunaOcultaPeriodicidade { get; set; }
        public bool IsEnsalamentos { get; set; }
        public string TipoDePesquisa { get; set; }
        public bool Conflitos { get; set; }             //Flag de Conflitos
        public bool IsPesquisa { get; set; }              //Flag de Pesquisa


        public IEnumerable<SelectListItem> ListarTurnos() {
            return FrontEnumHelper.AtribuirItensEnumInt(EnumHelper.GetEnums<EnumPeriodoEnsalamento>());
        }

        public List<SelectListItem> ListarTipoDePesquisa() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumPesquisa>());
        }

        public EnsalamentoVM() {
            Ensalamento = new Ensalamento();
            Ensalamentos = new List<Ensalamento>();
            Salas = new List<Sala>();
            Turmas = new List<Turma>();
            Ensalamento = new Ensalamento();
            ValueTurma = new List<string>();
            Periodicidade = new List<bool> { false, false, false, false, false, false, false };
            PeriodicidadeTabela = new List<List<bool>>();
            NomeTabela = "Registros";
            EstiloNomeTabela = "color:black";
            Conflitos = false;
            IsPesquisa = false;
        }

        public IEnumerable<SelectListItem> ListarTurmas() {
            return GetListaTurmas(Turmas);
        }

         List<SelectListItem> GetListaTurmas(IEnumerable<Turma> elements) {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements) {
                selectList.Add(new SelectListItem {
                    Value = element.Id.ToString() + "/" + element.Ano.ToString(),
                    Text = $"{element.Id.ToString()} / {element.Ano} - {element.Curso.Descricao.ToString()}"
                });
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> ListarSalas() {
            return AtribuirItensListaSalas(Salas);
        }

         List<SelectListItem> AtribuirItensListaSalas(IEnumerable<Sala> salas) {
            var selectList = new List<SelectListItem>();
            foreach (var sala in salas) {
                selectList.Add(new SelectListItem {
                    Value = sala.Id.ToString(),
                    Text = $"{sala.Numero.ToString()} | {sala.Andar} | {sala.Descricao} | {sala.Tipo}"
                });
            }
            return selectList;
        }
    }
}