using Parva.Application.Interfaces.Repository;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parva.Application.Services.Account
{
    public class UserService
    {
        private ISystemDataRepository _repo;
        public  UserService(ISystemDataRepository repo)
        {
            this._repo = repo;
        }

        public User GetUserInfo(string username, string password)
        {
             
            var Sql = @"SELECT user.username,
                                   role.name,
                                   role.Permisions,
                                   role.id
                              FROM roleuser
                                   JOIN
                                   role ON role.id = roleuser.role_id
                                   JOIN
                                   user ON user.id = roleuser.User_Id
                             WHERE user.UserName = '@username' AND 
                                   user.Password = '@password'";

            Sql = Sql.Replace("@username", username);
            Sql = Sql.Replace("@password", password);

            var ds = _repo.ExecuteDataset(Sql);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            User user = new User();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (user.Roles == null)
                    user.Roles = new List<Role>();

                Role role = new Role();
                role.Name = row[1].ToString();
                role.Permisions = (Permission)row[2];
                role.Id = (int)row[3];
                user.Roles.Add(role);
            }

            user.UserName = username;
            user.Password = password;
            return user;
        }


    }
}
