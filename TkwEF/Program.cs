using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.UI;
using static TkwEF.UI.f_BUI_Base;

namespace TkwEF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Applicatioin_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new UI.f_Main());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string msg = string.Format("Возникла непредвиденная необработанная ошибка уровня домена. Приложение будет закрыто\n{0}", e.ExceptionObject.ToString());
            new UiException(msg, new Exception(e.ExceptionObject.ToString())).ShowMessage(null);
            Application.Exit();
        }

        private static void Applicatioin_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string msg = "Возникла не предвиденная ошибка в работе потоков. Приложение будет закрыто\n\r";
            new UiException(msg, e.Exception).ShowMessage(null);
            Application.ExitThread();
        }
    }
}
