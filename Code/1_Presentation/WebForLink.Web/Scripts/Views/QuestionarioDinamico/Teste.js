function criarCabecalho(questionario) {
    var Titulo = criarTitulo(questionario.Titulo, questionario.Subtitulo);
    var Aba = criarMenuAba(questionario.MenuAbas);
    return Titulo + Aba;
};
function criarTitulo(Titulo, SubTiulo) {
    return '<h3 class="box-title">' + Titulo + '</h3>';
};
function criarMenuAba(menuAba) {
    var codigoMenu = '';
    for (var i = 0; i < menuAba.length; i++) {
        if (menuAba[i].Visivel != false) {
            var idTab = "#tab_" + menuAba[i].Id;
            if (i == 0)
                codigoMenu += '<li role="presentation" class="active"><a href="' + idTab + '" data-toggle="tab">' + menuAba[i].Titulo + '</a></li>';
            else
                codigoMenu += '<li role="presentation"><a href="' + idTab + '" data-toggle="tab">' + menuAba[i].Titulo + '</a></li>';
        }
    }
    return '<ul class="nav nav-tabs">' + codigoMenu + '</ul>';
};
//--
function criarTab(questionario) {
    var codigoTab = '';
    var indexador = 4;
    for (var i = 0; i < questionario.Abas.length; i++) {
        switch (questionario.Abas[i].Perguntas.length) {
            case 1:
                indexador = 12;
            case 2:
                indexador = 6;
        }
        if (i == 0)
            codigoTab += '<div class="tab-pane fade in active" id="tab_' + questionario.Abas[i].Id + '">';
        else
            codigoTab += '<div class="tab-pane fade" id="tab_' + questionario.Abas[i].Id + '">';

        codigoTab += criarTituloTab(questionario.Abas[i].Titulo, questionario.Abas[i].SubTitulo);
        codigoTab += '<div class="box-body abas">';
        codigoTab += '<div class="row">';
        codigoTab += criarPerguntas(questionario.Abas[i].Perguntas, indexador);
        codigoTab += '</div>';
        codigoTab += '</div>';
        codigoTab += '</div>';
    }
    return codigoTab;
}
function criarTituloTab(Titulo, SubTitulo) {
    return '<div class="box-header"><h3 class="box-title">' + Titulo + '</h3></div>';
};
function criarPerguntas(listaPerguntas, indexador) {
    var perguntas = '';
    console.log("Lista", listaPerguntas);
    for (var i = 0; i < listaPerguntas.length; i++) {
        perguntas += '<div class="col-md-' + indexador + '">';
        perguntas += '<div class="form-group">';
        perguntas += criarTituloPergunta(listaPerguntas[i].Id, listaPerguntas[i].Titulo, listaPerguntas[i].Obrigatorio);
        perguntas += criarResposta(listaPerguntas[i]);
        perguntas += '</div>';
        perguntas += '</div>';
    }
    return perguntas;
};
function criarTituloPergunta(Id, Titulo, Obrigatorio) {
    var titulo = '';
    titulo += '<label for="pergunta_' + Id + '" class="control-label">' + Titulo + '</label>';
    if (Obrigatorio)
        titulo += '<span class="text-danger">*</span>';
    titulo += '<br />';
    return titulo;
}
function criarResposta(perguntaResposta) {
    var resposta = '';
    var bloqueado = false;
    if (perguntaResposta.Pais) {
        for (var i = 0; i < perguntaResposta.Pais.length; i++) {
            if (!perguntaResposta.Pais[i].Respondido)
                bloqueado = true;
        }
    }
    resposta += '<input type="hidden" name="Id" value=' + perguntaResposta.Id + ' />';

    if (perguntaResposta.Filhos != null) {
        var filhosArray = [];
        for (var i = 0; i < perguntaResposta.Filhos.length; i++) {
            filhosArray.push(perguntaResposta.Filhos[i].Id);
        }
        resposta += '<input type="hidden" name="Filhos" class="hdFilhos" value=[' + filhosArray + '] />';
    }
    //--
    //console.log("Pergunta e Resposta",perguntaResposta);
    switch (perguntaResposta.Tipo) {
        case 1:
        case "Texto":
        case "Data":
        case "Telefone":
        case "Numeral(2)":
        case "Numeral(3)":
        case "Numeral(4)":
        case "Numeral(5)":
            resposta += criarInputResposta(bloqueado || perguntaResposta.Bloqueado, perguntaResposta, filhosArray);
            break;
        case 2:
        case "Dominio":
            resposta += criarDropDownRespostas(perguntaResposta.Resposta, perguntaResposta.FormName, bloqueado, perguntaResposta.DominioResposta, perguntaResposta.Filho);
            break;
        case 3:
        case "Checkbox":
            resposta += criarCheckboxRespostas(perguntaResposta.Resposta, perguntaResposta.FormName, bloqueado, perguntaResposta.DominioResposta, perguntaResposta.Filho);
            break;
        case 4:
        case "Radio":
            resposta += criarRadioButtonRespostas(perguntaResposta.Resposta, perguntaResposta.FormName, bloqueado, perguntaResposta.DominioResposta, perguntaResposta.Filho);
            break;
    }
    return resposta;
}

