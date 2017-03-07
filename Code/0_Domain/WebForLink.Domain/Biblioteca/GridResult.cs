using System;

namespace WebForDocs.Biblioteca
{
    public class GridResult
    {
        /// <summary>
        /// Index da página consultada
        /// </summary>        
        private int _page;
        public int page {
            get
            {
                return records <= pageSize ? 1 : _page;
            }
            set
            {
                _page = value;
            }
        }

        public int pageSize { get; set; }

        /// <summary>
        /// Total de registros retornado pela consulta
        /// </summary>
        public int records { get; set; }

        public Object[] rows { get; set; }


        /// <summary>
        /// Total de páginas existentes
        /// </summary>
        public int total
        {
            get
            {
                return (int)Math.Ceiling((double)records / pageSize);
            }
        }

    }
}