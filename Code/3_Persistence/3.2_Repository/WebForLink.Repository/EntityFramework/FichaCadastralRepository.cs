using System;
using System.Diagnostics;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FichaCadastralRepository : EntityFrameworkRepository<FichaCadastral, ChMasterDataContext>,
        IFichaCadastralRepository
    {
        public void IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal, int size,
            string url)
        {
            try
            {
                //var documentoSolicitacao = new DocumentosSolicitacaoRepository();
                //var documentoRepository = new DocumentosSolicitacao
                //{

                //};
                var solicitacaoRepository = new SolicitacaoRepository();
                var solicitacao = solicitacaoRepository.Get(idSolicitacao);


                var documentioAnexado = new DocumentoAnexado
                {
                    IdDocumentoSolicitado = idDocumentoSolicitado
                };

                var arquivo = new Arquivo
                {
                    NomeOriginal = nomeOriginal,
                    ExtensaoArquivo = nomeOriginal.Substring(nomeOriginal.Length - 4, 4),
                    DocumentoAnexado = documentioAnexado
                };


                var documentioAnexadoRepository = new DocumentoAnexadoRepository();
                var arquivoRepository = new ArquivoRepository();
                documentioAnexadoRepository.Add(documentioAnexado);
                arquivoRepository.Add(arquivo);

                //var documento = new DocumentoAnexadoRepository();
                //var anexado = new DocumentoAnexado
                //{
                //    IdSolicitacao = idSolicitacao,
                //    IdDocumentoSolicitado = idDocumentoSolicitado
                //};
                //documento.Add(anexado);
                //_fichaCadastralService.UpdateReadOnly(anexado);
                //Arquivo arquivo = new Arquivo() {  }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }
    }
}