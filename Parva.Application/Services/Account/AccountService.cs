using Parva.Application.Core;
using Parva.Application.Interfaces.Encryption;
using Parva.Application.Interfaces.Repository;
using Parva.Domain.Models;
using System;
using System.Linq;
using System.Security.Permissions;

namespace Parva.Application.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {
        private IEncryptionService _encryptionService;
        private IRepository<User> _customerRepo;

        public AccountService(IEncryptionService encryptionService, IRepository<User> customerRepo)
        {
            this._encryptionService = encryptionService;
            this._customerRepo = customerRepo;
        }

        [PrincipalPermission(SecurityAction.Assert)]
        public bool ValidationCustomer(string userName, string password)
        {
            var customer = this._customerRepo.Find().FirstOrDefault(o => o.UserName == userName);
            if (customer == null)
            {
                return false;
            }
            var passwordHash = _encryptionService.CreatePasswordHash(password, customer.SaltKey);
            if (customer.Password != passwordHash)
            {
                return false;
            }
            return true;
        }

        [PrincipalPermission(SecurityAction.Assert)]
        public void CreateCustomer(string userName, string password)
        {
            var customer = new User { UserName = userName, SaltKey = _encryptionService.CreateSaltKey() };
            customer.Password = _encryptionService.CreatePasswordHash(password, customer.SaltKey);
            _customerRepo.Add(customer);
            _customerRepo.SaveChanges();
        }

        [PrincipalPermission(SecurityAction.Demand)]
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            var customer = _customerRepo.Find().FirstOrDefault(o => o.UserName == this.UserName);
            var oldPasswordHash = _encryptionService.CreatePasswordHash(oldPassword, customer.SaltKey);
            if (customer.Password != oldPasswordHash)
            {
                return false;
            }
            customer.Password = _encryptionService.CreatePasswordHash(newPassword, customer.SaltKey);
            _customerRepo.SaveChanges();
            return true;
        }

        [PrincipalPermission(SecurityAction.Assert)]
        public string[] GetRolesForUser(string username)
        {
            var permissions = this._customerRepo.Find()
                .Where(o => o.UserName == username)
                .SelectMany(o => o.Roles).Select(o => o.Permisions).ToList();
            if (permissions.Count == 0)
            {
                return new string[] { };
            }
            var userPermissions = Permission.None;
            permissions.ForEach(o => userPermissions |= o);
            var result = Enum.GetValues(typeof(Permission)).Cast<Permission>()
                .Where(o => (userPermissions & o) == o)
                .Select(o => Enum.GetName(typeof(Permission), o))
                .ToArray();
            return result;
        }

        [PrincipalPermission(SecurityAction.Assert)]
        public bool IsUserInRole(string username, string roleName)
        {
            var permission = (Permission)Enum.Parse(typeof(Permission), roleName);
            return _customerRepo.Find()
                .Any(o => o.UserName == username
                    && o.Roles.Any(r => (permission | r.Permisions) == permission));
        }
    }
}