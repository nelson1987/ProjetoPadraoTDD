var codeCNPJ, CNPJSit, codeCPF, CPFSit, codeSintegra;
var urlReceitaCNPJ = '/Consulta/ReceitaFederalCNPJ';
var urlReceitaCPF = '/Consulta/ReceitaFederalCPF';
var urlSintegra = '/Consulta/Sintegra';
var urlSimples = '/Consulta/SimplesNacional';

function LimpaClassConsultaInterna() {
    $('#callInterna').removeClass("panel-default");
    $('#callInterna').removeClass("panel-sucess");
    $('#callInterna').removeClass("panel-warning");
    $('#callInterna').removeClass("panel-danger");
    $("#callInternaHeading").removeClass("panel-heading-default");
    $("#callInternaHeading").removeClass("panel-heading-success");
    $("#callInternaHeading").removeClass("panel-heading-warning");
    $("#callInternaHeading").removeClass("panel-heading-danger");
}

function LimpaClassConsultaReceitaCNPJ() {
    $('#callReceita').removeClass("panel-default");
    $('#callReceita').removeClass("panel-sucess");
    $('#callReceita').removeClass("panel-warning");
    $('#callReceita').removeClass("panel-danger");
    $("#callReceitaHeading").removeClass("panel-heading-default");
    $("#callReceitaHeading").removeClass("panel-heading-success");
    $("#callReceitaHeading").removeClass("panel-heading-warning");
    $("#callReceitaHeading").removeClass("panel-heading-danger");

    $('#callReceita').addClass("panel-default");
    $("#callReceitaHeading").addClass("panel-heading-default");
}

function LimpaClassConsultaReceitaCPF() {
    $('#callReceitaCPF').removeClass("panel-default");
    $('#callReceitaCPF').removeClass("panel-sucess");
    $('#callReceitaCPF').removeClass("panel-warning");
    $('#callReceitaCPF').removeClass("panel-danger");
    $("#callReceitaCPFHeading").removeClass("panel-heading-default");
    $("#callReceitaCPFHeading").removeClass("panel-heading-success");
    $("#callReceitaCPFHeading").removeClass("panel-heading-warning");
    $("#callReceitaCPFHeading").removeClass("panel-heading-danger");

    $('#callReceitaCPF').addClass("panel-default");
    $("#callReceitaCPFHeading").addClass("panel-heading-default");
}

function LimpaClassConsultaSintegra() {
    $('#callReceitaSintegra').removeClass("panel-default");
    $('#callReceitaSintegra').removeClass("panel-sucess");
    $('#callReceitaSintegra').removeClass("panel-warning");
    $('#callReceitaSintegra').removeClass("panel-danger");
    $("#callReceitaSintegraHeading").removeClass("panel-heading-default");
    $("#callReceitaSintegraHeading").removeClass("panel-heading-success");
    $("#callReceitaSintegraHeading").removeClass("panel-heading-warning");
    $("#callReceitaSintegraHeading").removeClass("panel-heading-danger");

    $('#callReceitaSintegra').addClass("panel-default");
    $("#callReceitaSintegraHeading").addClass("panel-heading-default");
}

function LimpaClassConsultaSimples() {
    $('#callSimples').removeClass("panel-default");
    $('#callSimples').removeClass("panel-sucess");
    $('#callSimples').removeClass("panel-warning");
    $('#callSimples').removeClass("panel-danger");
    $("#callSimplesHeading").removeClass("panel-heading-default");
    $("#callSimplesHeading").removeClass("panel-heading-success");
    $("#callSimplesHeading").removeClass("panel-heading-warning");
    $("#callSimplesHeading").removeClass("panel-heading-danger");

    $('#callSimples').addClass("panel-default");
    $("#callSimplesHeading").addClass("panel-heading-default");
}

