using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Context.Mapping;
using WebForLink.Domain.Entities;
using WebForLink.Win.Banco;

namespace WebForLink.Data.Context
{
    public class WebForLinkContexto : BaseDbContext
    {
        static WebForLinkContexto()
        {
            Database.SetInitializer<WebForLinkContexto>(null);
        }

        public WebForLinkContexto()
            : base("WebForLinkContexto")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            // Log all the database calls when in Debug.
            Database.Log = message => Debug.Write(message);
        }

        public DbSet<Aplicacao> Aplicacao { get; set; }
        public DbSet<Contratante> Contratante { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Solicitacao> Solicitacao { get; set; }

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
                .Where(p => p.Name.Equals("Id"))
                .Configure(p => p.IsKey());
            modelBuilder.Properties()
                .Where(p => p.Name.Equals("Id"))
                .Configure(x => x.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar")
                    .HasMaxLength(255));
            
            modelBuilder.Ignore<Domain.Validation.ValidationResult>();
            /*
            modelBuilder.Configurations.Add(new AplicacaoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            //modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
            modelBuilder.Configurations.Add(new FabricanteMap());
            modelBuilder.Configurations.Add(new ContratanteMap());
            modelBuilder.Configurations.Add(new SolicitacaoMap());
            modelBuilder.Configurations.Add(new SolicitacaoCadastroMap());
            modelBuilder.Configurations.Add(new SolicitacaoModificacaoBancoMap());
            */
            base.OnModelCreating(modelBuilder);
        }
    }
}