function criarInputResposta(bloqueado, perguntaResposta, filhos) {
    var valorResposta = perguntaResposta.Resposta;
    var nameForm = perguntaResposta.FormName;
    var tipo = perguntaResposta.Tipo;
    var idPergunta = perguntaResposta.Id;
    var classe = "form-control ";
    if (tipo == "Texto") { classe += ""; }
    if (tipo == "Data") { classe += "dataMask "; }
    if (tipo == "Telefone") { classe += "phoneMask "; }
    if (tipo == "Numeral(2)") { classe += "numeral2Mask "; }
    if (tipo == "Numeral(3)") { classe += "numeral3Mask "; }
    if (tipo == "Numeral(4)") { classe += "numeral4Mask "; }
    if (tipo == "Numeral(5)") { classe += "numeral5Mask "; }
    //
    var dataValFilhos = '';
    if (perguntaResposta.Filhos != null) {
        var filhosArray = [];
        for (var i = 0; i < perguntaResposta.Filhos.length; i++) {
            filhosArray.push(perguntaResposta.Filhos[i].Id);
        }
        dataValFilhos = 'data-val-idFilhos="[' + filhos + ']"';
        classe += "blurFilhos"
    }
    //--

    if (bloqueado)
        return '<input type="text" ' + dataValFilhos + ' data-val-idPergunta="' + idPergunta + '" value="' + valorResposta + '" name="' + nameForm + '" id="' + nameForm + '" class="' + classe + '" disabled="disabled"  />';
    return '<input type="text" ' + dataValFilhos + ' data-val-idFilhos data-val-idPergunta="' + idPergunta + '" value="' + valorResposta + '" name="' + nameForm + '" id="' + nameForm + '" class="' + classe + '" />';
}
function criarDropDownRespostas(valorResposta, nameForm, bloqueado, listaRespostas, filhos) {
    var selectInput = '';
    // onchange="liberarRespostasFilhos(this);"
    selectInput += '<select class="form-control" name="' + nameForm + '" id="' + nameForm + '">';
    selectInput += criarOptionsSelect(listaRespostas);
    selectInput += '</select>';
    return selectInput;
}
function criarOptionsSelect(listaRespostas) {
    var options = '';
    for (var i = 0; i < listaRespostas.length; i++) {
        if (listaRespostas[i].Selected)
            //options += '<option value="' + listaRespostas[i].Id + '" selected="selected">' + listaRespostas[i].Valor + '</option>';
            options += '<option value="' + listaRespostas[i].Valor + '" name="' + nameForm + '" id="' + nameForm + '" selected="selected">' + listaRespostas[i].Texto + '</option>';
        else
            //options += '<option value="' + listaRespostas[i].Id + '">' + listaRespostas[i].Valor + '</option>';
            options += '<option value="' + listaRespostas[i].Valor + '" name="' + nameForm + '" id="' + nameForm + '">' + listaRespostas[i].Texto + '</option>';
    }
    return options;
}
function criarCheckboxRespostas(valorResposta, nameForm, bloqueado, listaRespostas, filhos) {
    var checboxList = '';
    for (var i = 0; i < listaRespostas.length; i++) {
        if (valorResposta == listaRespostas[i].Valor)
            checboxList += '<div class="checkbox"><label><input type="checkbox" name="' + nameForm + '" id="' + nameForm + '" disabled="disabled" value="' + listaRespostas[i].Valor + '" checked="checked"/>' + listaRespostas[i].Valor + '</label></div>';
        else
            checboxList += '<div class="checkbox"><label><input type="checkbox" name="' + nameForm + '" id="' + nameForm + '" disabled="disabled" value="' + listaRespostas[i].Valor + '"/>' + listaRespostas[i].Valor + '</label></div>';
    }
    return checboxList;
}
function criarRadioButtonRespostas(valorResposta, nameForm, bloqueado, listaRespostas, filhos) {
    var radioList = '';
    for (var i = 0; i < listaRespostas.length; i++) {
        if (valorResposta == listaRespostas[i].Valor)
            radioList += '<div class="radio"><label><input type="radio" name="' + nameForm + '" id="' + nameForm + '" value="' + listaRespostas[i].Valor + '" checked="checked"/>' + listaRespostas[i].Valor + '</label></div>';
        else
            radioList += '<div class="radio"><label><input type="radio" name="' + nameForm + '" id="' + nameForm + '" value="' + listaRespostas[i].Valor + '"/>' + listaRespostas[i].Valor + '</label></div>';
    }
    return radioList;
}
