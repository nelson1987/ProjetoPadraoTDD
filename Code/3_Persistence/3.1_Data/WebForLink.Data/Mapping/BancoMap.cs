using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
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
}