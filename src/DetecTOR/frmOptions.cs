using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }
        AppSettingsManager settings = null;
        FormWorker fw = null;
        public bool Changed = false;

        private void frmOptions_Load(object sender, EventArgs e)
        {
            settings = new AppSettingsManager(CommonFunctions.SettingsPath +
            CommonFunctions.AppConfigFile);
            settings.LoadConfig();
            fw = new FormWorker(settings.Settings, this);
            fw.FillForm();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (txtDataUrl.Text.Trim()==string.Empty)
            {
                CommonFunctions.ErrMessage("URL данных не может быть пустым!");
                return;
            }

            if (!fw.GetData())
            {
                CommonFunctions.ErrMessage(fw.ErrorMessage);
                return;
            }

            if (!settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(settings.ConfigError);
                return;
            }

            Changed = true;
            this.Close();
 
        }

        private void rbDataFormatIPAddInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDataFormatIPAddInfo.Checked)
            {
                btnFormatOptions.Enabled = true;
            }
            else
            {
                btnFormatOptions.Enabled = false;
            }
        }

        private void btnFormatOptions_Click(object sender, EventArgs e)
        {
            frmFormatOptions fFormatOptions = new frmFormatOptions();
            fFormatOptions.ShowDialog();
        }

        private void btnNetworkOption_Click(object sender, EventArgs e)
        {
            frmNetworkSettings fNetSettings = new frmNetworkSettings();
            fNetSettings.ShowDialog();
        }        
        
    }
}