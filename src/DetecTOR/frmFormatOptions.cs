using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmFormatOptions : Form
    {
        public frmFormatOptions()
        {
            InitializeComponent();
        }

        public bool Changed = false;
        AppSettingsManager settings = null;
        FormWorker fw = null;

        private void frmFormatOptions_Load(object sender, EventArgs e)
        {
            settings = new AppSettingsManager(CommonFunctions.SettingsPath +
            CommonFunctions.AppConfigFile);
            settings.LoadConfig();
            fw = new FormWorker(settings.Settings, this);            
            fw.FillForm();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtIPColumn.Text.Trim() == string.Empty)
            {
                CommonFunctions.ErrMessage("Не введено имя колонки, содержащей IP-адрес!");
                return;
            }
            if (txtFieldSeparator.Text.Trim() == string.Empty)
            {
                CommonFunctions.ErrMessage("Не указан разделитель столбцов!");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmColumnsDesr fCD = new frmColumnsDesr();
            fCD.ShowDialog();
        }

        

        
    }
}