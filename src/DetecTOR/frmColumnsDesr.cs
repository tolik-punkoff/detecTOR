using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DetecTOR
{
    public partial class frmColumnsDesr : Form
    {
        public frmColumnsDesr()
        {
            InitializeComponent();
        }

        ColumnsData colData = null;
        FormWorker fw = null;

        private void frmColumnsDesr_Load(object sender, EventArgs e)
        {
            ttInfo.SetToolTip(btnAdd, "Добавить");
            ttInfo.SetToolTip(btnEdit, "Редактировать");
            ttInfo.SetToolTip(btnDel, "Удалить");

            colData = new ColumnsData(CommonFunctions.SettingsPath +
                CommonFunctions.ColumnsDesrFile);
            if (!colData.LoadData())
            {
                CommonFunctions.ErrMessage(colData.ErrorMessage);
                return;
            }

            gvColumns.DataSource = colData.dsColumns.Tables[colData.TableName].DefaultView;            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (colData.IsDataChanged())
            {
                DialogResult Ans = MessageBox.Show("Изменения будут потеряны!", "Закрыть?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (Ans == DialogResult.No) return;
            }
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!colData.SaveData())
            {
                CommonFunctions.ErrMessage(colData.ErrorMessage);
                return;
            }

            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddColumn fAddColumn = new frmAddColumn();
            fAddColumn.Edit = false;
            fw = new FormWorker(colData.dsColumns, colData.TableName, fAddColumn);
            fAddColumn.fw = fw;
            fAddColumn.ShowDialog();
        }

        private void frmColumnsDesr_Shown(object sender, EventArgs e)
        {
            if (gvColumns.DataSource == null) return;
            if (gvColumns.Columns.Count < 3) return;

            gvColumns.Columns[0].Visible = false;
            gvColumns.Columns[1].HeaderText="Имя колонки";
            gvColumns.Columns[2].HeaderText="Описание колонки";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gvColumns.CurrentRow == null) return;
            object ID = gvColumns.CurrentRow.Cells["ID"].Value;

            frmAddColumn fAddColumn = new frmAddColumn();
            fAddColumn.Edit = true;
            fAddColumn.ID = ID;
            fw = new FormWorker(colData.dsColumns, colData.TableName, fAddColumn);
            fAddColumn.fw = fw;
            fAddColumn.ShowDialog();
        }

        private void gvColumns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gvColumns.CurrentRow == null) return;
            object ID = gvColumns.CurrentRow.Cells["ID"].Value;

            DialogResult Ans = MessageBox.Show("Удалить запись?", "Удаление",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (Ans == DialogResult.No) return;

            if (!colData.Remove(ID))
            {
                CommonFunctions.ErrMessage(colData.ErrorMessage);
            }
        }
        
    }
}
