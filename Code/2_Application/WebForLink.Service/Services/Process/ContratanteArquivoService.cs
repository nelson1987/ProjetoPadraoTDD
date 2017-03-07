using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IContratanteArquivoWebForLinkAppService : IAppService<ARQUIVOS>
    {
        int GravarArquivo(string arquivoSubido, string tipoArquivoSubido);
    }

    public class ContratanteArquivoWebForLinkAppService : AppService<WebForLinkContexto>,
        IContratanteArquivoWebForLinkAppService
    {
        private readonly IArquivoWebForLinkService _arquivo;
        private readonly IConfiguracaoWebForLinkService _configuracaoService;

        public ContratanteArquivoWebForLinkAppService(
            IArquivoWebForLinkService arquivo,
            IConfiguracaoWebForLinkService configuracao)
        {
            try
            {
                _arquivo = arquivo;
                _configuracaoService = configuracao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public int GravarArquivo(string arquivo, string tipoArquivo)
        {
            var vDescricaoArquivo = arquivo.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));

            var caminhoInicial = _configuracaoService.BuscarConfigGeral().CAMINHO_ARQUIVOS;
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
                    BeginTransaction();
                    _arquivo.Add(wfdArquivo);
                    Commit();
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

        public ARQUIVOS BuscarPorId(int arquivoId)
        {
            return _arquivo.Get(arquivoId);
        }

        public int SubstituirArquivo(int idArquivo, string arquivoSubido, string tipoArquivoSubido,
            string nomeArquivoCadastrado)
        {
            var vDescricaoArquivo = arquivoSubido.Split(new[] {"##"}, StringSplitOptions.None);
            var CnpfCpf = vDescricaoArquivo[0].Substring(0, vDescricaoArquivo[0].IndexOf("_"));
            var caminhoInicial = _configuracaoService.BuscarConfigGeral().CAMINHO_ARQUIVOS;
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

    public interface IArquivoWebForLinkAppService : IAppService<ARQUIVOS>
    {
        ARQUIVOS BuscarPorId(int arquivoId);
        void ExcluirMeusDocumentos(int arquivoId, int documentoId, int contratanteId);
    }

    public class ArquivoWebForLinkAppService : AppService<WebForLinkContexto>, IArquivoWebForLinkAppService
    {
        private readonly IArquivoWebForLinkService _arquivoService;
        private readonly IFornecedorDocumentoWebForLinkService _pjpfDocumentoService;
        public ArquivoWebForLinkAppService(IArquivoWebForLinkService arquivoService,
            IFornecedorDocumentoWebForLinkService pjpfDocumentoService)
        {
            try
            {
                _arquivoService = arquivoService;
                _pjpfDocumentoService = pjpfDocumentoService;
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
            return _arquivoService.Get(arquivoId);
        }

        public void ExcluirMeusDocumentos(int arquivoId, int documentoId, int contratanteId)
        {
            var arquivo = _arquivoService.Get(arquivoId);
            if (arquivo != null)
            {
                _arquivoService.Delete(arquivo);
            }
            _pjpfDocumentoService.Find(
                d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == contratanteId && d.ID == documentoId).FirstOrDefault();
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
    }
}