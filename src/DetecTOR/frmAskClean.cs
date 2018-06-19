using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmAskClean : Form
    {
        public ClearType Result = ClearType.Cancel;
        public frmAskClean()
        {
            InitializeComponent();            
        }

        private void frmAskClean_Load(object sender, EventArgs e)
        {

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            Result = ClearType.SearchResult;
            this.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            Result = ClearType.All;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = ClearType.Cancel;
            this.Close();
        }        
    }
}
