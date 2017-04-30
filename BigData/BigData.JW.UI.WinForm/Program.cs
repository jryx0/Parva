using System;
using System.Windows.Forms;

namespace BigData.JW.UI.WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            AppInit.BootStrap();
            System.Windows.Forms.Application.Run(new Form1());
        }      
    }
}
