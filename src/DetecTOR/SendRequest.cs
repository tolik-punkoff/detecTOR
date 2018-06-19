using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace DetecTOR
{
    public enum SendRequestErrorType
    {
        RequestError = 0,
        OtherError = 1,
        ProtocolError = 2,        
    }
    public class SendRequestErrorEventArgs : EventArgs
    {
        public SendRequestErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }        
    }

    class SendRequest
    {
        public delegate void OnConnecting(object sender);
        public delegate void OnOK(object sender);
        public delegate void OnError(object sender, SendRequestErrorEventArgs e);
        public event OnConnecting Connecting;
        public event OnError Error;
        public event OnOK OK;
        
        private HttpWebRequest request = null;
        private WebProxy proxy = null;
        private SendRequestErrorEventArgs e = new SendRequestErrorEventArgs();
        private string fileName = "";
        
        public string URL { get; set; }        

        public NetConnectionType ConnectionType { get; set; }
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }        
        public int ConnectionTimeout { get; set; }

        public string ErrorMessage { get; private set; }


        public static string ALZ(string Str, int Size)
        {
            if (Str.Length >= Size) return Str;
            Str = Str.PadLeft(Size, '0');            
            return Str;
        }

        private string GetTimestamp()
        {
            DateTime dt = DateTime.Now;
            string ts = ALZ(dt.Day.ToString(),2) + "." + ALZ(dt.Month.ToString(),2) +
                "." + ALZ(dt.Year.ToString(),2) +
                " " + ALZ(dt.Hour.ToString(),2)+ ":" + ALZ(dt.Minute.ToString(),2) +
                ":" + ALZ(dt.Second.ToString(),2);
            return ts;
        }
        
        public SendRequest(string url, string filename)
        {
            fileName = filename;
            URL = url;
        }

        public bool CreateRequest()
        {
            //устанавливаем минимальные параметры для запроса
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(URL);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                e.ErrorMessage = ex.Message;
                e.ErrorType = SendRequestErrorType.RequestError;
                if (Error != null) Error(this, e);
                return false;
            }

            switch (ConnectionType)
            {
                case NetConnectionType.NoProxy:
                    {
                        proxy = null;
                        HttpWebRequest.DefaultWebProxy = null;
                        request.Proxy = proxy;
                    }; break;

                case NetConnectionType.SystemProxy:
                    {
                        HttpWebRequest.DefaultWebProxy = HttpWebRequest.GetSystemWebProxy();
                        proxy = null;
                    }; break;
                case NetConnectionType.ManualProxy:
                    {
                        proxy = new WebProxy(ProxyAddress, ProxyPort);
                        if (!string.IsNullOrEmpty(ProxyUser)) //есть имя пользователя, надобно авторизоваться
                        {
                            CredentialCache cred = new CredentialCache();
                            cred.Add(ProxyAddress, ProxyPort, "Basic",
                                new NetworkCredential(ProxyUser, ProxyPassword));

                            proxy.Credentials = cred;
                        }

                        request.Proxy = proxy;
                    }; break;
            }


            if (ConnectionTimeout > 0) request.Timeout = ConnectionTimeout;
            request.AllowAutoRedirect = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:52.0) Gecko/20100101 Firefox/52.0";

            return true;
        }
        
        public void Send()
        {            
            if (Connecting != null) Connecting(this);
         
            //получаем ответ
            HttpWebResponse resp = null;
            Stream temp = null;
            StreamReader sr = null;
            StreamWriter sw = null;
            string Answer = "";
            try
            {
                resp = (HttpWebResponse)request.GetResponse();
                //не вывалились в ошибку, значит все OK

                temp = resp.GetResponseStream(); 
                sr = new StreamReader(temp);
                sw = new StreamWriter(fileName);
                while (Answer != null)
                {
                    Answer = sr.ReadLine();
                    if (Answer != null)
                    {
                        sw.WriteLine(Answer);
                    }
                }
                
                File.WriteAllText(fileName + ".timestamp", GetTimestamp());
                
                temp.Close();
                sr.Close();
                sw.Close();
                
            }
            catch (WebException ex)
            {
                if (temp!=null) temp.Close();
                if (sr!=null) sr.Close();
                if (sw != null) sw.Close();

                ErrorMessage = ex.Message;

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    //ошибка протокола (404, например)
                    //интернет может и есть

                    e.ErrorMessage = ex.Message;
                    e.ErrorType = SendRequestErrorType.ProtocolError;
                    if (Error != null) Error(this, e);                    
                    return;
                }
                else //какая-то другая ошибка
                {
                    e.ErrorMessage = ex.Message;
                    e.ErrorType = SendRequestErrorType.OtherError;
                    if (Error != null) Error(this, e);
                    return;
                }
            }

            if (OK != null) OK(this);

            return;
        }

        public void Start()
        {
            System.Threading.Thread sendThread = new System.Threading.Thread(Send);
            sendThread.Start();
        }
    }
}
