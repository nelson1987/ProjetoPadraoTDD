using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Context.Mapping;
using WebForLink.Domain.Entities;

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

        public DbSet<Usuario> WFD_USUARIO { get; set; }

        public virtual void ChangeObjectState(object model, EntityState state)
        {
            //Aqui trocamos o estado do objeto, 
            //facilita quando temos alterações e exclusões
            ((IObjectContextAdapter)this)
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
            modelBuilder.Configurations.Add(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}