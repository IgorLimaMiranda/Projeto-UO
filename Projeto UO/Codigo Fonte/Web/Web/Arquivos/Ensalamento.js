function DatePicker() {
    $('#data_cadastrar_ensalamento').daterangepicker();
}

function funcaoVerificar() {
    var checkDomingo = document.getElementById("multiChoice1");
    var checkSegunda = document.getElementById("multiChoice2");
    var checkTerca = document.getElementById("multiChoice3");
    var checkQuarta = document.getElementById("multiChoice4");
    var checkQuinta = document.getElementById("multiChoice5");
    var checkSexta = document.getElementById("multiChoice6");
    var checkSabado = document.getElementById("multiChoice7");

    if (checkDomingo.checked == false && checkSegunda.checked == false && checkTerca.checked == false && checkQuarta.checked == false && checkQuinta.checked == false && checkSexta.checked == false && checkSabado.checked == false) {
        checkQuarta.setCustomValidity("Selecione ao menos um campo da periodiciade.");
    }
    else {
        checkQuarta.setCustomValidity("");
    }
}

function EnviarEdicaoParcial() {
    var parciais = new Array(), tabela, trs, tds;
    tabela = document.getElementById('table-body');
    trs = tabela.getElementsByTagName('tr');
    for (var i = 0; i < trs.length; i++) {
        var ensalamento = new Array();
        tds = trs[i].getElementsByTagName('td');
        if (tds.length <= 5) {
            break;
        }
        for (var j = 0; j < 6; j++) {
            ensalamento.push(tds[j].innerHTML);
        }
        parciais.push(ensalamento);
    }


    var periodicidade = [$('#domingoEditar').is(":checked"), $('#segundaEditar').is(":checked"), $('#tercaEditar').is(":checked"), $('#quartaEditar').is(":checked"), $('#quintaEditar').is(":checked"), $('#sextaEditar').is(":checked"), $('#sabadoEditar').is(":checked")];
    $.ajax({
        url: 'CadastroEnsalamento/VerificarParcial',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            IntervaloEnsalamento: $('#reservationEditar').val(),
            Periodicidade: periodicidade,
            Periodo: $('#IdListaPeriodoEditar').val(),
            ChaveTurma: $('#IdListaTurmasEditar').val(),
            IdSala: $('#IdListaSalasEditar').val(),
            Ocupante: $('#ocupanteEditar').val(),
            IdOriginal: $('#idEnsalamento').val(),
            parciais,
            idParcial: $('#idParcialEnsalamento').val()
        }),
        success: function (result) {
            var botao, aviso, dias = "";
            if (periodicidade[0]) { dias += "Dom "; }
            if (periodicidade[1]) { dias += "Seg "; }
            if (periodicidade[2]) { dias += "Ter "; }
            if (periodicidade[3]) { dias += "Qua "; }
            if (periodicidade[4]) { dias += "Qui "; }
            if (periodicidade[5]) { dias += "Sex "; }
            if (periodicidade[6]) { dias += "Sab "; }
            var intervalo = $("#reservationEditar").val();
            var periodo = $("#IdListaPeriodoEditar option:selected").text();
            var turma = $("#IdListaTurmasEditar").val();
            var turmaTitle = $("#IdListaTurmasEditar option:selected").text();
            var salaTitle = $("#IdListaSalasEditar option:selected").text();
            var sala = salaTitle.split("|")[0];
            var ocupante = "" + $("#ocupanteEditar").val();
            if (result == "") {
                botao = "/Arquivos/images/icons/check.png"; aviso = "none";
                toastr.info('Novo intervalo adicionado com sucesso na tabela.', 'Adicionado', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 5000
                });
            }
            else {
                botao = "/Arquivos/images/icons/close.png"; aviso = "Conflitos com:\n" + "normal";
                toastr.warning('Houve um ou mais conflitos. Confira na tabela.', 'Atenção!', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 10000
                });
            }
            var table = $('#tableParcial').DataTable();
            var e = new Array(intervalo, periodo, dias, sala, turma, ocupante,
                "<button type='button' id='delete' class='btn btn-social-icon btn-warning' onclick='Remover(this); SalvarAny()'><i class='fa fa-trash'></i></button>",
                "<img src='" + botao + "' style='height:35px; width:35px;'>" +
                "<button style='display:" + aviso + "' type='button' class='btn btn-box-tool' data-toggle='tooltip'" + "title=" + "'" + result + "'" + "data-widget='chat-pane-toggle'> <big><i class='fa fa-info-circle'></i></big></button>");
            table.row.add(e).draw();
            alterarAdicionar();
            alterarAdicionados();
        },
        error: function (response) {
            toastr.error('Não foi possivel realizar a comunicação com o servidor.', 'Desculpe!', {
                "progressBar": true,
                "closeButton": true,
                "showMethod": "slideDown",
                "hideMethod": "slideUp",
                "timeOut": 5000
            });
        }
    });
}

function alterarAdicionados() {
    $('#tabela_novos_editados').boxWidget('expand');
}

