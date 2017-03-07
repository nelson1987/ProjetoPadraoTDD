using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface IFornecedorWebForLinkService : IService<Fornecedor>
    {
        int BuscarPorCnpj(string cnpj, int contratante);
        int BuscarPorCnpj(string cnpj);
        Fornecedor RetornaFornecedor(int id);
        Fornecedor Buscar(Expression<Func<Fornecedor, bool>> filtro);
        Fornecedor BuscarPorId(int id);
        Fornecedor BuscarPorIdETermoAceite(int id);
        Fornecedor BuscarPorRazaoSocial(string razaoSocial);
        Fornecedor BuscarPorId(int id, int contratanteId);
        Fornecedor BuscarPorIdComRelacionamentos(int id);
        Fornecedor BuscarPorIdModificacaoFornecedor(int id);
        Fornecedor BuscarPreCadastro(Expression<Func<Fornecedor, bool>> filtroPreCadastroFornecedor);
        IQueryable<Fornecedor> ListarFornecedoresIndividuais();
        IQueryable<Fornecedor> ListarFornecedoresConvencionaisPorContratante(int contratanteId);
        IQueryable<Fornecedor> ListarFornecedores();
        List<Fornecedor> ListarTodosPorContratanteIdAtivoChave(int idContratante, string chave);
        List<Fornecedor> ListarPreCadastro(Expression<Func<Fornecedor, bool>> filtroPreCadastroFornecedor);
        List<Fornecedor> ListarFornecedoresIndividuaisEConvencionaisDeContratante(int contratanteId);
        List<WFD_CONTRATANTE_PJPF> BuscarPorIdCompleto(int id);

        RetornoPesquisa<WFD_CONTRATANTE_PJPF> PesquisarFornecedoresDisponiveis(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtro, int pagina, int tamanhoPagina,
            Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao);

        RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedoresBase(int? CategoriaSelecionada, string Fornecedor,
            string CNPJ, string CPF, int grupoId, int pagina, int tamanhoPagina, int contratanteId);

        RetornoPesquisa<Fornecedor> PesquisarFornecedoresDisponiveis(Expression<Func<Fornecedor, bool>> filtro,
            int pagina, int tamanhoPagina, Func<Fornecedor, IComparable> ordenacao, int contratanteId);

        Fornecedor CarregarDadosPjpf(int idFornecedor);
    }
}