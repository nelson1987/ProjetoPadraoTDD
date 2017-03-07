$(document).ready(function () {
    $('#modalServicos').on('show.bs.modal', function (e) {
        BuscaServicos(0, 0, null, false, false);
    });

    $("#BuscaServico").keypress(function (e) {
        if (e.which == 13) {
            BuscaServicosPorChave();
            e.preventDefault();
        }
    });
});

function BuscaServicos(codigo, niv, obj, marcar, checked) {
    if (codigo == 0) {
        if ($('#ListaServicos').children().length > 0)
            return;
    }

    if (obj != null) {
        if ($(obj).parent().children('ul').length > 0) {
            if (!marcar)
                $(obj).parent().children('ul').toggle();
            return;
        }
    }

    $.ajax({
        url: '/Documento/BuscaServicos',
        type: 'get',
        data: { Codigo: codigo, Niv: niv },
        dataType: 'json',
        success: function (json) {
            if (codigo == 0) {
                MontaListaServicos(json);
            }
            else {
                MontaListaFilhoServicos(obj, json);
                if (marcar)
                    MarcarFilhosServico(obj, checked);
            }
        }
    });
}

function BuscaServicosPorChave() {
    var palavra = $('#BuscaServico').val();
    $('#ListaServicos').empty();
    $('#ListaServicos').append("<li><i class='fa fa-refresh fa-spin'></i> Aguarde...</li>");

    $.ajax({
        url: '/Documento/BuscaServicosPorChave',
        type: 'get',
        data: { chave: palavra },
        dataType: 'json',
        success: function (json) {
            MontaListaServicosPorPesquisa(json);
        }
    });
}

function MontaListaServicos(json) {
    $('#ListaServicos').empty();

    for (var i = 0; i < json.length; i++) {
        var li = "";
        li += '<li id="servicoLI' + json[i].value1 + '">'
        li += '<span ' + (json[i].value3 != 4 ? 'style="cursor: pointer;"' : '') + ' onclick="BuscaServicos(' + json[i].value2 + ', ' + json[i].value3 + ', this)">'
        if (json[i].value3 != 1 && json[i].value3 != 4)
            li += '<input type="checkbox" name="chServico" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" />';
        li += '<b>' + json[i].value2 + ' - ' + json[i].text + '</b>';
        li += ' <i class="fa fa-chevron-circle-right"></i></span></li>';

        $('#ListaServicos').append(li);
    }
}

function MontaListaFilhoServicos(obj, json) {
    var li = "";
    li += "<ul style='list-style:none'>";

    for (var i = 0; i < json.length; i++) {

        li += '<li id="servicoLI' + json[i].value1 + '">'

        if (json[i].value3 != '1' && json[i].value3 != '4')
            li += '<span><input type="checkbox" name="chServico" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" onclick="MarcarServico(' + json[i].value2 + ', ' + json[i].value3 + ', this, this.checked);" /></span> ';

        if (json[i].value3 != '4')
            li += '<span style="cursor: pointer;" onclick="BuscaServicos(' + json[i].value2 + ', ' + json[i].value3 + ', this, false, false)">'
        else
            li += '<span style="color: gray;">'

        li += json[i].value2 + ' - ' + json[i].text;

        if (json[i].value3 != '4')
            li += ' <i class="fa fa-chevron-circle-right"></i></span></li>';
        else
            li += ' </span></li>';
    }

    li += "</ul>";

    $(obj).parent().append(li);
}

