using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class FichaCadastralMap : EntityTypeConfiguration<FichaCadastral>
    {
        public FichaCadastralMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("FichaCadastral");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
