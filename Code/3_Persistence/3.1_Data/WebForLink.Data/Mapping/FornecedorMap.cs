using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Data.Context.Mapping
{
    public class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            // Primary Key
            //HasKey(t => t.Id);
            
            // Properties
            Property(t => t.RazaoSocial)
                //.HasMaxLength(50)
                .IsRequired();

            Property(t => t.Documento)
                //.HasMaxLength(14)
                .IsRequired();

            //Property(t => t.Contratante)
            //    .IsRequired();

            //Property(t => t.UF)
            //    .HasMaxLength(2);

            //Ignore(t => t.ValidationResult);

            ToTable("WFD_PJPF");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RazaoSocial).HasColumnName("RAZAO_SOCIAL");
            this.Property(t => t.Documento).HasColumnName("CNPJ");
            //this.Property(t => t.Contratante).HasColumnName("CONTRATANTE_ID");
            //this.Property(t => t.UF).HasColumnName("UF");
        }
    }
}
