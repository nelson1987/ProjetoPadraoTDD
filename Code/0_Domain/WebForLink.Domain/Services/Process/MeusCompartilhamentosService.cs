using System;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IMeusCompartilhamentosWebForLinkService : IService<Compartilhamentos>
    {
        Compartilhamentos BuscarPorID(int id);
        void CriarCompartilhamento();
        DESTINATARIO SalvarDestinatario(int contratanteId, string email);
        int BuscarId(string email);
        Compartilhamentos Buscar(Expression<Func<Compartilhamentos, bool>> filtro);
        FORNECEDOR_CONTATOS BuscarContatoPorId(int id);
        BancoDoFornecedor BuscarBancoPorId(int id);
        Compartilhamentos IncluirCompartilhamento(Compartilhamentos compartilhamentos);

        Compartilhamentos AlterarCompartilhamento(Compartilhamentos compartilhamentos,
            DocumentosCompartilhados documentosCompartilhados, int emailId);

        Compartilhamentos AlterarMeuCompartilhamento(Compartilhamentos compartilhamentos);
    }

    public class MeusCompartilhamentosWebForLinkService : Service<Compartilhamentos>,
        IMeusCompartilhamentosWebForLinkService
    {
        private readonly ICompartilhamentoWebForLinkRepository _compartilhamentos;
        private readonly IDestinatarioWebForLinkRepository _destinatarioRepository;
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IFornecedorBancoWebForLinkRepository _fornecedorRepositoryBanco;
        private readonly IFornecedorContatoWebForLinkRepository _fornecedorRepositoryContatos;

        public MeusCompartilhamentosWebForLinkService(
            ICompartilhamentoWebForLinkRepository compartilhamentos,
            IDestinatarioWebForLinkRepository destinatario,
            IFornecedorWebForLinkRepository fornecedor,
            IFornecedorContatoWebForLinkRepository fornecedorContato,
            IFornecedorBancoWebForLinkRepository fornecedorBanco) : base(compartilhamentos)
        {
            try
            {
                _compartilhamentos = compartilhamentos;
                _destinatarioRepository = destinatario;
                _fornecedorRepository = fornecedor;
                _fornecedorRepositoryContatos = fornecedorContato;
                _fornecedorRepositoryBanco = fornecedorBanco;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Compartilhamentos BuscarPorID(int id)
        {
            try
            {
                return _compartilhamentos.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public void CriarCompartilhamento()
        {
        }

        public DESTINATARIO SalvarDestinatario(int contratanteId, string email)
        {
            var destinatario = new DESTINATARIO
            {
                CONTRATANTE_ID = contratanteId,
                EMAIL = email,
                EMAIL_AVULSO = true,
                ATIVO = true
            };
            _destinatarioRepository.Add(destinatario);
            return destinatario;
        }

        public int BuscarId(string email)
        {
            return _fornecedorRepository.Find(x => x.EMAIL == email).FirstOrDefault().ID;
        }

        public Compartilhamentos Buscar(Expression<Func<Compartilhamentos, bool>> filtro)
        {
            return _compartilhamentos.Find(filtro).FirstOrDefault();
        }

        public FORNECEDOR_CONTATOS BuscarContatoPorId(int id)
        {
            return _fornecedorRepositoryContatos.Get(id);
        }

        public BancoDoFornecedor BuscarBancoPorId(int id)
        {
            return _fornecedorRepositoryBanco.Get(id);
        }

        public Compartilhamentos IncluirCompartilhamento(Compartilhamentos compartilhamentos)
        {
            _compartilhamentos.Add(compartilhamentos);
            return compartilhamentos;
        }

        public Compartilhamentos AlterarCompartilhamento(Compartilhamentos compartilhamentos,
            DocumentosCompartilhados documentosCompartilhados, int emailId)
        {
            compartilhamentos.DocumentosCompartilhados.Add(documentosCompartilhados);
            compartilhamentos.WFD_DESTINATARIO.Add(_destinatarioRepository.Get(emailId));
            return compartilhamentos;
        }

        public Compartilhamentos AlterarMeuCompartilhamento(Compartilhamentos compartilhamentos)
        {
            _compartilhamentos.Update(compartilhamentos);
            return compartilhamentos;
        }

        public void Dispose()
        {
        }
    }
}