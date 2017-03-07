var uploads = 0;

function aplicarPluginUpload(selector, CnpjCpfSeletor) {
    $(selector).fileupload({
        dataType: 'json',
        url: '/Fornecedores/UploadArquivoFornecedor',
        autoUpload: true,
        change: function (e, data) {
            if (data.files != null) {
                var extNaoPermitido = ['BAT', 'BIN', 'CMD', 'COM', 'CPL', 'EXE', 'GADGET', 'INF1', 'INS', 'INX', 'ISU', 'JOB', 'JSE', 'LNK', 'MSC', 'MSI', 'MSP', 'MST', 'PAF', 'PIF', 'PS1', 'REG', 'RGS', 'SCR', 'SCT', 'SHB', 'SHS', 'U3P', 'VB', 'VBE', 'VBS', 'VBSCRIPT', 'WS', 'WSF', 'WSH']
                var vExt = data.files[0].name.split('.');
                var ext = vExt[vExt.length - 1].toUpperCase();

                if (data.files[0].size > 29999999) {
                    $(this).parent().parent().parent().find('[percentual]').empty().append('<span style="color: #d9534f;"><i class="fa fa-exclamation-triangle"></i></span>');
                    $(this).parent().parent().parent().find('[nomearquivo]').text('Não é permitido arquivos maiores de 30MB');
                    data.Abort();
                }
                if (extNaoPermitido.indexOf(ext) >= 0) {
                    $(this).parent().parent().parent().find('[percentual]').empty().append('<span style="color: #d9534f;"><i class="fa fa-exclamation-triangle"></i></span>');
                    $(this).parent().parent().parent().find('[nomearquivo]').text('Este tipo de Arquivo não é permitido');
                    data.Abort();
                }
            }
        },
        done: function (e, data) {
            $(this).parent().parent().parent().find('[percentual]').empty().append('<span style="color: #00a65a;" title="Arquivo subido com Sucesso!"><i class="fa fa-check-circle"></i></span>');
            $(this).parent().parent().parent().find('[ArquivoSubido]').val(data.result.nome);
            $(this).parent().parent().parent().find('[TipoArquivoSubido]').val(data.result.tipo);
            $(this).parent().parent().parent().find('[ArquivoSubidoOriginal]').val(data.result.original);
            var nomearquivo = $(this).parent().parent().parent().find('[nomearquivo]');
            nomearquivo.find('span').remove();

            uploads -= 1;
        },
        fail: function (e, data) {
            $(this).parent().parent().parent().find('[percentual]').empty().append('<span style="color: #d9534f;" title="Erro ao tentar subir este arquivo!"><i class="fa fa-exclamation-triangle"></i></span>');
            uploads -= 1;
        },
        progress: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $(this).parent().parent().parent().find('[percentual]').text(progress + '% - ');
        },
        submit: function (e, data) {
            var htmlNome = data.files[0].name + " <span class='badge bg-red' style='cursor: pointer;' title='Cancelar Upload' onclick='$(this).parent().parent().find(\"[cancelarupload]\").toggleClass(\"hidden\");'><i class='fa fa-trash-o'></i></span>";
            var nomearquivo = $(this).parent().parent().parent().find('[nomearquivo]');
            nomearquivo.empty();
            nomearquivo.append(htmlNome);

            cancelaruploadSim = $(this).parent().parent().parent().find('[cancelaruploadSim]');
            cancelaruploadSim.unbind("click");
            cancelaruploadSim.on("click", { upload: data }, function (e) {
                e.data.upload.abort();
                $(this).parent().toggleClass('hidden')
                $(this).parent().parent().find('[percentual]').empty();
                $(this).parent().parent().find('[nomearquivo]').empty().append('Nenhum Comprovante Selecionado!');
            });
            data.formData = { cnpj_cpf: $(CnpjCpfSeletor).val(), arqTmp: $(this).parent().parent().parent().find('[ArquivoSubido]').val() };
            uploads += 1;
        }
    });
}
