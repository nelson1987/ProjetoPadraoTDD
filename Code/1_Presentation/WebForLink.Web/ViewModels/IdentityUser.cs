using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebForLink.Data.Context;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WebForLink.Web.ViewModels
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            :base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleManager = new ApplicationRoleManager(
                new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>() 
                as MusicStoreContext));
            return roleManager;
        }
    }
    internal class UserRolesRepository
    {
        private readonly MusicStoreContext _databaseContext;
        public UserRolesRepository(MusicStoreContext database)
        {
            _databaseContext = database;
        }
        public IList<string> FindByUserid(string userId)
        {
            var roles = _databaseContext.AspNetUsers
                .Where(x => x.Id == userId)
                .SelectMany(x => x.AspNetRoles);
            return roles.Select(x => x.Name).ToList();
        }
    }
    public class UserRepository<T> where T : IdentityUser
    {
        private readonly MusicStoreContext _databaseContext;

        public UserRepository(MusicStoreContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        internal T GeTByName(string userName)
        {
            var user = _databaseContext.AspNetUsers.SingleOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                T result = (T)Activator.CreateInstance(typeof(T));
                result.Id = user.Id;
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.SecurityStamp = user.SecurityStamp;
                result.Email = result.Email;
                result.EmailConfirmed = user.EmailConfirmed;
                result.PhoneNumber = user.PhoneNumber;
                result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                result.LockoutEnabled = user.LockoutEnabled;
                result.LockoutEndDateUtc = user.LockoutEndDateUtc;
                result.AccessFailedCount = user.AccessFailedCount;
                return result;
            }
            return null;
        }

        internal T GeTByEmail(string email)
        {
            var user = _databaseContext.AspNetUsers.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                T result = (T)Activator.CreateInstance(typeof(T));

                result.Id = user.Id;
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.SecurityStamp = user.SecurityStamp;
                result.Email = result.Email;
                result.EmailConfirmed = user.EmailConfirmed;
                result.PhoneNumber = user.PhoneNumber;
                result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                result.LockoutEnabled = user.LockoutEnabled;
                result.LockoutEndDateUtc = user.LockoutEndDateUtc;
                result.AccessFailedCount = user.AccessFailedCount;
                return result;
            }
            return null;
        }

        internal int Insert(T user)
        {
            _databaseContext.AspNetUsers.Add(new AspNetUsers
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                AccessFailedCount = user.AccessFailedCount
            });

            return _databaseContext.SaveChanges();
        }

        /// <summary>
        /// Returns an T given the user’s id
        /// </summary>
        /// <param name=”userId”>The user’s id</param>
        /// <returns></returns>
        public T GeTById(string userId)
        {
            var user = _databaseContext.AspNetUsers.Find(userId);
            T result = (T)Activator.CreateInstance(typeof(T));

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.PasswordHash = user.PasswordHash;
            result.SecurityStamp = user.SecurityStamp;
            result.Email = result.Email;
            result.EmailConfirmed = user.EmailConfirmed;
            result.PhoneNumber = user.PhoneNumber;
            result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            result.LockoutEnabled = user.LockoutEnabled;
            result.LockoutEndDateUtc = user.LockoutEndDateUtc;
            result.AccessFailedCount = user.AccessFailedCount;
            return result;
        }

        /// <summary>
        /// Return the user’s password hash
        /// </summary>
        /// <param name=”userId”>The user’s id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            var user = _databaseContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);
            var passHash = user != null ? user.PasswordHash : null;
            return passHash;
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name=”user”></param>
        /// <returns></returns>
        public int Update(T user)
        {
            var result = _databaseContext.AspNetUsers.FirstOrDefault(u => u.Id == user.Id);
            if (result != null)
            {
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.SecurityStamp = user.SecurityStamp;
                result.Email = result.Email;
                result.EmailConfirmed = user.EmailConfirmed;
                result.PhoneNumber = user.PhoneNumber;
                result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                result.LockoutEnabled = user.LockoutEnabled;
                result.LockoutEndDateUtc = user.LockoutEndDateUtc;
                result.AccessFailedCount = user.AccessFailedCount;
                return _databaseContext.SaveChanges();
            }
            return 0;
        }
    }
    public class IdentityUser : IdentityUser<string,
        IdentityUserLogin,
        IdentityUserRole,
        IdentityUserClaim>, IUser, IUser<string>
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }
    }
    public class IdentityRole : IRole
    {
        internal object Description;

        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        public IdentityRole(string nome)
            : this()
        {
            Name = nome;
        }
        public IdentityRole(string id, string nome)
        {
            Id = id;
            Name = nome;
        }
        public string Id { get; }

        public string Name { get; set; }
    }
    public class UserStore<T> :
        IUserRoleStore<T>,
        IUserStore<T>,
        IUserPasswordStore<T>,
        IUserEmailStore<T>,
        IUserLockoutStore<T, string>,
        IUserTwoFactorStore<T, string> where T : IdentityUser
    {
        private readonly UserRepository<T> _userTable;
        private readonly UserRolesRepository<T> _userRolesTable;
        public UserStore(MusicStoreContext databaseContext)
        {
            _userTable = new UserRepository<T>(databaseContext);
            _userRolesTable = new UserRolesRepository<T>(databaseContext);
        }
        public Task CreateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.Run(() => _userTable.Insert(user));
        }

        public Task<T> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            return Task.Run(() => _userTable.GeTById(userId));
        }

        public Task<bool> GetTwoFactorEnabledAsync(T user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<T> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            return Task.Run(() => _userTable.GeTByName(userName));
        }

        public Task<IList<string>> GetRolesAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(“user”);
            }

            return Task.Run(() => _userRolesTable.FindByUserId(user.Id));
        }

        public Task<string> GetPasswordHashAsync(T user)
        {
            return Task.Run(() => _userTable.GetPasswordHash(user.Id));
        }

        public Task SetPasswordHashAsync(T user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task<T> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            return Task.Run(() => _userTable.GeTByEmail(email));
        }

        public Task<string> GetEmailAsync(T user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<int> GetAccessFailedCountAsync(T user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(T user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(T user)
        {
            return
            Task.FromResult(user.LockoutEndDateUtc.HasValue
            ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
            : new DateTimeOffset());
        }

        public Task SetLockoutEnabledAsync(T user, bool enabled)
        {
            user.LockoutEnabled = enabled;

            return Task.Run(() => _userTable.Update(user));
        }

        public Task SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(T user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(T user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(T user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
    internal class RoleRepository<T> where T : IdentityRole
    {
        private readonly MusicStoreContext _databaseContext;

        public RoleRepository(MusicStoreContext dataBaseContext)
        {
            _databaseContext = dataBaseContext;
        }

        internal IQueryable<T> GetRoles()
        {
            List<T> result = (List<T>)Activator.CreateInstance(typeof(List<T>));
            var roles = _databaseContext.AspNetRoles.ToList();
            foreach (var role in roles)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                item.Id = role.Id;
                item.Name = role.Name;
                item.Description = role.Description;
                result.Add(item);
            }
            return result.AsQueryable();
        }

        internal void Dispose()
        {
            _databaseContext.Dispose();
        }

        internal int Create(T role)
        {
            _databaseContext.AspNetRoles.Add(new AspNetRoles
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            });
            return _databaseContext.SaveChanges();
        }

        internal int Delete(string id)
        {
            var existingRole = _databaseContext.AspNetRoles.Find(id.Trim());
            if (existingRole != null)
            {
                _databaseContext.Entry(existingRole).State = EntityState.Deleted;
                return _databaseContext.SaveChanges();
            }
            return -1;
        }

        internal T FindByNamec(string roleName)
        {
            var role = _databaseContext.AspNetRoles.FirstOrDefault(r => r.Name == roleName.Trim());
            if (role == null)
            {
                return default(T);
            }
            T item = (T)Activator.CreateInstance(typeof(T));
            item.Id = role.Id;
            item.Name = role.Name;
            item.Description = role.Description;
            return item;
        }

        internal T FindById(string roleId)
        {
            var role = _databaseContext.AspNetRoles.Find(roleId.Trim());
            if (role == null)
            {
                return default(T);
            }
            T item = (T)Activator.CreateInstance(typeof(T));
            item.Id = role.Id;
            item.Name = role.Name;
            item.Description = role.Description;
            return item;
        }

        internal int Update(T role)
        {
            var existingRole = _databaseContext.AspNetRoles.Find(role.Id.Trim());
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.Description = role.Description;
                _databaseContext.Entry(existingRole).State = EntityState.Modified;
                return _databaseContext.SaveChanges();
            }
            return -1;
        }
    }
    public class RoleStore<TRole> : IQueryableRoleStore<TRole>
        where TRole : IdentityRole
    {
        private readonly RoleRepository<TRole> _roleRepository;

        public RoleStore(MusicStoreContext musicStoreContext)
        {
            _roleRepository = new RoleRepository<TRole>(musicStoreContext);
        }

        public IQueryable<TRole> Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task CreateAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TRole role)
        {
            throw new NotImplementedException();
        }
    }
}