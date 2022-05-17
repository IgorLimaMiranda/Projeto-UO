using Backend.Enum;
using System.Web.Mvc;
using Web.Helper.EnumMensagens;

public static class ActionResultExtensions {
    /// <summary>
    /// Redireciona para uma ActionResult retornando uma mensagem de confirmação para a View
    /// </summary>
    /// <param name="actionResult"></param>
    /// <param name="mensagem">Mensagem a ser exibida</param>
    /// <param name="titulo">titulo a ser exibido, sendo omitido apresenta defaut 'Atenção'</param>
    /// <returns></returns>

    public static ActionResult Mensagem(this ActionResult actionResult, string mensagem, EnumTipoToastr tipoToastr, string titulo = "") {
        if (titulo.Equals(string.Empty))
            titulo = TempDataActionResult.RetornarTituloMensagem(tipoToastr);
        return new TempDataActionResult(actionResult, mensagem, tipoToastr.GetDescription(), titulo);
    }
}

public class TempDataActionResult : ActionResult {
    private readonly ActionResult _actionResult;
    private readonly string _mensagem;
    private readonly string _tipo;
    private readonly string _titulo;

    public TempDataActionResult(ActionResult actionResult, string Mensagem, string Tipo, string Titulo) {
        _actionResult = actionResult;
        _mensagem = Mensagem;
        _tipo = Tipo;
        _titulo = Titulo;
    }

    public override void ExecuteResult(ControllerContext context) {
        context.Controller.TempData["Mensagem"] = _mensagem;
        context.Controller.TempData["Tipo"] = _tipo;
        context.Controller.TempData["Titulo"] = _titulo;
        _actionResult.ExecuteResult(context);
    }

    public static string RetornarTituloMensagem(EnumTipoToastr tipoToastr) {
        switch (tipoToastr) {
            case EnumTipoToastr.ERROR:
                return EnumMensagensTitulo.DESCULPE.GetDescription();
            case EnumTipoToastr.INFO:
                return EnumMensagensTitulo.NOTIFICACAO.GetDescription();
            case EnumTipoToastr.SUCCESS:
                return EnumMensagensTitulo.BEM_SUCEDIDO.GetDescription();
            default:
                return EnumMensagensTitulo.ATENCAO.GetDescription();
        }
    }
}