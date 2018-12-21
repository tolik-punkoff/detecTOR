using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DetecTOR
{
    public enum BatchFinderStatus
    {
        Working = 0,
        Error = 1,
        Complete = 2
    }

    public class BatchFinderStatusEventArgs : EventArgs
    {
        public BatchFinderStatus  Status { get; set; }
        public string Message { get; set; }
    }
    
    public class IPBatchFinder
    {
        public delegate void OnStatusChange(object sender, BatchFinderStatusEventArgs e);
        public event OnStatusChange StatusChange;
        
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string ErrorMessage { get; private set; }
        public bool AllTorData { get; set; }
        public string SxPath { get; set; }

        private CSVWork CSVLoader = null;
        private ColumnsData ColData = null;

        public IPBatchFinder(string inputfile, string outputfile, 
            CSVWork csvloader, ColumnsData coldata)
        {
            InputFile = inputfile;
            OutputFile = outputfile;
            AllTorData = false;
            SxPath = string.Empty;
            CSVLoader = csvloader;
            ColData = coldata;
        }

        public string[] GetAllIPs()
        {
            //читаем файл
            string readBuf = "";
            try
            {
                readBuf = File.ReadAllText(InputFile);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

            //делаем поиск по регулярке
            string rExpr=@"(25[0-5]|2[0-4]\d|[01]?\d\d?)(\.(25[0-5]|2[0-4]\d|[01]?\d\d?)){3}";
            Regex IPRegex = new Regex(rExpr);
            
            MatchCollection mc = IPRegex.Matches(readBuf);

            if (mc.Count != 0)
            {
                List<string> listBuf = new List<string>();
                foreach (Match m in mc)
                {
                    listBuf.Add(m.Value);
                }
                string[] outputBuf = listBuf.ToArray();
                return outputBuf;
            }
            else
            {
                ErrorMessage = "IP addresses not found!";
                return null;
            }
        }

        public void Start()
        {
            System.Threading.Thread workThread = new System.Threading.Thread(GetData);
            workThread.Start();
        }

        private void SendStatus(BatchFinderStatus status, string message)
        {
            BatchFinderStatusEventArgs e = new BatchFinderStatusEventArgs();
            e.Status = status;
            e.Message = message;
            if (StatusChange != null) StatusChange(this, e);
        }

        private void GetData()
        {
            IPGeoinfo GeoInfo = null;
            
            SendStatus(BatchFinderStatus.Working, "Получаю список IP...");
            string[] IPs = GetAllIPs();
            if (IPs == null)
            {
                SendStatus(BatchFinderStatus.Error, ErrorMessage);
                return;
            }
            
            SendStatus(BatchFinderStatus.Working, "Подготавливаю данные...");
            if (!string.IsNullOrEmpty(SxPath)) //открываем базу SxGeo
            {
                GeoInfo = new IPGeoinfo(SxPath);
                if (!GeoInfo.Open())
                {
                    ErrorMessage = GeoInfo.ErrorMessage;
                    SendStatus(BatchFinderStatus.Error, ErrorMessage);
                    return;
                }
            }
            
            //объект для отчета
            PrintReport Log = new PrintReport(OutputFile);

            //добавляем строку с полями
            string stFields = "";
            if (GeoInfo != null)
            {
                stFields = GeoInfo.GetDescriptions(ColData);
            }

            if (AllTorData)
            {
                stFields = stFields + ";" + ColData.GetDesription("In_Tor") +
                    ";" + CSVLoader.GetDescriptions(ColData);
            }
            else
            {
                stFields = stFields + ";" + ColData.GetDesription("In_Tor");
            }
            Log.ReportAdd(stFields);

            //начинаем обработку IP
            int Count = IPs.Length;
            int Current = 0;
            int ErrCount = 0;
            if (GeoInfo != null) GeoInfo.PrepareBatch();
            
            //обрабатываем IP
            foreach (string IP in IPs)
            {
                //прибавили счетчик, послали статус, подготовили буфер
                Current++;
                SendStatus(BatchFinderStatus.Working, "IP " + Current.ToString() + "/" +
                    Count.ToString());
                string buf = string.Empty;
                string sxinfo = string.Empty;

                //информация SxGeo
                if (GeoInfo != null)
                {
                    sxinfo = GeoInfo.GetDataString(IP);
                }

                //информация Tor
                List<string> listTor = CSVLoader.FindList(IP);

                if (listTor == null) //ошибка
                {
                    SendStatus(BatchFinderStatus.Error, CSVLoader.ErrorMessage);
                    ErrCount++;
                    buf = sxinfo + ";" + CSVLoader.ErrorMessage;
                    Log.ReportAdd(buf);
                }
                else
                {
                    if (listTor.Count == 0) //не нашли
                    {
                        buf = sxinfo + ";Not found in TOR";
                        Log.ReportAdd(buf);
                    }
                    else
                    {
                        string found = ";Found ("+listTor.Count.ToString()+")";

                        if (!AllTorData) //полная информация не нужна
                        {
                            buf = sxinfo + found;
                            Log.ReportAdd(buf);
                        }
                        else //нужна полная информация по tor-ноде(ам)
                        {
                            int torI = 0;                            
                            foreach (string node in listTor)
                            {
                                torI++;
                                buf = sxinfo + found + " #" + torI + ";"
                                    + node;
                                Log.ReportAdd(buf);
                            }
                        }
                    }                    
                }
                //конец обработки IP
                
            }
            //конец цикла обработки всех IP
            //сохраняем отчет
            if (!Log.ReportSave(OutputFile))
            {
                ErrorMessage = Log.ErrorMessage;
                SendStatus(BatchFinderStatus.Error, "Ошибка сохранения отчета: "+
                    ErrorMessage);
                return;
            }

            if (ErrCount != 0)
            {
                ErrorMessage = "При обработке адресов произошли ошибки.";
                SendStatus(BatchFinderStatus.Error, ErrorMessage);
            }
            else
            {
                SendStatus(BatchFinderStatus.Complete, "Отчет успешно сохранен.");
            }
        }

    }
}
