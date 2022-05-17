using Backend.Enum;
using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using Backend.Repository.Base;

namespace Backend.Repository {
	public class ChamadoRepository : Repository<Chamado> {
		public List<Chamado> GetAll(Usuario usuario, bool apenasMeusChamados) {
			if((!apenasMeusChamados) &&
				(usuario.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription()) ||
				usuario.TipoUsuario.Equals(EnumTipoUsuario.COORDENADOR.GetDescription()) ||
				usuario.TipoUsuario.Equals(EnumTipoUsuario.GERENTE.GetDescription()))) {
				List<Chamado> chamados = (from c in ctx.Chamado
							.Include("Setor")
							.Include("Usuario")
							.Include("IncidenteRecorrente")
										  orderby c.HoraData descending
										  select c).ToList();
				if(chamados.Any()) {
					AtendimentoRepository atendimentoRepository = new AtendimentoRepository();
					for(int i = 0; i < chamados.Count(); i++) {
						chamados[i].Atendimentos = atendimentoRepository.GetAllAtendimentosDeChamado(chamados[i].IdChamado);
					}
				}

				return chamados;
			}

			return GetAllPorTipoUsuario(usuario);
		}

		public Chamado Get(int id) {
			var chamado = (from c in ctx.Chamado
							.Include("Setor")
							.Include("Usuario")
							.Include("IncidenteRecorrente")
						   where c.IdChamado.Equals(id)
						   select c);

            if(chamado.Any()) {
                AtendimentoRepository atendimentoRepository = new AtendimentoRepository();
                Chamado _chamado = chamado.First();
                _chamado.Atendimentos = atendimentoRepository.GetAllAtendimentosDeChamado(chamado.First().IdChamado);
                return _chamado;
            }

			return null;
		}

		public List<Chamado> GetAllPorTipoUsuario(Usuario usuario) {
			List<Chamado> chamados = (from c in ctx.Chamado
							.Include("Setor")
							.Include("Usuario")
							.Include("IncidenteRecorrente")
									  where c.CpfUsuario.Equals(usuario.Cpf)
									  orderby c.HoraData descending
									  select c).ToList();

			if(chamados.Any()) {
				AtendimentoRepository atendimentoRepository = new AtendimentoRepository();
				for(int i = 0; i < chamados.Count(); i++) {
					chamados[i].Atendimentos = atendimentoRepository.GetAllAtendimentosDeChamado(chamados[i].IdChamado);
				}
			}

			return chamados;
		}

		public IQueryable<Chamado> GetAllPorUsuarioAbertos(Usuario usuario) {
			//return (from c in ctx.Chamado
			//                .Include("Setor")
			//                .Include("Usuario")
			//                .Include("IncidenteRecorrente")
			//        where c.CpfUsuario.Equals(usuario.Cpf)
			//        select c).Where(c => (from a in ctx.Atendimento where c.Id == a.IdChamado select a).ToList().Count == 0);

			return (from c in ctx.Chamado
							.Include("Setor")
							.Include("Usuario")
							.Include("IncidenteRecorrente")
					where c.CpfUsuario.Equals(usuario.Cpf)
					select c).Where(c => (from a in ctx.Atendimento where c.IdChamado == a.IdChamado && !a.Status.Equals("Aberta") select a).ToList().Count == 0);
		}
	}
}
