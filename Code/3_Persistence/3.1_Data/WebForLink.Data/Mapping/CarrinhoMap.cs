using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class CarrinhoMap : EntityTypeConfiguration<Carrinho>
    {
        public CarrinhoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.LoginUsuario)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CodigoClienteUsuario)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CnpjConvidado)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.RadicalConvidado)
                .IsRequired()
                .HasMaxLength(50);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            ToTable("Carrinho");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.LoginUsuario).HasColumnName("LoginUsuario");
            Property(t => t.CodigoClienteUsuario).HasColumnName("CodigoClienteUsuario");
            Property(t => t.DataConvite).HasColumnName("DataConvite");
            Property(t => t.StatusConvite).HasColumnName("StatusConvite");
            Property(t => t.CnpjConvidado).HasColumnName("CnpjConvidado");
            Property(t => t.RadicalConvidado).HasColumnName("RadicalConvidado");
        }
    }
}