using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOLICITACAO_STATUSConfiguration : EntityTypeConfiguration<SOLICITACAO_STATUS>
    {
        public WFD_SOLICITACAO_STATUSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_SOLICITACAO_STATUS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NOME).HasColumnName("NOME");
        }
    }
}