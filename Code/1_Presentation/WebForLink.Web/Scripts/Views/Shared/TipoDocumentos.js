var acao = '';
var ID = '';

function TipoIncluir() {
    acao = 'Incluir';
    $('#divBotaoIncluir').hide();
    $('#divFormTipoDocumento').fadeIn('fast');
    $('#alert_confirm_TipoDocumento').hide();
}

function TipoEditar(id, descricao, ativo) {
    acao = 'Alterar';
    ID = id;

    $('#Descricao').val(descricao);
    $('#TipoDocumentoAtivo').prop('checked', ativo);
    $('#divBotaoIncluir').hide();
    $('#divFormTipoDocumento').fadeIn('fast');
    $('#alert_confirm_TipoDocumento').hide();
}

function TipoExcluir(id, descricao) {
    $('#divBotaoIncluir').hide();
    $('#divFormTipoDocumento').hide();
    $('#alert_confirm_TipoDocumento span').text('Tem certeza que deseja Excluir o Tipo de Documento "' + descricao + '"');
    $('#alert_confirm_TipoDocumento').hide().fadeIn('fast');

    acao = 'Excluir';
    ID = id;
}

function TipoCancelar() {
    acao = "";
    ID = "";

    $('#Descricao').val('');
    $('#TipoDocumentoAtivo').prop('checked', false);
    $('#divBotaoIncluir').fadeIn('fast');
    $('#divFormTipoDocumento').hide();
}

function TipoSalvar(url) {
    if (acao != 'Excluir') {
        if ($.trim($('#Descricao').val()) == '') {
            $('.alert-danger').text('Digite a Descrição do Tipo de Documento!');
            return false;
        }
    }

    $('#tblTipoDocumentos tbody').html('<tr><td colspan="3">Aguarde...</td></tr>');

    $.getJSON(url,
    {
        id: ID,
        descricaoTipoDocumento: $('#Descricao').val(),
        ativo: $("#TipoDocumentoAtivo").is(':checked'),
        acao: acao
    },
    function (data) {
        if (data.Erro != 0) {
            $('#divTipoDocumento .alert-danger').text(data.Msg);
            $('#divTipoDocumento .alert-danger').fadeIn('fast');
            setTimeout('$("#divTipoDocumento .alert-danger").fadeOut("slow")', 10000);
            return;
        }

        $('#tblTipoDocumentos tbody').html('');
        $.each(data.ListaTiposDocumentos, function (index, optionData) {
            var shtml = "";
            shtml += "<tr>"
            shtml += "<td>" + optionData.Descricao + "</td>";
            shtml += "<td class='text-center'><input " + ((optionData.Ativo) ? "checked=\"checked\"" : "") + " class=\"check-box\" disabled=\"disabled\" type=\"checkbox\"></td>";
            shtml += "<td class='text-center text-nowrap'>";
            if (optionData.IDCH == null) {
                shtml += "<button type=\"button\" class=\"btn btn-primary btn-xs\" onclick=\"TipoEditar({0}, '{1}', {2});\">Editar</button>".replace('{0}', optionData.ID).replace('{1}', optionData.Descricao).replace('{2}', optionData.Ativo);
                shtml += " <button type=\"button\" class=\"btn btn-danger btn-xs\" onclick=\"TipoExcluir({0}, '{1}');\">Excluir</button>".replace('{0}', optionData.ID).replace('{1}', optionData.Descricao);
            }
            shtml += "</td>";
            shtml += "</tr>";

            $('#tblTipoDocumentos > tbody:first').append(shtml);

            $('#Descricao').val('');
            $("#TipoDocumentoAtivo").prop('checked', false);
            $('#divBotaoIncluir').fadeIn('fast');
            $('#divFormTipoDocumento').hide();

            $('#divTipoDocumento .alert-success').text(data.Msg);
            $('#divTipoDocumento .alert-success').fadeIn('fast');
            setTimeout('$("#divTipoDocumento .alert-success").fadeOut("slow")', 5000);
        });
        montaListaTipoDocumento();
    });
}