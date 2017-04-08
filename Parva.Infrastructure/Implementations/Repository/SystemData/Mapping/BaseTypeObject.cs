using Parva.Domain.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using Parva.Application.Core;
using Parva.Application;

namespace Parva.Infrastructure.Implementations.Repository.SystemData.Mapping
{
    public class BaseTypeObject : IBaseObject<BaseDataType>
    {
        public string InsertSql
        {
            get
            {
                return @"INSERT INTO BaseDataType ( BaseTypeName,
                             Comment,
                             LastModifier,
                             LastModifyDate,
                             Seq,
                             Status)
                         VALUES (
                             @BaseTypeName,
                             @Comment,
                             @LastModifier,
                             @LastModifyDate,
                             @Seq,
                             @Status )";
            }
        }

        public string UpdateSql
        {
            get
            {
                return @"
                            UPDATE BaseDataType
                               SET 
                                   BaseTypeName = @BaseTypeName,
                                   Comment = @Comment,
                                   LastModifier = @LastModifier,
                                   LastModifyDate = @LastModifyDate,                                  
                                   Seq = @Seq,
                                   Status = @Status
                             WHERE Id = @Id";
            }
 
        }

        public string SelectSql
        {
            get
            {
                //Utility.Expression2Sql.Expre2Sql.DatabaseType = Utility.Expression2Sql.DatabaseType.SQLite;


                //var result = Utility.Expression2Sql.Expre2Sql.Select<BaseDataType>(
                //    p => new  {
                //        p.Id,
                //        p.BaseTypeName,
                //        p.Comment,
                //        p.LastModifier,
                //        p.LastModifyDate,
                //        p.Seq,
                //        p.Status  }
                //    );

                //return result.SqlStr;

                return @"SELECT Id,
                           BaseTypeName,
                           Comment,
                           LastModifier,
                           LastModifyDate,
                           Seq,
                           Status
                      FROM BaseDataType Where  1 = 1";
            }
        }

        public string DeleteSql
        {
            get
            {
                return @"DELETE FROM BaseDataType WHERE Id = @Id";
            }            
        }

        public BaseDataType Entity { get; set; }

        public string Name { get; set; }
        public BaseTypeObject()
        {
            this.Name = "BaseTypeObject";
        }

        public IQueryable<BaseDataType> Mapor(IDataReader reader)
        {
            List<BaseDataType> basetypelist = new List<BaseDataType>();
            if (reader.FieldCount > 0)
            {
                while (reader.Read())
                {
                    BaseDataType bdt = new BaseDataType();

                    bdt.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    bdt.BaseTypeName = reader.IsDBNull(1) ? String.Empty : reader.GetString(1);
                    bdt.Comment = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                    bdt.LastModifier = reader.IsDBNull(3) ? String.Empty : reader.GetString(3);

                    DateTime dt = System.DateTime.Now;
                    DateTime.TryParse(reader.GetValue(4).ToString(), out dt);
                    bdt.LastModifyDate = dt;


                    bdt.Seq = reader.IsDBNull(5) ? 0 : reader.GetInt64(5);
                    bdt.Status = reader.IsDBNull(6) ? false : reader.GetBoolean(6);

                    bdt.ModifyStatus = BaseEntityStatus.Unchanged;

                    basetypelist.Add(bdt);
                }
            }

            return basetypelist.AsQueryable();
        }

        public IDbDataParameter[] getParameter()
        {
            return this.Entity == null ? null :
               new SQLiteParameter[]
               {
                   new SQLiteParameter("@Id", Entity.Id),
                   new SQLiteParameter("@BaseTypeName", Entity.BaseTypeName),
                   new SQLiteParameter("@Comment", Entity.Comment),
                   new SQLiteParameter("@LastModifier", Entity.LastModifier),
                   new SQLiteParameter("@LastModifyDate", Entity.LastModifyDate),
                   new SQLiteParameter("@Seq", Entity.Seq),
                   new SQLiteParameter("@Status", Entity.Status)
               };
        }

        //public IQueryable<BaseEntity> IncludeDetail(IQueryable<BaseDataType> entities, string name, Type _type)
        //{
        //    var service = AppEngine.Container.GetInstance(typeof(IBaseObjectService<>).MakeGenericType(_type));

        //    if (name == "HaveValue")
        //    {
        //        var objectService = service as BaseObjectService<DataValue>;
        //        var datalist = objectService.Find(entities);

        //        entities.ToList().ForEach(x =>
        //            {
        //                var _v = datalist.Where(y => y.BaseDataTypeId == x.Id);
        //                if (_v != null && _v.Count() > 0)
        //                {
        //                    x.HaveValue = _v.ToList();
        //                    x.HaveValue.ForEach(y => y.DataParent = x);
        //                }
        //                else x.HaveValue = null;
        //            });

        //        return datalist;
        //    }

        //    return null;
        //}

        //public IQueryable<BaseEntity> IncludeDetail(BaseDataType entity, string name, Type _type)
        //{
        //    var service = AppEngine.Container.GetInstance(typeof(IBaseObjectService<>).MakeGenericType(_type));

        //    if (name == "HaveValue")
        //    {
        //        var objectService = service as BaseObjectService<DataValue>;

        //        entity.HaveValue = objectService.Find(x => x.BaseDataTypeId == entity.Id).ToList();
        //        entity.HaveValue.ForEach(y => y.DataParent = entity);

        //        return entity.HaveValue.AsQueryable();
        //    }

        //    return null;
        //}

        //public IQueryable<BaseEntity> IncludeDetail(string name, Type _type)
        //{
        //    var service = AppEngine.Container.GetInstance(typeof(IBaseObjectService<>).MakeGenericType(_type));

        //    if (name == "HaveValue")
        //    {
        //        var objectService = service as BaseObjectService<DataValue>;
        //        return objectService.Find(x => true);
        //    }

        //    return null;
        //}

        //public IQueryable<DataValue> IncludeDetail<DataValue>(string detailName)
        //{
        //    var dlist = IncludeDetail(detailName, typeof(DataValue));
        //    return dlist == null ? null : dlist.Cast<DataValue>();
        //}

         
    }
}
