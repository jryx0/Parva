
using Parva.Application.Interfaces.Encryption;
using Parva.Domain.Models;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Parva.Application.Services
{
    public class DefaultSeedService : ISeedService
    {
        private IEncryptionService _encryptionService;
        public DefaultSeedService(IEncryptionService encryptionService)
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
                 
            };
            return entities;
        }
    }
}