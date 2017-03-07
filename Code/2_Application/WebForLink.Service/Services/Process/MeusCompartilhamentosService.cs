using System;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class MeusCompartilhamentosWebForLinkAppService : AppService<WebForLinkContexto>,
        IMeusCompartilhamentosWebForLinkAppService
    {
        private readonly ICompartilhamentoWebForLinkService _compartilhamentos;
        private readonly IDestinatarioWebForLinkService _destinatarioService;
        private readonly IFornecedorWebForLinkService _fornecedorService;
        private readonly IFornecedorBancoWebForLinkService _fornecedorServiceBanco;
        private readonly IFornecedorContatoWebForLinkService _fornecedorServiceContatos;

        public MeusCompartilhamentosWebForLinkAppService(ICompartilhamentoWebForLinkService compartilhamentos,
            IDestinatarioWebForLinkService destinatario,
            IFornecedorWebForLinkService fornecedor, IFornecedorContatoWebForLinkService fornecedorContato,
            IFornecedorBancoWebForLinkService fornecedorBanco)
        {
            try
            {
                _compartilhamentos = compartilhamentos;
                _destinatarioService = destinatario;
                _fornecedorService = fornecedor;
                _fornecedorServiceContatos = fornecedorContato;
                _fornecedorServiceBanco = fornecedorBanco;
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
            BeginTransaction();
            var destinatario = new DESTINATARIO
            {
                CONTRATANTE_ID = contratanteId,
                EMAIL = email,
                EMAIL_AVULSO = true,
                ATIVO = true
            };
            _destinatarioService.Add(destinatario);
            Commit();
            return destinatario;
        }

        public int BuscarId(string email)
        {
            return _fornecedorService.Get(x => x.EMAIL == email).ID;
        }

        public Compartilhamentos Buscar(Expression<Func<Compartilhamentos, bool>> filtro)
        {
            return _compartilhamentos.Get(filtro);
        }

        public FORNECEDOR_CONTATOS BuscarContatoPorId(int id)
        {
            return _fornecedorServiceContatos.Get(id);
        }

        public BancoDoFornecedor BuscarBancoPorId(int id)
        {
            return _fornecedorServiceBanco.Get(id);
        }

        public Compartilhamentos IncluirCompartilhamento(Compartilhamentos compartilhamentos)
        {
            BeginTransaction();
            _compartilhamentos.Add(compartilhamentos);
            Commit();
            return compartilhamentos;
        }

        public Compartilhamentos AlterarCompartilhamento(Compartilhamentos compartilhamentos,
            DocumentosCompartilhados documentosCompartilhados, int emailId)
        {
            BeginTransaction();
            compartilhamentos.DocumentosCompartilhados.Add(documentosCompartilhados);
            compartilhamentos.WFD_DESTINATARIO.Add(_destinatarioService.Get(emailId));
            Commit();
            return compartilhamentos;
        }

        public Compartilhamentos AlterarMeuCompartilhamento(Compartilhamentos compartilhamentos)
        {
            BeginTransaction();
            _compartilhamentos.Update(compartilhamentos);
            Commit();
            return compartilhamentos;
        }
    }
}