using WebForLink.Domain.Interfaces.Validation;

namespace WebForLink.Domain.Entities
{
    public class EmpresaSistema //: ISelfValidation
    {
        public Contratante Contratante { get; set; }
        public Empresa Empresa { get; set; }
    }
}
