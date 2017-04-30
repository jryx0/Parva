using BigData.JW.Models;
using Parva.Application.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace BigData.JW.Infrastructure.Implementation.Repository.SystemData.Mapping
{
    public class CompareItemObject : IBaseObject<CompareItem>
    {
        public CompareItemObject()
        {
            Name = "CompareItemObject";
        }

        public string DeleteSql
        {
            get
            {
                return @"DELETE FROM CompareItem  WHERE Id = @Id";
            }
        }

        public CompareItem Entity { set; get; }

        public string InsertSql
        {
            get
            {
                return @"INSERT INTO CompareItem (   
                            ItemName,
                            TableName,
                            Path,
                            Describle,
                            Reserved1,
                            Reserved2,
                            DataType,
                            LastModifiedDate,
                            Name,
                            ParentId,
                            Level,                            
                            Seq,
                            Status
                        )
                        VALUES (                             
                            @ItemName,
                            @TableName,
                            @Path,
                            @Describle,
                            @Reserved1,
                            @Reserved2,
                            @DataType,
                            @LastModifiedDate,
                            @Name,
                            @ParentId,
                            @Level,                            
                            @Seq,
                            @Status
                        )";
            }
        }

        public string Name { set; get; }

        public string SelectSql
        {
            get
            {
                return @"SELECT Id,
                               ItemName,
                               TableName,
                               Path,
                               Describle,
                               Reserved1,
                               Reserved2,
                               LastModifiedDate,
                               Name,
                               ParentId,
                               Level,        
                               Seq,
                               Status,
                               DataType
                          FROM CompareItem where 1 = 1";
            }
        }
        public string UpdateSql
        {
            get
            {
                return @"UPDATE CompareItem
                               SET ItemName = @ItemName,
                                   TableName = @TableName,
                                   Path = @Path,
                                   Describle = @Describle,
                                   Reserved1 = @Reserved1,
                                   Reserved2 = @Reserved2,
                                   DataType = @DataType,
                                   LastModifiedDate = @LastModifiedDate,
                                   ParentId = @ParentId,
                                   Name = @Name,                                  
                                   Level = @Level,       
                                   Seq = @Seq,
                                   Status = @Status
                             WHERE Id = @Id";
            }
        }

        public IDbDataParameter[] getParameter()
        {
            return this.Entity == null ? null :
              new SQLiteParameter[]
              { 
                   new SQLiteParameter("@Id", Entity.Id),
                   new SQLiteParameter("@ItemName", Entity.ItemName),
                   new SQLiteParameter("@TableName", Entity.TableName),
                   new SQLiteParameter("@Path", Entity.Path),
                   new SQLiteParameter("@Describle", Entity.Describle),
                   new SQLiteParameter("@Reserved1", Entity.Reserved1),
                   new SQLiteParameter("@Reserved2", Entity.Reserved2),
                   new SQLiteParameter("@DataType", Entity.DataType),
                   new SQLiteParameter("@LastModifiedDate", Entity.LastModifiedDate),
                   new SQLiteParameter("@Name", Entity.Name),
                   new SQLiteParameter("@ParentId", Entity.Parent ==  null ? Entity.ParentId : Entity.Parent.Id),
                   new SQLiteParameter("@Level", Entity.Level),
                   new SQLiteParameter("@Seq", Entity.Seq),
                   new SQLiteParameter("@Status", Entity.Status)
              };
        }
        public IQueryable<CompareItem> Mapor(IDataReader reader)
        {
            List<CompareItem> compareItemlist = new List<CompareItem>();
            if (reader.FieldCount > 0)
            {
                while (reader.Read())
                {
                    CompareItem cItem = new CompareItem();

                    cItem.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    cItem.ItemName = reader.IsDBNull(1) ? String.Empty : reader.GetString(1);
                    cItem.TableName = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                    cItem.Path = reader.IsDBNull(3) ? String.Empty : reader.GetString(3);
                    cItem.Describle = reader.IsDBNull(4) ? String.Empty : reader.GetString(4);
                    cItem.Reserved1 = reader.IsDBNull(5) ? String.Empty : reader.GetString(5);
                    cItem.Reserved2 = reader.IsDBNull(6) ? String.Empty : reader.GetString(6);

                    DateTime dt = System.DateTime.Now;
                    DateTime.TryParse(reader.GetValue(7).ToString(), out dt);
                    cItem.LastModifiedDate = dt;

                    cItem.Name = reader.IsDBNull(8) ? String.Empty : reader.GetString(8);

                    cItem.ParentId = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                    cItem.Level = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                    cItem.Seq = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                    cItem.Status = reader.IsDBNull(12) ? false : reader.GetBoolean(12);
                    cItem.DataType = (ItemType)(reader.IsDBNull(13) ? 0 :  reader.GetInt32(13));
                    compareItemlist.Add(cItem);
                }
            }

            return compareItemlist.AsQueryable();
        }
    }
}
