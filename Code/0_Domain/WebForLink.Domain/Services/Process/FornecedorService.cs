using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class FornecedorWebForLinkService : Service<Fornecedor>, IFornecedorWebForLinkService
    {
        private readonly IContratanteFornecedorWebForLinkRepository _contratanteFornecedor;
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IFornecedorBaseWebForLinkRepository _fornecedorRepositoryBase;
        private readonly IRoboWebForLinkRepository _roboRepository;

        public FornecedorWebForLinkService(
            IFornecedorWebForLinkRepository fornecedor,
            IContratanteFornecedorWebForLinkRepository contratanteFornecedor,
            IRoboWebForLinkRepository robo,
            IFornecedorBaseWebForLinkRepository fornecedorBase) : base(fornecedor)
        {
            try
            {
                _fornecedorRepository = fornecedor;
                _contratanteFornecedor = contratanteFornecedor;
                _roboRepository = robo;
                _fornecedorRepositoryBase = fornecedorBase;
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
                return _fornecedorRepository.Get(filtro);
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
                return _fornecedorRepository.Get(id);
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
                return _fornecedorRepository.Get(id);
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
                return
                    _fornecedorRepository.Get(x => x.NOME.Contains(razaoSocial) || x.RAZAO_SOCIAL.Contains(razaoSocial));
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
                return _fornecedorRepository.Get(x => x.ID == id && x.CONTRATANTE_ID == contratanteId);
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

                //return _fornecedorRepository.Pesquisar(filtro, tamanhoPagina, pagina, a => a.ID);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

        public Fornecedor CarregarDadosPjpf(int idFornecedor)
        {
            throw new NotImplementedException();
        }

        public int BuscarPorCnpj(string cnpj)
        {
            try
            {
                var pjpf = _fornecedorRepository.Find(x => x.CNPJ == cnpj || x.CPF == cnpj).FirstOrDefault();
                return pjpf != null ? pjpf.ID : 0;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

        public RetornoPesquisa<WFD_CONTRATANTE_PJPF> PesquisarFornecedoresDisponiveis(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtro, int pagina, int tamanhoPagina,
            Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        {
            try
            {
                return _contratanteFornecedor.Pesquisar(filtro, tamanhoPagina, pagina, a => a.ID);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Fornecedor por CNPJ", ex);
            }
        }

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

        //        return _fornecedorRepositoryBase.Pesquisar(filtro, tamanhoPagina, pagina, x => x.ID);
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
                    _fornecedorRepository.Find(x => (x.CNPJ == cnpj || x.CPF == cnpj) && x.CONTRATANTE_ID == contratante)
                        .FirstOrDefault();
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
                var fornecedorRobo = _roboRepository.Get((int) fornecedor.ROBO_ID);
                if (fornecedorRobo != null)
                    fornecedor.ROBO = fornecedorRobo;
            }
            return fornecedor;
        }

        public List<Fornecedor> ListarTodosPorContratanteIdAtivoChave(int idContratante, string chave)
        {
            try
            {
                return _fornecedorRepository.Find(d => d.CONTRATANTE_ID == idContratante
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
                return _fornecedorRepository.Find(filtroPreCadastroFornecedor).ToList();
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
                _fornecedorRepository.Find(x => (x.ATIVO && x.TIPO_PJPF_ID == 3 && x.WFD_CONTRATANTE_PJPF.Any()))
                    .AsQueryable();
        }

        public IQueryable<Fornecedor> ListarFornecedoresConvencionaisPorContratante(int contratanteId)
        {
            return
                _fornecedorRepository.Find(x => (x.ATIVO && x.TIPO_PJPF_ID == 2 && x.CONTRATANTE_ID == contratanteId))
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
            return _fornecedorRepository.Find(x => x.ATIVO && x.TIPO_PJPF_ID == 2).AsQueryable();
        }

        public RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedoresBase(int? CategoriaSelecionada, string Fornecedor,
            string CNPJ, string CPF, int grupoId, int pagina, int tamanhoPagina, int contratanteId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}