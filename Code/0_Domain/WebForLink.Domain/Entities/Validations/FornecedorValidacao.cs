using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class FornecedorValidacao : Validation<Fornecedor>
    {
        public FornecedorValidacao()
        {

            AddRule(new ValidationRule<Fornecedor>(new FornecedorDeveTerONomePreenchido(),
                ValidationMessages.TitleIsRequired));
            AddRule(new ValidationRule<Fornecedor>(new FornecedorDocumentoNaoPodeSerMaiorQue13Caracteres(),
                ValidationMessages.TitleIsRequired));
        }
    }
}
