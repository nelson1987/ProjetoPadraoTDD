using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FornecedorArquivoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorArquivoWebForLinkAppService
    {
        private readonly IArquivoWebForLinkService _arquivos;
        private readonly IFornecedorBancoWebForLinkService _bancoFornecedorService;
        private readonly IConfiguracaoWebForLinkService _configuracao;
        private readonly IContratanteWebForLinkService _contratante;
        private readonly ISolicitacaoModificacaoBancoWebForLinkService _solicitacaoBancoService;
        private readonly ISolicitacaoDocumentoWebForLinkService _solicitacaoDocumentoService;

        public FornecedorArquivoWebForLinkAppService(
            IArquivoWebForLinkService arquivos,
            IFornecedorBancoWebForLinkService bancoFornecedor,
            IContratanteWebForLinkService contratante,
            IConfiguracaoWebForLinkService configuracao,
            ISolicitacaoDocumentoWebForLinkService solicitacaoDocumento,
            ISolicitacaoModificacaoBancoWebForLinkService solicitacaoBanco)
        {
            try
            {
                _arquivos = arquivos;
                _bancoFornecedorService = bancoFornecedor;
                _contratante = contratante;
                _configuracao = configuracao;
                _solicitacaoDocumentoService = solicitacaoDocumento;
                _solicitacaoBancoService = solicitacaoBanco;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public ARQUIVOS BuscarPorId(int arquivoId)
        {
            return _arquivos.Get(arquivoId);
        }

        public int GravarArquivoSolicitacao(int contratanteId, string arquivo, string tipoArquivo)
        {
            var vDescricaoArquivo = arquivo.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));

            var CnpjCpfContratante = _contratante.Get(contratanteId).CNPJ;

            var caminhoInicial = _configuracao.BuscarConfig().CAMINHO_ARQUIVOS;
            var caminhoTemp = caminhoInicial + "\\Temp\\";
            var caminhoForn = "";

            if (CnpjCpfContratante != CnpfCpf)
                caminhoForn = caminhoInicial + "\\" + CnpjCpfContratante + "\\" + CnpfCpf + "\\";
            else
                caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";

            var arquivoTemp = caminhoTemp + arquivo;

            try
            {
                if (File.Exists(arquivoTemp))
                {
                    var f = new FileInfo(arquivoTemp);
                    var wfdArquivo = new ARQUIVOS();
                    wfdArquivo.CAMINHO = caminhoForn;
                    wfdArquivo.NOME_ARQUIVO = vDescricaoArquivo[1];
                    wfdArquivo.DATA_UPLOAD = DateTime.Now;
                    wfdArquivo.TAMANHO = f.Length;
                    wfdArquivo.TIPO_ARQUIVO = tipoArquivo;

                    _arquivos.Add(wfdArquivo);

                    var arquivoForn = caminhoForn + wfdArquivo.ID + "##" + vDescricaoArquivo[1];

                    if (!Directory.Exists(caminhoForn))
                        Directory.CreateDirectory(caminhoForn);

                    File.Move(arquivoTemp, arquivoForn);

                    return wfdArquivo.ID;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int SubstituirArquivoSolicitacaoDocumento(int contratanteId, int idDocumento, int idArquivo,
            string arquivoSubido, string tipoArquivoSubido, string nomeArquivoCadastrado)
        {
            var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));
            var caminhoInicial = _configuracao.BuscarConfig().CAMINHO_ARQUIVOS;
            var CnpjCpfContratante = _contratante.Get(contratanteId).CNPJ;
            var caminhoForn = "";
            if (CnpjCpfContratante != CnpfCpf)
                caminhoForn = caminhoInicial + "\\" + CnpjCpfContratante + "\\" + CnpfCpf + "\\";
            else
                caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";

            var idNovoArquivo = GravarArquivoSolicitacao(contratanteId, arquivoSubido, tipoArquivoSubido);

            //DELETAR ARQUIVO EXISTENTE PARA SUBSTITUIÇÃO
            if (idNovoArquivo > 0)
            {
                var documento = _solicitacaoDocumentoService.Get(idDocumento);
                documento.ARQUIVO_ID = idNovoArquivo;
                _solicitacaoDocumentoService.Update(documento);

                var wfdArquivo = _arquivos.Get(idArquivo);
                _arquivos.Delete(wfdArquivo);

                File.Delete(caminhoForn + idArquivo + "##" + nomeArquivoCadastrado);
            }

            return idNovoArquivo;
        }

        public int SubstituirArquivoSolicitacaoBancario(int contratanteId, int idBanco, int idArquivo,
            string arquivoSubido, string tipoArquivoSubido, string nomeArquivoCadastrado)
        {
            var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));
            var caminhoInicial = _configuracao.BuscarConfig().CAMINHO_ARQUIVOS;
            var CnpjCpfContratante = _contratante.Get(contratanteId).CNPJ;
            var caminhoForn = "";
            if (CnpjCpfContratante != CnpfCpf)
                caminhoForn = caminhoInicial + "\\" + CnpjCpfContratante + "\\" + CnpfCpf + "\\";
            else
                caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";


            var idNovoArquivo = GravarArquivoSolicitacao(contratanteId, arquivoSubido, tipoArquivoSubido);

            //DELETAR ARQUIVO EXISTENTE PARA SUBSTITUIÇÃO
            if (idNovoArquivo > 0)
            {
                var banco = _solicitacaoBancoService.Get(idBanco);
                banco.ARQUIVO_ID = idNovoArquivo;
                _solicitacaoBancoService.Update(banco);

                var wfdArquivo = _arquivos.Get(idArquivo);
                _arquivos.Delete(wfdArquivo);

                File.Delete(caminhoForn + idArquivo + "##" + nomeArquivoCadastrado);
            }

            return idNovoArquivo;
        }

        public int SubstituirArquivoMeusBancario(int contratanteId, int idBanco, int idArquivo, string arquivoSubido,
            string tipoArquivoSubido, string nomeArquivoCadastrado)
        {
            var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));
            var caminhoInicial = _configuracao.BuscarConfig().CAMINHO_ARQUIVOS;
            var CnpjCpfContratante = _contratante.Get(contratanteId).CNPJ;
            var caminhoForn = "";
            if (CnpjCpfContratante != CnpfCpf)
                caminhoForn = caminhoInicial + "\\" + CnpjCpfContratante + "\\" + CnpfCpf + "\\";
            else
                caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";


            var idNovoArquivo = GravarArquivoSolicitacao(contratanteId, arquivoSubido, tipoArquivoSubido);

            //DELETAR ARQUIVO EXISTENTE PARA SUBSTITUIÇÃO
            if (idNovoArquivo > 0)
            {
                var banco = _bancoFornecedorService.Get(idBanco);
                banco.ARQUIVO_ID = idNovoArquivo;
                _bancoFornecedorService.Update(banco);

                var wfdArquivo = _arquivos.Get(idArquivo);
                _arquivos.Delete(wfdArquivo);

                File.Delete(caminhoForn + idArquivo + "##" + nomeArquivoCadastrado);
            }

            return idNovoArquivo;
        }

        public string PegaNomeArquivoSubido(string arquivoSubido)
        {
            if (!String.IsNullOrEmpty(arquivoSubido) && arquivoSubido.IndexOf("##") > 0)
            {
                var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);

                return vDescricaoArquivo[1];
            }
            return null;
        }

        public ARQUIVOS Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ARQUIVOS Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ARQUIVOS GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ARQUIVOS> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ARQUIVOS> Find(Expression<Func<ARQUIVOS, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ARQUIVOS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ARQUIVOS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ARQUIVOS entity)
        {
            throw new NotImplementedException();
        }

        public ARQUIVOS Get(int id)
        {
            throw new NotImplementedException();
        }

        public ARQUIVOS Get(Expression<Func<ARQUIVOS, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ARQUIVOS> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ARQUIVOS> Find(Expression<Func<ARQUIVOS, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}