using System.Collections.Generic;

namespace WebForLink.Domain.Infrastructure
{
    public class RetornoPesquisa<TReturn>
    {
        /// <summary>
        ///     Total de registros encontrados para consulta
        /// </summary>
        public int TotalRegistros { get; set; }

        /// <summary>
        ///     Total de páginas encontradas para consulta
        /// </summary>
        public int TotalPaginas { get; set; }

        /// <summary>
        ///     Registros da página solicitada
        /// </summary>
        public IList<TReturn> RegistrosPagina { get; set; }
    }
}