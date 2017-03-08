using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForLink.Domain.Entities.Tipos
{
    public class ImportacaoEmpresa : Importacao
    {
        public ImportacaoEmpresa(Contratante contratante, Arquivo arquivo)
            : base(contratante, arquivo)
        {
        }
    }
}
