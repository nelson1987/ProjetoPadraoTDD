using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class CarrinhoMap : EntityTypeConfiguration<Carrinho>
    {
        public CarrinhoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.LoginUsuario)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CodigoClienteUsuario)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CnpjConvidado)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RadicalConvidado)
                .IsRequired()
                .HasMaxLength(50);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("Carrinho");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LoginUsuario).HasColumnName("LoginUsuario");
            this.Property(t => t.CodigoClienteUsuario).HasColumnName("CodigoClienteUsuario");
            this.Property(t => t.DataConvite).HasColumnName("DataConvite");
            this.Property(t => t.StatusConvite).HasColumnName("StatusConvite");
            this.Property(t => t.CnpjConvidado).HasColumnName("CnpjConvidado");
            this.Property(t => t.RadicalConvidado).HasColumnName("RadicalConvidado");
        }
    }
}
