using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Mapping;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Context
{
    public class WebForLinkContexto : BaseDbContext
    {
        static WebForLinkContexto()
        {
            Database.SetInitializer<WebForLinkContexto>(null);
        }

        public WebForLinkContexto()
            : base("name=WFDModel")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.UseDatabaseNullSemantics = true;
            //Configuration.AutoDetectChangesEnabled = true;
            Database.SetInitializer<WebForLinkContexto>(null);
            Database.Log = sql => Debug.Write(sql);
        }

        
        public DbSet<WAC_ACESSO_LOG> WAC_ACESSO_LOG { get; set; }
        public DbSet<APLICACAO> WAC_APLICACAO { get; set; }
        public DbSet<FUNCAO> WAC_FUNCAO { get; set; }
        public DbSet<Perfil> WAC_PERFIL { get; set; }
        public DbSet<ARQUIVOS> WFD_ARQUIVOS { get; set; }
        public DbSet<CONFIGURACAO> WFD_CONFIG { get; set; }
        public DbSet<DescricaoDeDocumentos> WFD_DESCRICAO_DOCUMENTOS { get; set; }
        public DbSet<WFD_DESCRICAO_DOCUMENTOS_CH> WFD_DESCRICAO_DOCUMENTOS_CH { get; set; }
        public DbSet<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
        public DbSet<DocumentosCompartilhados> MEU_DOCUMENTOS_COMPARTILHADOS { get; set; }
        public DbSet<DESTINATARIO> WFD_DESTINATARIO { get; set; }
        public DbSet<GRUPO> WFD_GRUPO { get; set; }
        public DbSet<WFD_INFORM_COMPL> WFD_INFORM_COMPL { get; set; }
        public DbSet<Usuario> WFD_USUARIO { get; set; }
        public DbSet<USUARIO_SENHAS> WFD_USUARIO_SENHAS_HIST { get; set; }
        public DbSet<Papel> WFL_PAPEL { get; set; }
        public DbSet<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
        public DbSet<WFD_PJPF_CONTRATANTE_ORG_COMPRAS> WFD_PJPF_CONTRATANTE_ORG_COMPRAS { get; set; }

        #region Fluxo

        public DbSet<Fluxo> WFL_FLUXO { get; set; }
        public DbSet<FLUXO_SEQUENCIA_PRE_REQUIS> WFL_FLUXO_SEQ_PRE_REQUIS { get; set; }
        public DbSet<FLUXO_SEQUENCIA> WFL_FLUXO_SEQUENCIA { get; set; }

        #endregion

        #region QuestionarioDinamico

        public DbSet<QUESTIONARIO> QIC_QUESTIONARIO { get; set; }
        public DbSet<QUESTIONARIO_CATEGORIA> QIC_QUESTIONARIO_CATEGORIA { get; set; }
        public DbSet<QUESTIONARIO_ABA> QIC_QUEST_ABA { get; set; }
        public DbSet<QUESTIONARIO_PERGUNTA> QIC_QUEST_ABA_PERG { get; set; }
        public DbSet<QUESTIONARIO_PAPEL> QIC_QUEST_ABA_PERG_PAPEL { get; set; }
        public DbSet<QUESTIONARIO_RESPOSTA> QIC_QUEST_ABA_PERG_RESP { get; set; }

        #endregion

        #region Contratante

        public DbSet<Contratante> Contratante { get; set; }
        public DbSet<CONTRATANTE_CONFIGURACAO> WFD_CONTRATANTE_CONFIG { get; set; }
        public DbSet<CONTRATANTE_CONFIGURACAO_EMAIL> WFD_CONTRATANTE_CONFIG_EMAIL { get; set; }
        public DbSet<CONTRATANTE_LOG> WFD_CONTRATANTE_LOG { get; set; }
        public DbSet<CONTRATANTE_ORGANIZACAO_COMPRAS> WFD_CONTRATANTE_ORG_COMPRAS { get; set; }

        #endregion

        #region FornecedorBase

        public DbSet<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
        public DbSet<FORNECEDORBASE_CONTATOS> WFD_PJPF_BASE_CONTATOS { get; set; }
        public DbSet<FORNECEDORBASE_CONVITE> WFD_PJPF_BASE_CONVITE { get; set; }
        public DbSet<FORNECEDORBASE_ENDERECO> WFD_PJPF_BASE_ENDERECO { get; set; }
        public DbSet<FORNECEDORBASE_IMPORTACAO> WFD_PJPF_BASE_IMPORTACAO { get; set; }
        public DbSet<FORNECEDORBASE_UNSPSC> WFD_PJPF_BASE_UNSPSC { get; set; }

        #endregion

        #region Fornecedor

        public virtual DbSet<Fornecedor> WFD_PJPF { get; set; }
        public DbSet<BancoDoFornecedor> WFD_PJPF_BANCO { get; set; }
        public DbSet<FORNECEDOR_CATEGORIA> WFD_PJPF_CATEGORIA { get; set; }
        public DbSet<FORNECEDOR_CATEGORIA_CH> WFD_PJPF_CATEGORIA_CH { get; set; }
        public DbSet<FORNECEDOR_CONTATOS> WFD_PJPF_CONTATOS { get; set; }
        public DbSet<DocumentosDoFornecedor> WFD_PJPF_DOCUMENTOS { get; set; }
        public DbSet<VersionamentoDeDocumentoDoFornecedor> WFD_PJPF_DOCUMENTOS_VERSAO { get; set; }
        public DbSet<FORNECEDOR_ENDERECO> WFD_PJPF_ENDERECO { get; set; }
        public DbSet<FORNECEDOR_INFORM_COMPL> WFD_PJPF_INFORM_COMPL { get; set; }
        public DbSet<ListaDeDocumentosDeFornecedor> WFD_PJPF_LISTA_DOCUMENTOS { get; set; }
        public DbSet<FORNECEDOR_SOLICITACAO> WFD_PJPF_SOLICITACAO { get; set; }
        public DbSet<FORNECEDOR_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get; set; }
        public DbSet<FORNECEDOR_STATUS> WFD_PJPF_STATUS { get; set; }
        public DbSet<FORNECEDOR_UNSPSC> WFD_PJPF_UNSPSC { get; set; }

        #endregion

        #region Robo

        public DbSet<ROBO> WFD_PJPF_ROBO { get; set; }
        public DbSet<ROBO_LOG> WFD_PJPF_ROBO_LOG { get; set; }

        #endregion

        #region Solicitacao

        public DbSet<SOLICITACAO> WFD_SOLICITACAO { get; set; }
        public DbSet<SOLICITACAO_BLOQUEIO> WFD_SOL_BLOQ { get; set; }
        public DbSet<SolicitacaoCadastroFornecedor> WFD_SOL_CAD_PJPF { get; set; }
        public DbSet<SOLICITACAO_DESBLOQUEIO> WFD_SOL_DESBLOQ { get; set; }
        public DbSet<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
        public DbSet<SOLICITACAO_MENSAGEM> WFD_SOL_MENSAGEM { get; set; }
        public DbSet<SolicitacaoModificacaoDadosBancario> WFD_SOL_MOD_BANCO { get; set; }
        public DbSet<SolicitacaoModificacaoDadosContato> WFD_SOL_MOD_CONTATO { get; set; }
        public DbSet<SOLICITACAO_MODIFICACAO_DADOSGERAIS> WFD_SOL_MOD_DGERAIS_SEQ { get; set; }
        public DbSet<SOLICITACAO_MODIFICACAO_ENDERECO> WFD_SOL_MOD_ENDERECO { get; set; }
        public DbSet<SOLICITACAO_UNSPSC> WFD_SOL_UNSPSC { get; set; }
        public DbSet<SOLICITACAO_PRORROGACAO> WFD_SOLICITACAO_PRORROGACAO { get; set; }
        public DbSet<SOLICITACAO_STATUS> WFD_SOLICITACAO_STATUS { get; set; }
        public DbSet<SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }

        #endregion

        #region Tipos

        public DbSet<TIPO_CADASTRO_FORNECEDOR> WFD_TIPO_CADASTRO { get; set; }
        public DbSet<TIPO_CONTRATANTE> WFD_TIPO_CONTRATANTE { get; set; }
        public DbSet<TipoDeDocumento> WFD_TIPO_DOCUMENTOS { get; set; }
        public DbSet<TIPO_DOCUMENTOS_CH> WFD_TIPO_DOCUMENTOS_CH { get; set; }
        public DbSet<TiposDeBanco> T_BANCO { get; set; }
        public DbSet<TiposDePais> T_PAIS { get; set; }
        public DbSet<TiposDeEstado> T_UF { get; set; }
        public DbSet<TIPO_UNSPSC> T_UNSPSC { get; set; }
        public DbSet<TIPO_DESCRICAO> WFD_T_DESCRICAO { get; set; }
        public DbSet<TIPO_FUNCAO_BLOQUEIO> WFD_T_FUNCAO_BLOQUEIO { get; set; }
        public DbSet<TIPO_GRUPO> WFD_T_GRUPO { get; set; }
        public DbSet<TIPO_PERIODICIDADE> WFD_TIPO_PERIODICIDADE { get; set; }
        public DbSet<TIPO_STATUS_PRECADASTRO> WFD_T_STATUS_PRECADASTRO { get; set; }
        public DbSet<TIPO_CONTATO> WFD_T_TP_CONTATO { get; set; }
        public DbSet<TIPO_EMAIL> WFD_T_TP_EMAIL { get; set; }
        public DbSet<TIPO_ENDERECO> WFD_T_TP_ENDERECO { get; set; }
        public DbSet<TIPO_FORNECEDOR> WFD_T_TP_PJPF { get; set; }
        public DbSet<TIPO_VISAO> WFL_TP_VISAO { get; set; }
        public DbSet<TipoDeFluxo> WFL_T_TP_FLUXO { get; set; }
        public DbSet<TipoDePapel> WFL_T_TP_PAPEL { get; set; }

        #endregion
        public virtual void ChangeObjectState(object model, EntityState state)
        {
            //Aqui trocamos o estado do objeto, 
            //facilita quando temos alterações e exclusões
            ((IObjectContextAdapter) this)
                .ObjectContext
                .ObjectStateManager
                .ChangeObjectState(model, state);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Pluraliza de Tabelas
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //Deletar em cascata
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //Deletar em cascata
            //---
            modelBuilder.Configurations.Add(new MEU_COMPARTILHAMENTOSConfiguration());
            modelBuilder.Configurations.Add(new MEU_DOCUMENTOS_COMPARTILHADOSConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUEST_ABAConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUEST_ABA_PERGConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUEST_ABA_PERG_PAPELConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUEST_ABA_PERG_RESPConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUESTIONARIOConfiguration());
            modelBuilder.Configurations.Add(new QIC_QUESTIONARIO_CATEGORIAConfiguration());
            modelBuilder.Configurations.Add(new T_BANCOConfiguration());
            modelBuilder.Configurations.Add(new T_PAISConfiguration());
            modelBuilder.Configurations.Add(new T_UFConfiguration());
            modelBuilder.Configurations.Add(new T_UNSPSCConfiguration());
            modelBuilder.Configurations.Add(new WAC_ACESSO_LOGConfiguration());
            modelBuilder.Configurations.Add(new WAC_APLICACAOConfiguration());
            modelBuilder.Configurations.Add(new WAC_FUNCAOConfiguration());
            modelBuilder.Configurations.Add(new WAC_PERFILConfiguration());
            modelBuilder.Configurations.Add(new WFD_ARQUIVOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONFIGConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTEConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTE_CONFIGConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTE_CONFIG_EMAILConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTE_LOGConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTE_ORG_COMPRASConfiguration());
            modelBuilder.Configurations.Add(new WFD_CONTRATANTE_PJPFConfiguration());
            modelBuilder.Configurations.Add(new WFD_DESCRICAO_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_DESCRICAO_DOCUMENTOS_CHConfiguration());
            modelBuilder.Configurations.Add(new WFD_DESTINATARIOConfiguration());
            modelBuilder.Configurations.Add(new WFD_GRUPOConfiguration());
            modelBuilder.Configurations.Add(new WFD_INFORM_COMPLConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPFConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BANCOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASEConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASE_CONTATOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASE_CONVITEConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASE_ENDERECOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASE_IMPORTACAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_BASE_UNSPSCConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_CATEGORIAConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_CATEGORIA_CHConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_CONTATOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_CONTRATANTE_ORG_COMPRASConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_DOCUMENTOS_VERSAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_ENDERECOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_INFORM_COMPLConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_LISTA_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_ROBOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_ROBO_LOGConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_SOLICITACAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_SOLICITACAO_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_STATUSConfiguration());
            modelBuilder.Configurations.Add(new WFD_PJPF_UNSPSCConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_BLOQConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_CAD_PJPFConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_DESBLOQConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_MENSAGEMConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_MOD_BANCOConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_MOD_CONTATOConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_MOD_DGERAIS_SEQConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_MOD_ENDERECOConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOL_UNSPSCConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOLICITACAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOLICITACAO_PRORROGACAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOLICITACAO_STATUSConfiguration());
            modelBuilder.Configurations.Add(new WFD_SOLICITACAO_TRAMITEConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_DESCRICAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_FUNCAO_BLOQUEIOConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_GRUPOConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_PERIODICIDADEConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_STATUS_PRECADASTROConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_TP_CONTATOConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_TP_EMAILConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_TP_ENDERECOConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_TP_PJPFConfiguration());
            modelBuilder.Configurations.Add(new WFD_T_VISAOConfiguration());
            modelBuilder.Configurations.Add(new WFD_TIPO_CADASTROConfiguration());
            modelBuilder.Configurations.Add(new WFD_TIPO_CONTRATANTEConfiguration());
            modelBuilder.Configurations.Add(new WFD_TIPO_DOCUMENTOSConfiguration());
            modelBuilder.Configurations.Add(new WFD_TIPO_DOCUMENTOS_CHConfiguration());
            modelBuilder.Configurations.Add(new WFD_USUARIOConfiguration());
            modelBuilder.Configurations.Add(new WFD_USUARIO_SENHAS_HISTConfiguration());
            modelBuilder.Configurations.Add(new WFL_FLUXOConfiguration());
            modelBuilder.Configurations.Add(new WFL_FLUXO_SEQ_PRE_REQUISConfiguration());
            modelBuilder.Configurations.Add(new WFL_FLUXO_SEQUENCIAConfiguration());
            modelBuilder.Configurations.Add(new WFL_PAPELConfiguration());
            modelBuilder.Configurations.Add(new WFL_T_TP_FLUXOConfiguration());
            modelBuilder.Configurations.Add(new WFL_T_TP_PAPELConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}