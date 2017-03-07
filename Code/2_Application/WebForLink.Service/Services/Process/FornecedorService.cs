using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorWebForLinkAppService : IAppService<Fornecedor>
    {
        int BuscarPorCnpj(string cnpj, int contratante);
        int BuscarIdFornecedorPorCnpj(string cnpj);
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
    }

    public class FornecedorWebForLinkAppService : AppService<WebForLinkContexto>, IFornecedorWebForLinkAppService
    {
        private readonly IContratantePjpfWebForLinkService _contratanteFornecedor;
        private readonly IFornecedorWebForLinkService _fornecedorService;
        private readonly IFornecedorBaseWebForLinkService _fornecedorServiceBase;
        private readonly IRoboWebForLinkService _roboService;

        public FornecedorWebForLinkAppService(IFornecedorWebForLinkService fornecedor,
            IContratantePjpfWebForLinkService contratanteFornecedor, IRoboWebForLinkService robo,
            IFornecedorBaseWebForLinkService fornecedorBase)
        {
            try
            {
                _fornecedorService = fornecedor;
                _contratanteFornecedor = contratanteFornecedor;
                _roboService = robo;
                _fornecedorServiceBase = fornecedorBase;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public Fornecedor Buscar(Expression<Func<Fornecedor, bool>> filtro)
        {
            try
            {
                return _fornecedorService.Get(filtro);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public Fornecedor BuscarPorId(int id)
        {
            try
            {
                return _fornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public Fornecedor BuscarPorIdETermoAceite(int id)
        {
            try
            {
                return _fornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o termo de aceite", ex);
            }
        }



        public Fornecedor BuscarPorRazaoSocial(string razaoSocial)
        {
            try
            {
                return _fornecedorService.Get(x => x.NOME.Contains(razaoSocial) || x.RAZAO_SOCIAL.Contains(razaoSocial));
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o fornecedor por razão social", ex);
            }
        }

        public Fornecedor BuscarPorId(int id, int contratanteId)
        {
            try
            {
                return _fornecedorService.Get(x => x.ID == id && x.CONTRATANTE_ID == contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o fornecedor por ID", ex);
            }
        }

        public RetornoPesquisa<Fornecedor> PesquisarFornecedoresDisponiveis(Expression<Func<Fornecedor, bool>> filtro,
            int pagina, int tamanhoPagina, Func<Fornecedor, IComparable> ordenacao, int contratanteId)
        {
            try
            {
                var registros = ListarFornecedoresIndividuaisEConvencionaisDeContratante(contratanteId);
                var lista = registros.AsQueryable()
                    .Where(filtro)
                    .OrderBy(ordenacao)
                    .Skip(tamanhoPagina*(pagina - 1))
                    .Take(tamanhoPagina)
                    .ToList();
                return new RetornoPesquisa<Fornecedor>
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista,
                    TotalPaginas = (int) Math.Ceiling(registros.Count()/(double) tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

        public int BuscarIdFornecedorPorCnpj(string cnpj)
        {
            try
            {
                var pjpf = _fornecedorService.Get(x => x.CNPJ == cnpj || x.CPF == cnpj);
                return pjpf != null ? pjpf.ID : 0;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

        //public RetornoPesquisa<WFD_CONTRATANTE_PJPF> PesquisarFornecedoresDisponiveis(Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtro, int pagina, int tamanhoPagina, Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _contratanteFornecedor.Pesquisar(filtro, tamanhoPagina, pagina, a => a.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
        //    }
        //}

        //public RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedoresBase(int? CategoriaSelecionada, string Fornecedor, string CNPJ, string CPF, int grupoId, int pagina, int tamanhoPagina, int contratanteId)
        //{
        //    try
        //    {
        //        //BUSCA FORNECEDORES E MONTA PAGINAÇÃO
        //        var filtro = PredicateBuilder.New<FORNECEDORBASE>();
        //        filtro = filtro.And(d => d.Contratante.WFD_GRUPO.Any(g => g.ID == grupoId));

        //        if (CategoriaSelecionada != null)
        //            filtro = filtro.And(f => f.CATEGORIA_ID == CategoriaSelecionada);

        //        if (!string.IsNullOrEmpty(Fornecedor))
        //            filtro = filtro.And(c => c.RAZAO_SOCIAL.Contains(Fornecedor));

        //        if (!string.IsNullOrEmpty(CNPJ))
        //            filtro = filtro.And(c => c.CNPJ == CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", ""));

        //        if (!string.IsNullOrEmpty(CNPJ))
        //            filtro = filtro.And(c => c.CNPJ == CPF.Replace(".", "").Replace("-", ""));

        //        return _fornecedorServiceBase.Pesquisar(filtro, tamanhoPagina, pagina, x => x.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
        //    }
        //}

        public int BuscarPorCnpj(string cnpj, int contratante)
        {
            try
            {
                var pjpf =
                    _fornecedorService.Get(x => (x.CNPJ == cnpj || x.CPF == cnpj) && x.CONTRATANTE_ID == contratante);
                return pjpf != null ? pjpf.ID : 0;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

        public Fornecedor RetornaFornecedor(int id)
        {
            var fornecedor = BuscarPorId(id);
            if (fornecedor != null)
            {
                var fornecedorRobo = _roboService.Get((int) fornecedor.ROBO_ID);
                if (fornecedorRobo != null)
                    fornecedor.ROBO = fornecedorRobo;
            }
            return fornecedor;
        }

        public List<Fornecedor> ListarTodosPorContratanteIdAtivoChave(int idContratante, string chave)
        {
            try
            {
                return _fornecedorService.Find(d => d.CONTRATANTE_ID == idContratante
                                                    && d.ATIVO
                                                    && (d.CNPJ.Contains(chave) || d.RAZAO_SOCIAL.Contains(chave)))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public Fornecedor BuscarPorIdComRelacionamentos(int id)
        {
            try
            {
                return BuscarPorId(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar o fornecedor", e);
            }
        }

        public Fornecedor BuscarPorIdModificacaoFornecedor(int id)
        {
            try
            {
                return BuscarPorId(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar o fornecedor", e);
            }
        }

        public List<WFD_CONTRATANTE_PJPF> BuscarPorIdCompleto(int id)
        {
            try
            {
                return _contratanteFornecedor.Find(x => x.PJPF_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o fornecedor", ex);
            }
        }

        public List<Fornecedor> ListarPreCadastro(Expression<Func<Fornecedor, bool>> filtroPreCadastroFornecedor)
        {
            try
            {
                return _fornecedorService.Find(filtroPreCadastroFornecedor).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar os fornecedores", ex);
            }
        }

        public Fornecedor BuscarPreCadastro(Expression<Func<Fornecedor, bool>> filtroPreCadastroFornecedor)
        {
            try
            {
                return ListarPreCadastro(filtroPreCadastroFornecedor).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o fornecedor", ex);
            }
        }

        public IQueryable<Fornecedor> ListarFornecedoresIndividuais()
        {
            return
                _fornecedorService.Find(x => (x.ATIVO && x.TIPO_PJPF_ID == 3 && x.WFD_CONTRATANTE_PJPF.Any()))
                    .AsQueryable();
        }

        public IQueryable<Fornecedor> ListarFornecedoresConvencionaisPorContratante(int contratanteId)
        {
            return
                _fornecedorService.Find(x => (x.ATIVO && x.TIPO_PJPF_ID == 2 && x.CONTRATANTE_ID == contratanteId))
                    .AsQueryable();
        }

        public List<Fornecedor> ListarFornecedoresIndividuaisEConvencionaisDeContratante(int contratanteId)
        {
            var pjpf = new List<Fornecedor>();
            pjpf.AddRange(ListarFornecedoresIndividuais());
            pjpf.AddRange(ListarFornecedoresConvencionaisPorContratante(contratanteId));
            return pjpf;
        }

        public IQueryable<Fornecedor> ListarFornecedores()
        {
            return _fornecedorService.Find(x => x.ATIVO && x.TIPO_PJPF_ID == 2).AsQueryable();
        }

        public void Dispose()
        {
        }

        public Fornecedor Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Fornecedor Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Fornecedor GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fornecedor> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fornecedor> Find(Expression<Func<Fornecedor, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<WFD_CONTRATANTE_PJPF> PesquisarFornecedoresDisponiveis(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtro, int pagina, int tamanhoPagina,
            Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedoresBase(int? CategoriaSelecionada, string Fornecedor,
            string CNPJ, string CPF, int grupoId, int pagina, int tamanhoPagina, int contratanteId)
        {
            throw new NotImplementedException();
        }
    }
}