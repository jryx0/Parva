using Parva.Domain.Models;
using System;
using System.Data;
using System.Linq;
using Parva.Application.Core;
using System.Collections.Generic;
using System.Data.SQLite;
using Parva.Domain.Core;

namespace Parva.Infrastructure.Implementations.Repository.SystemData.Mapping
{
    public class DataValueObject : IBaseObject<DataValue>
    {
        public string DeleteSql
        {
            get
            {
                return @"Delete From DataValue Where Id = @Id";
            }
        }

        public DataValue Entity { set; get; }
         
        public string InsertSql
        {
            get
            {
                return @"INSERT INTO DataValue (
                          BaseDataTypeId,
                          Name,
                          Value,
                          ValueType,
                          LastModifier,
                          LastModifyDate,
                          Comment,
                          Seq,
                          Status
                      )
                      VALUES (
                          @BaseDataTypeId,
                          @Name,
                          @Value,
                          @ValueType,
                          @LastModifier,
                          @LastModifyDate,
                          @Comment,
                          @Seq,
                          @Status
                      )"; ;
            }
        }

        public string Name { get; set; }
        public DataValueObject()
        {
            this.Name = "DataValueObject";
        }

        public string SelectSql
        {
            get
            {
                //Utility.Expression2Sql.Expre2Sql.Init(Utility.Expression2Sql.DatabaseType.SQLite);
                //var result = Utility.Expression2Sql.Expre2Sql.Select<DataValue>(
                //    p => new
                //    {
                //        p.Id,
                //        p.BaseDataTypeId,
                //        p.Value,
                //        p.ValueType,
                //        p.LastModifier,
                //        p.LastModifyDate,
                //        p.Comment,
                //        p.Seq,
                //        p.Status
                //    }
                //    );
                //return result.SqlStr;

                return @"SELECT Id,
                               BaseDataTypeId,
                               Name,
                               Value,
                               ValueType,
                               LastModifier,
                               LastModifyDate,
                               Comment,
                               Seq,
                               Status
                          FROM DataValue Where 1 = 1";
            }
        }

        public string UpdateSql
        {
            get
            {
                return @"UPDATE DataValue
                           SET
                               BaseDataTypeId = @BaseDataTypeId,
                               Name = @Name,
                               Value = @Value,
                               ValueType = @ValueType,
                               LastModifier = @LastModifier,
                               LastModifyDate = @LastModifyDate,
                               Comment = @Comment,
                               
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
                    new SQLiteParameter("@BaseDataTypeId", Entity.BaseDataTypeId),
                    new SQLiteParameter("@Name", Entity.Name),
                    new SQLiteParameter("@Value", Entity.Value),
                    new SQLiteParameter("@ValueType", Entity.ValueType),
                    new SQLiteParameter("@LastModifier", Entity.LastModifier),
                    new SQLiteParameter("@LastModifyDate", Entity.LastModifyDate),
                    new SQLiteParameter("@Comment", Entity.Comment),
                    new SQLiteParameter("@Seq", Entity.Seq),
                    new SQLiteParameter("@Status", Entity.Status)
                };
        }

        //public IQueryable<BaseEntity> IncludeDetail(string name, Type _type)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<BaseEntity> IncludeDetail(DataValue entity, string name, Type _type)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<BaseEntity> IncludeDetail(IQueryable<DataValue> entities, string name, Type _type)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<TDetail> IncludeDetail<TDetail>(string detailName)
        //{
        //    throw new NotImplementedException();
        //}

        public IQueryable<DataValue> Mapor(IDataReader reader)
        {
            List<DataValue> dataList = new List<DataValue>();

            if (reader.FieldCount > 0)

                while (reader.Read())
                {
                    var dv = new DataValue();

                    dv.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    dv.BaseDataTypeId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    dv.Name = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                    dv.Value = reader.IsDBNull(3) ? String.Empty : reader.GetString(3);
                    dv.ValueType = reader.IsDBNull(4) ? String.Empty : reader.GetString(4);
                    dv.LastModifier = reader.IsDBNull(5) ? String.Empty : reader.GetValue(5).ToString();
                    DateTime dt = System.DateTime.Now;
                    DateTime.TryParse(reader.GetString(6).ToString(), out dt);
                    dv.LastModifyDate = dt;

                    dv.Comment = reader.IsDBNull(7) ? String.Empty : reader.GetString(7);
                    dv.Seq = reader.IsDBNull(8) ? 0 : reader.GetInt64(8);
                    dv.Status = reader.IsDBNull(9) ? false : reader.GetBoolean(9);



                    dv.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;

                    dataList.Add(dv);
                }

            return dataList.AsQueryable();
        }
    }
}
