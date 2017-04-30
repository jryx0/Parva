using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BigData.JW
{
    public class GlobalEnviroment
    {
        public static string MainDBFile;  //主数据文件
        public static string WorkDir; //工作目录

        public static string InputDBDir; //中间数据库目录
        public static string InputExcelDir; //输入excel目录
        public static string ResultOutputDir; //结果目录 Excle

        public static bool LocalLogin = true;
        public static bool isCryt = true;
        public static int MaxThreadNum = Environment.ProcessorCount;
        

        public GlobalEnviroment()
        {
            InitEnviroment();
        }

        public static void InitEnviroment()
        {
            if (!Directory.Exists(Properties.Settings.Default.WorkDir))
                Properties.Settings.Default.WorkDir = AppDomain.CurrentDomain.BaseDirectory;
            WorkDir = Properties.Settings.Default.WorkDir;
            Properties.Settings.Default.Save();

            InputExcelDir = WorkDir + "\\输入数据";
            ResultOutputDir = WorkDir + "\\Excel结果";
            InputDBDir = WorkDir + "\\导入中间数据库\\";

            MainDBFile = Properties.Settings.Default.MainDBFile;
        }

    }
}
