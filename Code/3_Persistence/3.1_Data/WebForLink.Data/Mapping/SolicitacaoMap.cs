using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    /*
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {
            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            //Ignore(t => t.FichaCadastral);

            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Solicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.StatusSolicitacao).HasColumnName("StatusSolicitacao");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");
            this.Property(t => t.DataReenvio).HasColumnName("DataReenvio");
            this.Property(t => t.DataCancelamento).HasColumnName("DataCancelamento");
            this.Property(t => t.DataVisualizado).HasColumnName("DataVisualizado");
            this.Property(t => t.FichaCadastralObrigatoria).HasColumnName("FichaCadastralObrigatoria");

            // Relationships
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdSolicitante);

            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Solicitacoes)
                .HasForeignKey(d => d.IdSolicitado);

            this.HasOptional(t => t.FichaCadastral)
                .WithMany(t => t.Solicitacao)
                .HasForeignKey(d => d.IdFichaCadastral);
        }
    }
    */
}