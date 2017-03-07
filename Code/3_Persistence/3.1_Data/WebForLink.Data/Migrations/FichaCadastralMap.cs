using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class FichaCadastralMap : EntityTypeConfiguration<FichaCadastral>
    {
        public FichaCadastralMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FichaCadastral");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
