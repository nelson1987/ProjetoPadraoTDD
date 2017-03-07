using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebForLink.Data.Context.Mapping;

namespace WebForLink.Data.Context.Models
{
    public partial class ChMasterDataContext : DbContext
    {
        static ChMasterDataContext()
        {
            Database.SetInitializer<ChMasterDataContext>(null);
        }

        public ChMasterDataContext()
            : base("Name=ChMasterDataContext")
        {
        }

        public DbSet<Arquivo> Arquivoes { get; set; }
        public DbSet<Banco> Bancoes { get; set; }
        public DbSet<Carrinho> Carrinhoes { get; set; }
        public DbSet<Contato> Contatoes { get; set; }
        public DbSet<DocumentoAnexado> DocumentoAnexadoes { get; set; }
        public DbSet<DocumentosSolicitacao> DocumentosSolicitacaos { get; set; }
        public DbSet<Endereco> Enderecoes { get; set; }
        public DbSet<FichaCadastral> FichaCadastrals { get; set; }
        public DbSet<ListaDocumento> ListaDocumentoes { get; set; }
        public DbSet<ListasSolicitante> ListasSolicitantes { get; set; }
        public DbSet<Responsavel> Responsavels { get; set; }
        public DbSet<Solicitacao> Solicitacaos { get; set; }
        public DbSet<Solicitado> Solicitadoes { get; set; }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
