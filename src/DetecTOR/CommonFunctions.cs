using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace DetecTOR
{
    public enum ClearType
    {
        SearchResult=0,
        All = 1,
        Cancel = 2
    }

    public static class CommonFunctions
    {
        public static string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DetecTOR\\";
        public static string AppConfigFile = "detector.xml";
        public static string NetSettingsFile = "network.xml";
        public static string ColumnsDesrFile = "columns.xml";
        public static string IPDatabaseFile = "tornodes.csv";        
        private static string AppPath = Application.StartupPath;

        public static void ErrMessage(string stMessage)
        {
            MessageBox.Show(stMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }                

    }
}
