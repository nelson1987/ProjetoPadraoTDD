using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForDocs.Biblioteca
{
    public class RetornoPesquisaGrid<TReturn>
    {
        /// <summary>
        /// Total de registros encontrados para consulta
        /// </summary>
        public int TotalRegistros { get; set; }

        /// <summary>
        /// Registros da página solicitada
        /// </summary>
        public IList<TReturn> RegistrosPagina { get; set; }
    }
}