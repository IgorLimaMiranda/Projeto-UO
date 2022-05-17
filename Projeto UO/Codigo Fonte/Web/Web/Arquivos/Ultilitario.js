$(function () {
    
    //$.fn.dataTable.moment('ddmmYYYY');

    var tabela_curso = $('#tab_cadastro_curso').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true,
        'selected': true
    });
    $('#tab_cadastro_curso tbody').on('click', 'tr', function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        }
        else {
            tabela_curso.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });

    $('#tab_dashboard_logistica').DataTable({
        'dom': "rBtlp",
        'paging': true,
        'lengthChange': true,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': true,
        buttons: [
            {
                extend: 'copy',
                className: 'btn',
                text: '<i class="fa fa-files-o"></i> COPIAR',
                titleAttr: 'COPIAR',
                exportOptions: {
                    columns: [0, 1, 2, 3],
                    stripHtml: true
                }
            },
            {
                extend: 'excel',
                className: 'btn',
                text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                titleAttr: 'EXCEL',
                exportOptions: {
                    columns: [0, 1, 2, 3],
                    stripHtml: true
                }
            },
            {
                extend: 'pdf',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: [0, 1, 2, 3],
                    stripHtml: true
                }
            },
            {
                extend: 'print',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-print"></i> IMPRIMIR',
                titleAttr: 'IMPRIMIR',
                exportOptions: {
                    columns: [0, 1, 2, 3],
                    stripHtml: false
                }
            }
        ]
    })
    $('#tab_cadastro_turma').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    })
    $('#tab_cadastro_recurso').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    })
    $('#tab_cadastro_setor').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    })
    $('#tab_cadastro_usuario').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    })
    $('#tab_recurso_sala').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    })

    var tabela_sala = $('#tab_cadastro_sala').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    });
    $('#tab_cadastro_sala tbody').on('click', 'tr', function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        }
        else {
            tabela_sala.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });

    var tablePesquisaEnsalamento = $('#tab_pesquisa_ensalamento').DataTable({
        lengthChange: false,
        "order": [7, "desc"],
        buttons: [
            {
                extend: 'copy',
                className: 'btn',
                text: '<i class="fa fa-files-o"></i> COPIAR',
                titleAttr: 'COPIAR',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5, 6, 7]
                }
            },
            {
                extend: 'excel',
                className: 'btn',
                text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                titleAttr: 'EXCEL',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdf',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5, 6, 7]
                }
            },
            {
                extend: 'print',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-print"></i> IMPRIMIR',
                titleAttr: 'IMPRIMIR',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5, 6, 7]
                }
            }
        ]
    });
    tablePesquisaEnsalamento.buttons().container().appendTo('#tab_pesquisa_ensalamento_wrapper .col-sm-6:eq(0)');

    var tableCadastroEnsalamento = $('#tab_cadastro_ensalamento').DataTable({
        lengthChange: false,
        "dom": "Bfrtlp",
        "order": [7, "asc"],
        buttons: [
            {
                extend: 'copy',
                className: 'btn',
                text: '<i class="fa fa-files-o"></i> COPIAR',
                titleAttr: 'COPIAR',
                exportOptions: {
                    columns: [0, 1, 3, 5, 7]
                }
            },
            {
                extend: 'excel',
                className: 'btn',
                text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                titleAttr: 'EXCEL',
                exportOptions: {
                    columns: [0, 1, 3, 5, 7]
                }
            },
            {
                extend: 'pdf',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: [0, 1, 3, 5, 7]
                }
            },
            {
                extend: 'print',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-print"></i> IMPRIMIR',
                titleAttr: 'IMPRIMIR',
                exportOptions: {
                    columns: [0, 1, 3, 5, 7]
                }
            }
        ]
    });
    tableCadastroEnsalamento.buttons().container().appendTo('#tab_cadastro_ensalamento_wrapper .col-sm-6:eq(0)');

    $('#example').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'
        ],
        columnDefs: [{
            targets: -1,
            visible: false
        }]
    });

    var tableCadastroocorrencia = $('#tab_cadastro_ocorrencia').DataTable({
        lengthChange: false,
        "order": [7, "asc"],
        buttons: [
            {
                extend: 'copy',
                className: 'btn',
                text: '<i class="fa fa-files-o"></i> COPIAR',
                titleAttr: 'COPIAR',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excel',
                className: 'btn',
                text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                titleAttr: 'EXCEL',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'pdf',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }

            },
            {
                extend: 'print',
                className: 'btn',
                download: 'open',
                text: '<i class="fa fa-print"></i> IMPRIMIR',
                titleAttr: 'IMPRIMIR',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }
        ]
    });
    tableCadastroocorrencia.buttons().container().appendTo('#tab_cadastro_ocorrencia_wrapper .col-sm-6:eq(0)');

    $('.date-picker').datepicker({
        format: 'yyyy',
        autoclose: true,
        forceParse: true,
        inmediateUpdates: true,
        keyboardNavigation: true,
        language: 'pt-br',
        startView: 'decade',
        viewSelect: 'decade',
        minView: 'years',
        viewMode: "years",
        minViewMode: "years"
    });

    $(".custom-checkbox").click(function () {
        var activeInput = $(this).children("input");

        if (activeInput.is(':checked')) {
            $(activeInput).prop("checked", false);
        } else {
            $(activeInput).prop("checked", true);
        }

        if (activeInput.is('[type=radio]')) {
            var nonActiveInput = $(this).siblings().children("input");
            $(nonActiveInput).prop("checked", false);
        }
    });

    $('#caixa_filtro').boxWidget('collapse');
    $('#caixa_adicionar_parcial').boxWidget('collapse');
    $('#reservationeditar').daterangepicker();
    $('#data_ranger').daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Limpar'
        }
    });
    $('#data_ranger').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
    });
    $('#data_ranger').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

    $(".custom-checkbox").click(function () {
        var activeInput = $(this).children("input");

        if (activeInput.is(':checked')) {
            $(activeInput).prop("checked", false);
        } else {
            $(activeInput).prop("checked", true);
        }

        if (activeInput.is('[type=radio]')) {
            var nonActiveInput = $(this).siblings().children("input");
            $(nonActiveInput).prop("checked", false);
        }
    });
});

