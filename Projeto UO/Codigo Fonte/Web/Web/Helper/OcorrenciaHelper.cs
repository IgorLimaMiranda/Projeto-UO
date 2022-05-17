using Backend.Model;
using System.Collections.Generic;
using Backend.Enum;
using Web.Helper.EnumWeb;

namespace Web.Helper {
    public class OcorrenciaHelper {
        public static List<string> DefinirIconesAtendimentos(List<Atendimento> atendimentos) {
            List<string> icones = new List<string>();
            foreach (Atendimento atendimento in atendimentos) {
                icones.Add(ResolveIconeAtendimento(atendimento.Status));
            }

            return icones;
        }

        public static string ResolveIconeAtendimento(string status) {
            if (status.Equals(EnumStatusChamado.ABERTA.GetDescription()))
                return EnumOcorrencia.ICONE_ABERTA.GetDescription();
            if (status.Equals(EnumStatusChamado.EM_ATENDIMENTO.GetDescription()))
                return EnumOcorrencia.ICONE_EM_ATENDIMENTO.GetDescription();
            if (status.Equals(EnumStatusChamado.CONCLUIDA.GetDescription()))
                return EnumOcorrencia.ICONE_CONCLUIDA.GetDescription();
            if (status.Equals(EnumStatusChamado.CANCELADA.GetDescription()))
                return EnumOcorrencia.ICONE_CANCELADA.GetDescription();
            if (status.Equals(EnumStatusChamado.EXCLUIDA.GetDescription()))
                return EnumOcorrencia.ICONE_EXCLUIDA.GetDescription();
            if (status.Equals(EnumStatusChamado.REABERTA.GetDescription()))
                return EnumOcorrencia.ICONE_REABERTA.GetDescription();

            return string.Empty;
        }
    }
}