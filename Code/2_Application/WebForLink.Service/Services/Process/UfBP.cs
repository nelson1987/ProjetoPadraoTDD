using System;
using System.Linq;
using WebForLink.Service.Infrastructure.Exceptions;
using WebForLink.Data.Contextos;
using WebForLink.Domain.Models;

namespace WebForLink.Service.Process
{
    public class UfBP : PadraoService<UnitOfWork>, IDisposable
    {
        private static UnitOfWork Processo { get; set; }

        public void Dispose()
        {
            Processo.Finalizar();
        }

        public UfBP()
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

        /// <summary>
        /// Buscar Estado por Sigla
        /// </summary>
        /// <param name="sigla">Sigla do Estado</param>
        /// <returns>T_UF</returns>
        public TIPO_UF BuscarPorID(string sigla)
        {
            try
            {
                return Processo.Uf.BuscarPorID(sigla);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}

