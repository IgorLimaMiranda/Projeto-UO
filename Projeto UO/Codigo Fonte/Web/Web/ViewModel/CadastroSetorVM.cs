using Backend.Enum;
using Backend.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helper;

namespace Web.ViewModel {
    public class CadastroSetorVM : PermissaoVM {
        public Setor Setor { get; set; }
        public List<Setor> Setores { get; set; }

        public CadastroSetorVM() {
            Setor = new Setor();
            Setores = new List<Setor>();
        }

        public IEnumerable<SelectListItem> ListarAndarSetor() {
            return FrontEnumHelper.AtribuirItensEnum(EnumHelper.GetEnums<EnumAndarSetor>());
        }
    }
}