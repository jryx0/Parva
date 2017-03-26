using Parva.Domain.Models;
using Parva.Infrastructure.Implementations.Repository.EntityFramework;
using Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite;
//using Infrastructure.Implementations.Repository.EntityFramework.MySql;
using System.Data.Entity;
using System.Linq;

namespace Parva.Infrastructure.Implementations.Repository
{
    public class RepositoryTests
    {
        public static void Test()
        {

            EfRepository<District> regionRepo = new EfRepository<District>(new SqliteDbContext());


            var r = regionRepo.Find();





            //var db1 = GetContext();

            //for (int i = 0; i < 10; i++)
            //{
            //    var user1 = new User();
            //    db1.Set<User>().Resgister(user1);
            //    user1.UserName = "t1" + System.Guid.NewGuid().ToString();
            //    db1.SaveChanges();
            //}

            //var entity = db1.Set<Region>();

            //var regionList = entity.Where(x => x.Parent == null).Include(x => x.Child).ToList().ToList();

            //using (var db2 = GetContext())
            //{
            //    var user2 = new Role();
            //    user2.Name = "t2" + System.Guid.NewGuid().ToString();
            //    db2.Set<Role>().Resgister(user2);

            //    var user3 = new Role();
            //    user3.Name = "t3" + System.Guid.NewGuid().ToString();
            //    db2.Set<Role>().Resgister(user3);


            //    var r = db2.Set<Region>().Find(1);
            //    if (r == null)
            //    {
            //        r = new Region();
            //        r.Name = "root";
            //        db2.Set<Region>().Resgister(r);
            //    }

            //    Region r2 = new Region();
            //    r2.Name = "test2";
            //    r2.Parent = r;
              

            //    db2.Set<Region>().Resgister(r2);
            //    db2.SaveChanges();
            //}
        }

        private static DbContext GetContext()
        {
            return new SqliteDbContext();
        }
    }
}