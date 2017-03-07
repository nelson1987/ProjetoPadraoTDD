using System;
using System.IO;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorArquivoWebForLinkService : IService<ARQUIVOS>
    {
        ARQUIVOS BuscarPorId(int arquivoId);
        int GravarArquivoSolicitacao(int contratanteId, string arquivo, string tipoArquivo);

        int SubstituirArquivoSolicitacaoDocumento(int contratanteId, int idDocumento, int idArquivo,
            string arquivoSubido, string tipoArquivoSubido, string nomeArquivoCadastrado);

        int SubstituirArquivoSolicitacaoBancario(int contratanteId, int idBanco, int idArquivo, string arquivoSubido,
            string tipoArquivoSubido, string nomeArquivoCadastrado);

        int SubstituirArquivoMeusBancario(int contratanteId, int idBanco, int idArquivo, string arquivoSubido,
            string tipoArquivoSubido, string nomeArquivoCadastrado);

        string PegaNomeArquivoSubido(string arquivoSubido);
    }

    public class FornecedorArquivoWebForLinkService : Service<ARQUIVOS>, IFornecedorArquivoWebForLinkService
    {
        private readonly IArquivosWebForLinkRepository _arquivos;
        private readonly IFornecedorBancoWebForLinkRepository _bancoFornecedorRepository;
        private readonly IConfiguracaoWebForLinkRepository _configuracao;
        private readonly IContratanteWebForLinkRepository _contratante;
        private readonly ISolicitacaoBancoWebForLinkRepository _solicitacaoBancoRepository;
        private readonly ISolicitacaoDocumentoWebForLinkRepository _solicitacaoDocumentoRepository;

        public FornecedorArquivoWebForLinkService(
            IArquivosWebForLinkRepository arquivos,
            IFornecedorBancoWebForLinkRepository bancoFornecedor,
            IContratanteWebForLinkRepository contratante,
            IConfiguracaoWebForLinkRepository configuracao,
            ISolicitacaoDocumentoWebForLinkRepository solicitacaoDocumento,
            ISolicitacaoBancoWebForLinkRepository solicitacaoBanco) : base(arquivos)
        {
            try
            {
                _arquivos = arquivos;
                _bancoFornecedorRepository = bancoFornecedor;
                _contratante = contratante;
                _configuracao = configuracao;
                _solicitacaoDocumentoRepository = solicitacaoDocumento;
                _solicitacaoBancoRepository = solicitacaoBanco;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
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

            var caminhoInicial = _configuracao.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
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
            var caminhoInicial = _configuracao.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
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
                var documento = _solicitacaoDocumentoRepository.Get(idDocumento);
                documento.ARQUIVO_ID = idNovoArquivo;
                _solicitacaoDocumentoRepository.Update(documento);

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
            var caminhoInicial = _configuracao.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
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
                var banco = _solicitacaoBancoRepository.Get(idBanco);
                banco.ARQUIVO_ID = idNovoArquivo;
                _solicitacaoBancoRepository.Update(banco);

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
            var caminhoInicial = _configuracao.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
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
                var banco = _bancoFornecedorRepository.Get(idBanco);
                banco.ARQUIVO_ID = idNovoArquivo;
                _bancoFornecedorRepository.Update(banco);

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

        public void Dispose()
        {
        }
    }
}