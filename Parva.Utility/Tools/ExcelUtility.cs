using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Parva.Utility.Tools
{
    public class ExcelUtility
    {
        public static string GetExcelValue(ICell cell)
        {
            string strValue = "";
            if (cell == null)
                strValue = null;
            else if (cell.CellType == CellType.Formula)
                try
                {
                    if (cell.CachedFormulaResultType == CellType.Numeric)
                        strValue = cell.NumericCellValue.ToString();
                    else
                        strValue = cell.StringCellValue;
                }
                catch
                {
                    strValue = cell.ToString();
                }
            else if (cell.CellType == CellType.Numeric)
            {
                try
                {
                    short format = cell.CellStyle.DataFormat;
                    if (format == 14 || format == 31 || format == 57 || format == 58 || format == 27 || format == 176)
                        strValue = cell.DateCellValue.ToShortDateString();
                    else
                        strValue = cell.NumericCellValue.ToString();
                }
                catch (Exception ex)
                {
                    if (cell.DateCellValue != null)
                        strValue = cell.ToString();
                    else strValue = "0";
                }
            }
            else if (cell.CellType == CellType.Error)
                strValue = cell.ErrorCellValue.ToString();
            else strValue = cell.StringCellValue;



            return strValue;
        }
        public static System.Collections.IEnumerator getExcelFileRows(string FileName)
        {
            ISheet sheet = null;
            try
            {
                string ext = Path.GetExtension(FileName);
                using (FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    if (ext == ".xls")
                    {
                        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                        sheet = hssfworkbook.GetSheetAt(0);

                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {                
                return null;
            }

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            return rows;
        }
        public static DataTable ReadXLSToDataTable(string FileName, int top = int.MaxValue)
        {
            System.Collections.IEnumerator rows = getExcelFileRows(FileName);
            if (rows == null)
            {
               // log.Error("文件" + Path.GetFileName(FileName) + "无法打开");
                return null;
            }

            DataTable dt = new DataTable();
            int readlines = 0;
            int colnumbers = 20;
            string ext = Path.GetExtension(FileName);
            if (ext != ".xls") return null;

             while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                if (colnumbers > row.LastCellNum) colnumbers =  row.LastCellNum;
                if (readlines == 0)                
                    for (int j = 0; j < colnumbers; j++)
                        dt.Columns.Add((j + 1).ToString());              

                for (int i = 0; i < colnumbers; i++)
                {
                    dr[i] = GetExcelValue(row.GetCell(i));                    
                }

                dt.Rows.Add(dr);
                if (readlines++ > top)
                    break;
            }
            return dt;
        }
        public static DataTable ReadCSVToDataTable(string FileName, int top = int.MaxValue)
        {
            DataTable dt = new DataTable();
            try
            {
                StreamReader mysr = new StreamReader(FileName, System.Text.Encoding.Default);

                String Line = "";
                int colnumbers = 0;
                int readlines = 0;

                while ((Line = mysr.ReadLine()) != null)
                {
                    DataRow dr = dt.NewRow();
                    string[] cols = Line.Split(',');

                    if (colnumbers < cols.Length)
                        for (int j = colnumbers; j < cols.Length; j++)
                        {
                            dt.Columns.Add((j + 1).ToString());
                            colnumbers = cols.Length;
                        }


                    if (readlines++ > top)
                        break;

                    for (int i = 0; i < cols.Length; i++)
                        dr[i] = cols[i];

                    dt.Rows.Add(dr);
                }
                mysr.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

    }
}
