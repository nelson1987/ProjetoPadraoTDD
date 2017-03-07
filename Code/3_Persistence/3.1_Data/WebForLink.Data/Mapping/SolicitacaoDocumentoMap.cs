using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {

            // Primary Key
            HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            Property(t => t.IdSolicitante)
                .HasColumnName("IdSolicitante")
                .IsRequired();
            Property(t => t.IdSolicitado)
                .HasColumnName("IdSolicitado")
                .IsRequired();
            //Property(t => t.IdFichaCadastral)
            //    .HasColumnName("IdFichaCadastral")
            //    .IsOptional();

            Ignore(t => t.Solicitado);
            Ignore(t => t.Solicitante);
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            ToTable("Solicitacao");

            //HasOptional(s => s.FichaCadastral)
            //            .WithOptionalDependent(ad => ad.Solicitacao);

            //this.HasOptional(t => t.FichaCadastral)
            //    (t => t.Solicitacao)
            //    .HasForeignKey(d => d.IdFichaCadastral);

            //this.HasOptional(t => t.WFD_CONTRATANTE_PJPF)
            //    .WithMany(t => t.WFD_PJPF_CONTATOS)
            //    .HasForeignKey(d => d.CONTRATANTE_PJPF_ID);
            //this.Property(t => t.Id).HasColumnName("Id");
            //this.Property(t => t.Solicitado.Email).HasColumnName("EmailSolicitante");
        }
    }
}
