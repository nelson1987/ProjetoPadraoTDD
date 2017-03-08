using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class ContratanteMap : EntityTypeConfiguration<Contratante>
    {
        public ContratanteMap()
        {
            //Map(m => m.Requires("Tipo").HasValue(1));
            ToTable("WFL_CONTRATANTE");

            Property(t => t.RazaoSocial)
                .IsRequired();

            Property(t => t.Documento)
                .IsRequired();

            HasMany(t => t.ConfiguracaoSistemas)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.CategoriasCadastradas)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.Importacoes)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.Usuarios)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.Solicitacoes)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.Perfis)
                .WithRequired(x => x.Contratante);
            HasMany(t => t.Papeis)
                .WithRequired(x => x.Contratante);

            HasMany(t => t.Empresas)
                .WithMany(x => x.Contratantes)
                .Map(m =>
                {
                    m.ToTable("EMPRESA_CONTRATANTE");
                    m.MapLeftKey("EMPRESA_ID");
                    m.MapRightKey("CONTRATANTE_ID");
                });
            Map<ClienteAncora>(m => m.Requires("TIPO_CONTRATANTE").HasValue(1));
            Map<FornecedorIndividual>(m => m.Requires("TIPO_CONTRATANTE").HasValue(2));
            Map<FabricanteAncora>(m => m.Requires("TIPO_CONTRATANTE").HasValue(3));
            /*
            HasMany(t => t.ConfiguracaoSistemas)
                .WithMany(x => x.Contratante)
                .Map(m =>
                {
                    m.ToTable("CLIENTE_FORNECEDOR");
                    m.MapLeftKey("CLIENTE_ID");
                    m.MapRightKey("FORNECEDOR_ID");
                });

            HasMany(t => t.Fabricantes)
                .WithMany(x => x.Clientes)
                .Map(m =>
                {
                    m.ToTable("CLIENTE_FABRICANTE");
                    m.MapLeftKey("CLIENTE_ID");
                    m.MapRightKey("FABRICANTE_ID");
                });
            */
        }
    }

}
