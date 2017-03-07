using System;
using System.Collections.Generic;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorBaseImportacaoWebForLinkAppService
    {
        FORNECEDORBASE_IMPORTACAO Inserir(FORNECEDORBASE_IMPORTACAO planilha);
        List<FORNECEDORBASE_IMPORTACAO> ListarTodas(int contratanteId);
    }

    public class FornecedorBaseImportacaoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorBaseImportacaoWebForLinkAppService
    {
        private readonly IFornecedorBaseImportacaoWebForLinkService _fornecedorBaseServiceImportacao;

        public FornecedorBaseImportacaoWebForLinkAppService(
            IFornecedorBaseImportacaoWebForLinkService fornecedorBaseImportacao)
        {
            try
            {
                _fornecedorBaseServiceImportacao = fornecedorBaseImportacao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDORBASE_IMPORTACAO Inserir(FORNECEDORBASE_IMPORTACAO planilha)
        {
            _fornecedorBaseServiceImportacao.Inserir(planilha);
            return planilha;
        }

        public List<FORNECEDORBASE_IMPORTACAO> ListarTodas(int contratanteId)
        {
            return _fornecedorBaseServiceImportacao.ListarTodas(contratanteId);
        }

        public void Dispose()
        {
        }
    }
}