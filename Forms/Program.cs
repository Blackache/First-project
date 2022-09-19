using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splash spl = new Splash();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(4.5);
            spl.Show();
            while(end > DateTime.Now)
            {
                Application.DoEvents();
            }
            spl.Close();
            spl.Dispose();
            Application.Run(new Form1());
            
        }
    }
}
