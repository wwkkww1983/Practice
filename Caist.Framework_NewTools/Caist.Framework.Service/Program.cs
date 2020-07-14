using System;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMian());
            //Application.Run(new FrmMian_New());
        }
    }
}
