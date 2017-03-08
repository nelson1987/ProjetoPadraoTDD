using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Win.Banco
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Login)
                .IsRequired();

            // Table & Column Mappings
            ToTable("WFL_USUARIO");
            Property(t => t.Id).HasColumnName("ID_USUARIO");
            Property(t => t.Login).HasColumnName("LOGIN");

            HasRequired(x => x.Contratante)
                .WithMany(x => x.Usuarios);
            HasMany(x => x.Perfis)
                .WithMany(x => x.Usuarios);
            HasMany(x => x.Papeis)
                .WithMany(x => x.Usuarios);
        }
    }
}