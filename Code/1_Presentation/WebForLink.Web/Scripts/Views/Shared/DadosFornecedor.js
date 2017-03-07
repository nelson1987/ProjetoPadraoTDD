function incluirDados(strTipo, strUrlAction) {
    $.ajax({
        type: "POST",
        url: strUrlAction,
        dataType: "html",
        success: function (data, textStatus, jqXHR) {
            renderizarCampos(strTipo, data);
            $('.TelefoneMask').inputmask({ mask: ['(99) 9999-9999', '(99) 99999-9999'] });
        },
        error: function (data, textStatus, jqXHR) {
        },
    });
}

function editarDados(strTipo, strUrlAction, intTipoFluxoId) {
    console.log("1", strTipo);
    console.log("2", strUrlAction);
    console.log("3", intTipoFluxoId);
    $('#divAguarde' + strTipo).show();
    $.ajax({
        type: "POST",
        url: strUrlAction,
        dataType: "html",
        data: obterDadosPost(intTipoFluxoId),
        success: function (data, textStatus, jqXHR) {
            renderizarDados(strTipo, data);
        },
        error: function (data, textStatus, jqXHR) {
        }
    });
}

function salvarDados(strTipo, strUrlAction) {
    $.ajax({
        type: "POST",
        url: strUrlAction,
        dataType: "html",
        success: function (data, textStatus, jqXHR) {
            renderizarDados(strTipo, data);
        },
        error: function (data, textStatus, jqXHR) {
        }
    });
}

function cancelarDados(strTipo, strUrlAction, intTipoFluxoId) {
    $.ajax({
        type: "POST",
        url: strUrlAction,
        dataType: "html",
        data: obterDadosPost(intTipoFluxoId),
        success: function (data, textStatus, jqXHR) {
            renderizarDados(strTipo, data);
        },
        error: function (data, textStatus, jqXHR) {
        }
    });
}

function consultarSolicitacoesEmAberto(strTipo, intTipoFluxoId) {
    //Unspsc não precisa verificar se já existe solicitação em tramite. Unspsc não cria solicitacaoDeDocumentos
    if (strTipo != "Unspsc") {
        $.ajax({
            type: "POST",
            url: "/Solicitacao/ConsultarSolicitacoesEmAberto",
            dataType: "json",
            data: obterDadosPost(intTipoFluxoId),
            success: function (data, textStatus, jqXHR) {
                if (data.length > 0)
                    exibirJanelaConfirmacao(strTipo);
                else {
                    if (intTipoFluxoId == 150)
                        editarDados(strTipo, "/DadosEnderecos/Editar", intTipoFluxoId);
                    //editarDados(strTipo, "/DadosEnderecos/Editar" + strTipo, intTipoFluxoId);
                    if (intTipoFluxoId == 90)
                        editarDados(strTipo, "/DadosBancarios/Editar", intTipoFluxoId);
                        //editarDados(strTipo, "/DadosBancarios/Editar" + strTipo, intTipoFluxoId);
                    else
                        editarDados(strTipo, "/Fornecedores/Editar" + strTipo, intTipoFluxoId);
                    //editarDados(strTipo, "/Fornecedores/Editar" + strTipo, intTipoFluxoId);
                }

            },
            error: function (data, textStatus, jqXHR) {
            }
        });
    }
    else {
        editarDados(strTipo, "/Fornecedores/Editar" + strTipo, intTipoFluxoId);
    }
}

function obterDadosPost(intTipoFluxoId) {
    var ret = {
        contratanteID: $("#ContratanteID").val(),
        fornecedorID: $("#FornecedorID").val(),
        contratanteFornecedorID: $("#ContratanteFornecedorID").val(),
        tipoFluxoID: intTipoFluxoId,
        categoriaID: $("#CategoriaContratanteID").val(),
        TpPapel: $("#TpPapel").val()
    };

    return ret;
}

function excluirDados(objBotao) {
    var objItemLista = $(objBotao).parent().parent().parent();

    objItemLista.remove();
}

function renderizarDados(strTipo, data) {
    //console.log(data);
    switch (strTipo) {
        case "DadosBancarios":
            $("#dadosBancarios").html(data);
            aplicarPluginUpload($('#dadosBancarios').find('.upload'), '#CNPJ_CPF');
            break;
        case "DadosEnderecos":
            $("#dadosEnderecos").html(data);
            break;
        case "DadosContatos":
            $("#dadosContatos").html(data);
            aplicarMascaras(data);
            break;
        case "Documentos":
            $("#documentos").html(data);
            aplicarPluginUpload($('#documentos').find('.upload'), '#CNPJ_CPF');
            break;
        case "Unspsc":
            $("#Unspsc").html(data);
            break;
        case "QuestionarioDinamico":
            $("#dadosQuestionarioDinamico").html(data);
            break;
        case "DocumentosSolicitacao":
            $("#dv_ViewDocumento").html(data);
            break;
    }
}

