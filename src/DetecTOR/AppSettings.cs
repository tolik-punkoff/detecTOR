using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DetecTOR
{
    public enum FormatType
    {
        IPList = 0,
        IPAddInfo = 1
    }

    [Serializable]
    public class AppSettings
    {
        public string DataUrl { get; set; }
        public FormatType DataFormat { get; set; }
        public string IPColumn { get; set; }
        public string FieldSeparator { get; set; }
        public string FlagColumn { get; set; }
        public string TrueValue { get; set; }
        public string FalseValue { get; set; }
        public bool LoadUpdate { get; set; }
        public bool AllTorData { get; set; }

        public AppSettings()
        {
        }
    }
        
    public class AppSettingsManager
    {
        private string fileName = "";        
        public string ConfigError { get; private set; }
        public AppSettings Settings = new AppSettings();
        
        public AppSettingsManager(string filename)
        {
            fileName = filename;

            Settings.DataUrl = "http://torstatus.blutmagie.de/query_export.php/Tor_query_EXPORT.csv";
            Settings.DataFormat = FormatType.IPAddInfo;
            Settings.FlagColumn = "Flag -";
            Settings.IPColumn = "IP Address";
            Settings.FieldSeparator=",";
            Settings.TrueValue = "1";
            Settings.FalseValue = "0";

            Settings.LoadUpdate = false;
            Settings.AllTorData = false;
        }

        public bool SaveConfig()
        {
            FileStream fs = null;
            
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            XmlSerializer formatter = new XmlSerializer(typeof(AppSettings));
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                formatter.Serialize(fs, Settings);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            fs.Close();
            return true;
        }

        public bool LoadConfig()
        {
            FileStream fs = null;
            XmlSerializer formatter = new XmlSerializer(typeof (AppSettings));

            if (!File.Exists(fileName)) return true;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                Settings = (AppSettings)formatter.Deserialize(fs);
            }
            catch (Exception ex)
            {
                if (fs != null) fs.Close();
                ConfigError = ex.Message;
                return false;
            }

            fs.Close();
            return true;
        }
    }
}
