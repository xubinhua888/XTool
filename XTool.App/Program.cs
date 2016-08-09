using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XTool.App
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
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnHandledException);
            XTool.BLL.OrderBLL.DatabaseInit(System.IO.Path.Combine(Application.StartupPath, "data", "data.db"));
            Application.Run(new frmMain());
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (!e.Exception.Message.Contains("VS2005DockPaneStrip"))
            {
                MessageBox.Show(e.Exception.Message);
            }
        }

        static void AppDomain_UnHandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ce = e.ExceptionObject as Exception;
            if (ce != null)
            {
                MessageBox.Show(ce.Message);
            }
        }
    }
}
