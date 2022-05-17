using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("CadastroDocente")]
    public class DocenteController 
    {
        private DocenteRepository docenteRepository;

        public DocenteController()
        {
            docenteRepository = new DocenteRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastroDocente(Docente docente)
        {
            try
            {
                docenteRepository.Add(new Docente
                {
                    Nome_Completo = docente.Nome_Completo,
                    Rg = docente.Rg,
                    Data_Nascimento = docente.Data_Nascimento,
                    Telefone_Fixo = docente.Telefone_Fixo,
                    Celular = docente.Celular,
                    Celular2 = docente.Celular2,
                    Email = docente.Email,
                    Foto = docente.Foto,
                    Disp_Viajar = docente.Disp_Viajar,
                    Cep = docente.Cep,
                    Endereco = docente.Endereco,
                    Cidade = docente.Cidade,
                    UF = docente.UF,
                    Disp_Horario = docente.Disp_Horario,
   
               });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
//}

//[AcceptVerbs("POST")]
//[Route("Listar")]
//public IEnumerable<Docente> ListarDocente(Docente docente, bool)
//{
//    try
//    {
//        List<Docente> docentes = docenteRepository.GetAll(docente , );
//        return docentes;
//    }
//    catch (Exception e)
//    {
//        return null;
//    }
//}

//[AcceptVerbs("GET")]
//[Route("BuscarPorId/{id}")]
//public Chamado BuscarChamado(int id)
//{
//    try
//    {
//        return docenteRepository.Get(id);
//    }
//    catch (Exception e)
//    {
//        return null;
//    }
//}

//        [AcceptVerbs("PUT")]
//        [Route("Alterar")]
//        public bool AlterarChamado(Chamado chamado)
//        {
//            try
//            {
//                docenteRepository.Update(c => c.Id == chamado.Id, chamado);
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }

//        [AcceptVerbs("POST")]
//        [Route("ListarChamadosDashboard")]
//        public IEnumerable<Chamado> ListarChamadosDashboard(Usuario usuario)
//        {
//            try
//            {
//                return docenteRepository.GetAllPorUsuarioAbertos(usuario);
//            }
//            catch (Exception e)
//            {
//                return null;
//            }
//        }
//    }
//}