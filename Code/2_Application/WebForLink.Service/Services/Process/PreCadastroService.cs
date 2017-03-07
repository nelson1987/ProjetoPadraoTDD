using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LinqKit;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class PreCadastroWebForLinkAppService : AppService<WebForLinkContexto>, IPreCadastroWebForLinkAppService
    {
        private readonly IFornecedorBaseWebForLinkService _fornecedorBaseService;
        private readonly IFornecedorBaseContatosWebForLinkService _fornecedorBaseServiceContato;
        private readonly IFornecedorBaseEnderecoWebForLinkService _fornecedorBaseServiceEndereco;
        private readonly IFornecedorBaseUnspscWebForLinkService _fornecedorBaseServiceUnspsc;
        private readonly IEstadoWebForLinkService _ufService;

        public PreCadastroWebForLinkAppService(
            IFornecedorBaseWebForLinkService fornecedorBaseService,
            IFornecedorBaseEnderecoWebForLinkService fornecedorBaseServiceEndereco,
            IFornecedorBaseContatosWebForLinkService fornecedorBaseServiceContato,
            IFornecedorBaseUnspscWebForLinkService fornecedorBaseServiceUnspsc,
            IEstadoWebForLinkService ufService)
        {
            try
            {
                _fornecedorBaseService = fornecedorBaseService;
                _fornecedorBaseServiceEndereco = fornecedorBaseServiceEndereco;
                _fornecedorBaseServiceContato = fornecedorBaseServiceContato;
                _fornecedorBaseServiceUnspsc = fornecedorBaseServiceUnspsc;
                _ufService = ufService;
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
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new Exception(ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar incluir pré-cadastro.", ex);
                //Log.Error(ex);
            }
        }

        public void Dispose()
        {
        }

        public FORNECEDORBASE Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDORBASE Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDORBASE GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDORBASE> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDORBASE> Find(Expression<Func<FORNECEDORBASE, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(FORNECEDORBASE entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDORBASE entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(FORNECEDORBASE entity)
        {
            throw new NotImplementedException();
        }

        public FORNECEDORBASE Get(int id)
        {
            throw new NotImplementedException();
        }

        public FORNECEDORBASE Get(Expression<Func<FORNECEDORBASE, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDORBASE> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDORBASE> Find(Expression<Func<FORNECEDORBASE, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<FORNECEDORBASE> ListarTodos(PreCadastroFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            var lista = _fornecedorBaseService.Find(Predicativos(filtros)).ToList();
            RetornoPesquisa<FORNECEDORBASE> retorno = new RetornoPesquisa<FORNECEDORBASE>();
            retorno.RegistrosPagina = lista;
            return retorno;
        }

        private void AlterarEntidadeIncluirSolicitacao(int acao)
        {
            var novoFornecedor = _fornecedorBaseService
                .Find(x => x.CNPJ == DocumentoFornecedor
                           || x.CPF == DocumentoFornecedor
                           && x.CONTRATANTE_ID == ContratanteId).FirstOrDefault();
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
                x => { _fornecedorBaseServiceContato.Delete(x); });
            FornecedorBaseContato.ForEach(x => novoFornecedor.WFD_PJPF_BASE_CONTATOS.Add(x));

            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { _fornecedorBaseServiceEndereco.Delete(x); });

            FornecedorBaseEndereco.ForEach(x => novoFornecedor.WFD_PJPF_BASE_ENDERECO.Add(x));

            novoFornecedor.WFD_PJPF_BASE_UNSPSC.ToList().ForEach(
                x => { _fornecedorBaseServiceUnspsc.Delete(x); });
            FornecedoresBaseUnspsc.ForEach(x => novoFornecedor.WFD_PJPF_BASE_UNSPSC.Add(x));

            _fornecedorBaseService.Update(novoFornecedor);
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

            _fornecedorBaseService.Add(FornecedorBase);
            PjpfBaseId = FornecedorBase.ID;
        }

        private void AlterarEstadoEntidades(int acao)
        {
            var novoFornecedor = _fornecedorBaseService.Get(PjpfBaseId);
            novoFornecedor.RAZAO_SOCIAL = FornecedorBase.RAZAO_SOCIAL;
            novoFornecedor.CNPJ = FornecedorBase.CNPJ;
            novoFornecedor.CPF = FornecedorBase.CPF;
            novoFornecedor.NOME_FANTASIA = FornecedorBase.NOME_FANTASIA;
            novoFornecedor.INSCR_MUNICIPAL = FornecedorBase.INSCR_MUNICIPAL;
            if (acao == 2)
                novoFornecedor.STATUS_PRECADASTRO = 1;

            novoFornecedor.WFD_PJPF_BASE_CONTATOS.ToList().ForEach(
                x => { _fornecedorBaseServiceContato.Delete(x); });
            FornecedorBaseContato.ForEach(x => novoFornecedor.WFD_PJPF_BASE_CONTATOS.Add(x));

            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { _fornecedorBaseServiceEndereco.Delete(x); });
            FornecedorBaseEndereco.ForEach(x => novoFornecedor.WFD_PJPF_BASE_ENDERECO.Add(x));


            novoFornecedor.WFD_PJPF_BASE_ENDERECO.ToList().ForEach(
                x => { x.UF = _ufService.Get(y => y.UF_SGL == x.UF).UF_SGL; });

            _fornecedorBaseService.Update(novoFornecedor);
            PjpfBaseId = FornecedorBase.ID;
        }
        
        private static Expression<Func<FORNECEDORBASE, bool>> Predicativos(PreCadastroFiltrosDTO filtros)
        {
            Expression<Func<FORNECEDORBASE, bool>> predicate = x => x.PRECADASTRO && (x.WFD_T_STATUS_PRECADASTRO.ID == 2);

            if (filtros.ContratanteId != 0)
                predicate = predicate.And(x => x.CONTRATANTE_ID == filtros.ContratanteId);
            if (filtros.CategoriaId != 0)
                predicate = predicate.And(x => x.CATEGORIA_ID == filtros.CategoriaId);
            if (!string.IsNullOrEmpty(filtros.RazaoSocial))
                predicate = predicate.And(x => x.RAZAO_SOCIAL.Contains(filtros.RazaoSocial));
            if (!string.IsNullOrEmpty(filtros.CNPJ))
                predicate = predicate.And(x => x.CNPJ.Contains(filtros.CNPJ));
            if (!string.IsNullOrEmpty(filtros.CPF))
                predicate = predicate.And(x => x.CPF.Contains(filtros.CPF));
            
            return predicate;
        }

        private static Expression<Func<FORNECEDORBASE, bool>> Predicativos(
            Expression<Func<FORNECEDORBASE, bool>> predicate, bool ativo)
        {
            predicate = predicate.And(x => x.PRECADASTRO == ativo);
            return predicate;
        }
    }
}