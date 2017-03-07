function preencherTipoFuncionalidade(enumTipoFuncionalidade) {
    $("#TipoFuncionalidade").val(enumTipoFuncionalidade);
}

function gerenciarSelecionados(strId, objCheckBox) {
    var strSelecionados = $("#Selecionados").val(),
        strSelecionadosDetalhes = $("#SelecionadosDetalhes").val(),
        arrSelecionados = null,
        arrSelecionadosDetalhes = null,
        boolCheckBoxChecked = objCheckBox.checked,
        boolIncluirRelatorio = false;

    if (strSelecionados != "") {
        arrSelecionados = strSelecionados.split(",");
        arrSelecionadosDetalhes = JSON.parse(strSelecionadosDetalhes);
    }
    else {
        arrSelecionados = new Array(strId);

        var objFornecedorDetalhe = {
            FornecedorBase: {
                Id: strId,
                CNPJCPF: $("#CNPJ_CPF_" + strId).html(),
                RazaoSocialNome: $("#RazaoSocial_Nome_" + strId).html()
            }
        }

        arrSelecionadosDetalhes = new Array(objFornecedorDetalhe);
        boolIncluirRelatorio = true;
    }

    var intIndex = arrSelecionados.indexOf(strId);

    if (intIndex < 0) {
        arrSelecionados.push(strId);

        arrSelecionadosDetalhes.push({
            FornecedorBase: {
                Id: strId,
                CNPJCPF: $("#CNPJ_CPF_" + strId).html(),
                RazaoSocialNome: $("#RazaoSocial_Nome_" + strId).html()
            }
        });

        boolIncluirRelatorio = true;
    }
    else if (!boolCheckBoxChecked) {
        arrSelecionados.splice(intIndex, 1);
        arrSelecionadosDetalhes.splice(intIndex, 1);
    }

    if (arrSelecionados.length > 0)
        habilitarBotaoFuncionalidade();
    else
        desabilitarBotaoFuncionalidade();

    gerenciarRelatorio(strId, boolIncluirRelatorio);
    preencherBadge(arrSelecionados.length);

    $("#Selecionados").val(arrSelecionados.toString());
    $("#SelecionadosDetalhes").val(JSON.stringify(arrSelecionadosDetalhes));
}

function gerenciarRelatorio(strId, boolIncluir) {
    if (boolIncluir) {
        var strCNPJCPF = $("#CNPJ_CPF_" + strId).html(),
            strRazaoSocialNome = $("#RazaoSocial_Nome_" + strId).html(),
            objLista = $("#relatorioSelecionados ul"),
            objItem = "<li id=\"Item_" + strId + "\">" + "<b>" + strCNPJCPF + "</b>" + "<br/>" + strRazaoSocialNome + "</li>";

        $(objLista).append($.parseHTML(objItem));
    } else {
        var arrItens = $("#relatorioSelecionados ul").children();

        arrItens.each(function () {
            var id = obterId(this);

            if (id == ("Item_" + strId))
                $(this).detach();
        });
    }
}

function gerenciarRelatorio2(objFornecedorBase, boolIncluir) {
    if (boolIncluir) {
        var objLista = $("#relatorioSelecionados ul"),
                objItem = "<li id=\"Item_" + objFornecedorBase.Id + "\">" +
                "<b>" + objFornecedorBase.CNPJCPF + "</b>" + "<br/>" +
                objFornecedorBase.RazaoSocialNome + "</li>";

        $(objLista).append($.parseHTML(objItem));
    } else {
        var arrItens = $("#relatorioSelecionados ul").children();

        arrItens.each(function () {
            var id = obterId(this);

            if (id == ("Item_" + strId))
                $(this).detach();
        });
    }
}

function selecionarCheckBoxes() {
    var checkboxes = $("tbody").find(":checkbox"),
        strSelecionados = $("#Selecionados").val(),
        arrSelecionados = (strSelecionados != "") ? strSelecionados.split(",") : null,
        strSelecionadosDetalhes = $("#SelecionadosDetalhes").val(),
        arrSelecionadosDetalhes = (strSelecionadosDetalhes != "") ? JSON.parse(strSelecionadosDetalhes) : null;

    if (arrSelecionados != null) {
        checkboxes.each(function () {
            var strId = obterId(this),
                intIndex = arrSelecionados.indexOf(strId);

            if (intIndex > -1) {
                $(this).prop('checked', true);
            }
        });

        habilitarBotaoFuncionalidade();
        preencherBadge(arrSelecionados.length);
        atualizarRelatorio(arrSelecionadosDetalhes);
    }
}

function atualizarRelatorio(arrSelecionadosDetalhes) {
    for (var i = 0; i < arrSelecionadosDetalhes.length; i++) {
        gerenciarRelatorio2(arrSelecionadosDetalhes[i].FornecedorBase, true);
    }
}

function selecionarTodos(objCheckBox) {
    var boolCheck = false;

    if (objCheckBox.checked)
        boolCheck = true;

    var checkboxes = $("tbody").find(":checkbox");

    checkboxes.each(function () {
        var strId = obterId(this);

        $(this).prop('checked', boolCheck);
        gerenciarSelecionados(strId, this);
    });
}

function obterId(objCheckBox) {
    return $(objCheckBox).attr("id").toString();
}

function preencherBadge(intSelecionados) {
    $(".badge").html(intSelecionados);
}

function alternarCampos() {
    $("#camposPessoaFisica").toggleClass("hidden");
    $("#camposPessoaJuridica").toggleClass("hidden");
}

function exibirOcultarCategoria() {
    var objDiv = $("#Filtro_CategoriaId").parents('div')[1];
    $(objDiv).toggleClass("hidden");
}

function exibirConfirmacao(strMensagem) {
    $("#confirmacao span").text(strMensagem);
    $("#confirmacao").toggleClass("hidden");
    $("#confirmacao").fadeIn("fast");
}

function ocultarConfirmacao() {
    $("#confirmacao").fadeOut("fast");
    $("#confirmacao").toggleClass("hidden");
}

function habilitarBotaoFuncionalidade() {
    $("#BotaoFuncionalidade").prop("disabled", false);
    $("#BotaoFuncionalidadeFalse").prop("disabled", false);
    $("#BotaoFuncionalidadeFake").prop("disabled", false);
}

function desabilitarBotaoFuncionalidade() {
    $("#BotaoFuncionalidade").prop("disabled", true);
    $("#BotaoFuncionalidadeFalse").prop("disabled", true);
    $("#BotaoFuncionalidadeFake").prop("disabled", true);
}
//function mudarValorFuncao(botao, valor) {
//    $("#TipoFuncionalidade").val(valor);
//}