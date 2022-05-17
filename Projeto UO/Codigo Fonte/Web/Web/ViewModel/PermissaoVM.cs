using Backend.Enum;
using Backend.Model;
using Backend.Model.Metadata;
using System.Collections.Generic;
using Web.Helper;

namespace Web.ViewModel {
	public class PermissaoVM {

        //Nome dos botões de acordo com o usuário
        public string MenuAcao { get; set; }

        //Menus das views
        public string MenuCadastro { get; set; }
        public string MenuEnsalamento { get; set; }
        public string MenuOcorrencia { get; set; }

        //Botão Home
        public string MenuHome { get; set; }

        //Usuario da session
        public Usuario UsuarioLogado { get; set; }
        public List<Permissao> Permissoes { get; set; }

        //Armazenados pelo ActionFilter caso o usuário perca sua session e continue tentando utilizar
        public string Controller { get; set; }

        //Armazena o nome da ação sempre que o usuário fizer algo.
        public string Acao { get; set; }

        //Flag
        public string Result { get; set; }
        public bool PerdeuSession { get; set; }



        public PermissaoVM() {
            //Se o UsuarioLogado tiver privilégios máximos, esta prop será alterada para PermissaoHelper.Gerenciar
            //no método AtribuirPermissões(Usuario usuario), que fica na PermissaoController
            MenuAcao = PermissaoHelper.PESQUISAR.GetDescription();
            MenuCadastro = PermissaoHelper.OCULTAR.GetDescription();
            MenuEnsalamento = PermissaoHelper.OCULTAR.GetDescription();
            MenuOcorrencia = PermissaoHelper.OCULTAR.GetDescription();

            //Se o UsuárioLogado não for Aluno ou Professor, esta prop será alterada para Configuration.Dashboard
            //no método AtribuirPermissões(Usuario usuario), que fica na PermissaoController
            MenuHome = Configuration.Ocorrencia;

            UsuarioLogado = new Usuario();
            Permissoes = new List<Permissao>();
            PerdeuSession = false;
        }
    }
}