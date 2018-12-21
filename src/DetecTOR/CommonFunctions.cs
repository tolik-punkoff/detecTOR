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
        public static string SettingsPath = "";
        public static string AppConfigFile = "detector.xml";
        public static string NetSettingsFile = "network.xml";
        public static string ColumnsDesrFile = "columns.xml";
        public static string IPDatabaseFile = "tornodes.csv";
        public static bool UpdateInvariant = false;

        public static string SxGeoCity = "SxGeoCity.dat";
        public static string SxGeoCountry = "SxGeo.dat";
        public static string SxGeoDir = "SxGeo\\";
        public static string TorDir = "TorData\\";

        public static void ErrMessage(string stMessage)
        {
            MessageBox.Show(stMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static string AddSlash(string st)
        {
            if (st.EndsWith("\\"))
            {
                return st;
            }
            
            return st+"\\";
        }
        public static string CopyFile(string src, string dest)
        {
            FileInfo fi = new FileInfo(dest);
            if (!Directory.Exists(fi.DirectoryName))
            {
                try
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                catch (Exception ex)
                {
                    return "Ошибка " + ex.Message;
                }
            }

            try
            {
                File.Copy(src, dest, true);
            }
            catch (Exception ex)
            {
                return "Ошибка " + ex.Message;
            }

            return "Успешно скопирован.";
        }

        public static string OpenFile(string FileName)
        {
            ProcessStartInfo psi = new ProcessStartInfo(FileName);
            psi.UseShellExecute = true;
            
            Process p = new Process();
            p.StartInfo = psi;

            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

    }
}