function renderizarCampos(strTipo, data) {
    switch (strTipo) {
        case "DadosBancarios":
            $('#divBancos').append(data);
            aplicarPluginUpload($('#divBancos').children().last().find('.upload'), '#CNPJ_CPF');
            break;
        case "DadosEnderecos":
            $('#divEnderecos').append(data);
            break;
        case "DadosContatos":
            $("#divContatos").append(data);
            aplicarMascaras(data);
            break;
        case "Documentos":
            $("#divDocumentos").children().append(data);
            aplicarMascaras(data);
            break;
        case "QuestionarioDinamico":
            $("#divQuestionarioDinamico").children().append(data);
            break;
    }
}

function renderizarDadosAjaxBeginForm(data, status, xhr) {
    var strNomeDiv = "";
    switch (xhr) {
        case "DadosBancarios":
            strNomeDiv = "#dadosBancarios";
            break;
        case "DadosEnderecos":
            strNomeDiv = "#dadosEnderecos";
            break;
        case "DadosContatos":
            strNomeDiv = "#dadosContatos";
            break;
        case "Documentos":
            strNomeDiv = "#documentos";
            break;
        case "Unspsc":
            strNomeDiv = "#Unspsc";
            break;
        case "QuestionarioDinamico":
            strNomeDiv = "#questionarioDinamico";
            break;
        case "DocumentosSolicitacao":
            strNomeDiv = "#dv_ViewDocumento";
            break;
    }

    $(strNomeDiv).html(data);

    //TO DO: VERIFICAR O MOTIVO DE CHAMAR A FUNÇÃO HIDE() APÓS A EXECUÇÃO DO POST REALIZADO PELO AJAX.BEGINFORM.
    //       COM A UTILIZAÇÃO DO HTML.BEGINFORM ESTA CHAMADA NÃO ERA NECESSÁRIA.
    $(".aguarde").hide();

    exibirMensagemSucesso(xhr);
}

function exibirMensagemSucesso(strTipo) {
    var strNomeDivSucesso = "#alertaSucesso" + strTipo,
        objDivSuccesso = $(strNomeDivSucesso),
        strMensagem = "A Solicitação de Modificação foi enviada com sucesso.";

    if (strTipo == "Unspsc")
        strMensagem = "Modificação realizada com sucesso. Todos os seus clientes visualizarão esta atualização!";

    $(objDivSuccesso).fadeIn('slow');
    $(objDivSuccesso).html(strMensagem);

    setTimeout(function () {
        $(objDivSuccesso).fadeOut('slow');
    }, 10000);
}

function ocultarMensagem(objDivOrigem) {
    $(objDivOrigem).fadeOut('fast');
}

function exibirJanelaConfirmacao(strTipo) {
    var strNomeJanela = '#confirmacao' + strTipo,
        strSpan = strNomeJanela + ' span',
        strMensagem = "Já existe(m) uma ou mais Solicitações de Modificação para esta seção. Tem certeza de que deseja realizar uma nova modificação?";

    console.log("Teste ", strNomeJanela);

    $(strSpan).text(strMensagem);

    $(strNomeJanela).toggleClass("hidden");
    $(strNomeJanela).fadeIn("fast");

    //$('#btnSimFicha').focus();
}

function ocultarJanelaConfirmacao(strTipo) {
    //event.preventDefault();
    var strNomeJanela = '#confirmacao' + strTipo;

    $(strNomeJanela).fadeOut('fast');
    $(strNomeJanela).toggleClass("hidden");
}

function exibirConfirmacaoExclusao(obj) {
    //event.preventDefault();
    var objli = $(obj).parent().parent();
    var alert = $(objli).find(".alert");
    $(alert).toggleClass("hidden");
    //$(obj).toggleClass("disabled");

}

function aplicarMascaras(data) {
    var objItemLista = (typeof (data) != "object") ? $.parseHTML(data) : data,
        strGUID = $(objItemLista).children('input').val(),
        objInputTextTelefone = $("#DadosContatos_" + strGUID + "__Telefone"),
        objInputTextCelular = $("#DadosContatos_" + strGUID + "__Celular");

    objInputTextTelefone.inputmask('(99) 9999[9]-9999', { greedy: false });
    objInputTextCelular.inputmask('(99) 9999[9]-9999', { greedy: false });
}