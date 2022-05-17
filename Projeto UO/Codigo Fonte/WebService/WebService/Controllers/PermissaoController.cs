using Backend.Model.Metadata;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("Permissao")]
    public class PermissaoController : ApiController {

        private PermissaoRepository permissaoRepository;

        public PermissaoController() {
            permissaoRepository = new PermissaoRepository();
        }

        [AcceptVerbs("GET")]
        [Route("Listar/{tipoUsuario}")]
        public List<Permissao> ListarMenu(String tipoUsuario) {
            try {
                return permissaoRepository.GetPermissaoPorTipoUsuarioLinq(tipoUsuario);
            } catch (Exception e) {
                return null;
            }            
        }
    }
}