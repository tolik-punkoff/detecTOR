using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DetecTOR
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        AppSettingsManager settings = null;
        NetSettings netSettings = null;
        CSVWork CSVLoader = null;
        bool FromInternet = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(CommonFunctions.SettingsPath))
                {
                    Directory.CreateDirectory(CommonFunctions.SettingsPath);
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }

            settings = new AppSettingsManager(CommonFunctions.SettingsPath +
                CommonFunctions.AppConfigFile);
            netSettings = new NetSettings(CommonFunctions.SettingsPath +
                CommonFunctions.NetSettingsFile);
            if (!settings.LoadConfig())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = settings.ConfigError;
                return;
            }

            NetConfigStatus status = netSettings.LoadConfig();
            if (status != NetConfigStatus.OK)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = netSettings.ConfigError;
                return;
            }
        }

        public void LoadTORData()
        {
            string URL=settings.Settings.DataUrl.Trim();
            if (string.IsNullOrEmpty(URL))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Не указан адрес базы данных TOR-адресов!";
            }
            
            SendRequest req = new SendRequest(URL, CommonFunctions.SettingsPath +
                CommonFunctions.IPDatabaseFile);
            req.ProxyAddress = netSettings.ProxyAddress;
            req.ProxyPassword = netSettings.ProxyPassword;
            req.ProxyPort = netSettings.ProxyPort;
            req.ProxyUser = netSettings.ProxyUser;
            req.ConnectionTimeout = netSettings.ConnectionTimeout;
            req.ConnectionType = netSettings.ConnectionType;

            req.Connecting += new SendRequest.OnConnecting(req_Connecting);
            req.Error += new SendRequest.OnError(req_Error);
            req.OK += new SendRequest.OnOK(req_OK);
            
            if (!req.CreateRequest()) return;
            req.Start();
        }

        void req_OK(object sender)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Данные получены успешно!";
                pbConnecting.Visible = false;
                FromInternet = true;
                AnalyseTorData();
            });
            
        }

        void req_Error(object sender, SendRequestErrorEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = e.ErrorType.ToString() + ": " + e.ErrorMessage;
                pbConnecting.Visible = false;
            });
        }

        void req_Connecting(object sender)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Blue;
                lblMessage.Text = "Получаю данные...";
                pbConnecting.Visible = true;
            });
        }

        private void AnalyseTorData()
        {
            CSVLoader = new CSVWork();
            CSVLoader.DataFormat = settings.Settings.DataFormat;
            CSVLoader.Divider = settings.Settings.FieldSeparator[0];
            CSVLoader.Flag = settings.Settings.FlagColumn;
            CSVLoader.IPField = settings.Settings.IPColumn;
            CSVLoader.FalseValue = settings.Settings.FalseValue;
            CSVLoader.TrueValue = settings.Settings.TrueValue;
            CSVLoader.FileName = CommonFunctions.SettingsPath +
                CommonFunctions.IPDatabaseFile;
            
            CSVLoader.Loading += new CSVWork.OnLoading(CSVLoader_Loading);
            CSVLoader.OK += new CSVWork.OnOK(CSVLoader_OK);
            CSVLoader.CSVError += new CSVWork.OnCSVError(CSVLoader_CSVError);            

            CSVLoader.Start();
        }

        void CSVLoader_CSVError(CSVErrorEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = e.ErrorMessage;
                pbConnecting.Visible = false;
            });
        }

        void CSVLoader_OK()
        {
            string from = "";
            if (FromInternet)
            {
                from = "Интернета. ";
            }
            else
            {
                from = "локального хранилища. ";
            }
            
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Данные загружены из "+from+
                    "Обновление: "+CSVLoader.Timestamp +"."+
                    " Записей: " +
                    CSVLoader.dsTorData.Tables[CSVLoader.TableName].Rows.Count.ToString();
                pbConnecting.Visible = false;
            });
        }

        void CSVLoader_Loading()
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblMessage.ForeColor = Color.Blue;
                lblMessage.Text = "Загружаются данные...";
                pbConnecting.Visible = true;
            });
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            frmOptions fOptions = new frmOptions();
            fOptions.ShowDialog();
            
            if (!settings.LoadConfig())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = settings.ConfigError;
                return;
            }

            NetConfigStatus status = netSettings.LoadConfig();
            if (status != NetConfigStatus.OK)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = netSettings.ConfigError;
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadTORData();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (settings.Settings.LoadUpdate)
            {
                LoadTORData();
            }
            else
            {
                if (!File.Exists(CommonFunctions.SettingsPath +
                    CommonFunctions.IPDatabaseFile))
                {
                    LoadTORData();
                }
                else
                {
                    AnalyseTorData();
                }
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            frmViewAll fVA = new frmViewAll();
            fVA.LoadedData = CSVLoader;
            fVA.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!CSVWork.isIP(ipAddr.Text))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Введите правильный IP-адрес!";
                return; //что-то не ввели до конца
            }

            ColumnsData cd = new ColumnsData(CommonFunctions.SettingsPath +
                    CommonFunctions.ColumnsDesrFile);
            cd.LoadData();

            int count = CSVLoader.Find(ipAddr.Text, cd);

            if (count == 0)
            {
                if (!string.IsNullOrEmpty(CSVLoader.ErrorMessage))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = CSVLoader.ErrorMessage;
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "IP " + ipAddr.Text + " не найден в базе данных TOR.";
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "IP " + ipAddr.Text + " найден в базе данных TOR. "+
                    count.ToString()+" вхождений";

                grdAnswer.DataSource = CSVLoader.dsTorData.Tables[CSVLoader.
                    FindResultTableName];
                
                grdAnswer.Columns[0].HeaderText = "Параметр";
                grdAnswer.Columns[1].HeaderText = "Значение";
            }
        }

        private void ipAddr_KeyUp(object sender, KeyEventArgs e)
        //в IP-аддресс контроле по умолчанию не работает вставка и копирование
        //тут исправление
        {
            IDataObject iData = Clipboard.GetDataObject();//получаем объект из клипборда

            if (e.Control) //детектим нажатый контрол
            {
                if (e.KeyValue == 86) //ctrl+v
                {
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        ipAddr.Text = (String)iData.GetData(DataFormats.Text);//вставляем данные из клипборда в контрол
                        //проверка на валидность работает нормально
                    }
                }
                if (e.KeyValue == 67) //ctrl+c
                {
                    Clipboard.SetText(ipAddr.Text);
                }
                if (e.KeyValue == 88) //ctrl+x
                {
                    Clipboard.SetText(ipAddr.Text);
                    ipAddr.Text = "";
                }
                if (e.KeyValue == 46) //ctrl+del
                {
                    ipAddr.Text = "";
                }
            }
            if (e.Shift) //детектим нажатый шифт
            {
                if (e.KeyValue == 45) //shift+ins
                {
                    ipAddr.Text = (String)iData.GetData(DataFormats.Text); //аналогично вышеизложенному
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdAnswer.Rows.Count == 0) return;

            DialogResult Ans = dlgSave.ShowDialog();
            if (Ans == DialogResult.Cancel) return;

            if (!CSVLoader.SaveFind(dlgSave.FileName))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = CSVLoader.ErrorMessage;
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Сохранено в " + dlgSave.FileName;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            frmAskClean fAsk = new frmAskClean();
            fAsk.ShowDialog();
            if (fAsk.Result == ClearType.SearchResult)
            {
                CSVLoader.ClearFind();
                grdAnswer.DataSource = null;
            }

            if (fAsk.Result == ClearType.All)
            {
                CSVLoader.ClearAll();
                grdAnswer.DataSource = null;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmReadme fReadme = new frmReadme();
            fReadme.Show();
        }
    }
}