// Alterar icone pesquisa Ensalamento/Ocorrência
$(function () {
    $("#toggle-button").click(function () {
        $(this).find('i').toggleClass('fa-angle-down fa-2x fa-angle-up fa-2x')
    });
});

function abrirCaixa() {
    $('#caixa_filtro').boxWidget('toggle');
}
function abrirCaixaParcial() {
    $('#caixa_adicionar_parcial').boxWidget('toggle');
}

//DATE PICKER DO SISTEMA

function formatarData(src, mask, e) {
    var tecla = ""
    if (document.all) {
        tecla = event.keyCode;
    } else {
        tecla = e.which;
    }
    if (tecla != 8) {
        if (src.value.length == src.maxlength) {
            return;
        }
        var i = src.value.length;
        var saida = mask.substring(0, 1);
        var texto = mask.substring(i);
        if (texto.substring(0, 1) != saida) {
            src.value += texto.substring(0, 1);
        }
    }

}

function minha_funcao_cadastro_curso() {
    document.getElementById("nome_curso").value = "";
    document.getElementById("sigla_curso").value = "";
}

function minha_funcao_cadastro_incidente() {
    document.getElementById("descricao_incidente").value = "";
}

function minha_funcao_cadastro_local() {
    document.getElementById("nome_andar").value = "";
    document.getElementById("nome_local").value = "";
}

function minha_funcao_cadastro_setor() {
    $("#nome_setor").val("");
    $("#combo_box_andar_setor").val("");

}

function limpar_funcao_cadastro_recurso() {
    document.getElementById("descricao_recurso").value = "";
}

function minha_funcao_cadastro_sala() {
    document.getElementById("nome_numero").value = "";
    document.getElementById("nome_andar").value = "";
    document.getElementById("nome_capacidade").value = "";
    document.getElementById("nome_sala").value = "";

}

function minha_funcao_cadastro_turma() {
    document.getElementById("nome_curso").value = "";
    document.getElementById("ano_turma").value = "";
    document.getElementById("nome_turma").value = "";
}

function minha_funcao_cadastro_usuario() {
    document.getElementById("nome_confirmar_senha").value = "";
    document.getElementById("nome_senha").value = "";
    document.getElementById("nome_email").value = "";
    document.getElementById("nome_completo").value = "";
    document.getElementById("nome_cpf").value = "";
}

function EixoVisivel() {
    if (document.getElementById("combo_box_tipo_usuario").selectedIndex == 5) {
        document.getElementById("eixovisivelprofessor").style.display = 'block';
    } else {
        document.getElementById("eixovisivelprofessor").style.display = "none";
    }
}

function EixoVisivelVisualizar() {
    if (document.getElementById("usuario_tipo").value == "Professor(a)") {
        document.getElementById("eixo_visivel").style.display = 'block';
    }
}