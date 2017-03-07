using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("Solicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");

            // Relationships
            this.HasOptional(t => t.FichaCadastral)
                .WithMany(t => t.Solicitacaos)
                .HasForeignKey(d => d.IdFichaCadastral);
            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Solicitacaos)
                .HasForeignKey(d => d.IdSolicitado);
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.Solicitacaos)
                .HasForeignKey(d => d.IdSolicitante);

        }
    }
}
