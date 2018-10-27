using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassPro
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            } else if (args.Length == 1)
            {
                // Install package :D

            } else
            {
                // Toomany arguments! Don't install the package :o
                MessageBox.Show("Too many command-line arguments provided! PassPro will now exit", "PassPro | Error");
            }
            
        }
    }
}
