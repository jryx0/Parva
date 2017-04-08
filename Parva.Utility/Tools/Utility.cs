using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Parva.Utility.Tools
{
    public static class CustomLINQtoDataSetMethods
    {

        public class ObjectShredder<T>
        {
            private System.Reflection.FieldInfo[] _fi;
            private System.Reflection.PropertyInfo[] _pi;
            private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
            private System.Type _type;

            // ObjectShredder constructor.
            public ObjectShredder()
            {
                _type = typeof(T);
                _fi = _type.GetFields();
                _pi = _type.GetProperties();
                _ordinalMap = new Dictionary<string, int>();
            }

            /// <summary>
            /// Loads a DataTable from a sequence of objects.
            /// </summary>
            /// <param name="source">The sequence of objects to load into the DataTable.</param>
            /// <param name="table">The input table. The schema of the table must match that 
            /// the type T.  If the table is null, a new table is created with a schema 
            /// created from the public properties and fields of the type T.</param>
            /// <param name="options">Specifies how values from the source sequence will be applied to 
            /// existing rows in the table.</param>
            /// <returns>A DataTable created from the source sequence.</returns>
            public DataTable Shred(IQueryable<T> source, DataTable table, LoadOption? options)
            {
                // Load the table from the scalar sequence if T is a primitive type.
                if (typeof(T).IsPrimitive)
                {
                    return ShredPrimitive(source, table, options);
                }

                // Create a new table if the input table is null.
                if (table == null)
                {
                    table = new DataTable(typeof(T).Name);
                }

                // Initialize the ordinal map and extend the table schema based on type T.
                table = ExtendTable(table, typeof(T));

                // Enumerate the source sequence and load the object values into rows.
                table.BeginLoadData();
                using (IEnumerator<T> e = source.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (options != null)
                        {
                            table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                        }
                        else
                        {
                            table.LoadDataRow(ShredObject(table, e.Current), true);
                        }
                    }
                }
                table.EndLoadData();

                // Return the table.
                return table;
            }

            public DataTable ShredPrimitive(IQueryable<T> source, DataTable table, LoadOption? options)
            {
                // Create a new table if the input table is null.
                if (table == null)
                {
                    table = new DataTable(typeof(T).Name);
                }

                if (!table.Columns.Contains("Value"))
                {
                    table.Columns.Add("Value", typeof(T));
                }

                // Enumerate the source sequence and load the scalar values into rows.
                table.BeginLoadData();
                using (IEnumerator<T> e = source.GetEnumerator())
                {
                    Object[] values = new object[table.Columns.Count];
                    while (e.MoveNext())
                    {
                        values[table.Columns["Value"].Ordinal] = e.Current;

                        if (options != null)
                        {
                            table.LoadDataRow(values, (LoadOption)options);
                        }
                        else
                        {
                            table.LoadDataRow(values, true);
                        }
                    }
                }
                table.EndLoadData();

                // Return the table.
                return table;
            }

            public object[] ShredObject(DataTable table, T instance)
            {

                FieldInfo[] fi = _fi;
                PropertyInfo[] pi = _pi;

                if (instance.GetType() != typeof(T))
                {
                    // If the instance is derived from T, extend the table schema
                    // and get the properties and fields.
                    ExtendTable(table, instance.GetType());
                    fi = instance.GetType().GetFields();
                    pi = instance.GetType().GetProperties();
                }

                // Resgister the property and field values of the instance to an array.
                Object[] values = new object[table.Columns.Count];
                foreach (FieldInfo f in fi)
                {
                    values[_ordinalMap[f.Name]] = f.GetValue(instance);
                }

                foreach (PropertyInfo p in pi)
                {
                    values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
                }

                // Return the property and field values of the instance.
                return values;
            }

            public DataTable ExtendTable(DataTable table, Type type)
            {
                // Extend the table schema if the input table was null or if the value 
                // in the sequence is derived from type T.            
                foreach (FieldInfo f in type.GetFields())
                {
                    if (!_ordinalMap.ContainsKey(f.Name))
                    {
                        // Resgister the field as a column in the table if it doesn't exist
                        // already.
                        DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                            : table.Columns.Add(f.Name, f.FieldType);

                        // Resgister the field to the ordinal map.
                        _ordinalMap.Add(f.Name, dc.Ordinal);
                    }
                }
                foreach (PropertyInfo p in type.GetProperties())
                {
                    if (!_ordinalMap.ContainsKey(p.Name))
                    {
                        // Resgister the property as a column in the table if it doesn't exist
                        // already.
                        DataColumn dc;
                        if (table.Columns.Contains(p.Name))
                            dc = table.Columns[p.Name];
                        else
                        {
                            if (p.PropertyType.Name.Contains("Nullable"))
                            {
                                var types = p.PropertyType.GetGenericArguments();
                                dc = table.Columns.Add(p.Name, types[0]);
                            }
                               
                            else
                                dc = table.Columns.Add(p.Name, p.PropertyType);
                        }


                        //DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        //    : table.Columns.Add(p.Name, p.PropertyType);

                            // Resgister the property to the ordinal map.
                        _ordinalMap.Add(p.Name, dc.Ordinal);
                    }
                }

                // Return the table.
                return table;
            }
        }


        public static DataTable CopyToDT<T>(this IQueryable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDT<T>(this IQueryable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }

    }

    public class DataGridViewTools
    {
        //公共删除方法
        public static void DelGridData(DataGridView dg)
        {
            foreach (DataGridViewRow r in dg.SelectedRows)
            {
                if (r.Index < dg.Rows.Count - 1)
                    dg.Rows.Remove(r);
            }
        }

        //公共保存方法
        public static void SavaGridData(DataGridView dg)
        {
            DataTable cdt = ((DataTable)(dg.DataSource)).GetChanges();///dt 就是datagridview.datasouce

            if (cdt == null)
                return;

            foreach (DataGridViewRow r in cdt.Rows)
            {
                if (cdt.Rows[r.Index].RowState == DataRowState.Deleted)
                {

                }
                else if (cdt.Rows[r.Index].RowState == DataRowState.Modified)
                {

                }
                else if (cdt.Rows[r.Index].RowState == DataRowState.Added)
                {

                }
            }

            ((DataTable)(dg.DataSource)).AcceptChanges();
        }
    }

     
}
