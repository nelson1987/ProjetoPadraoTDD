using System;
using System.IO;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IContratanteArquivoWebForLinkService : IService<ARQUIVOS>
    {
        int GravarArquivo(string arquivoSubido, string tipoArquivoSubido);
    }

    public class ContratanteArquivoWebForLinkService : Service<ARQUIVOS>, IContratanteArquivoWebForLinkService
    {
        private readonly IArquivosWebForLinkRepository _arquivo;
        private readonly IConfiguracaoWebForLinkRepository _configuracaoRepository;

        public ContratanteArquivoWebForLinkService(IArquivosWebForLinkRepository arquivo,
            IConfiguracaoWebForLinkRepository configuracao) : base(arquivo)
        {
            try
            {
                _arquivo = arquivo;
                _configuracaoRepository = configuracao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public int GravarArquivo(string arquivo, string tipoArquivo)
        {
            var vDescricaoArquivo = arquivo.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));

            var caminhoInicial = _configuracaoRepository.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
            var caminhoTemp = caminhoInicial + "\\Temp\\";
            var caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";
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

                    _arquivo.Add(wfdArquivo);

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

        public void Dispose()
        {
        }

        public ARQUIVOS BuscarPorId(int arquivoId)
        {
            return _arquivo.Get(arquivoId);
        }

        public int SubstituirArquivo(int idArquivo, string arquivoSubido, string tipoArquivoSubido,
            string nomeArquivoCadastrado)
        {
            var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));
            var caminhoInicial = _configuracaoRepository.All(true).FirstOrDefault().CAMINHO_ARQUIVOS;
            var caminhoForn = caminhoInicial + "\\" + CnpfCpf + "\\";

            var idNovoArquivo = GravarArquivo(arquivoSubido, tipoArquivoSubido);

            //DELETAR ARQUIVO EXISTENTE PARA SUBSTITUIÇÃO
            if (idNovoArquivo > 0)
            {
                //var documento = Db.SolicitacaoDeDocumentos.FirstOrDefault(x => x.ID == idDocumento);
                //documento.ARQUIVO_ID = idNovoArquivo;
                //Db.Entry(documento).State = EntityState.Modified;

                var wfdArquivo = _arquivo.Get(idArquivo);
                _arquivo.Delete(wfdArquivo);

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
    }

    public class ArquivosWebForLinkService : Service<ARQUIVOS>, IArquivoWebForLinkService
    {
        private readonly IArquivosWebForLinkRepository _arquivoRepository;
        private readonly IFornecedorDocumentoWebForLinkRepository _pjpfDocumentoRepository;

        public ArquivosWebForLinkService(IArquivosWebForLinkRepository arquivoRepository,
            IFornecedorDocumentoWebForLinkRepository pjpfDocumentoRepository) : base(arquivoRepository)
        {
            try
            {
                _arquivoRepository = arquivoRepository;
                _pjpfDocumentoRepository = pjpfDocumentoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public ARQUIVOS BuscarPorId(int arquivoId)
        {
            return _arquivoRepository.Get(arquivoId);
        }

        public void ExcluirMeusDocumentos(int arquivoId, int documentoId, int contratanteId)
        {
            var arquivo = _arquivoRepository.Get(arquivoId);
            if (arquivo != null)
            {
                _arquivoRepository.Delete(arquivo);
            }
            _pjpfDocumentoRepository.Find(
                d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == contratanteId && d.ID == documentoId);
        }

        public void Dispose()
        {
        }
    }
}