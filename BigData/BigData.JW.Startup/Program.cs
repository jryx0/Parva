using System;
using System.Windows.Forms;

namespace BigData.JW.Startup
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppInit.BootStrap();
            Application.Run(new Login());
        }
    }
}
