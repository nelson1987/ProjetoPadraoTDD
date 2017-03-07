using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Logradouro)
                .HasMaxLength(255);

            Property(t => t.Numero)
                .HasMaxLength(50);

            Property(t => t.Complemento)
                .HasMaxLength(255);

            Property(t => t.CEP)
                .HasMaxLength(9);

            Property(t => t.Bairro)
                .HasMaxLength(100);

            Property(t => t.Cidade)
                .HasMaxLength(100);

            Property(t => t.UF)
                .HasMaxLength(2);

            Property(t => t.Pais)
                .HasMaxLength(100);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            ToTable("Endereco");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            Property(t => t.Tipo).HasColumnName("Tipo");
            Property(t => t.Logradouro).HasColumnName("Logradouro");
            Property(t => t.Numero).HasColumnName("Numero");
            Property(t => t.Complemento).HasColumnName("Complemento");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.Bairro).HasColumnName("Bairro");
            Property(t => t.Cidade).HasColumnName("Cidade");
            Property(t => t.UF).HasColumnName("UF");
            Property(t => t.Pais).HasColumnName("Pais");
            this.Ignore(t => t.EhValido);
            this.Ignore(t => t.ValidationResult);

            // Relationships
            HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Endereco)
                .HasForeignKey(d => d.IdFichaCadastral);
        }
    }
}