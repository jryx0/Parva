

using Parva.Application.Interfaces.Encryption;
using Parva.Domain.Models;

using System.Collections.Generic;
using System.Security.Permissions;

namespace Parva.Application.Services
{
    public class BlogSeedService : ISeedService
    {
        private IEncryptionService _encryptionService;
        public BlogSeedService(IEncryptionService encryptionService)
        {
            this._encryptionService = encryptionService;
        }

        [PrincipalPermission(SecurityAction.Assert)]
        public List<object> GetSeeds()
        {
            var entities = new List<object> {
                
                new User{
                    UserName="user",SaltKey="1",Password=_encryptionService.CreatePasswordHash("111111","1"),Email="76527413@qq.com"
                },
                new User{
                    UserName="admin",SaltKey="1",Password=_encryptionService.CreatePasswordHash("111111","1"),Email="csurn@163.com"
                    ,Roles = new List<Role>{new Role{ Name="管理员", Permisions=Permission.Admin}}
                }
            };
            return entities;
        }
    }
}