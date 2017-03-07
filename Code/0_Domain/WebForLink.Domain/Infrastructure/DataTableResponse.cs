using System.Collections.Generic;

namespace WebForLink.Domain.Infrastructure
{
    public class DataTableResponse<TEntity> where TEntity : class
    {
        public DataTableResponse(int echo, int total, int totalVisualizado, IEnumerable<TEntity> dados)
        {
            sEcho = echo;
            iTotalRecords = total;
            iTotalDisplayRecords = totalVisualizado;
            aaData = dados;
        }

        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public IEnumerable<TEntity> aaData { get; set; }
    }
}