using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class FichaCadastralMap : EntityTypeConfiguration<FichaCadastral>
    {
        public FichaCadastralMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("FichaCadastral");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Status).HasColumnName("Status");


            this.HasRequired(o => o.Solicitacao)
            .WithOptional();
        }
    }
}