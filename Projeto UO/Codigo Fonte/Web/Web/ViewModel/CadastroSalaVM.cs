using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;

namespace Web.ViewModel {
    public class CadastroSalaVM : PermissaoVM {
        public Sala Sala { get; set; }
        public Equipamento Equipamento { get; set; }
        public EquipamentoSetor EquipamentoSetor { set; get; }
        public List<Sala> Salas { get; set; }
        public List<Equipamento> Equipamentos { get; set; }
        public List<EquipamentoSetor> EquipamentosSetor { get; set; }
        
        public CadastroSalaVM() {
            Sala = new Sala();
            Equipamento = new Equipamento();
            EquipamentoSetor = new EquipamentoSetor();
            Salas = new List<Sala>();
            Equipamentos = new List<Equipamento>();
            EquipamentosSetor = new List<EquipamentoSetor>();
        }

        public IEnumerable<SelectListItem> ListarTipoSala() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumTipoSala>());
        }

        public IEnumerable<SelectListItem> ListarAndarSetor() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumAndarSetor>());
        }

        public IEnumerable<SelectListItem> ListarDescricaoSala() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumDescricaoSala>());
        }

        public IEnumerable<SelectListItem> ListarEquipamentos() {
            return AtribuirItensListaEquipamentos(Equipamentos);
        }

        private List<SelectListItem> AtribuirItensListaEquipamentos(IEnumerable<Equipamento> elements) {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements) {
                selectList.Add(new SelectListItem {
                    Value = element.Id.ToString(),
                    Text = element.Descricao
                });
            }
            return selectList;
        }
    }
}