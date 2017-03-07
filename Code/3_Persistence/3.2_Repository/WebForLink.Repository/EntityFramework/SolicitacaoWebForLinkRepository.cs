using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoWebForLinkRepository : EntityFrameworkRepository<SOLICITACAO, WebForLinkContexto>,
        ISolicitacaoWebForLinkRepository
    {
        public int BuscarTipoFluxoId(int solicitacaoId)
        {
            return DbSet
                .Include(x => x.Fluxo)
                .FirstOrDefault(x => x.ID == solicitacaoId)
                .Fluxo.FLUXO_TP_ID;
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <param name="fornecedorId"></param>
        /// <param name="tipoFluxoId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public List<int> ListarPorId(int contratanteId, int? fornecedorId, int tipoFluxoId, int statusId)
        {
            try
            {
                return DbSet.Include(x => x.Fluxo)
                    .Where(x =>
                        x.CONTRATANTE_ID == contratanteId &&
                        x.PJPF_ID == fornecedorId &&
                        x.Fluxo.FLUXO_TP_ID == tipoFluxoId &&
                        x.SOLICITACAO_STATUS_ID == statusId)
                    .Select(x => x.ID).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao buscar a solicitação.", e);
            }
        }

        public int[] BuscarSolicitacaoAguardandoCarga()
        {
            return DbSet
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == 1 &&
                        y.Papel.PAPEL_TP_ID == 70
                    )
                )
                .Select(x => x.CONTRATANTE_ID).Distinct()
                .ToArray();
        }

        public int[] BuscarSolicitacaoAguardandoRetornoCarga()
        {
            return DbSet
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == 1 &&
                        y.Papel.PAPEL_TP_ID == 80
                    )
                )
                .Select(x => x.CONTRATANTE_ID).Distinct()
                .ToArray();
        }

        public SOLICITACAO BuscarPorIdIncluindoFluxo(int id)
        {
            try
            {
                return DbSet
                    .Include(x => x.Fluxo)
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public List<SOLICITACAO> ListarSolicitacaoAprovadaPorId(int statusId)
        {
            return DbSet
                .Include(x => x.Contratante)
                .Include(x => x.WFD_SOLICITACAO_STATUS)
                .Include(x => x.Usuario)
                .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                .Include(x =>x.Fornecedor)
                .Include(x => x.Fluxo)
                .Include(x =>x.SolicitacaoCadastroFornecedor)
                .Include(x => x.SolicitacaoModificacaoDadosBancario)
                .Include(x =>x.SolicitacaoModificacaoDadosContato)
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == statusId
                        && y.Papel.PAPEL_TP_ID == 70
                    )
                )
                .OrderBy(x => x.ID)
                .ToList();
        }

        public List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao)
        {
            return DbSet
                .Include(x => x.Contratante)
                .Include(x =>x.WFD_SOLICITACAO_STATUS)
                .Include(x =>x.Usuario)
                .Include(x =>x.Fornecedor)
                .Include(x => x.Fluxo)
                .Include(x => x.SolicitacaoCadastroFornecedor)
                .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == (int) EnumStatusTramite.Aguardando
                        && y.Papel.PAPEL_TP_ID == 70
                    )
                            && x.ID == idSolicitacao
                )
                .OrderBy(x => x.ID)
                .ToList();
        }

        public List<SOLICITACAO> ListarSolicitacaoCarga(int idContratante)
        {
            ////Filtros
            //var predicate = PredicateBuilder.New<SOLICITACAO>();

            //if (idContratante > 0)
            //{
            //    predicate = predicate.And(x => x.CONTRATANTE_ID == idContratante);
            //    predicate = predicate.And(x => x.WFD_SOLICITACAO_TRAMITE
            //        .Any(y =>
            //            y.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Aguardando &&
            //            y.Papel.PAPEL_TP_ID == 70
            //            )
            //    );
            //}
            return DbSet.Include("Contratante.WFD_CONTRATANTE_ORG_COMPRAS")
                .Include("Contratante.WFD_PJPF_CATEGORIA")
                .Include(x => x.Fluxo)
                .Include(x => x.SolicitacaoCadastroFornecedor)
                .Include(x => x.SolicitacaoModificacaoDadosBancario)
                .Include(x => x.SolicitacaoModificacaoDadosContato)
                .Include("WFD_SOL_BLOQ.TipoDeFuncaoDuranteBloqueio")
                .Include("WFD_SOL_DESBLOQ.TipoDeFuncaoDuranteBloqueio")
                .AsQueryable()
                //.Where(predicate)
                .OrderBy(x => x.ID)
                .ToList();
        }

        public SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId)
        {
            try
            {
                return DbSet
                    .Include(x=>x.SOLICITACAO_BLOQUEIO)
                    .Include(x=>x.WFD_SOL_DESBLOQ)
                    .Include(x=>x.WFD_SOL_UNSPSC)
                    .Include(x=>x.WFD_SOL_MOD_ENDERECO)
                    .Include(x=>x.SolicitacaoCadastroFornecedor)
                    .Include(x=>x.SolicitacaoModificacaoDadosBancario)
                    .Include(x=>x.SolicitacaoModificacaoDadosContato)
                    .Include(x=>x.SolicitacaoDeDocumentos)
                    .Include(x=>x.Contratante.WFD_CONTRATANTE_ORG_COMPRAS)
                    .Include(x=>x.WFD_INFORM_COMPL)
                    .FirstOrDefault(s => s.ID == solicitacaoId);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarAprovacaoPorId(int id)
        {
            try
            {
                return DbSet
                    .Include(x => x.Contratante)
                    .Include(x => x.ROBO)
                    .Include(x => x.Fluxo)
                    .Include(x => x.Usuario)
                    .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                    .Include("WFD_SOLICITACAO_TRAMITE.WFD_SOLICITACAO_STATUS")
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoDeDocumentos.WFD_ARQUIVOS")
                    .Include("SolicitacaoCadastroFornecedor.WFD_PJPF_CATEGORIA")
                    .Include("SolicitacaoModificacaoDadosBancario.T_BANCO")
                    .Include("SolicitacaoModificacaoDadosBancario.WFD_ARQUIVOS")
                    .Include("WFD_SOL_MOD_ENDERECO.WFD_T_TP_ENDERECO")
                    .Include("WFD_SOL_MOD_ENDERECO.T_UF")
                    .Include(x => x.SolicitacaoModificacaoDadosContato)
                    .Include(x => x.Fornecedor.ROBO)
                    .Include("Fornecedor.WFD_CONTRATANTE_PJPF.WFD_PJPF_CATEGORIA")
                    .Include(x => x.SOLICITACAO_BLOQUEIO)
                    .Include(x => x.WFD_SOL_DESBLOQ)
                    .Include("FORNECEDORBASE.WFD_PJPF_CATEGORIA")
                    .Include("WFD_SOL_UNSPSC.T_UNSPSC")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorSolicitacaoId(int id)
        {
            try
            {
                return DbSet
                    .Include("Contratante")
                    .Include("SolicitacaoCadastroFornecedor.ROBO")
                    .Include(x => x.Fluxo)
                    .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                    .Include("Usuario")
                    .Include("DocumentosDeFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="pjpf"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorDocumento(string pjpf)
        {
            try
            {
                return DbSet
                    .Include("SolicitacaoCadastroFornecedor")
                    .FirstOrDefault(x => x.SolicitacaoCadastroFornecedor.Any(y => y.CNPJ == pjpf || y.CPF == pjpf));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComSolicitacaoCadastroPJPF(int id)
        {
            try
            {
                return DbSet
                    .Include("Contratante")
                    .Include("SolicitacaoCadastroFornecedor")
                    .Include("SolicitacaoModificacaoDadosBancario")
                    .Include("SolicitacaoModificacaoDadosContato")
                    .Include("ROBO")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("ocorreu um erro ao buscar a solicitação.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdControleSolicitacoes(int id)
        {
            try
            {
                return DbSet
                    .Include("Contratante")
                    .Include("ROBO")
                    .Include(x => x.Fluxo)
                    .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                    .Include("WFD_SOLICITACAO_TRAMITE.WFD_SOLICITACAO_STATUS")
                    .Include(x=>x.Usuario)
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoDeDocumentos.WFD_ARQUIVOS")
                    .Include("WFD_SOL_MOD_ENDERECO.WFD_T_TP_ENDERECO")
                    .Include("SolicitacaoCadastroFornecedor.WFD_PJPF_CATEGORIA")
                    .Include("FORNECEDORBASE.WFD_PJPF_CATEGORIA")
                    .Include("FORNECEDORBASE.ROBO")
                    .Include("SolicitacaoModificacaoDadosBancario.T_BANCO")
                    .Include("SolicitacaoModificacaoDadosBancario.WFD_ARQUIVOS")
                    .Include("SolicitacaoModificacaoDadosContato")
                    .Include("SOLICITACAO_BLOQUEIO.TipoDeFuncaoDuranteBloqueio")
                    .Include(x=>x.Fornecedor.ROBO)
                    .Include("Fornecedor.WFD_CONTRATANTE_PJPF.WFD_PJPF_CATEGORIA")
                    .Include("WFD_SOL_UNSPSC.T_UNSPSC")
                    .Include("WFD_SOLICITACAO_PRORROGACAO")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComFornecedoresDireto(int id)
        {
            try
            {
                return DbSet
                    .Include("SolicitacaoDeDocumentos.ListaDeDocumentosDeFornecedor")
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoDeDocumentos.PeriodicidadeDoDocumento")
                    .Include("Contratante")
                    .Include("Contratante.WFD_CONTRATANTE_CONFIG")
                    .Include("WFD_SOL_MENSAGEM")
                    .Include("SolicitacaoModificacaoDadosBancario.T_BANCO")
                    .Include("SolicitacaoModificacaoDadosContato")
                    .Include("WFD_SOL_MOD_ENDERECO.WFD_T_TP_ENDERECO")
                    .Include("WFD_SOL_MOD_ENDERECO.T_UF")
                    .Include("SolicitacaoCadastroFornecedor.WFD_PJPF_CATEGORIA")
                    .Include("WFD_SOL_UNSPSC.T_UNSPSC")
                    .FirstOrDefault(s => s.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdDocumentosSolicitados(int id)
        {
            try
            {
                return DbSet
                    .Include("SolicitacaoDeDocumentos.ListaDeDocumentosDeFornecedor")
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoDeDocumentos.WFD_ARQUIVOS")
                    .Include("Contratante")
                    .Include("Contratante.WFD_CONTRATANTE_CONFIG")
                    .Include("WFD_SOL_MENSAGEM")
                    .Include("SolicitacaoModificacaoDadosBancario.T_BANCO")
                    .Include("SolicitacaoModificacaoDadosBancario.WFD_ARQUIVOS")
                    .Include("SolicitacaoModificacaoDadosContato")
                    .Include("WFD_SOL_MOD_ENDERECO.WFD_T_TP_ENDERECO")
                    .Include("WFD_SOL_MOD_ENDERECO.T_UF")
                    .Include("SolicitacaoCadastroFornecedor.WFD_PJPF_CATEGORIA")
                    .Include("WFD_SOL_UNSPSC.T_UNSPSC")
                    .Include("WFD_SOLICITACAO_PRORROGACAO")
                    .FirstOrDefault(s => s.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComCadPjpf(int id)
        {
            try
            {
                return DbSet.Include("SolicitacaoCadastroFornecedor").FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarX(int id)
        {
            try
            {
                return DbSet
                    .Include(x => x.SolicitacaoDeDocumentos)
                    .Include(x => x.SolicitacaoCadastroFornecedor)
                    .Include(x => x.Contratante)
                    .Include(x => x.Contratante.WFD_CONTRATANTE_CONFIG)
                    .Include(x => x.Contratante.Usuario)
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao buscar a solicitação.", e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComDocumentos(int id)
        {
            try
            {
                return DbSet
                    .Include(x => x.SolicitacaoCadastroFornecedor)
                    .Include("SolicitacaoDeDocumentos.ListaDeDocumentosDeFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include(x => x.WFD_SOL_MENSAGEM)
                    .FirstOrDefault(s => s.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public List<SOLICITACAO> TrazerTodasSolicitacoesAprovadas(int idSolicitacao)
        {
            return DbSet
                .Include("Contratante")
                .Include("WFD_SOLICITACAO_STATUS")
                .Include("Usuario")
                .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                .Include("Fornecedor")
                .Include(x => x.Fluxo)
                .Include("SolicitacaoCadastroFornecedor")
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == 1
                        && y.Papel.PAPEL_TP_ID == 70
                    )
                            && x.ID == idSolicitacao
                )
                .OrderBy(x => x.ID)
                .ToList();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<SOLICITACAO> TrazerSolicitacaoAprovadaPorId()
        {
            return DbSet
                .Include("Contratante")
                .Include("WFD_SOLICITACAO_STATUS")
                .Include("Usuario")
                .Include("WFD_SOLICITACAO_TRAMITE.Papel")
                .Include("Fornecedor")
                .Include(x => x.Fluxo)
                .Include("SolicitacaoCadastroFornecedor")
                .Include("SolicitacaoModificacaoDadosBancario")
                .Include("SolicitacaoModificacaoDadosContato")
                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                    .Any(y =>
                        y.SOLICITACAO_STATUS_ID == 1
                        && y.Papel.PAPEL_TP_ID == 70
                    )
                )
                .OrderBy(x => x.ID)
                .ToList();
        }

        public SOLICITACAO SolicitarProrrogacao(int idSolicitacao, DateTime dias, string motivo)
        {
            try
            {
                var solicitacao = DbSet.FirstOrDefault(x => x.ID == idSolicitacao);
                var prorrogacao = new SOLICITACAO_PRORROGACAO
                {
                    DT_PRORROGACAO_PRAZO = dias,
                    MOTIVO_PRORROGACAO = motivo,
                    DT_SOL_PRORROGACAO = DateTime.Now,
                    WFD_SOLICITACAO = solicitacao
                };

                Context.Entry(prorrogacao).State = EntityState.Added;
                Context.SaveChanges();
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar as solicitar prorrogação de prazo.", ex);
            }
        }

        public SOLICITACAO BuscarSolicitacaoFichaCadastral(int id)
        {
            try
            {
                return DbSet
                    .Include("SolicitacaoDeDocumentos.DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("SolicitacaoCadastroFornecedor")
                    .Include("SolicitacaoModificacaoDadosBancario.T_BANCO")
                    .Include("SolicitacaoModificacaoDadosContato")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar a SOlicitação para Ficha Cadastral", ex);
            }
        }

        public SOLICITACAO BuscarSolicitacaoComBase(int Solicitacaoid)
        {
            try
            {
                return DbSet
                    .Include("FORNECEDORBASE")
                    .Include("WFD_SOLICITACAO_PRORROGACAO")
                    .FirstOrDefault(x => x.ID == Solicitacaoid);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar a Solicitação para Ficha Cadastral", ex);
            }
        }

        public RetornoPesquisa<SOLICITACAO> BuscarPesquisaAcompanhamento(Expression<Func<SOLICITACAO, bool>> filtros,
            int tamanhoPagina, int pagina
            , Func<SOLICITACAO, IComparable> ordenacao)
        {
            try
            {
                var registros = DbSet
                    .Include(x => x.Contratante)
                    .Include(x => x.WFD_SOLICITACAO_STATUS)
                    .Include(x => x.Usuario)
                    .Include("WFD_SOLICITACAO_TRAMITE.WFL_PAPEL")
                    .Include("Fornecedor.WFD_CONTRATANTE_PJPF.WFD_PJPF_CATEGORIA")
                    .Include("SolicitacaoCadastroFornecedor.WFD_PJPF_CATEGORIA")
                    .Include(x => x.FORNECEDORBASE)
                    .Include(x => x.FORNECEDORBASE.WFD_PJPF_CATEGORIA)
                    .Include(x => x.Fluxo)
                    .AsQueryable()
                    .Where(filtros)
                    .ToList();
                var lista = registros.AsQueryable()
                    //.Where(filtros)
                    .OrderBy(ordenacao)
                    .Skip(tamanhoPagina*(pagina - 1))
                    .Take(tamanhoPagina)
                    .ToList();
                return new RetornoPesquisa<SOLICITACAO>
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista,
                    TotalPaginas = (int) Math.Ceiling(registros.Count()/(double) tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }
    }
}