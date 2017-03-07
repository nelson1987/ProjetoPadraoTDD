using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Logradouro)
                .HasMaxLength(255);

            this.Property(t => t.Numero)
                .HasMaxLength(50);

            this.Property(t => t.Complemento)
                .HasMaxLength(255);

            this.Property(t => t.CEP)
                .HasMaxLength(9);

            this.Property(t => t.Bairro)
                .HasMaxLength(100);

            this.Property(t => t.Cidade)
                .HasMaxLength(100);

            this.Property(t => t.UF)
                .HasMaxLength(2);

            this.Property(t => t.Pais)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Endereco");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Logradouro).HasColumnName("Logradouro");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.Bairro).HasColumnName("Bairro");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.Pais).HasColumnName("Pais");

            // Relationships
            this.HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Endereco)
                .HasForeignKey(d => d.IdFichaCadastral);

        }
    }
}
