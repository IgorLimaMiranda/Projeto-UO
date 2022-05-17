/* Não mexa aqui */
function CancelarWindowsForms() {
    Popup.dialog('destroy').remove();
}

function LimparPartialView() {
    //$("#cadastro").val(""); <Estava assim. Alterado por Gabriel Souza
    //$("#cadastro").remove(); <Outra opção. Mas a partial view morre de vez até que atualize a página
    $("#cadastro").html("");
}

var Popup;
/* Não mexa aqui */

function PartialViewCadastrarEnsalamento(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("data_cadastrar_ensalamento").focus();
        });
}

function PartialViewCadastrarCurso(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("curso_nome").focus();
        });
}
function PartialViewCadastrarSala(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("sala_numero").focus();
        });
}

function PartialViewCadastrarIncidente(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("descricao_incidente").focus();
        });
}

//function PopupFormEditarUsuario(url) {
//    var formDiv = $("<div/>");
//    $.get(url)
//        .done(function (response) {
//            document.getElementById("cadastro").innerHTML = (response);
//            document.getElementById("usuario_nome").focus();
//        });
//}

function PopupFormEditarUsuario(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("usuario_nome").focus();
            console.log(response);
        });
}

function PartialViewCadastrarOcorrencia(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("chamado_patrimonio").focus();
        });
}


function PopupFormExcluirOcorrencia(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 250,
                width: 450,
                modal: true,
                overflow: false,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirEnsalamento(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 250,
                width: 450,
                modal: true,
                overflow: false,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormEditarEnsalamento(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Edição de Ensalamento',
                height: 700,
                width: 1200,
                modal: true,
                overflow: false,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormVisualizarEnsalamento(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Visualização de Ensalamento',
                height: 600,
                width: 1200,
                modal: true,
                overflow: false,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PartialViewCadastrarSetor(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("nome_setor").focus();
            console.log(response);
        });
}

function PopupFormEditarSetor(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("nome_setor").focus();
            console.log(response);
        });
}

function PopupFormEditarEquipamento(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("descricao_recurso").focus();
            console.log(response);
        });
}



function PopupFormVisualizarHistoricoEnsalamento(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Detalhes do Ensalamento',
                height: 660,
                width: 850,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormVisualizarOcorrencia(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Detalhes da Ocorrência',
                width: 1250,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormVisualizarUsuario(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: true,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Visualizar Usuário',
                width: 550,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormEditarTurma(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Editar Turma',
                height: 450,
                width: 850,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}


function PopupFormEditarCurso(url) {
    var formDiv = $("<div/>");
    $.get(url).done(function (response) {
        document.getElementById("cadastro").innerHTML = (response);
        document.getElementById("curso_nome").focus();
    });
}

function PopupFormExcluirUsuario(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirUsuario(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 250,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormEditarSala(url) {
    var formDiv = $("<div/>");
    $.get(url).done(function (response) {
        document.getElementById("cadastro").innerHTML = (response);
        document.getElementById("sala_numero").focus();
    });
}

function PopupFormEditarEquipamento(url) {
    var formDiv = $("<div/>");
    $.get(url).done(function (response) { document.getElementById("cadastro").innerHTML = (response); });
}

function PopupFormEditarOcorrencia(url) {
    var formDiv = $("<div/>");
    $.get(url).done(function (response) { document.getElementById("cadastro").innerHTML = (response); });
}

function PopupFormEditarIncidente(url) {
    var formDiv = $("<div/>");
    $.get(url).done(function (response) {
        document.getElementById("cadastro").innerHTML = (response);
        document.getElementById("descricao_incidente").focus();
    });
}


function PopupFormEditarSeguranca(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Editar Perfil',
                height: 450,
                width: 850,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirIncidente(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500,
                    
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirTurma(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}
function PopupFormExcluirSala(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirCurso(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormExcluirRecurso(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}


function PopupFormSair(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormRecursoSala(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Adicionar Recurso',
                height: 560,
                width: 850,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}
function PopupFormExcluirSetor(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Atenção!',
                height: 240,
                width: 450,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}

function PopupFormVisualizarSala(url) {
    var formDiv = $("<div/>");
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: true,
                show: {
                    effect: "fade",
                    duration: 500
                },
                hide: {
                    effect: "fade",
                    duration: 500
                },
                title: 'Visualizar Sala',
                height: 800,
                width: 900,
                modal: true,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });

        });
}
function PartialViewCadastrarUsuario(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("usuario_cpf").focus();
            console.log(response);
        });
}



function PartialViewCadastrarTurma(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("turma_id").focus();
            console.log(response);
        });
}

function PartialViewAlterarTurma(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("combo_box_cursos").focus();
            console.log(response);
        });
}

function PartialViewCadastrarRecurso(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("descricao_recurso").focus();
            console.log(response);
        });
}

function PartialViewEditarRecurso(url) {
    $.get(url)
        .done(function (response) {
            document.getElementById("cadastro").innerHTML = (response);
            document.getElementById("recurso_descricao").focus();
            console.log(response);
        });
}
