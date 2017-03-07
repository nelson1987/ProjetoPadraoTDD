using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace WebForLink.Web.Areas.Admin.Models
{
    public class IdentityUser : IUser<int>
    {
        public IdentityUser()
        {
        }
        public IdentityUser(string userName)
        {
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        // can also define optional properties such as:
        //    PasswordHash
        //    SecurityStamp
        //    Claims
        //    Logins
        //    Roles
    }
    public class UserStore : IUserStore<IdentityUser, int>
    {
        public UserStore()
        {
        }

        public UserStore(ExampleStorage database)
        {
        }

        public Task CreateAsync(IdentityUser utilizador)
        {
        }

        public Task DeleteAsync(IdentityUser utilizador)
        {
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
        }

        public Task UpdateAsync(IdentityUser utilizador)
        {

        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}