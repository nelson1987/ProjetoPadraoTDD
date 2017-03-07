using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
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
                .HasMaxLength(20);
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            this.ToTable("Solicitado");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Cnpj).HasColumnName("Cnpj");
            this.Property(t => t.RazaoSocial).HasColumnName("RazaoSocial");
        }
    }
}
