$(document).ready(function () {
    var HabilitaEdicaoUnspsc = $("#HabilitaEdicaoUnspsc").val() == "True";
    if (HabilitaEdicaoUnspsc) {
        $('#divEditarServicoMaterial').toggleClass("hidden");
    }
});