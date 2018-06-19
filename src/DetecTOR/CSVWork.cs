using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace DetecTOR
{
    public class CSVErrorEventArgs : EventArgs
    {        
        public string ErrorMessage { get; set; }
    }

    public class CSVWork
    {
        public delegate void OnCSVError(CSVErrorEventArgs e);
        public delegate void OnLoading();
        public delegate void OnOK();

        public event OnCSVError CSVError;
        public event OnLoading Loading;
        public event OnOK OK;

        public string TableName { get; private set; }
        public string FindResultTableName { get; private set; } 
        public char Divider { get; set; }
        public string FileName { get; set; }
        public string Flag { get; set; }
        public string FalseValue { get; set; }
        public string TrueValue { get; set; }
        public string IPField { get; set; }
        public string Timestamp { get; private set; }
        public string ErrorMessage { get; private set; }
        
        public FormatType DataFormat { get; set; }
        
        public DataSet dsTorData = null;

        public CSVWork()
        {
            FindResultTableName = "FindResult";
            TableName = "TorDataTable";
            dsTorData = new DataSet();
        }
        private void GetTimestamp()
        {
            string TimestampFile = FileName + ".timestamp";
            if (!File.Exists(TimestampFile)) return;
            try
            {
                string buf = File.ReadAllText(TimestampFile);
                Timestamp = buf;
            }
            catch
            {
                return;
            }
        }
        private bool IsFlag(string FieldName)
        {
            if (string.IsNullOrEmpty(FieldName)) return false;
            if (FieldName.Contains(Flag)) return true;
            else return false;
        }
        private bool CreateDataTable(string FieldsNames)
        {
            if (string.IsNullOrEmpty(FieldsNames))
            {
                ErrorMessage = "Can't create table. No fields.";
                return false;
            }
            
            dsTorData.Tables.Add(TableName);
            string[] splitbuf = FieldsNames.Split(Divider);

            //небольшая проверка формата
            if (splitbuf.Length < 2)
            {
                ErrorMessage = "No CSV format or wrong separator [" + Divider + "]";
                return false;
            }

            foreach (string colName in splitbuf)
            {
                if (IsFlag(colName))
                {
                    dsTorData.Tables[TableName].Columns.Add(colName, typeof(bool));
                }
                else
                {
                    dsTorData.Tables[TableName].Columns.Add(colName, typeof(string));
                }
            }

            return true;
        }

        public int Find(string IP, ColumnsData cd)
        {
            CSVErrorEventArgs e = new CSVErrorEventArgs();
            DataRow[] selected = null;
            if (DataFormat == FormatType.IPList) IPField = "IP";
            try
            {
                selected = dsTorData.Tables[TableName].
                    Select("[" + IPField + "]='" + IP + "'");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                e.ErrorMessage = ex.Message;
                if (CSVError != null) CSVError(e);
                return 0;
            }

            if (selected == null) return 0;
            if (selected.Length == 0) return 0;

            if (dsTorData.Tables[FindResultTableName] != null) 
                dsTorData.Tables.Remove(FindResultTableName);

            dsTorData.Tables.Add(FindResultTableName);
            dsTorData.Tables[FindResultTableName].Columns.Add("Desr",typeof (string ));
            dsTorData.Tables[FindResultTableName].Columns.Add("Value", typeof(string ));

            foreach (DataRow row in selected)
            {
                foreach (DataColumn col in dsTorData.Tables[TableName].Columns)
                {
                    string Desr = cd.GetDesription(col.ColumnName);
                    string  Value = row[col].ToString();
                    dsTorData.Tables[FindResultTableName].Rows.Add(Desr, Value);
                }
            }
            return selected.Length;
        }

        public static string AESP(string Str, int Size)
        {
            if (Str.Length >= Size) return Str;
            Str = Str.PadRight(Size, ' ');
            return Str;
        }


        private int GetMaxDesrLen()
        {
            int MaxLen = 0;
            foreach (DataRow row in dsTorData.Tables[FindResultTableName].Rows)
            {
                string s = row["Desr"].ToString();
                if (s.Length > MaxLen) MaxLen = s.Length;
            }
            return MaxLen;
        }

        public bool SaveFind(string filename)
        {
            CSVErrorEventArgs e = new CSVErrorEventArgs();

            if (dsTorData.Tables[FindResultTableName] == null)
            {                
                ErrorMessage = "No data to save!";
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                return false;
            }

            if (dsTorData.Tables[FindResultTableName].Rows.Count == 0)
            {
                ErrorMessage = "No data to save!";
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                return false;
            }

            int max = GetMaxDesrLen();

            string buf = string.Empty;

            foreach (DataRow row in dsTorData.Tables[FindResultTableName].Rows)
            {
                buf = buf + AESP(row["Desr"].ToString(),max) + "\t" + row["Value"];

                buf = buf + "\r\n";
            }

            try
            {
                File.WriteAllText(filename, buf, Encoding.GetEncoding(1251));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                return false;
            }
            
            return true;
        }

        public void ClearFind()
        {
            if (dsTorData.Tables[FindResultTableName] != null)
                dsTorData.Tables.Remove(FindResultTableName);
        }

        public void ClearAll()
        {
            dsTorData.Tables.Clear();
            try
            {
                File.Delete(FileName);
                File.Delete(FileName + ".timestamp");
            }
            catch { }
        }

        //файлы на соответствие формату проверяются хреново
        //если пользователь подсунул программе URL картинок с котиками
        //то он сам дурак
        public void LoadToDataset()
        {
            if (Loading != null) Loading();
            dsTorData.Clear();
            dsTorData.Tables.Clear();
            FileStream fs = null;
            StreamReader sr = null;
            CSVErrorEventArgs e = new CSVErrorEventArgs();
            int stringCount = 0;

            try
            {
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.GetEncoding(1251));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                e.ErrorMessage = ex.Message;
                if (CSVError != null) CSVError(e);
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                return;
            }
            
            string buf = "";
            string[] splitbuf = null;
            bool fields = true;
            int ColCount = 0;

            while (buf != null) //читаем построчно и анализируем
            {
                buf = sr.ReadLine();
                stringCount++;
                if (buf == null) break; //прерываем чтение
                buf = buf.Trim(); //удаляем граничные пробелы (а то бывало)
                if (buf == string.Empty) continue; //пустые строки пропускаем
                if (buf.EndsWith(Divider.ToString())) buf = buf.Substring(0, buf.Length - 1); //обрезаем последний разделитель, если он есть

                //если досюда не вылетели, значит читаем первую строку
                //в ней должны быть заголовки полей
                if (fields)
                {
                    if (!CreateDataTable(buf))
                    {                        
                        e.ErrorMessage = ErrorMessage;
                        if (CSVError != null) CSVError(e);
                        if (sr != null) sr.Close();
                        if (fs != null) fs.Close();
                        return;
                    }
                    fields = false;
                    //тут сохраняем количество столбцов (определяется по заголовку)
                    ColCount = dsTorData.Tables[TableName].Columns.Count;
                    continue;
                }    

                splitbuf = buf.Split(Divider); //делим строку на составляющие
                if (splitbuf.Length == ColCount) //еще небольшая проверка формата
                {                    
                    for (int i = 0; i<splitbuf.Length;i++)
                    {                       
                        Type t = dsTorData.Tables[TableName].Columns[i].DataType;
                        if (t == typeof(bool))
                        {
                            splitbuf[i] = splitbuf[i].Replace(TrueValue, "true");
                            splitbuf[i] = splitbuf[i].Replace(FalseValue, "false");
                        }                                                                        
                    }
                    dsTorData.Tables[TableName].Rows.Add(splitbuf);
                }
                else
                {
                    ErrorMessage = "Bad fields count in string "
                        +stringCount.ToString();
                    e.ErrorMessage = ErrorMessage;
                    if (CSVError != null) CSVError(e);
                    if (sr != null) sr.Close();
                    if (fs != null) fs.Close();
                    return;
                }
            }                                                                         
            
            sr.Close();
            fs.Close();
            
            if (dsTorData.Tables[TableName].Rows.Count == 0)
            {
                ErrorMessage = "No data in file!";
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                return;
            }

            GetTimestamp();
            if (OK != null) OK();
        }

        public void LoadOnlyIPToDataset()
        {
            dsTorData.Clear();
            dsTorData.Tables.Clear();
            CSVErrorEventArgs e = new CSVErrorEventArgs();
            FileStream fs = null; StreamReader sr = null;
            try
            {
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.GetEncoding(1251));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                return;
            }
            dsTorData.Tables.Add(TableName); //добавляем в DataSet таблицу
            dsTorData.Tables[TableName].Columns.Add("IP",typeof(string)); //И колонку для IP
            string buf=string.Empty;
            while (!sr.EndOfStream)
            {
                buf = sr.ReadLine();                
                buf = buf.Trim(); //избавляемся от граничных пробелов
                if (!string.IsNullOrEmpty(buf)) //если строка не пустая
                {
                    if (isIP(buf)) //если в строке IP
                    {
                        dsTorData.Tables[TableName].Rows.Add(buf);
                    }
                    else //А если какая-то ерунда...
                    {
                        ErrorMessage = "This is no IP list!";
                        e.ErrorMessage = ErrorMessage;
                        if (CSVError != null) CSVError(e);
                        if (sr != null) sr.Close();
                        if (fs != null) fs.Close();
                        return;                         
                    }
                }
            }
            sr.Close();
            fs.Close();

            if (dsTorData.Tables[TableName].Rows.Count == 0)
            {
                ErrorMessage = "No data in file!";
                e.ErrorMessage = ErrorMessage;
                if (CSVError != null) CSVError(e);
                return;
            }

            GetTimestamp();
            if (OK != null) OK();
        }

        public static bool isIP(string IPAddress)
        {
            string[] IPArr = IPAddress.Split('.');

            if (IPArr.Length != 4) return false;

            byte b = 0;            

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    b = Convert.ToByte(IPArr[i]);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }        

        public void Start()
        {
            System.Threading.Thread workThread = null;

            if (DataFormat == FormatType.IPList)
            {
                workThread = new System.Threading.Thread(LoadOnlyIPToDataset);
            }
            else
            {
                workThread = new System.Threading.Thread(LoadToDataset);
            }

            workThread.Start();
        }
    }
}
