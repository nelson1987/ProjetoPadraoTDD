$(document).ready(function () {
    $('#modalMateriais').on('show.bs.modal', function (e) {
        BuscaMateriais(0, 0, null, false, false);
    });
    $("#BuscaMaterial").keypress(function (e) {
        if (e.which == 13) {
            BuscaMateriaisPorChave();
            e.preventDefault();
        }
    });
});

function BuscaMateriais(codigo, niv, obj, marcar, checked) {
    if (codigo == 0) {
        if ($('#ListaMateriais').children().length > 0)
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
        url: '/Documento/BuscaMateriais',
        type: 'get',
        data: { Codigo: codigo, Niv: niv },
        dataType: 'json',
        success: function (json) {
            if (codigo == 0) {
                MontaListaMateriais(json);
            }
            else {
                MontaListaFilhoMateriais(obj, json);
                if (marcar)
                    MarcarFilhos(obj, checked);
            }
        }
    });
}

function BuscaMateriaisPorChave() {
    var palavra = $('#BuscaMaterial').val();
    $('#ListaMateriais').empty();
    $('#ListaMateriais').append("<li><i class='fa fa-refresh fa-spin'></i> Aguarde...</li>");

    $.ajax({
        url: '/Documento/BuscaMateriaisPorChave',
        type: 'get',
        data: { chave: palavra },
        dataType: 'json',
        success: function (json) {
            MontaListaMateriaisPorPesquisa(json);
        }
    });
}

function MontaListaMateriais(json) {
    $('#ListaMateriais').empty();

    for (var i = 0; i < json.length; i++) {
        var li = "";
        li += '<li id="MaterialLI' + json[i].value1 + '">'
        li += '<span ' + (json[i].value3 != 4 ? 'style="cursor: pointer;"' : '') + ' onclick="BuscaMateriais(' + json[i].value2 + ', ' + json[i].value3 + ', this)">'
        if (json[i].value3 != 1 && json[i].value3 != 4)
            li += '<input type="checkbox" name="chMaterial" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" />';
        li += '<b>' + json[i].value2 + ' - ' + json[i].text + '</b>';
        li += ' <i class="fa fa-chevron-circle-right"></i></span></li>';

        $('#ListaMateriais').append(li);
    }
}

function MontaListaFilhoMateriais(obj, json) {
    var li = "";
    li += "<ul style='list-style:none'>";

    for (var i = 0; i < json.length; i++) {

        li += '<li id="MaterialLI' + json[i].value1 + '">'

        if (json[i].value3 != '1' && json[i].value3 != '4')
            li += '<span><input type="checkbox" name="chMaterial" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" onclick="Marcar(' + json[i].value2 + ', ' + json[i].value3 + ', this, this.checked);" /></span> ';

        if (json[i].value3 != '4')
            li += '<span style="cursor: pointer;" onclick="BuscaMateriais(' + json[i].value2 + ', ' + json[i].value3 + ', this, false, false)">'
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

function MontaListaMateriaisPorPesquisa(json) {
    var niv = "";
    $('#ListaMateriais').empty();

    var li1, ul1;
    var li2, ul2;
    var li3, ul3;
    var li4;

    var chaves = $('#BuscaMaterial').val().split(" ");

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
            liConteudo += '<span><input type="checkbox" name="chMaterial" value="' + json[i].value1 + '|' + json[i].value2 + '|' + json[i].value3 + '|' + json[i].text + '" onclick="Marcar(' + json[i].value2 + ', ' + json[i].value3 + ', this, this.checked);" /></span> ';

        if (json[i].value3 != '4')
            liConteudo += '<span style="cursor: pointer;" onclick="BuscaMateriais(' + json[i].value2 + ', ' + json[i].value3 + ', this, false, false)">'
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
                $('#ListaMateriais').append(li1);
                li1 = null;
            }
        }
        else {
            $('#ListaMateriais').append(li1);
            li1 = null;
        }


    }
}

function Marcar(codigo, niv, checkbox, checked) {
    var span = $(checkbox).parent();

    //se nao tem filhos
    if ($(span).parent().children('ul').length <= 0) {
        BuscaMateriais(codigo, niv, span, true, checked);
    }
    else {
        MarcarFilhos(span, checked);
    }
}

function MarcarFilhos(span, checked) {
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

function selecionaMateriais() {
    var MateriaisSelecionados = $('input[name="chMaterial"]:checked').map(function () { return this.value; }).get();
    var materiaisCadastrados = $('#MateriaisSelecionados').val().split("|");

    var tr = "";
    for (var i = 0; i < MateriaisSelecionados.length; i++) {
        var Material = MateriaisSelecionados[i].split("|");

        if (materiaisCadastrados.indexOf(Material[0]) < 0) {
            if (Material[2] == '3') {
                tr += '<tr id="MaterialTR' + Material[0] + '">';
                tr += '<td class="no-border" width="20"><button type="button" class="btn btn-danger btn-xs" onclick="removerMaterial(\'#MaterialTR' + Material[0] + '\', \'' + Material[0] + '\')"><i class="fa fa-trash"></i></button></td>';
                tr += '<td class="no-border">' + Material[3] + '</td>';
                tr += '</tr>';

                materiaisCadastrados.push(Material[0]);
            }
        }
    }

    $('#tblMateriaisSelecionados').append(tr);
    $('#MateriaisSelecionados').val(materiaisCadastrados.join('|'));
    $('#modalMateriais').modal('hide');
}