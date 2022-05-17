using Backend.Context;
using Backend.Model.Metadata;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class PermissaoRepository : Repository<Permissao> {
        public List<Permissao> GetPermissaoPorTipoUsuarioLinq(String tipoUsuario) {
            ctx = new ContextModel();
            var menu = from m in ctx.Permissao
                       where m.TipoUsuario == tipoUsuario
                       select m;
            return menu.ToList();
        }
    }
}
