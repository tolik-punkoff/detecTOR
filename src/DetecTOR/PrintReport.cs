using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DetecTOR
{
    public class PrintReport
    {
        public string FileName { get; set; }
        public string ErrorMessage { get; private set; }
        public DataGridView Grid { get; set; }
        private List<string> ReportBuf = new List<string>();
        
        public PrintReport(string filename)
        {
            FileName = filename;
        }

        public PrintReport(string filename, DataGridView grid)
        {
            FileName = filename;
            Grid = grid;
        }

        public bool SaveFromGrid()
        {            

            if (Grid == null)
            {                
                ErrorMessage = "No data to save!";                
                return false;
            }

            if (Grid.Rows.Count == 0)
            {
                ErrorMessage = "No data to save!";                
                return false;
            }

            int max = GetMaxDesrLen();

            string buf = string.Empty;

            foreach (DataGridViewRow row in Grid.Rows)
            {
                buf = buf + AESP(row.Cells["Desr"].Value.ToString(),max) + "\t" 
                    + row.Cells["Value"].Value.ToString();

                buf = buf + "\r\n";
            }

            try
            {
                File.WriteAllText(FileName, buf, Encoding.GetEncoding(1251));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;                
                return false;
            }

            return true;
        }

        private int GetMaxDesrLen()
        {
            int MaxLen = 0;
            foreach (DataGridViewRow row in Grid.Rows)
            {
                string s = row.Cells["Desr"].Value.ToString();
                if (s.Length > MaxLen) MaxLen = s.Length;
            }
            return MaxLen;
        }

        public static string AESP(string Str, int Size)
        {
            if (Str.Length >= Size) return Str;
            Str = Str.PadRight(Size, ' ');
            return Str;
        }

        public void ReportClear()
        {
            ReportBuf.Clear();
        }

        public void ReportAdd(string St)
        {
            ReportBuf.Add(St);
        }
        
        public void ReportAddRange(List<string> RepList)
        {
            ReportBuf.AddRange(RepList);
        }

        public bool ReportSave(string outputFile)
        {
            string[] buf = ReportBuf.ToArray();

            try
            {
                File.WriteAllLines(outputFile, buf, Encoding.GetEncoding(1251));
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
