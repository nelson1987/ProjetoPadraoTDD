function EmpresaOrganizacaoCompras(e) {
    var selectorVal = "";

    if (e === "") {
        selectorVal = $('#Empresa').val();
    } else {
        selectorVal = e;
    }

    $.ajax({
        url: '/OrganizacaoCompra/Listar',
        data: {
            empresa: selectorVal
        },
        type: "POST",
        dataType: "json",
        async: true,
        success: function (data) {
            $('#Compras').empty();
            $.each(data, function (index, data) {
                $('#Compras').append('<option value="' + data.Value + '">' + data.Text + ' </option>');
            });
            $("#Compras option").show();
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}

function adicionarContato() {
    var shtml
    shtml = '<div id="contato" name="contato">';
    shtml += '<div class="col-xs-6 col-md-6">';
    shtml += '<input type="hidden" id="ContatoID" name="ContatoID" value="0" />'
    shtml += '<div class="form-group">';
    shtml += '<div class="input-group">';
    shtml += '<div class="input-group-btn">';
    shtml += '<button type="button" class="btn btn-danger" onclick="removerContato(this);"><i class="fa fa-trash"></i></button>';
    shtml += '</div>';
    shtml += '<input type="text" class="form-control input-sm" name="NomeContato" value=""/>';
    shtml += '</div>';
    shtml += '</div>';
    shtml += '</div>';
    shtml += '<div class="col-xs-6 col-md-6">';
    shtml += '<div class="form-group">';
    shtml += '<input type="text" class="form-control input-sm" name="EmailContato" value=""/>';
    shtml += '</div>';
    shtml += '</div>';
    shtml += '</div>';
    $('#divContatos').append(shtml);
}

function removerContato(o) {
    $(o).parent('div').parent('div').parent('div').parent('div').parent('div').remove();
}

function TipoFornecedor(limpar, strTipoCadastro) {
    var tp = $('input:radio[name=TipoFornecedor]').filter(":checked").val(),
        bolForcarCadastroDireto = false;

    if (tp == 1) {
        if (limpar) {
            $('#CNPJ').val('');
            $('#Telefone').val('');
            $('#DataNascimento').val('');
        }

        $('#CNPJ').inputmask('99.999.999/9999-99');
        $('#Telefone').inputmask({ mask: ['(99) 9999-9999', '(99) 99999-9999'] });
        $('#CNPJ').prop("disabled", "");
        $('#cnpj_alert').show();
        $('#divRazaoSocial').hide();
        $('#EmailValidacao').show();
        $('#EmailValidacaoMensagem').show();
        $('#callReceita').show();
        $('#callReceitaSintegra').show();
        $('#callSimples').show();
        $('#callReceitaCPF').hide();
        $('.collapse').collapse();
        $('#divDataNascimento').hide();
        $('#btnConsultaRobo').prop("disabled", false);
        $('#divFormCriacao').hide();
    }
    if (tp == 2) {
        if (limpar) {
            $('#CNPJ').val('');
            $('#Telefone').val('');
            $('#DataNascimento').val('');
        }
        $('#Telefone').inputmask({ mask: ['99+ (99) 9999-9999', '99+ (99) 99999-9999'] });
        $('#CNPJ').prop("disabled", "disabled");
        $('#cnpj_alert').hide();
        $('#divRazaoSocial').show();
        $('#EmailValidacao').hide();
        $('#EmailValidacaoMensagem').hide();
        $('#btnConsultaRobo').prop("disabled", true);
        $('#divFormCriacao').show();
        $('#divDataNascimento').hide();
        bolForcarCadastroDireto = true;
        strTipoCadastro = 2;
    }
    if (tp == 3) {
        if (limpar) {
            $('#CNPJ').val('');
            $('#Telefone').val('');
            $('#DataNascimento').val('');
        }
        $('#CNPJ').inputmask('999.999.999-99');
        $('#Telefone').inputmask({ mask: ['(99) 9999-9999', '(99) 99999-9999'] });
        $('#CNPJ').prop("disabled", "");
        $('#cnpj_alert').show();
        $('#divRazaoSocial').hide();
        $('#EmailValidacao').show();
        $('#EmailValidacaoMensagem').show();
        $('#callReceita').hide();
        $('#callReceitaSintegra').hide();
        $('#callSimples').hide();
        $('#callReceitaCPF').show();
        $('.collapse').collapse();
        $('#divDataNascimento').show();
        $('#btnConsultaRobo').prop("disabled", false);
        $('#divFormCriacao').hide();
    }
    manipularTipoCadastro(bolForcarCadastroDireto, strTipoCadastro);
}

function SolicitarValidacao() {
    var tipoFornecedor = $("input[name='TipoFornecedor']:checked").val();
    $('#alert_confirm').fadeIn('fast');

    if (tipoFornecedor != 2)
        $('#alert_confirm span').text('Os dados serão enviados para consulta no robô da Receita Federal. Esta validação pode demorar.');
    else
        $('#alert_confirm span').text('Tem certeza que deseja Solicitar a criação deste Fornecedor?');
}

function manipularTipoCadastro(bolForcarCadastroDireto, strTipoCadastro) {
    var strRadioTipoCadastroChecked = 'input[name=TipoCadastro][value=' + strTipoCadastro + ']';
    $(strRadioTipoCadastroChecked).prop("checked", true);
    $('input:radio[name=TipoCadastro]').each(function (i) {
        if (!bolForcarCadastroDireto)
            $(this).prop("disabled", false)
        else
            $(this).prop("disabled", true)
    });
}