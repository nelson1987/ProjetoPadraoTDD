using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public SolicitacaoMap()
        {
            // Primary Key
            //HasKey(t => t.Id);

            // Properties
            //Property(t => t.Aprovado)
            //    .IsRequired();

            // Table & Column Mappings
            //ToTable("WFL_SOLICITACAO");
            //Property(t => t.Id).HasColumnName("ID_APLICACAO");
            Ignore(t => t.Aprovado);
            HasRequired(x => x.Criador).WithMany(x => x.Solicitacoes);
            HasRequired(x => x.Solicitado).WithMany(x => x.Solicitacoes);
            //HasMany(x => x.Contratantes)
            //    .WithRequired(x => x.Aplicacao);

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("OnsiteCourse");
            //});
        }
    }
}
