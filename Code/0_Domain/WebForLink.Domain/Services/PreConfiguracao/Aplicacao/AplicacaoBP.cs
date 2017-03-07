using System;
using WebForLink.Service.Infrastructure.Exceptions;
using WebForLink.Service.Process;
using WebForLink.Data.Contextos;

namespace WebForLink.Service.PreConfiguracao.Aplicacao
{
    public class AplicacaoService : PadraoService<UnitOfWork>, IDisposable
    {
        private static UnitOfWork Processo { get; set; }
        public AplicacaoService()
            : base(Processo)
        {
            try
            {
                if (Processo == null)
                    Processo = new UnitOfWork(new WebForLinkContexto());
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
            Processo.Finalizar();
        }
    }
}