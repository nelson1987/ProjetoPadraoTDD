using System;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface IMeusCompartilhamentosWebForLinkAppService
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
}
