using BigData.JW.Models;
using Parva.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace BigData.JW.Infrastructure.Implementation.Repository.SystemData.Mapping
{
    public class ItemFormatObject : IBaseObject<ItemFormat>
    {
        public ItemFormatObject()
        {
            Name = "ItemFormatObject";
        }

        public string DeleteSql
        {
            get
            {
                return @"DELETE FROM ItemDataFormat
                              WHERE FormatId IN (
                            SELECT id
                              FROM ItemFormat
                             WHERE id = @id);
                        Delete from ItemFormat WHERE id = @id;";
            }
        }

        public ItemFormat Entity { set; get; }
         

        public string InsertSql
        {
            get
            {
                return @" INSERT INTO ItemFormat(
                           StartPos,
                           Seq,
                           Status,
                           ParentItemId
                       )
                       VALUES(
                           @StartPos,                           
                           @Seq,
                           @Status,
                           @ParentItemId
                       );";
            }
        }

        public string Name { set; get; }

        public string SelectSql
        {
            get
            {
                return @"SELECT a.Id,
                               a.ParentItemId,
                               a.StartPos,
                               a.Status,
                               a.Seq,
                               b.ColIndex,
                               b.ColInfoId,
                               b.Id
                          FROM ItemFormat a
                               LEFT JOIN
                               ItemDataFormat b ON b.FormatId == a.Id Where 1 = 1 ";
            }
        }

        public string UpdateSql
        {
            get
            {
                return @"
                            UPDATE ItemFormat
                               SET StartPos = 'StartPos',
                                   Seq = 'Seq',
                                   Status = 'Status',
                                   ParentItemId = 'ParentItemId'
                             WHERE Id = 'Id';

                            UPDATE ItemDataFormat
                               SET FormatId = 'FormatId',
                                   ColInfoId = 'ColInfoId',
                                   ColIndex = 'ColIndex',
                                   Seq = 'Seq',
                                   Status = 'Status'
                             WHERE Id = 'Id';";
            }
        }

        public IDbDataParameter[] getParameter()
        {
            return this.Entity == null ? null :
            new SQLiteParameter[]
            {
                   new SQLiteParameter("@Id", Entity.Id),
                   new SQLiteParameter("@StartPos", Entity.StartPos),
                   new SQLiteParameter("@Seq", Entity.Seq),
                   new SQLiteParameter("@ParentItemId", Entity.ParentId) 
            };
        }

        public IQueryable<ItemFormat> Mapor(IDataReader reader)
        {
            List<ItemFormat> formatlist = new List<ItemFormat>();

            if (reader.FieldCount > 0)
                while (reader.Read())
                {
                    ItemFormat cItem = new ItemFormat();

                    cItem.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    var ci = formatlist.Find(x => x.Id == cItem.Id);
                    if (ci == null)
                    {//first
                        ci = cItem;
                        ci.ParentId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        ci.StartPos = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        ci.Status = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                        ci.Seq = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

                        //if (!reader.IsDBNull(6))
                        //{
                        //    ci.DataFormats = new List<ItemDataFormat>();
                        //    ItemDataFormat idf = new ItemDataFormat();
                        //    idf.ColIndex = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        //    idf.ColInfoId = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        //    idf.Id = reader.IsDBNull(6) ? 0 : reader.GetInt32(7);
                        //    ci.DataFormats.Add(idf);
                        //}

                        formatlist.Add(ci);
                    }
                    else
                    {//second
                        //if (!reader.IsDBNull(6))
                        //{
                        //    if(ci.DataFormats == null)
                        //        ci.DataFormats = new List<ItemDataFormat>();
                        //    ItemDataFormat idf = new ItemDataFormat();
                        //    idf.ColIndex = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        //    idf.ColInfoId = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        //    idf.Id = reader.IsDBNull(6) ? 0 : reader.GetInt32(7);
                        //    ci.DataFormats.Add(idf);
                        //}
                    }

                    if (!reader.IsDBNull(6))
                    {
                        if (ci.DataFormats == null)
                            ci.DataFormats = new List<ItemDataFormat>();
                        ItemDataFormat idf = new ItemDataFormat();
                        idf.ColIndex = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        idf.ColInfoId = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        idf.Id = reader.IsDBNull(6) ? 0 : reader.GetInt32(7);
                        ci.DataFormats.Add(idf);
                    }

                }


            return formatlist.AsQueryable();
        }
    }
}
