var acaoDescricao = '';
var IdDescricao= '';

function DescricaoIncluir() {
    acaoDescricao = 'Incluir';
    $('#divBotaoIncluir').hide();
    $('#divFormDescricaoDocumento').fadeIn('fast');
    $('#alert_confirm_DescricaoDocumento').hide();
}

function DescricaoEditar(id, descricao, ativo) {
    acaoDescricao = 'Alterar';
    IdDescricao= id;

    $('#NomeDescricaoDocumento').val(descricao);
    $('#DescricaoDocumentoAtivo').prop('checked', ativo);
    $('#divBotaoIncluir').hide();
    $('#divFormDescricaoDocumento').fadeIn('fast');
    $('#alert_confirm_DescricaoDocumento').hide();
}

function DescricaoExcluir(id, descricao) {
    $('#divBotaoIncluir').hide();
    $('#divFormDescricaoDocumento').hide();
    $('#alert_confirm_DescricaoDocumento span').text('Tem certeza que deseja Excluir a Descrição do Documento "' + descricao + '"');
    $('#alert_confirm_DescricaoDocumento').hide().fadeIn('fast');

    acaoDescricao = 'Excluir';
    IdDescricao= id;
}

function DescricaoCancelar() {
    acaoDescricao = "";
    IdDescricao= "";

    $('NomeDescricaoDocumento').val('');
    $('#DescricaoDocumentoAtivo').prop('checked', false);
    $('#divBotaoIncluir').fadeIn('fast');
    $('#divFormDescricaoDocumento').hide();
}

function DescricaoSalvar(url) {
    if (acaoDescricao != 'Excluir') {
        if ($.trim($('#NomeDescricaoDocumento').val()) == '') {
            $('#divDescricaoDocumento .alert-danger').text('Digite a Descrição do Documento!');
            $('#divDescricaoDocumento .alert-danger').hide().fadeIn('fast');
            setTimeout('$("#divDescricaoDocumento .alert-danger").fadeOut("slow")', 5000);
            return false;
        }
    }

    $('#tblDescricaoDocumentos tbody').html('<tr><td colspan="3">Aguarde...</td></tr>');

    $.getJSON(url,
    {
        id: IdDescricao,
        tipoDocumentoIdSelecionado: $('#TipoDocumentoIdSelecionado').val(),
        nomeDescricaoDocumento: $('#NomeDescricaoDocumento').val(),
        ativo: $("#DescricaoDocumentoAtivo").is(':checked'),
        acao: acaoDescricao
    },
    function (data) {
        if (data.Erro != 0) {
            $('#divDescricaoDocumento .alert-danger').text(data.Msg);
            $('#divDescricaoDocumento .alert-danger').fadeIn('fast');
            setTimeout('$("#divDescricaoDocumento .alert-danger").fadeOut("slow")', 10000);
            return;
        }

        $('#tblDescricaoDocumentos tbody').html('');
        $.each(data.ListaDescricaoDocumentos, function (index, optionData) {
            var shtml = "";
            shtml += "<tr>"
            shtml += "<td>" + optionData.Descricao + "</td>";
            shtml += "<td class='text-center'><input " + ((optionData.Ativo) ? "checked=\"checked\"" : "") + " class=\"check-box\" disabled=\"disabled\" type=\"checkbox\"></td>";
            shtml += "<td class='text-center text-nowrap'>";
            if (optionData.IDCH == null) {
                shtml += "<button type=\"button\" class=\"btn btn-primary btn-xs\" onclick=\"DescricaoEditar({0}, '{1}', {2});\">Editar</button>".replace('{0}', optionData.ID).replace('{1}', optionData.Descricao).replace('{2}', optionData.Ativo);
                shtml += "<button type=\"button\" class=\"btn btn-danger btn-xs\" onclick=\"DescricaoExcluir({0}, '{1}');\">Excluir</button>".replace('{0}', optionData.ID).replace('{1}', optionData.Descricao);
            }
            shtml += "</td>";
            shtml += "</tr>";

            $('#tblDescricaoDocumentos > tbody:first').append(shtml);

            $('NomeDescricaoDocumento').val('');
            $("#DescricaoDocumentoAtivo").prop('checked', false);
            $('#divBotaoIncluir').fadeIn('fast');
            $('#divFormDescricaoDocumento').hide();

            $('#divDescricaoDocumento .alert-success').text(data.Msg);
            $('#divDescricaoDocumento .alert-success').fadeIn('fast');
            setTimeout('$("#divDescricaoDocumento .alert-success").fadeOut("slow")', 5000);
        });

        montaListaDescricaoDocumento();
    });
}