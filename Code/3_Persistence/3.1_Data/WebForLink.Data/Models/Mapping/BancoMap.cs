using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class BancoMap : EntityTypeConfiguration<Banco>
    {
        public BancoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Agencia)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.AgenciaDv)
                .HasMaxLength(1);

            this.Property(t => t.Conta)
                .IsRequired()
                .HasMaxLength(18);

            this.Property(t => t.ContaDv)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("Banco");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Agencia).HasColumnName("Agencia");
            this.Property(t => t.AgenciaDv).HasColumnName("AgenciaDv");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.ContaDv).HasColumnName("ContaDv");

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            this.HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Bancoes)
                .HasForeignKey(d => d.IdFichaCadastral);

        }
    }
}
