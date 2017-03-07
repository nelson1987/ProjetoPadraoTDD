using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using WebForLink.Data.Context;

namespace WebForLink.Web.ViewModels
{
    public class UserLoginMap : EntityTypeConfiguration<ApplicationUserLogin>
    {
        public UserLoginMap()
        {
            // Primary Key
            //HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });
            HasKey(p => p.UserId);

            // Properties
            //Property(t => t.UserId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.LoginProvider)
                .HasMaxLength(50)
                .IsRequired();

            Property(t => t.ProviderKey)
                .HasMaxLength(50)
                .IsRequired();

            ToTable("UserLogin");
            Property(t => t.UserId).HasColumnName("ID");
            Property(t => t.LoginProvider).HasColumnName("Login");
            Property(t => t.ProviderKey).HasColumnName("Provider");
        }
    }

    public class RoleMap : EntityTypeConfiguration<ApplicationRole>
    {
        public RoleMap()
        {
            HasKey(p => p.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            ToTable("Role");

            Property(p => p.Id).HasColumnName("RoleId");

            HasMany(c => c.Users)
                .WithRequired()
                .HasForeignKey(c => c.RoleId);
        }
    }

    public class UserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public UserMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(c => c.AccessFailedCount).IsRequired();
            Property(c => c.Email).HasMaxLength(50).IsRequired();
            Property(c => c.EmailConfirmed).IsRequired();
            Property(c => c.PasswordHash).HasMaxLength(50).IsRequired();
            Property(c => c.PhoneNumber).HasMaxLength(50).IsRequired();
            Property(c => c.PhoneNumberConfirmed).IsRequired();
            Property(c => c.TwoFactorEnabled).IsRequired();
            Property(c => c.SecurityStamp).HasMaxLength(50).IsRequired();
            Property(c => c.LockoutEnabled).IsRequired();
            Property(c => c.LockoutEndDateUtc).IsRequired();
            Property(c => c.UserName).HasMaxLength(50).IsRequired();

            ToTable("User");

            Property(p => p.Id).HasColumnName("UserId");
            Property(p => p.AccessFailedCount).HasColumnName("UserId");
            Property(p => p.Email).HasColumnName("UserId");
            Property(p => p.EmailConfirmed).HasColumnName("UserId");
            Property(p => p.PasswordHash).HasColumnName("UserId");
            Property(p => p.PhoneNumber).HasColumnName("UserId");
            Property(p => p.PhoneNumberConfirmed).HasColumnName("UserId");
            Property(p => p.TwoFactorEnabled).HasColumnName("UserId");
            Property(p => p.SecurityStamp).HasColumnName("UserId");
            Property(p => p.LockoutEnabled).HasColumnName("UserId");
            Property(p => p.LockoutEndDateUtc).HasColumnName("UserId");
            Property(p => p.UserName).HasColumnName("UserId");

            HasMany(c => c.Logins)
                .WithOptional()
                .HasForeignKey(c => c.UserId);
            HasMany(c => c.Claims)
                .WithOptional()
                .HasForeignKey(c => c.UserId);
            HasMany(c => c.Roles)
                .WithRequired()
                .HasForeignKey(c => c.UserId);
        }
    }

    public class UserRoleMap : EntityTypeConfiguration<ApplicationUserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            //HasKey(c => new { c.UserId, c.RoleId });
            //HasKey(c => c.UserId);

            // Properties
            Property(t => t.RoleId)
                .HasMaxLength(50)
                .IsRequired();

            ToTable("UserRole");
            Property(t => t.UserId).HasColumnName("ID");
            Property(t => t.RoleId).HasColumnName("RoleId");
        }
    }

    public class UserClaim : EntityTypeConfiguration<ApplicationUserClaim>
    {
        public UserClaim()
        {
            HasKey(c => c.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.UserId)
                .HasMaxLength(50)
                .IsRequired();

            Property(t => t.ClaimValue)
                .HasMaxLength(50)
                .IsRequired();

            Property(t => t.ClaimType)
                .HasMaxLength(50)
                .IsRequired();

            ToTable("UserClaim");
            Property(p => p.Id).HasColumnName("ID");
            Property(t => t.UserId).HasColumnName("userid");
            Property(t => t.ClaimValue).HasColumnName("RoleId");
            Property(t => t.ClaimType).HasColumnName("RoleId");
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>, IUserStore<ApplicationUser>, IDisposable
    //: Microsoft.AspNet.Identity.EntityFramework.UserStore, Microsoft.AspNet.Identity.IUserStore, IDisposable
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        { }
    }

    public class ApplicationUserRole : IdentityUserRole
    {
    }

    public class ApplicationRole : IdentityRole
    {
    }

    public class ApplicationUserClaim : IdentityUserClaim
    {
    }

    public class ApplicationUserLogin : IdentityUserLogin
    {

    }

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : MusicStoreContext //IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base()
        //: base("MusicStoreEntities")//, throwIfV1Schema: false)
        {
            Database.SetInitializer<MusicStoreContext>(null);

        }

        public DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserLoginMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserClaim());
            base.OnModelCreating(modelBuilder);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}