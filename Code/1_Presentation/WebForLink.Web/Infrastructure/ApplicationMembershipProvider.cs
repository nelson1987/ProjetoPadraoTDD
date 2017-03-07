using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Security;
using log4net;
using Ninject;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;

namespace WebForLink.Web.Infrastructure
{
    public class ApplicationMembershipProvider : MembershipProvider
    {
        private static readonly List<Usuario> Usuarios = new List<Usuario>
        {
            new Usuario("nelson", "nelson"),
            new Usuario("nelson", "ch123456")
        };
        
        private readonly IControleAccessoService _service;

        //public ApplicationMembershipProvider()
        //{
        //    var ioc = new IoC();
        //    _service = ioc.Kernel.Get<IControleAccessoService>();
        //}
        public ApplicationMembershipProvider(IControleAccessoService albumService)
        {
            _service = albumService;
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
            }

        public override bool EnablePasswordReset
            {
            get { throw new NotImplementedException(); }
            }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
            }

        public override int MaxInvalidPasswordAttempts
            {
            get { throw new NotImplementedException(); }
            }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override bool ValidateUser(string username, string password)
        {
            try
        {
                return _service.AutenticarUsuario(username, password);
                //return Usuarios.Exists(x => x.Email == username && x.Senha == password);
                //if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                //{
                //    return false;
                //}
                ////ca.AutenticarUsuario(username, password);
                //return true;
        }
            catch (Exception ex)
        {
                LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                throw new Exception("Erro na autenticação", ex);
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey,
            out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

    }
}