function MontaListaServicosPorPesquisa(json) {
    var niv = "";
    $('#ListaServicos').empty();

    var li1, ul1;
    var li2, ul2;
    var li3, ul3;
    var li4;

    var chaves = $('#BuscaServico').val().split(" ");

    for (var i = 0; i < json.length; i++) {

        var rText = json[i].text.toString();
        for (var j = 0; j < chaves.length; j++) {
            var chave = chaves[j].toString().trim();
            var posicao = rText.toLowerCase().indexOf(chave.toLowerCase());

            if (chave != "" && posicao > -1) {
                rText = rText.substr(0, posicao) + "<u>" + rText.substr(posicao, chave.length) + "</u>" + rText.substr(posicao + chave.length);
            }
        }

        var liConteudo = "";
        if (json[i].value3 != '1' && json[i].value3 != '4')
            liConteudo += '<span><input type="checkbox" name="chServico" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" onclick="MarcarServico(' + json[i].value2 + ', ' + json[i].value3 + ', this, this.checked);" /></span> ';

        if (json[i].value3 != '4')
            liConteudo += '<span style="cursor: pointer;" onclick="BuscaServicos(' + json[i].value2 + ', ' + json[i].value3 + ', this, false, false)">'
        else
            liConteudo += '<span style="color: gray;">'

        if (json[i].value3 == '1')
            liConteudo += '<b>' + json[i].value2 + ' - ' + rText + '</b>';
        else
            liConteudo += json[i].value2 + ' - ' + rText;

        if (json[i].value3 != '4')
            liConteudo += ' <i class="fa fa-chevron-circle-right"></i></span>';
        else
            liConteudo += ' </span>';

        if (json[i].value3 == "1") {
            li1 = document.createElement("li");
            $(li1).append(liConteudo);

            if ((i + 1) < json.length) {
                if (json[i + 1].value3 == "2") {
                    ul1 = document.createElement("ul");
                    ul1.style.listStyle = "none";
                    li1.appendChild(ul1);
                }
            }
        }
        else if (json[i].value3 == "2") {
            li2 = document.createElement("li");
            $(li2).append(liConteudo);
            ul1.appendChild(li2);

            if ((i + 1) < json.length) {
                if (json[i + 1].value3 == "3") {
                    ul2 = document.createElement("ul");
                    ul2.style.listStyle = "none";
                    li2.appendChild(ul2);
                }
            }

        }
        else if (json[i].value3 == "3") {
            li3 = document.createElement("li");
            $(li3).append(liConteudo);
            ul2.appendChild(li3);

            if ((i + 1) < json.length) {
                if (json[i + 1].value3 == "4") {
                    ul3 = document.createElement("ul");
                    ul3.style.listStyle = "none";
                    li3.appendChild(ul3);
                }
            }
        }
        else if (json[i].value3 == "4") {
            li4 = document.createElement("li");
            $(li4).append(liConteudo);
            ul3.appendChild(li4);
        }

        if ((i + 1) < json.length) {
            if (json[i + 1].value3 == "1") {
                $('#ListaServicos').append(li1);
                li1 = null;
            }
        }
        else {
            $('#ListaServicos').append(li1);
            li1 = null;
        }


    }
}

function MarcarServico(codigo, niv, checkbox, checked) {
    var span = $(checkbox).parent();

    //se nao tem filhos
    if ($(span).parent().children('ul').length <= 0) {
        BuscaServicos(codigo, niv, span, true, checked);
    }
    else {
        MarcarFilhosServico(span, checked);
    }
}

function MarcarFilhosServico(span, checked) {
    var liPai = $(span).parent();
    var ulFilho = $(liPai).children('ul');
    var chFilhos = $(ulFilho).find(':checkbox');

    if (chFilhos.length > 0) {
        chFilhos.each(function () {
            if (checked)
                this.checked = true;
            else
                this.checked = false;
        });
    }
}

function selecionaServicos() {
    var servicosSelecionados = $('input[name="chServico"]:checked').map(function () { return this.value; }).get();
    var servicosCadastrados = $('#ServicosSelecionados').val().split("|");

    var tr = "";
    for (var i = 0; i < servicosSelecionados.length; i++) {
        var servico = servicosSelecionados[i].split("|");

        if (servicosCadastrados.indexOf(servico[0]) < 0) {
            if (servico[2] == '3') {
                tr += '<tr id="servicoTR' + servico[0] + '">';
                tr += '<td class="no-border" width="20"><button type="button" class="btn btn-danger btn-xs" onclick="removerServico(\'#servicoTR' + servico[0] + '\', \'' + servico[0] + '\')"><i class="fa fa-trash"></i></button></td>';
                tr += '<td class="no-border">' + servico[3] + '</td>';
                tr += '</tr>';

                servicosCadastrados.push(servico[0]);
            }
        }
    }

    $('#tblServicosSelecionados').append(tr);
    $('#ServicosSelecionados').val(servicosCadastrados.join("|"));
    $('#modalServicos').modal('hide');
}