function ConsultaRobo() {
    LimpaClassConsultaReceitaCNPJ();
    LimpaClassConsultaReceitaCPF();
    LimpaClassConsultaSintegra();
    LimpaClassConsultaSimples();

    $('#CodeCNPJ').val("");
    $('#CNPJSit').val("");
    $('#CodeCPF').val("");
    $('#CPFSit').val("");
    $('#CodeSintegra').val("");
    $('#CodeSimples').val("");

    $('#divOrgaosPublico').show();
    var tipoFornecedor = $("input[name='TipoFornecedor']:checked").val();

    if (tipoFornecedor == undefined || tipoFornecedor == "") {
        tipoFornecedor = $('#TipoFornecedor').val();
    }

    if (tipoFornecedor == 1) {
        ConsultaReceitaCNPJ();
    }
    else if (tipoFornecedor == 3) {
        ConsultaReceitaCPF();
    }
}

function ConsultaInterna() {
    $('.collapse').collapse('hide');

    $("#callInterna").addClass("panel-default");
    $("#callInternaHeading").addClass("panel-heading-default");

    $("#callInternaHeading").children("h4").children("a").children("span").empty();
    $("#callInternaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp; <i class='fa fa-refresh fa-spin'></i> Aguarde, validando CNPJ...");

    $.ajax({
        url: '/Fornecedores/ValidarInternaCNPJ',
        timeout: 120000,
        data: {
            cnpj: $('#CNPJ').val(),
            contratante: $('#Empresa').val(),
            tipoFornecedor: $('input:radio[name=TipoFornecedor]').filter(":checked").val(),
            categoria: $('#Categoria').val()
        },
        type: "POST",
        dataType: "json",
        success: function (data) {
            var codeInterno = data.Code;
            var message = data.Message;

            LimpaClassConsultaInterna();

            var tp = $('input:radio[name=TipoFornecedor]').filter(":checked").val();

            if (codeInterno == 1) {
                $("#callInterna").addClass("panel-success");
                $("#callInternaHeading").addClass("panel-heading-success");

                $("#callInternaHeading").children("h4").children("a").children("span").empty();
                $("#callInternaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeInterno + " - Sem Restrição");

                if (tp == 1) {
                    ConsultaReceitaCNPJ();
                }
                else if (tp == 3) {
                    ConsultaReceitaCPF();
                }
            }
            else {
                $("#callInterna").addClass("panel-danger");
                $("#callInternaHeading").addClass("panel-heading-danger");

                $("#callInternaHeading").children("h4").children("a").children("span").empty();
                $("#callInternaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeInterno + " - " + data.Message);
            }
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}

function ConsultaReceitaCNPJ() {
    $('.collapse').collapse('hide');
    LimpaClassConsultaReceitaCNPJ();

    $("#callReceitaHeading").children("h4").children("a").children("span").empty();
    $("#callReceitaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp; <i class='fa fa-refresh fa-spin'></i> Aguarde, validando CNPJ...");

    $.ajax({
        url: urlReceitaCNPJ,
        timeout: 120000,
        data: {
            cnpj: $('#CNPJ').val(),
            contratante: $('#Empresa').val(),
            tipoFornecedor: $('input:radio[name=TipoFornecedor]').filter(":checked").val(),
            solicitacaoId: $('#SolicitacaoID').val()
        },
        type: "POST",
        dataType: "json",
        success: function (data) {
            var codeCNPJ = data.Code;
            $('#CodeCNPJ').val(codeCNPJ);
            $('#CNPJSit').val(data.Data.SituacaoCadastral);
            $('#ProximosPapeis').val(data.ProximosPapeis);
            LimpaClassConsultaReceitaCNPJ();

            if (codeCNPJ == 1) {
                if (data.Data.SituacaoCadastral == 'ATIVA') {
                    $("#callReceita").addClass("panel-success");
                    $("#callReceitaHeading").addClass("panel-heading-success");
                }
                else {
                    $("#callReceita").addClass("panel-danger");
                    $("#callReceitaHeading").addClass("panel-heading-danger");
                }

                $("#callReceitaHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeCNPJ + " - Consulta Realizada com sucesso. (Situação Cadastral: " + data.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>");

                $('#RoboReceitaCNPJ_code').val(codeCNPJ);
                $('#RoboReceitaCNPJ_Data_message').val(data.Data.Message);

                $('#RoboReceitaCNPJ_Data_RazaoSocial').val(data.Data.RazaoSocial);
                $('#span_RoboReceitaCNPJ_Data_RazaoSocial').text(data.Data.RazaoSocial);

                $('#RoboReceitaCNPJ_Data_NomeFantasia').val(data.Data.NomeFantasia);
                $('#span_RoboReceitaCNPJ_Data_NomeFantasia').text(data.Data.NomeFantasia);

                $('#RoboReceitaCNPJ_Data_SituacaoCadastral').val(data.Data.SituacaoCadastral);
                $('#span_RoboReceitaCNPJ_Data_SituacaoCadastral').text(data.Data.SituacaoCadastral);

                $('#RoboReceitaCNPJ_Data_MotivoSituacaoCadastral').val(data.Data.MotivoSituacaoCadastral);
                $('#span_RoboReceitaCNPJ_Data_MotivoSituacaoCadastral').text(data.Data.MotivoSituacaoCadastral);

                $('#RoboReceitaCNPJ_Data_DataSituacaoCadastral').val(data.Data.DataSituacaoCadastral);
                $('#span_RoboReceitaCNPJ_Data_DataSituacaoCadastral').text(data.Data.DataSituacaoCadastral);

                $('#RoboReceitaCNPJ_Data_DataSituacaoCadastral').val(data.Data.DataSituacaoCadastral);
                $('#span_RoboReceitaCNPJ_Data_DataSituacaoCadastral').text(data.Data.DataSituacaoCadastral);

                $('#RoboReceitaCNPJ_Data_DataEmissao').val(data.Data.DatEmissao);
                $('#span_RoboReceitaCNPJ_Data_DataEmissao').text(data.Data.DatEmissao);

                $('#RoboReceitaCNPJ_Data_HoraEmissao').val(data.Data.HoraEmissao);
                $('#span_RoboReceitaCNPJ_Data_HoraEmissao').text(data.Data.HoraEmissao);

                $('#RoboReceitaCNPJ_Data_DataAbertura').val(data.Data.DataAbertura);
                $('#span_RoboReceitaCNPJ_Data_DataAbertura').text(data.Data.DataAbertura);

                $('#RoboReceitaCNPJ_Data_EnteFederativoResponsavel').val(data.Data.EnteFederativoResponsavel);
                $('#span_RoboReceitaCNPJ_Data_EnteFederativoResponsavel').text(data.Data.EnteFederativoResponsavel);

                $('#RoboReceitaCNPJ_Data_EnderecoEletronico').val(data.Data.EnderecoEletronico);
                $('#span_RoboReceitaCNPJ_Data_EnderecoEletronico').text(data.Data.EnderecoEletronico);

                $('#RoboReceitaCNPJ_Data_Telefone').val(data.Data.Telefone);
                $('#span_RoboReceitaCNPJ_Data_Telefone').text(data.Data.Telefone);

                $('#RoboReceitaCNPJ_Data_ObservacaoIBGE').val(data.Data.ObservacaoIBGE);
                $('#span_RoboReceitaCNPJ_Data_ObservacaoIBGE').text(data.Data.ObservacaoIBGE);

                $('#RoboReceitaCNPJ_Data_Logradouro').val(data.Data.Logradouro);
                $('#span_RoboReceitaCNPJ_Data_Logradouro').text(data.Data.Logradouro);

                $('#RoboReceitaCNPJ_Data_Numero').val(data.Data.Numero);
                $('#span_RoboReceitaCNPJ_Data_Numero').text(data.Data.Numero);

                $('#RoboReceitaCNPJ_Data_Complemento').val(data.Data.Complemento);
                $('#span_RoboReceitaCNPJ_Data_Complemento').text(data.Data.Complemento);

                $('#RoboReceitaCNPJ_Data_Bairro').val(data.Data.Bairro);
                $('#span_RoboReceitaCNPJ_Data_Bairro').text(data.Data.Bairro);

                $('#RoboReceitaCNPJ_Data_Municipio').val(data.Data.Municipio);
                $('#span_RoboReceitaCNPJ_Data_Municipio').text(data.Data.Municipio);

                $('#RoboReceitaCNPJ_Data_UF').val(data.Data.UF);
                $('#span_RoboReceitaCNPJ_Data_UF').text(data.Data.UF);

                $('#RoboReceitaCNPJ_Data_CEP').val(data.Data.CEP);
                $('#span_RoboReceitaCNPJ_Data_CEP').text(data.Data.CEP);

                $('#RoboReceitaCNPJ_Data_MatrizFilial').val(data.Data.MatrizFilial);
                $('#span_RoboReceitaCNPJ_Data_MatrizFilial').text(data.Data.MatrizFilial);

                $('#RoboReceitaCNPJ_Data_AtividadeEconomicaPrincipal').val(data.Data.AtividadeEconomicaPrincipal);
                $('#span_RoboReceitaCNPJ_Data_AtividadeEconomicaPrincipal').text(data.Data.AtividadeEconomicaPrincipal);

                $('#RoboReceitaCNPJ_Data_NaturezaJuridica').val(data.Data.NaturezaJuridica);
                $('#span_RoboReceitaCNPJ_Data_NaturezaJuridica').text(data.Data.NaturezaJuridica);

                $('#RoboReceitaCNPJ_Data_SituacaoEspecial').val(data.Data.SituacaoEspecial);
                $('#span_RoboReceitaCNPJ_Data_SituacaoEspecial').text(data.Data.SituacaoEspecial);

                $('#RoboReceitaCNPJ_Data_DataSituacaoEspecial').val(data.Data.DataSituacaoEspecial);
                $('#span_RoboReceitaCNPJ_Data_DataSituacaoEspecial').text(data.Data.DataSituacaoEspecial);

                ConsultaSintegra();
            }
            else {
                $("#callReceita").addClass("panel-danger");
                $("#callReceitaHeading").addClass("panel-heading-danger");

                $("#callReceitaHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;Falha na Consulta. " + codeCNPJ + " - " + data.Data.Message + "&nbsp;&nbsp;<button type='button' class='btn btn-primary btn-xs' onclick='ConsultaReceitaCNPJ();'>Tentar</button>");
            }
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}

function ConsultaReceitaCPF() {
    $('.collapse').collapse('hide');

    $("#callReceitaCPF").addClass("panel-default");
    $("#callReceitaCPFHeading").addClass("panel-heading-default");

    $("#callReceitaCPFHeading").children("h4").children("a").children("span").empty();
    $("#callReceitaCPFHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp; <i class='fa fa-refresh fa-spin'></i> Aguarde, validando CPF...");

    $.ajax({
        url: urlReceitaCPF,
        data: {
            cpf: $('#CNPJ').val(),
            contratante: $('#Empresa').val(),
            dataNascimento: $('#DataNascimento').val(),
            tipoFornecedor: $('input:radio[name=TipoFornecedor]').filter(":checked").val(),
            solicitacaoId: $('#SolicitacaoID').val()
        },
        type: "POST",
        dataType: "json",
        success: function (data) {
            var codeCPF = data.Code;
            $('#CodeCPF').val(codeCPF);
            $('#CPFSit').val(data.Data.SituacaoCadastral);
            LimpaClassConsultaReceitaCPF();

            if (codeCPF == 1) {
                if (data.Data.SituacaoCadastral == 'REGULAR') {
                    $("#callReceitaCPF").addClass("panel-success");
                    $("#callReceitaCPFHeading").addClass("panel-heading-success");
                }
                else {
                    $("#callReceitaCPF").addClass("panel-warning");
                    $("#callReceitaCPFHeading").addClass("panel-heading-warning");
                }

                $("#callReceitaCPFHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaCPFHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeCPF + " - Consulta Realizada com sucesso. (Situação Cadastral: " + data.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>");

                $('#RoboReceitaCPF_code').val(codeCPF);
                $('#RoboReceitaCPF_Data_message').val(data.Data.Message);

                $('#RoboReceitaCPF_Data_Nome').val(data.Data.Nome);
                $('#span_RoboReceitaCPF_Data_Nome').text(data.Data.Nome);

                $('#RoboReceitaCPF_Data_SituacaoCadastral').val(data.Data.SituacaoCadastral);
                $('#span_RoboReceitaCPF_Data_SituacaoCadastral').text(data.Data.SituacaoCadastral);

                $('#RoboReceitaCPF_Data_DataEmissao').val(data.Data.DataEmissao);
                $('#span_RoboReceitaCPF_Data_DataEmissao').text(data.Data.DataEmissao);

                $('#RoboReceitaCPF_Data_HoraEmissao').val(data.Data.HoraEmissao);
                $('#span_RoboReceitaCPF_Data_HoraEmissao').text(data.Data.HoraEmissao);

                $('#RoboReceitaCPF_Data_Contingencia').val(data.Data.Contingencia);
                $('#span_RoboReceitaCPF_Data_Contingencia').text(data.Data.Contingencia);

                $('#RoboReceitaCPF_Data_Comprovante').val(data.Data.Comprovante);
                $('#span_RoboReceitaCPF_Data_Comprovante').text(data.Data.Comprovante);

                //$('#divBtnContinuar').show();
            }
            else {
                $("#callReceitaCPF").addClass("panel-danger");
                $("#callReceitaCPFHeading").addClass("panel-heading-danger");

                $("#callReceitaCPFHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaCPFHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;Falha na Consulta. " + codeCPF + " - " + data.Data.Message + "&nbsp;&nbsp;<button type='button' class='btn btn-primary btn-xs' onclick='ConsultaReceitaCPF();'>Tentar</button>");
            }
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}

function ConsultaSintegra() {
    LimpaClassConsultaSintegra();

    $('.collapse').collapse('hide');

    $("#callReceitaSintegra").addClass("panel-default");
    $("#callReceitaSintegraHeading").addClass("panel-heading-default");

    $("#callReceitaSintegraHeading").children("h4").children("a").children("span").empty();
    $("#callReceitaSintegraHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp; <i class='fa fa-refresh fa-spin'></i> Aguarde, validando CNPJ...");

    $.ajax({
        url: urlSintegra,
        timeout: 120000,
        data: {
            cnpj: $('#CNPJ').val(),
            uf: $('#RoboReceitaCNPJ_Data_UF').val(),
            solicitacaoID: $('#SolicitacaoID').val()
        },
        type: "POST",
        dataType: "json",
        success: function (data) {
            var codeSintegra = data.Code;
            $('#CodeSintegra').val(codeSintegra);
            LimpaClassConsultaSintegra();

            if (codeSintegra == 1) {
                if (data.Data.SituacaoCadastral == 'HABILITADO ATIVO' || data.Data.SituacaoCadastral == 'HABILITADO') {
                    $("#callReceitaSintegra").addClass("panel-success");
                    $("#callReceitaSintegraHeading").addClass("panel-heading-success");
                }
                else {
                    $("#callReceitaSintegra").addClass("panel-warning");
                    $("#callReceitaSintegraHeading").addClass("panel-heading-warning");
                }

                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSintegra + " - Consulta Realizada com sucesso. (Situação Cadastral: " + data.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>");

                $('#RoboSintegra_Code').val(codeSintegra);
                $('#RoboSintegra_Data_Message').val(data.Data.Message);

                $('#RoboSintegra_Data_RazaoSocial').val(data.Data.RazaoSocial);
                $('#span_RoboSintegra_Data_RazaoSocial').text(data.Data.RazaoSocial);

                $('#RoboSintegra_Data_SituacaoCadastral').val(data.Data.SituacaoCadastral);
                $('#span_RoboSintegra_Data_SituacaoCadastral').text(data.Data.SituacaoCadastral);

                $('#RoboSintegra_Data_DataSituacaoCadastral').val(data.Data.DataSituacaoCadastral);
                $('#span_RoboSintegra_Data_DataSituacaoCadastral').text(data.Data.DataSituacaoCadastral);

                $('#RoboSintegra_Data_DataSituacaoCadastral').val(data.Data.DataSituacaoCadastral);
                $('#span_RoboSintegra_Data_DataSituacaoCadastral').text(data.Data.DataSituacaoCadastral);

                $('#RoboSintegra_Data_Telefone').val(data.Data.Telefone);
                $('#span_RoboSintegra_Data_Telefone').text(data.Data.Telefone);

                $('#RoboSintegra_Data_Logradouro').val(data.Data.Logradouro);
                $('#span_RoboSintegra_Data_Logradouro').text(data.Data.Logradouro);

                $('#RoboSintegra_Data_Numero').val(data.Data.Numero);
                $('#span_RoboSintegra_Data_Numero').text(data.Data.Numero);

                $('#RoboSintegra_Data_Complemento').val(data.Data.Complemento);
                $('#span_RoboSintegra_Data_Complemento').text(data.Data.Complemento);

                $('#RoboSintegra_Data_Bairro').val(data.Data.Bairro);
                $('#span_RoboSintegra_Data_Bairro').text(data.Data.Bairro);

                $('#RoboSintegra_Data_Municipio').val(data.Data.Municipio);
                $('#span_RoboSintegra_Data_Municipio').text(data.Data.Municipio);

                $('#RoboSintegra_Data_UF').val(data.Data.UF);
                $('#span_RoboSintegra_Data_UF').text(data.Data.UF);

                $('#RoboSintegra_Data_CEP').val(data.Data.CEP);
                $('#span_RoboSintegra_Data_CEP').text(data.Data.CEP);

                $('#RoboSintegra_Data_AtividadeEconomicaPrincipal').val(data.Data.AtividadeEconomicaPrincipal);
                $('#span_RoboSintegra_Data_AtividadeEconomicaPrincipal').text(data.Data.AtividadeEconomicaPrincipal);

                $('#RoboSintegra_Data_InscricaoEstadual').val(data.Data.InscricaoEstadual);
                $('#span_RoboSintegra_Data_InscricaoEstadual').text(data.Data.InscricaoEstadual);

                $('#RoboSintegra_Data_EnquadramentoFiscal').val(data.Data.EnquadramentoFiscal);
                $('#span_RoboSintegra_Data_EnquadramentoFiscal').text(data.Data.EnquadramentoFiscal);

                //$('#RoboSintegra_Data_Contingencia').val(data.Data.Contingencia);
                //$('#span_RoboSintegra_Data_Contingencia').text(data.Data.Contingencia);

                $('#RoboSintegra_Data_SituacaoEFD').val(data.Data.SituacaoEFD);
                $('#span_RoboSintegra_Data_SituacaoEFD').text(data.Data.SituacaoEFD);

                $('#RoboSintegra_Data_MultiplasIE').val(data.Data.MultiplasIE);
                $('#span_RoboSintegra_Data_MultiplasIE').text(data.Data.MultiplasIE);

                $('#RoboSintegra_Data_EmissaoNFEObrigatorio').val(data.Data.EmissaoNFEObrigatorio);
                $('#span_RoboSintegra_Data_EmissaoNFEObrigatorio').text(data.Data.EmissaoNFEObrigatorio);

                $('#RoboSintegra_Data_PerfilEFD').val(data.Data.PerfilEFD);
                $('#span_RoboSintegra_Data_PerfilEFD').text(data.Data.PerfilEFD);

                $('#RoboSintegra_Data_CTE').val(data.Data.CTE);
                $('#span_RoboSintegra_Data_CTE').text(data.Data.CTE);

                $('#RoboSintegra_Data_DataInclusao').val(data.Data.DataInclusao);
                $('#span_RoboSintegra_Data_DataInclusao').text(data.Data.DataInclusao);
            }
            else if (codeSintegra == 2) {
                $("#callReceitaSintegra").addClass("panel-warning");
                $("#callReceitaSintegraHeading").addClass("panel-heading-warning");

                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSintegra + " - " + data.Data.Message);
            }
            else {
                $("#callReceitaSintegra").addClass("panel-danger");
                $("#callReceitaSintegraHeading").addClass("panel-heading-danger");

                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").empty();
                $("#callReceitaSintegraHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSintegra + " - " + data.Data.Message);
            }

            ConsultaSimples();
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}

function ConsultaSimples() {
    LimpaClassConsultaSimples();

    $('.collapse').collapse('hide');

    $("#callSimples").addClass("panel-default");
    $("#callSimplesHeading").addClass("panel-heading-default");

    $("#callSimplesHeading").children("h4").children("a").children("span").empty();
    $("#callSimplesHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp; <i class='fa fa-refresh fa-spin'></i> Aguarde, validando CNPJ...");

    $.ajax({
        url: urlSimples,
        timeout: 120000,
        data: {
            cnpj: $('#CNPJ').val(),
            solicitacaoID: $('#SolicitacaoID').val()
        },
        type: "POST",
        dataType: "json",
        success: function (data) {
            var codeSimples = data.Code;
            $('#CodeSimples').val(codeSimples);
            LimpaClassConsultaSimples();

            if (codeSimples == 1) {
                if (data.Data.SituacaoSimplesNacional.indexOf('OPTANTE PELO SIMPLES NACIONAL') >= 0) {
                    $("#callSimples").addClass("panel-success");
                    $("#callSimplesHeading").addClass("panel-heading-success");
                }
                else {
                    $("#callSimples").addClass("panel-warning");
                    $("#callSimplesHeading").addClass("panel-heading-warning");
                }

                $("#callSimplesHeading").children("h4").children("a").children("span").empty();
                $("#callSimplesHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSimples + " - Consulta Realizada com sucesso. (Situação Cadastral: " + data.Data.SituacaoSimplesNacional + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>");

                $('#RoboSimples_Code').val(codeSimples);
                $('#RoboSimples_Data_Message').val(data.Data.Message);

                $('#RoboSimples_Data_RazaoSocial').val(data.Data.RazaoSocial);
                $('#span_RoboSimples_Data_RazaoSocial').text(data.Data.RazaoSocial);

                $('#RoboSimples_Data_EventosFuturosSimplesNacional').val(data.Data.EventosFuturosSimplesNacional);
                $('#span_RoboSimples_Data_EventosFuturosSimplesNacional').text(data.Data.EventosFuturosSimplesNacional);

                $('#RoboSimples_Data_SimplesNacionalPeriodosAnteriores').val(data.Data.SimplesNacionalPeriodosAnteriores);
                $('#span_RoboSimples_Data_SimplesNacionalPeriodosAnteriores').text(data.Data.SimplesNacionalPeriodosAnteriores);

                $('#RoboSimples_Data_SIMEIPeriodosAnteriores').val(data.Data.SIMEIPeriodosAnteriores);
                $('#span_RoboSimples_Data_SIMEIPeriodosAnteriores').text(data.Data.SIMEIPeriodosAnteriores);

                $('#RoboSimples_Data_SituacaoSIMEI').val(data.Data.SituacaoSIMEI);
                $('#span_RoboSimples_Data_SituacaoSIMEI').text(data.Data.SituacaoSIMEI);

                $('#RoboSimples_Data_SituacaoSimplesNacional').val(data.Data.SituacaoSimplesNacional);
                $('#span_RoboSimples_Data_SituacaoSimplesNacional').text(data.Data.SituacaoSimplesNacional);

                $('#RoboSimples_Data_AgendamentosSimplesNacional').val(data.Data.AgendamentosSimplesNacional);
                $('#span_RoboSimples_Data_AgendamentosSimplesNacional').text(data.Data.AgendamentosSimplesNacional);
            }
            else if (codeSimples == 2) {
                $("#callSimples").addClass("panel-warning");
                $("#callSimplesHeading").addClass("panel-heading-warning");

                $("#callSimplesHeading").children("h4").children("a").children("span").empty();
                $("#callSimplesHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSimples + " - " + data.Data.Message);
            }
            else {
                $("#callSimples").addClass("panel-danger");
                $("#callSimplesHeading").addClass("panel-heading-danger");

                $("#callSimplesHeading").children("h4").children("a").children("span").empty();
                $("#callSimplesHeading").children("h4").children("a").children("span").append("&nbsp;&nbsp;" + codeSimples + " - " + data.Data.Message);
            }
        },
        error: function (xhr, textStatus, error) {
            console.log(error);
        }
    });
}