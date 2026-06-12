using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.ApplicationExit += (sender, e) => Application.Exit();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += Application_ApplicationExit;

            Application.Run(new Form1());

        }
        private static void Application_ApplicationExit(object sender,EventArgs e)
        {
            try
            {
                string tempFolder =Path.Combine(Path.GetTempPath(),"JacobsDesktopApp");

                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder,true);
                }
            }
            catch
            {
            }
        }
    }
}
