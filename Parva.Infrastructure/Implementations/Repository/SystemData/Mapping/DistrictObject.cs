using Parva.Application.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Parva.Infrastructure.Implementations.Repository.SystemData.Mapping
{
    public class DistrictObject : IBaseObject<District>
    {
        public DistrictObject()
        {
            this.Name = "DistrictObject";
        }

        public string DeleteSql
        {
            get
            {
                return @"DELETE FROM District  WHERE Id = @Id";
            }
        }

        public District Entity { get; set; }

        public string InsertSql
        {
            get
            {
                return @"INSERT INTO District ( IP, RegionCode, Name, ParentId, Level, Seq, Status)
                                        VALUES (  @IP, @RegionCode, @Name, @ParentId, @Level,  @Seq,  @Status )";
            }
        }

        public string Name { set; get; }
         

        public string SelectSql
        {
            get
            {
                return @"SELECT    Id,
                                   IP,
                                   RegionCode,
                                   Name,
                                   ParentId,
                                   Level,       
                                   Seq,
                                   Status
                              FROM District Where 1 = 1";
            }
        }

        public string UpdateSql
        {
            get
            {
                return @"UPDATE District
                            SET IP = @IP,
                                RegionCode = @RegionCode,
                                Name = @Name,
                                ParentId = @ParentId,
                                Level = @Level,
                                Seq = @Seq,
                                Status = @Status
                            WHERE Id = @Id";
            }
        }

        public IDbDataParameter[] getParameter()
        {
            return
              new SQLiteParameter[]
              {
                    new SQLiteParameter("@Id", Entity.Id),
                    new SQLiteParameter("@IP", Entity.IP),
                    new SQLiteParameter("@RegionCode", Entity.RegionCode),
                    new SQLiteParameter("@Name", Entity.Name),
                    new SQLiteParameter("@ParentId", Entity.Parent == null? Entity.ParentId : Entity.Parent.Id),
                    new SQLiteParameter("@Level", Entity.Level),
                    new SQLiteParameter("@Seq", Entity.Seq),                    
                    new SQLiteParameter("@Status", Entity.Status) 
              };
        }

        public IQueryable<District> Mapor(IDataReader reader)
        {
            List<District> dataList = new List<District>();

            var sqliteReader = reader as SQLiteDataReader;

            while (sqliteReader.Read())
            {               
                var d = new District();

                d.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                d.IP = reader.IsDBNull(1) ? String.Empty : reader.GetString(1);
                d.RegionCode = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                d.Name = reader.IsDBNull(3) ? String.Empty : reader.GetString(3);
                d.ParentId = reader.IsDBNull(4) ? -1 : reader.GetInt32(4);
                d.Level = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                d.Seq = reader.IsDBNull(6) ? 0 : reader.GetInt64(6);
                d.Status = reader.IsDBNull(7) ? false : reader.GetBoolean(7);

                d.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;

                dataList.Add(d);
            }

            return dataList.AsQueryable();
        }
    }
}
