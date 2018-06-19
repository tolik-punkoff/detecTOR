using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmAddColumn : Form
    {        
        public frmAddColumn()
        {
            InitializeComponent();
        }

        public FormWorker fw = null;
        public bool Edit = false;
        public object ID = null;

        private void frmAddColumn_Load(object sender, EventArgs e)
        {                       

            if (Edit)
            {
                fw.FillFormFromDataSet(ID);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool editResult = false;
            if (Edit)
            {
                editResult = fw.AddOrEditRow(ID);
            }
            else
            {
                editResult = fw.AddOrEditRow(null);
            }            

            if (!editResult)
                {
                    CommonFunctions.ErrMessage(fw.ErrorMessage);
                    return;
                }
            this.Close();
        }
    }
}
