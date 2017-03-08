using System;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Services.Process
{
    public class EmpresaService : Service<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _usuarioRepository;

        public EmpresaService(
            IEmpresaRepository productReviewRepository)
            : base(productReviewRepository)
        {
            try
            {
                _usuarioRepository = productReviewRepository;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
