using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class FormacaoAcademicaRepository :Repository<FormacaoAcademica>
    {
		public List<FormacaoAcademica> GetAllFormacao()
		{
			var formacao = from f in ctx.FormacaoAcademica
						 .Include("Docente")
						 orderby f.Docente.Nome_Completo ascending, f.Nome_Do_Curso ascending
						 select f;
			if (formacao == null)
				return null;
			else
				return formacao.ToList();
		}
	}
}
