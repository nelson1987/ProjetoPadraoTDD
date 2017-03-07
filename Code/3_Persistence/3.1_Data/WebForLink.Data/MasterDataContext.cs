using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
/*using System.Diagnostics;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Context.Mapping;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context
{
    public class ChMasterDataContext : BaseDbContext
    {
        static ChMasterDataContext()
        {
            Database.SetInitializer<ChMasterDataContext>(null);
        }

        public ChMasterDataContext()
            : base("MusicStoreEntities")
        {
            Database.Log = sql => Debug.Write(sql);
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


            modelBuilder.Properties()
                        .Where(p => p.Name.StartsWith("Id"))
                        .Configure(p => p.IsKey());

            modelBuilder.Configurations.Add(new ArquivoMap());
            modelBuilder.Configurations.Add(new BancoMap());
            modelBuilder.Configurations.Add(new CarrinhoMap());
            modelBuilder.Configurations.Add(new ContatoMap());
            modelBuilder.Configurations.Add(new DocumentoAnexadoMap());
            modelBuilder.Configurations.Add(new DocumentosSolicitacaoMap());
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new FichaCadastralMap());
            modelBuilder.Configurations.Add(new ListaDocumentoMap());
            modelBuilder.Configurations.Add(new ListasSolicitanteMap());
            modelBuilder.Configurations.Add(new ResponsavelMap());
            modelBuilder.Configurations.Add(new SolicitacaoMap());
            modelBuilder.Configurations.Add(new SolicitadoMap());
            modelBuilder.Configurations.Add(new SolicitanteMap());

            base.OnModelCreating(modelBuilder);
        }

        #region DbSet

        public virtual DbSet<ListaDocumento> ListaDocumento { get; set; }
        public virtual DbSet<ListasSolicitante> ListasSolicitante { get; set; }
        public virtual DbSet<Solicitante> Solicitante { get; set; }
        public virtual DbSet<Solicitacao> Solicitacao { get; set; }
        public virtual DbSet<DocumentoSolicitacao> DocumentosSolicitacao { get; set; }
        public virtual DbSet<Solicitado> Solicitado { get; set; }
        public virtual DbSet<Responsavel> Responsavel { get; set; }
        public virtual DbSet<DocumentoAnexado> DocumentoAnexado { get; set; }
        public virtual DbSet<Arquivo> Arquivo { get; set; }
        public virtual DbSet<FichaCadastral> FichaCadastral { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<Banco> Banco { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Carrinho> Carrinho { get; set; }
        #endregion*/
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Context.Mapping;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context
{
    public partial class ChMasterDataContext : BaseDbContext
    {
        static ChMasterDataContext()
        {
            //Database.SetInitializer<ChMasterDataContext>(new DropCreateDatabaseAlways<ChMasterDataContext>());
            Database.SetInitializer<ChMasterDataContext>(null);
        }
        public ChMasterDataContext()
            : base("MusicStoreEntities")
        {
            //this.Configuration.LazyLoadingEnabled = true;
            Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<Solicitacao> Solicitacao { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<Solicitante> Solicitante { get; set; }
        public DbSet<ListasSolicitante> ListasSolicitante { get; set; }
        public DbSet<ListaDocumento> ListaDocumento { get; set; }
        public DbSet<Solicitado> Solicitado { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<FichaCadastral> FichaCadastral { get; set; }
        public DbSet<DocumentoSolicitacao> DocumentosSolicitacao { get; set; }
        public DbSet<DocumentoAnexado> DocumentoAnexado { get; set; }
        public DbSet<Arquivo> Arquivo { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            //throw new UnintentionalCodeFirstException();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Pluraliza de Tabelas
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //Deletar em cascata
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //Deletar em cascata

            modelBuilder.Configurations.Add(new ArquivoMap());
            modelBuilder.Configurations.Add(new DocumentoAnexadoMap());
            modelBuilder.Configurations.Add(new SolicitacaoMap());
            modelBuilder.Configurations.Add(new SolicitanteMap());
            modelBuilder.Configurations.Add(new ListasSolicitanteMap());
            modelBuilder.Configurations.Add(new ListaDocumentoMap());
            modelBuilder.Configurations.Add(new SolicitadoMap());
            modelBuilder.Configurations.Add(new ResponsavelMap());
            modelBuilder.Configurations.Add(new FichaCadastralMap());
            modelBuilder.Configurations.Add(new DocumentosSolicitacaoMap());
            modelBuilder.Configurations.Add(new BancoMap());
            modelBuilder.Configurations.Add(new ContatoMap());
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new CarrinhoMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class BancoMap : EntityTypeConfiguration<Banco>
    {
        public BancoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Agencia)
                .IsRequired()
                .HasMaxLength(4);

            Property(t => t.AgenciaDv)
                .HasMaxLength(1);

            Property(t => t.Conta)
                .IsRequired()
                .HasMaxLength(18);

            Property(t => t.ContaDv)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            ToTable("Banco");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            Property(t => t.Numero).HasColumnName("Numero");
            Property(t => t.Agencia).HasColumnName("Agencia");
            Property(t => t.AgenciaDv).HasColumnName("AgenciaDv");
            Property(t => t.Conta).HasColumnName("Conta");
            Property(t => t.ContaDv).HasColumnName("ContaDv");

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Banco)
                .HasForeignKey(d => d.IdFichaCadastral);
        }
    }
    public class ListaDocumentoMap : EntityTypeConfiguration<ListaDocumento>
    {
        public ListaDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ListaDocumento");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdListaSolicitante).HasColumnName("IdListaSolicitante");
            this.Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");
            this.Property(t => t.IdDescricaoDocumentoCH).HasColumnName("IdDescricaoDocumentoCH");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);


            // Relationships
            this.HasRequired(t => t.ListaSolicitante)
                .WithMany(t => t.ListaDocumento)
                .HasForeignKey(d => d.IdListaSolicitante);

        }
    }

    public class ListasSolicitanteMap : EntityTypeConfiguration<ListasSolicitante>
    {
        public ListasSolicitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ListasSolicitante");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);

            // Relationships
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.ListasDocumentosSolicitante)
                .HasForeignKey(d => d.IdSolicitante);

        }
    }

    public class SolicitanteMap : EntityTypeConfiguration<Solicitante>
    {
        public SolicitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CodigoCliente)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.NomeEmpresa)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Solicitante");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CodigoCliente).HasColumnName("CodigoCliente");
            this.Property(t => t.NomeEmpresa).HasColumnName("NomeEmpresa");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);
        }
    }

    public class FichaCadastralMap : EntityTypeConfiguration<FichaCadastral>
    {
        public FichaCadastralMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("FichaCadastral");
            this.Property(t => t.Id).HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Status).HasColumnName("Status");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);
        }
    }

    public class ResponsavelMap : EntityTypeConfiguration<Responsavel>
    {
        public ResponsavelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Responsavel");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);

            // Relationships
            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Responsaveis)
                .HasForeignKey(d => d.IdSolicitado);

        }
    }

    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Solicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.StatusSolicitacao).HasColumnName("StatusSolicitacao");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");
            this.Property(t => t.DataReenvio).HasColumnName("DataReenvio");
            this.Property(t => t.DataCancelamento).HasColumnName("DataCancelamento");
            this.Property(t => t.DataVisualizado).HasColumnName("DataVisualizado");
            this.Property(t => t.FichaCadastralObrigatoria).HasColumnName("FichaCadastralObrigatoria");

            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);
            this.Ignore(x => x.FichaCadastralId);

            // Relationships
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdSolicitante);

            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Solicitacoes)
                .HasForeignKey(d => d.IdSolicitado);

            //this.HasOptional(t => t.FichaCadastral)
            //.WithOptionalDependent(x => x.Solicitacao);


            this.HasOptional(t => t.FichaCadastral)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdFichaCadastral);
        }
    }

    public class SolicitadoMap : EntityTypeConfiguration<Solicitado>
    {
        public SolicitadoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Cnpj)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.RazaoSocial)
                .HasMaxLength(50);

            this.Property(t => t.Cidade)
                .HasMaxLength(50);

            this.Property(t => t.Estado)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Solicitado");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Cnpj).HasColumnName("Cnpj");
            this.Property(t => t.Estado).HasColumnName("Estado");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.RazaoSocial).HasColumnName("RazaoSocial");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);
        }
    }

    public class DocumentosSolicitacaoMap : EntityTypeConfiguration<DocumentoSolicitacao>
    {
        public DocumentosSolicitacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DescricaoDocumento)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentosSolicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitacao).HasColumnName("IdSolicitacao");
            this.Property(t => t.DescricaoDocumento).HasColumnName("DescricaoDocumento");
            this.Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");
            this.Property(t => t.DescricaoDocumentoCH).HasColumnName("DescricaoDocumentoCH");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);

            // Relationships
            this.HasRequired(t => t.Solicitacao)
                .WithMany(t => t.DocumentoSolicitacao)
                .HasForeignKey(d => d.IdSolicitacao);

            this.HasMany(t => t.DocumentoAnexados);

        }
    }

    public class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NomeOriginal)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Arquivo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdDocumentoAnexado).HasColumnName("IdDocumentoAnexado");
            this.Property(t => t.NomeOriginal).HasColumnName("NomeOriginal");
            this.Property(t => t.JaBaixado).HasColumnName("JaBaixado");
            this.Property(t => t.ExtensaoArquivo).HasColumnName("ExtensaoArquivo");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);

            // Relationships
            this.HasRequired(t => t.DocumentoAnexado)
                .WithMany(t => t.Arquivos)
                .HasForeignKey(d => d.IdDocumentoAnexado);
        }
    }

    public class DocumentoAnexadoMap : EntityTypeConfiguration<DocumentoAnexado>
    {
        public DocumentoAnexadoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("DocumentoAnexado");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdDocumentoSolicitado).HasColumnName("IdDocumentoSolicitado");
            //this.Property(t => t.IdArquivo).HasColumnName("IdArquivo");
            this.Property(t => t.DataReprovacao).HasColumnName("DataReprovacao");
            this.Property(t => t.MensagemReprovacao).HasColumnName("MensagemReprovacao");
            this.Property(t => t.Reprovado).HasColumnName("Reprovado");
            this.Property(t => t.DataUltimoDownload).HasColumnName("DataUltimoDownload");
            this.Ignore(x => x.EhValido);
            this.Ignore(x => x.ValidationResult);

            // Relationships
            this.HasRequired(t => t.DocumentosSolicitacao)// .Solicitacao)
                .WithMany(t => t.DocumentoAnexados)
                .HasForeignKey(d => d.IdDocumentoSolicitado);
        }
    }
}


/*

}
}*/
