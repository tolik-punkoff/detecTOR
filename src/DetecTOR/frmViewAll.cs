using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmViewAll : Form
    {
        public CSVWork LoadedData = null;
        bool bRenaming = false;
        int iCurrCol = -1;
        
        public frmViewAll()
        {
            InitializeComponent();
        }

        private void frmViewAll_Shown(object sender, EventArgs e)
        {
            gridData.Height = this.Height-70;
            gridData.Width = this.Width-10;                        
        }

        private void frmViewAll_Resize(object sender, EventArgs e)
        {
            gridData.Height = this.Height-70;
            gridData.Width = this.Width-10;
        }

        private void frmViewAll_Load(object sender, EventArgs e)
        {
            ColumnsData cd = new ColumnsData(CommonFunctions.SettingsPath +
                CommonFunctions.ColumnsDesrFile);
            cd.LoadData();
            if (LoadedData.dsTorData.Tables[LoadedData.TableName] != null)
            {
                gridData.DataSource = LoadedData.dsTorData.Tables[LoadedData.TableName]
                    .DefaultView;

                for (int i = 0; i < gridData.Columns.Count; i++)
                {
                    gridData.Columns[i].HeaderText =
                        cd.GetDesription(gridData.Columns[i].Name);
                }
                bRenaming = true;
                gridData_CurrentCellChanged(null, null);
            }
        }

        private void gridData_CurrentCellChanged(object sender, EventArgs e)
        {
            if ((gridData.CurrentCell != null)&&(bRenaming))
            {
                lblSearchIn.Text = "Поиск по столбцу [" + gridData.Columns[gridData.CurrentCell.ColumnIndex].HeaderText + "]:";
                iCurrCol = gridData.CurrentCell.ColumnIndex;
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            //поиск по любому полю в таблице
            for (int i = 0; i < gridData.RowCount; i++)
            {
                gridData.Rows[i].Selected = false;
                if (gridData.Rows[i].Cells[iCurrCol].Value != null)
                    if (gridData.Rows[i].Cells[iCurrCol].Value.ToString().StartsWith(txtFind.Text,true,null))
                        {
                            gridData.CurrentCell = gridData.Rows[i].Cells[iCurrCol];
                            gridData.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //поиск по любому полю в таблице (вниз)
            for (int i = gridData.CurrentCell.RowIndex+1; i < gridData.RowCount; i++)
            {
                gridData.Rows[i].Selected = false;
                if (gridData.Rows[i].Cells[iCurrCol].Value != null)
                    if (gridData.Rows[i].Cells[iCurrCol].Value.ToString().StartsWith(txtFind.Text, true, null))
                    {
                        gridData.CurrentCell = gridData.Rows[i].Cells[iCurrCol];
                        gridData.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            for (int i = gridData.CurrentCell.RowIndex-1; i >= 0; i--)
            {
                gridData.Rows[i].Selected = false;
                if (gridData.Rows[i].Cells[iCurrCol].Value != null)
                    if (gridData.Rows[i].Cells[iCurrCol].Value.ToString().StartsWith(txtFind.Text, true, null))
                    {
                        gridData.CurrentCell = gridData.Rows[i].Cells[iCurrCol];
                        gridData.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
            }
        }

        




    }
}