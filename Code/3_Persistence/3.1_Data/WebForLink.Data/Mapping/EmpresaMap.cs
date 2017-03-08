using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            // Properties
            Property(t => t.RazaoSocial)
                .IsRequired();
                //.HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_EMPRESA");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RazaoSocial).HasColumnName("RAZAO_SOCIAL");
            
            //Property(t => t.Login).HasColumnName("Login");

            //Mapeamento de Herança
            Map<Cliente>(m => m.Requires("Tipo").HasValue(1));
            Map<Fornecedor>(m => m.Requires("Tipo").HasValue(2));
            Map<Fabricante>(m => m.Requires("Tipo").HasValue(3));
        }
    }
}