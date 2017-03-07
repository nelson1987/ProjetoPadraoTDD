using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorBaseImportacaoWebForLinkService : IService<FORNECEDORBASE_IMPORTACAO>
    {
        FORNECEDORBASE_IMPORTACAO Inserir(FORNECEDORBASE_IMPORTACAO planilha);
        List<FORNECEDORBASE_IMPORTACAO> ListarTodas(int contratanteId);
    }

    public class FornecedorBaseImportacaoWebForLinkService : Service<FORNECEDORBASE_IMPORTACAO>,
        IFornecedorBaseImportacaoWebForLinkService
    {
        private readonly IFornecedorBaseImportacaoWebForLinkRepository _fornecedorBaseRepositoryImportacao;

        public FornecedorBaseImportacaoWebForLinkService(
            IFornecedorBaseImportacaoWebForLinkRepository fornecedorBaseImportacao) : base(fornecedorBaseImportacao)
        {
            try
            {
                _fornecedorBaseRepositoryImportacao = fornecedorBaseImportacao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDORBASE_IMPORTACAO Inserir(FORNECEDORBASE_IMPORTACAO planilha)
        {
            _fornecedorBaseRepositoryImportacao.Add(planilha);
            return planilha;
        }

        public List<FORNECEDORBASE_IMPORTACAO> ListarTodas(int contratanteId)
        {
            return _fornecedorBaseRepositoryImportacao.Find(x => x.WFD_CONTRATANTE.ID == contratanteId).ToList();
        }

        public void Dispose()
        {
        }
    }
}