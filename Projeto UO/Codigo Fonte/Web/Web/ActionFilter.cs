using System.Web.Mvc;
using Web.ViewModel;
using Web;

namespace Web {
	/// <summary>
	/// Camada de verificação de status da Session.
	/// <para>Realiza redirecionamentos para login caso o usuário perca a Session.</para>
	/// <para>Realiza redirecionamentos para Controllers específicas caso o usuário tenha tentado acessar alguma sem estar logado.</para>
	/// </summary>
    public class ActionFilter : ActionFilterAttribute {
        PermissaoVM permissaoVM;
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            permissaoVM = new PermissaoVM();
            permissaoVM = filterContext.HttpContext.Session.Contents["permissao"] as PermissaoVM;
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            int a = 0;
            //Se usuário não estiver logado
            if ((permissaoVM != null) && permissaoVM.Permissoes.Count > 0) {
                foreach (var permissao in permissaoVM.Permissoes) {
                    if (permissao.Controller.Equals(controller)) {
                        a++;
                        break;
                    }
                }
                if (permissaoVM.Controller != null)
                    filterContext.Result = new RedirectResult($"{Configuration.SistemaWeb}/{permissaoVM.Controller}");
                else if (a == 0)
                    filterContext.Result = new RedirectResult(permissaoVM.MenuHome);

                permissaoVM.Controller = null;
                permissaoVM.Acao = filterContext.ActionDescriptor.ActionName;
            }
            else {
                permissaoVM = new PermissaoVM();
                permissaoVM.Acao = filterContext.ActionDescriptor.ActionName;
                permissaoVM.Controller = controller;
                filterContext.Result = new RedirectResult(Configuration.Login);
                permissaoVM.PerdeuSession = true;
            }
            if(filterContext.Result != null)
                permissaoVM.Result = (filterContext.Result).ToString();
            filterContext.HttpContext.Session.Contents["permissao"] = permissaoVM;
        }
    }
}