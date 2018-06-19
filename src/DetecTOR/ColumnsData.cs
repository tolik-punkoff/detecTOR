using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace DetecTOR
{
    
    public class ColumnsData
    {
        private string fileName = "";
        public DataSet dsColumns = null;

        public string ErrorMessage { get; private set; }
        public string TableName { get; private set; }

        public ColumnsData(string filename)
        {
            fileName = filename;
            TableName = "ColumnsDesription";
            CreateDataSet();
        }

        private void CreateDataSet()
        {
            dsColumns = new DataSet();
            dsColumns.Tables.Add(TableName);
            
            dsColumns.Tables[TableName].Columns.Add("ID", typeof(int));
            dsColumns.Tables[TableName].Columns["ID"].Unique = true;
            dsColumns.Tables[TableName].Columns["ID"].AutoIncrement = true;
            dsColumns.Tables[TableName].Columns["ID"].AllowDBNull = false;

            dsColumns.Tables[TableName].Columns.Add("ColumnName", typeof(string));
            dsColumns.Tables[TableName].Columns["ColumnName"].Unique = true;
            dsColumns.Tables[TableName].Columns["ColumnName"].AllowDBNull = false;

            dsColumns.Tables[TableName].Columns.Add("ColumnDesr", typeof(string));
            
            DataColumn[] pk = new DataColumn[1];
            pk[0]=dsColumns.Tables[TableName].Columns["ID"];

           dsColumns.Tables[TableName].PrimaryKey = pk;           
            
        }

        public bool IsDataChanged()
        {
            DataTable temp = dsColumns.Tables[TableName].GetChanges();
            if (temp == null) return false;
            else return true;
        }

        public bool Remove(object ID)
        {
            DataRow row = dsColumns.Tables[TableName].Rows.Find(ID);
            if (row == null)
            {
                ErrorMessage = "Not found " + ID.ToString();
                return false;
            }

            dsColumns.Tables[TableName].Rows.Remove(row);
            return true;
        }
        
        public string GetDesription(string ColumnName)
        {
            DataRow[] selected = dsColumns.Tables[TableName].
                Select("[ColumnName]='" + ColumnName + "'");

            if (selected == null) return ColumnName;
            if (selected.Length == 0) return ColumnName;

            return selected[0]["ColumnDesr"].ToString();
        }

        public bool LoadData()
        {
            if (!File.Exists(fileName)) return true;

            try
            {
                dsColumns.ReadXml(fileName);
            }
            catch
            {
                return false;
            }

            dsColumns.Tables[TableName].AcceptChanges();
            return true;
        }

        public bool SaveData()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            try
            {
                dsColumns.WriteXml(fileName);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;

        }
    }
}
