using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BigData.JW
{
    public static class Utility
    {
        public static DateTime TryParseDateTime(string _date)
        {
            DateTime dt = System.DateTime.MinValue;
            Dictionary<string, string> regStr = new Dictionary<string, string>();

            try
            {
                _date = _date.Replace("元", "01");
                _date = _date.Replace(" ", "");

                if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{2}(\-|\/|\.|月|\\)\d{2}(日)*$"))
                {// yyyy-mm-dd
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    dt = System.DateTime.ParseExact(_date, "yyyyMMdd", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}\d{2}\d{2}$"))
                {//yyyymmdd
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    dt = System.DateTime.ParseExact(_date, "yyyyMMdd", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{1}(\-|\/|\.|月|\\)\d{2}(日)*$"))
                {//yyyy-m-dd
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    _date = _date.Insert(4, "0");
                    dt = System.DateTime.ParseExact(_date, "yyyyMMdd", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{1}(\-|\/|\.|月|\\)\d{1}(日)*$"))
                {//yyyy-m-d
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    _date = _date.Insert(4, "0");
                    _date = _date.Insert(6, "0");
                    dt = System.DateTime.ParseExact(_date, "yyyyMMdd", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{2}(\-|\/|\.|月|\\)\d{1}(日)*$"))
                {//yyyy-mm-d
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    _date = _date.Insert(6, "0");
                    dt = System.DateTime.ParseExact(_date, "yyyyMMdd", null);
                }

                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{2}$"))
                {//yyyy-mm
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    dt = System.DateTime.ParseExact(_date, "yyyyMM", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}(\-|\/|\.|年|\\)\d{1}$"))
                {//yyyy-m
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    _date = _date.Insert(4, "0");
                    dt = System.DateTime.ParseExact(_date, "yyyyMM", null);
                }
                else if (Regex.IsMatch(_date, @"^(20|19)\d{2}$"))
                {//yyyy
                    _date = Regex.Replace(_date, @"[^0-9]", "");
                    dt = System.DateTime.ParseExact(_date, "yyyy", null);
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        #region MakeSureDirectory
        public static string MakeSureDirectory(string dir)
        {
            string strRet = "";
            try
            {
                if (!System.IO.Directory.Exists(dir))
                    MakeSureDirectoryPathExists(dir);
            }
            catch (Exception ex)
            {
                strRet = ex.Message;
            }
            return strRet;
        }
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);
        #endregion
    }
}
