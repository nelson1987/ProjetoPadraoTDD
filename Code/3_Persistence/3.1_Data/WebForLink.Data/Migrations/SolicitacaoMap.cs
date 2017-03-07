using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    /*
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Solicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");

            // Relationships
            this.HasOptional(t => t.FichaCadastral)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdFichaCadastral);
            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Solicitacoes)
                .HasForeignKey(d => d.IdSolicitado);
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdSolicitante);

        }
    }
    */
}
