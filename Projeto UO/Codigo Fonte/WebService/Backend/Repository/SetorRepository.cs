using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class SetorRepository : Repository<Setor> {
       public List<Setor> ListarSetores() {
            var setores = ctx.Database.SqlQuery<Setor>("SELECT SE_ID as Id, SE_NOME as Nome, SE_ANDAR as Andar FROM TBL_SETOR WHERE DISCRIMINATOR = 'Setor'").OrderBy(s => s.Nome);

            if(setores.Count() > 0)
                return setores.ToList();

            return null;
        } 
    }
}
