//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        Program
//Description:  This class contains the main enry point to the application.

#region Assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace FloridaLM
{
    static class Program
    {
        #region Methods
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Main());
        }
        #endregion
    }
}
