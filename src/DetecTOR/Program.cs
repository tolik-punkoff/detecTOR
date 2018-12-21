using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DetecTOR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string cline = Environment.CommandLine.ToLowerInvariant(); //пкс

            if (cline.IndexOf("/help") != -1) //помощь
            {
                Application.Run(new frmReadme());
                return;
            }

            if (cline.IndexOf("/update") != -1) //обновление в любом случае
            {
                CommonFunctions.UpdateInvariant = true;                
            }

            if (cline.IndexOf("/np") != -1) //не портабельный режим
            {
                //данные в C:\Users\<пользователь>\AppData\Local\DetecTOR\
                CommonFunctions.SettingsPath =
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) 
                    + "\\DetecTOR\\";
            }
            else
            {
                //данные в папке с экзешником\data\
                CommonFunctions.SettingsPath = CommonFunctions.AddSlash(Application.StartupPath)+
                    "data\\";
                
            }

            
            Application.Run(new frmMain());
        }
    }
}
