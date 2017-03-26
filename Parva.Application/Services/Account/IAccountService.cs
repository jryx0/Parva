using Parva.Application.Core;

namespace Parva.Application.Services.Account
{
    public interface IAccountService : IBaseService
    {
        bool ValidationCustomer(string userName, string password);

        void CreateCustomer(string userName, string password);

        bool ChangePassword(string oldPassword, string newPassword);

        string[] GetRolesForUser(string username);

        bool IsUserInRole(string username, string roleName);
    }
}