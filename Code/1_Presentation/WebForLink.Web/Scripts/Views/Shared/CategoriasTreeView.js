
function abrirCategorias() {
    $('#divCatTree').css('overflow', 'visible');
    $('#divCatTree').css('border', '0');
    $('#divCatTreeContainer').addClass('treeview-simples-container-active');
    $('#divCatSelecionado').prop('onclick', null).off('click');
    $('#divCatSelecionado').click(function () { FecharCategorias() });
    $('#divTreeViewSimples').show();
}

function FecharCategorias() {
    $('#divCatTree').css('overflow', 'hidden');
    $('#divCatTree').css('border', '1px solid silver');
    $('#divCatTreeContainer').removeClass('treeview-simples-container-active');
    $('#divCatSelecionado').prop('onclick', null).off('click');
    $('#divCatSelecionado').click(function () { abrirCategorias() });
    $('#divTreeViewSimples').hide();
}

function selecionaCategoria(id, descricao) {
    $('#CategoriaSelecionada').val(id);
    $('#CategoriaSelecionadaNome').val(descricao);
    $('#spanCategoria').text(descricao);
    FecharCategorias();
}
function buscaFilhos(li, id, url) {
    var action = url + '?idPai=' + id + "&tipoExibicao=2";
    var paddingLeft = parseInt($(li).css("padding-left").match(/\d+/));
    paddingLeft += 40;

    $(li).children('i').removeClass('fa-chevron-circle-right').addClass('fa-chevron-circle-down');
    $(li).after('<li style="padding-left:' + paddingLeft + 'px;" id="liTemp' + id + '"><i class="fa fa-refresh fa-spin"></i> Aguarde...</li>');

    $.ajax({
        url: action,
        cache: false,
        dataType: "html",
        success: function (data) {
            $('#liTemp' + id).remove();
            $(li).prop('onclick', null).off('click');
            $(li).after(data);

            $("[liPai=" + id + "]").each(function () {
                $(this).css('padding-left', paddingLeft.toString() + 'px');
            });
        }
    });
}