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
            Control.CheckForIllegalCrossThreadCalls = false;

            /**
            * 当前用户是管理员的时候，直接启动应用程序
            * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                Application.Run(new frmMain());
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return;
                }
                //退出
                Application.Exit();
            }
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
