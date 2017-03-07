using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IPreCadastroWebForLinkService : IService<FORNECEDORBASE>
    {
        int ContratanteId { get; set; }
        string DocumentoFornecedor { get; set; }
        FORNECEDORBASE FornecedorBase { get; set; }
        List<FORNECEDORBASE_ENDERECO> FornecedorBaseEndereco { get; set; }
        List<FORNECEDORBASE_CONTATOS> FornecedorBaseContato { get; set; }
        List<FORNECEDORBASE_UNSPSC> FornecedoresBaseUnspsc { get; set; }
        int PjpfBaseId { get; set; }
        void IncluirPreCadastro(CasosPreCadastroEnum preCadastroEnum, int acao);
        RetornoPesquisa<FORNECEDORBASE> ListarTodos(PreCadastroFiltrosDTO filtros, int pagina, int tamanhoPagina);
    }

    public class PreCadastroWebForLinkService : Service<FORNECEDORBASE>, IPreCadastroWebForLinkService
    {
        private readonly IFornecedorBaseWebForLinkRepository _fornecedorBaseRepository;
        private readonly IFornecedorBaseContatosWebForLinkRepository _fornecedorBaseRepositoryContato;
        private readonly IFornecedorBaseEnderecoWebForLinkRepository _fornecedorBaseRepositoryEndereco;
        private readonly IFornecedorBaseUnspscWebForLinkRepository _fornecedorBaseRepositoryUnspsc;
        private readonly IEstadoWebForLinkRepository _ufRepository;

        public PreCadastroWebForLinkService(
            IFornecedorBaseWebForLinkRepository fornecedorBaseRepository,
            IFornecedorBaseEnderecoWebForLinkRepository fornecedorBaseRepositoryEndereco,
            IFornecedorBaseContatosWebForLinkRepository fornecedorBaseRepositoryContato,
            IFornecedorBaseUnspscWebForLinkRepository fornecedorBaseRepositoryUnspsc,
            IEstadoWebForLinkRepository ufRepository) : base(fornecedorBaseRepository)
        {
            try
            {
                _fornecedorBaseRepository = fornecedorBaseRepository;
                _fornecedorBaseRepositoryEndereco = fornecedorBaseRepositoryEndereco;
                _fornecedorBaseRepositoryContato = fornecedorBaseRepositoryContato;
                _fornecedorBaseRepositoryUnspsc = fornecedorBaseRepositoryUnspsc;
                _ufRepository = ufRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public int ContratanteId { get; set; }
        public string DocumentoFornecedor { get; set; }
        public FORNECEDORBASE FornecedorBase { get; set; }
        public List<FORNECEDORBASE_ENDERECO> FornecedorBaseEndereco { get; set; }
        public List<FORNECEDORBASE_CONTATOS> FornecedorBaseContato { get; set; }
        public List<FORNECEDORBASE_UNSPSC> FornecedoresBaseUnspsc { get; set; }
        public int PjpfBaseId { get; set; }

        public void IncluirPreCadastro(CasosPreCadastroEnum preCadastroEnum, int acao)
        {
            try
            {
                switch (preCadastroEnum)
                {
                    case CasosPreCadastroEnum.PreCadastradoOutroContratante:
                        //Adicionar de BASE com contratante diferente para Contratante atual
                        if (PjpfBaseId == 0) MudarEstadoEntidades(acao);
                        else AlterarEstadoEntidades(acao);
                        break;
                    case CasosPreCadastroEnum.PreCadastradoProprio:
                        //Alterar a base
                        AlterarEntidadeIncluirSolicitacao(acao);
                        break;
                    case CasosPreCadastroEnum.CadastradoOutroContratante:
                        //Adicionar de OutroContratante para Base com contratante atual
                        MudarEstadoEntidades(acao);
                        break;
                    case CasosPreCadastroEnum.CadastradoProprio:
                        //Adicionar de OutroContratante para Base com contratante atual
                        MudarEstadoEntidades(acao);
                        break;
                    case CasosPreCadastroEnum.CadastradoPorContratante:
                        AlterarEntidadeIncluirSolicitacao(acao);
                        break;
                    default:
                        break;
                }
            }
                //catch (DbEntityValidationException ex)
                //{
                //    var sb = new StringBuilder();

                //    foreach (var failure in ex.EntityValidationErrors)
                //    {
                //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //        foreach (var error in failure.ValidationErrors)
                //        {
                //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //            sb.AppendLine();
                //        }
                //    }

                //    //Log.Error(
                //    //    "Entity Validation Failed - errors follow:\n" +
                //    //    sb.ToString(), ex
                //    //    );
                //    throw new Exception(ex.ToString(), ex);
                //}
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar incluir pré-cadastro.", ex);
                //Log.Error(ex);
            }
        }

        public RetornoPesquisa<FORNECEDORBASE> ListarTodos(PreCadastroFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            throw new NotImplementedException();
        }

        private void AlterarEntidadeIncluirSolicitacao(int acao)
        {
            var novoFornecedor = _fornecedorBaseRepository
                .Get(x => x.CNPJ == DocumentoFornecedor
                          || x.CPF == DocumentoFornecedor
                          && x.CONTRATANTE_ID == ContratanteId);
            if (novoFornecedor != null)
                AlterarEntidadeBase(novoFornecedor, acao);
            else
                MudarEstadoEntidades(acao);
        }

        private void AlterarEntidadeBase(FORNECEDORBASE novoFornecedor, int acao)
        {
            novoFornecedor.RAZAO_SOCIAL = FornecedorBase.RAZAO_SOCIAL;
            novoFornecedor.NOME_FANTASIA = FornecedorBase.NOME_FANTASIA;
            novoFornecedor.CNPJ = FornecedorBase.CNPJ;
            novoFornecedor.CPF = FornecedorBase.CPF;
            novoFornecedor.INSCR_MUNICIPAL = FornecedorBase.INSCR_MUNICIPAL;
            if (acao == 2)
                novoFornecedor.STATUS_PRECADASTRO = 1;

            novoFornecedor.WFD_PJPF_BASE_CONTATOS.ToList().ForEach(
                x => { _fornecedorBaseRepositoryContato.Delete(x); });
            FornecedorBaseContato.ForEach(x => novoFornecedor.WFD_PJPF_BASE_CONTATOS.Add(x));

            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { _fornecedorBaseRepositoryEndereco.Delete(x); });

            FornecedorBaseEndereco.ForEach(x => novoFornecedor.WFD_PJPF_BASE_ENDERECO.Add(x));

            novoFornecedor.WFD_PJPF_BASE_UNSPSC.ToList().ForEach(
                x => { _fornecedorBaseRepositoryUnspsc.Delete(x); });
            FornecedoresBaseUnspsc.ForEach(x => novoFornecedor.WFD_PJPF_BASE_UNSPSC.Add(x));

            _fornecedorBaseRepository.Update(novoFornecedor);
        }

        private void MudarEstadoEntidades(int acao)
        {
            FornecedorBase.UF = null;
            FornecedorBase.PLANILHA_ID = null;
            FornecedorBase.CATEGORIA_ID = null;
            FornecedorBase.ROBO_ID = null;
            FornecedorBase.PRECADASTRO = true;
            if (acao == 2)
                FornecedorBase.STATUS_PRECADASTRO = 1;

            _fornecedorBaseRepository.Add(FornecedorBase);


            PjpfBaseId = FornecedorBase.ID;
        }

        private void AlterarEstadoEntidades(int acao)
        {
            var novoFornecedor = _fornecedorBaseRepository.Get(PjpfBaseId);
            novoFornecedor.RAZAO_SOCIAL = FornecedorBase.RAZAO_SOCIAL;
            novoFornecedor.CNPJ = FornecedorBase.CNPJ;
            novoFornecedor.CPF = FornecedorBase.CPF;
            novoFornecedor.NOME_FANTASIA = FornecedorBase.NOME_FANTASIA;
            novoFornecedor.INSCR_MUNICIPAL = FornecedorBase.INSCR_MUNICIPAL;
            if (acao == 2)
                novoFornecedor.STATUS_PRECADASTRO = 1;

            novoFornecedor.WFD_PJPF_BASE_CONTATOS.ToList().ForEach(
                x => { _fornecedorBaseRepositoryContato.Delete(x); });
            FornecedorBaseContato.ForEach(x => novoFornecedor.WFD_PJPF_BASE_CONTATOS.Add(x));

            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { _fornecedorBaseRepositoryEndereco.Delete(x); });
            FornecedorBaseEndereco.ForEach(x => novoFornecedor.WFD_PJPF_BASE_ENDERECO.Add(x));

            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { x.UF = _ufRepository.Get(y => y.UF_SGL == x.UF).UF_SGL; });

            _fornecedorBaseRepository.Update(novoFornecedor);

            PjpfBaseId = FornecedorBase.ID;
        }

        //public RetornoPesquisa<FORNECEDORBASE> ListarTodos(PreCadastroFiltrosDTO filtros, int pagina, int tamanhoPagina)
        //{
        //    var predicate = PredicateBuilder.New<FORNECEDORBASE>();
        //    predicate = Predicativos(filtros, predicate);

        //    return _fornecedorBaseRepository.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
        //}

        //private static Expression<Func<FORNECEDORBASE, bool>> Predicativos(PreCadastroFiltrosDTO filtros, Expression<Func<FORNECEDORBASE, bool>> predicate)
        //{
        //    if (filtros.ContratanteId != 0)
        //        predicate = predicate.And(x => x.CONTRATANTE_ID == filtros.ContratanteId);
        //    if (filtros.CategoriaId != 0)
        //        predicate = predicate.And(x => x.CATEGORIA_ID == filtros.CategoriaId);
        //    if (!string.IsNullOrEmpty(filtros.RazaoSocial))
        //        predicate = predicate.And(x => x.RAZAO_SOCIAL.Contains(filtros.RazaoSocial));
        //    if (!string.IsNullOrEmpty(filtros.CNPJ))
        //        predicate = predicate.And(x => x.CNPJ.Contains(filtros.CNPJ));
        //    if (!string.IsNullOrEmpty(filtros.CPF))
        //        predicate = predicate.And(x => x.CPF.Contains(filtros.CPF));

        //    predicate = predicate.And(x => x.PRECADASTRO == true && (x.WFD_T_STATUS_PRECADASTRO.ID == 2));
        //    return predicate;
        //}
        //private static Expression<Func<FORNECEDORBASE, bool>> Predicativos(Expression<Func<FORNECEDORBASE, bool>> predicate, bool ativo)
        //{
        //    predicate = predicate.And(x => x.PRECADASTRO == ativo);
        //    return predicate;
        //}

        public void Dispose()
        {
        }
    }
}