function alterarAdicionar() {
    $('#caixa_adicionar_parcial').boxWidget('toggle');
}

function continuarEditando() {
    $('#caixa_adicionar_parcial').boxWidget('expand');
    $('#validar_parcial').boxWidget('expand');
    $('#tabela_novos_editados').boxWidget('expand');
    $('#tabela_validados').boxWidget('collapse');
    var table = $('#tableValidados').DataTable();
    table.clear().draw();
}

function SalvarAny() {
    var table = $('#tableParcial').DataTable();
    var trs = table.rows();
    if (trs.count() == 1) {
        $('#validar_parcial').boxWidget('collapse');
        $('#tabela_novos_editados').boxWidget('collapse');
        $('#caixa_adicionar_parcial').boxWidget('expand');
    }
}
function Validar() {
    $('#validar_parcial').boxWidget('collapse');
    $('#tabela_novos_editados').boxWidget('collapse');
    $('#caixa_adicionar_parcial').boxWidget('collapse');

    var tableValidados = $('#tableValidados').DataTable();
    var intervalo, periodicidade, periodo, turma, sala, ocupante = "", ensalamentos = new Array(), tabela, trs, tds;
    tabela = document.getElementById('table-body');
    trs = tabela.getElementsByTagName('tr');
    for (var i = 0; i < trs.length; i++) {
        var ensalamento = new Array();
        tds = trs[i].getElementsByTagName('td');
        if (tds.length <= 5) {
            break;
        }
        for (var j = 0; j < 6; j++) {
            ensalamento.push(tds[j].innerHTML);
        }
        ensalamentos.push(ensalamento);
    }
    $.ajax({
        url: 'CadastroEnsalamento/Validar',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            ensalamentos,
            idOriginal: $('#idEnsalamento').val()
        }),
        success: function (response) {
            if (response.length > 0) {
                if (response[0][0] == "false") {
                    toastr.error('Erro, a tabela está vazia!', 'Desculpe', {
                        "progressBar": true,
                        "closeButton": true,
                        "showMethod": "slideDown",
                        "hideMethod": "slideUp",
                        "timeOut": 5000
                    });
                    $('#validar_parcial').boxWidget('collapse');
                    $('#tabela_novos_editados').boxWidget('collapse');
                    $('#caixa_adicionar_parcial').boxWidget('expand');
                    $('#tabela_validados').boxWidget('toggle');
                }
                else {
                    for (var i = 0; i < response.length; i++) {
                        var ensalamento = new Array();
                        for (var j = 0; j < 6; j++) {
                            ensalamento.push(response[i][j]);
                        }
                        tableValidados.row.add(ensalamento).draw();
                    }
                    toastr.info('Confira os resultados na tabela.', 'Sucesso na validação', {
                        "progressBar": true,
                        "closeButton": true,
                        "showMethod": "slideDown",
                        "hideMethod": "slideUp",
                        "timeOut": 5000
                    });
                }
            }
            else {
                toastr.warning('Confira os resultados na tabela.', 'Ensalamento original completamente sobrescrito!', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 15000
                });
            }
            $('#tabela_validados').boxWidget('expand');
        },
        error: function (response) {
            toastr.error('Não foi possivel validar.', 'Desculpe', {
                "progressBar": true,
                "closeButton": true,
                "showMethod": "slideDown",
                "hideMethod": "slideUp",
                "timeOut": 5000
            });
            $('#validar_parcial').boxWidget('expand');
            $('#tabela_novos_editados').boxWidget('expand');
            $('#tabela_validados').boxWidget('collapse');
        }
    });
}

function Salvar() {
    var intervalo, periodicidade, periodo, turma, sala, ocupante = "", ensalamentos = new Array(), tabela, trs, tds;
    tabela = document.getElementById('table-body-validados');
    trs = tabela.getElementsByTagName('tr');
    for (var i = 0; i < trs.length; i++) {
        var ensalamento = new Array();
        tds = trs[i].getElementsByTagName('td');
        if (tds.length <= 5) {
            break;
        }
        for (var j = 0; j < 6; j++) {
            ensalamento.push(tds[j].innerHTML);
        }
        ensalamentos.push(ensalamento);
    }
    $.ajax({
        url: 'CadastroEnsalamento/Editar',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            ensalamentos,
            idOriginal: $('#idEnsalamento').val()
        }),
        success: function (response) {
            if (response === false) {
                toastr.error('Nenhuma alteração realizada, pois a lista está vazia!', 'Desculpe', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 5000
                });
            }
            else {
                CancelarWindowsForms();
                location.reload();
            }
        },
        error: function (response) {
            toastr.error('Não foi possivel realizar a edição!', 'Desculpe', {
                "progressBar": true,
                "closeButton": true,
                "showMethod": "slideDown",
                "hideMethod": "slideUp",
                "timeOut": 5000
            });
        }
    });

}

function Remover(item) {
    var tr = $(item).closest('tr');
    var table = $('#tableParcial').DataTable();
    tr.fadeOut(400, function () { table.row(tr).remove().draw(); });
}