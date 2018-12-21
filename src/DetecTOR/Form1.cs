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
        string SxPath = "";
        string ReportName = "";
        string TorPath = "";

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

            TorPath = CommonFunctions.SettingsPath +
                CommonFunctions.TorDir + CommonFunctions.IPDatabaseFile;
        }

        public void LoadTORData()
        {
            string URL=settings.Settings.DataUrl.Trim();
            if (string.IsNullOrEmpty(URL))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Не указан адрес базы данных TOR-адресов!";
            }

            
            
            SendRequest req = new SendRequest(URL, TorPath);
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
            CSVLoader.FileName = TorPath;
            
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

        void ValidateSxGeo()
        {
            //проверяем наличие БД SxGeo
            SxPath = CommonFunctions.SettingsPath +
                CommonFunctions.SxGeoDir + CommonFunctions.SxGeoCity;
            IPGeoinfo tmpGeoInfo = new IPGeoinfo(SxPath);
            if (tmpGeoInfo.IsValidSxGeoFile()) //SxGeoSity найдена и валидная
            {
                lblSxGeoStatus.ForeColor = Color.Green;
                lblSxGeoStatus.Text = "БД SxGeoCity готова";
                btnSxGeoInfo.Enabled = true;
            }
            else
            {
                SxPath = CommonFunctions.SettingsPath +
                CommonFunctions.SxGeoDir + CommonFunctions.SxGeoCountry;
                tmpGeoInfo = new IPGeoinfo(SxPath);
                if (tmpGeoInfo.IsValidSxGeoFile()) //SxGeoCountry найдена и валидная
                {
                    lblSxGeoStatus.ForeColor = Color.Green;
                    lblSxGeoStatus.Text = "БД SxGeoCountry готова";
                    btnSxGeoInfo.Enabled = true;
                }
                else
                {
                    lblSxGeoStatus.ForeColor = Color.Brown;
                    lblSxGeoStatus.Text = tmpGeoInfo.ErrorMessage;
                    SxPath = string.Empty;
                    btnSxGeoInfo.Enabled = false;
                }
            }
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
            if (!Directory.Exists(CommonFunctions.SettingsPath +
                CommonFunctions.TorDir))
            {
                try
                {
                    Directory.CreateDirectory(CommonFunctions.SettingsPath +
                CommonFunctions.TorDir);
                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
            
            if (settings.Settings.LoadUpdate || CommonFunctions.UpdateInvariant)
            {
                LoadTORData();
            }
            else
            {
                if (!File.Exists(TorPath))
                {
                    LoadTORData();
                }
                else
                {
                    AnalyseTorData();
                }
            }

            ValidateSxGeo();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            frmViewAll fVA = new frmViewAll();
            fVA.LoadedData = CSVLoader;
            fVA.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {            
            if (!IPConverter.IsIP(ipAddr.Text))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Введите правильный IP-адрес!";
                return; //что-то не ввели до конца
            }

            ColumnsData cd = new ColumnsData(CommonFunctions.SettingsPath +
                    CommonFunctions.ColumnsDesrFile);
            cd.LoadData();

            CSVLoader.OutputGrid = grdAnswer;
            CSVLoader.ClearFind();
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
            }

            if (!string.IsNullOrEmpty(SxPath))
            {
                IPGeoinfo GeoInfo = new IPGeoinfo(SxPath);

                if (!GeoInfo.Open())
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = lblMessage.Text + "Ошибка SxGeo: " +
                        GeoInfo.ErrorMessage;
                    return;
                }

                if (grdAnswer.Columns.Count < 2)
                {
                    grdAnswer.Columns.Add("Desr", "Параметр");
                    grdAnswer.Columns.Add("Value", "Значение");
                }

                GeoInfo.AddToDataGridView(ipAddr.Text, grdAnswer, cd);
                
                lblMessage.Text = lblMessage.Text + " Поиск по базе SxGeo выполнен.";
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

            PrintReport printReport = new PrintReport(dlgSave.FileName, grdAnswer);
            if (!printReport.SaveFromGrid())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = printReport.ErrorMessage;
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Сохранено в " + dlgSave.FileName;

                Ans = MessageBox.Show("Открыть сохраненный файл?","Вопрос", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Ans == DialogResult.No) return;

                string tmpMsg = CommonFunctions.OpenFile(dlgSave.FileName);
                if (tmpMsg != string.Empty)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = tmpMsg;
                }
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

        private void btnUptateSxGeo_Click(object sender, EventArgs e)
        {            
            DialogResult Ans = dlgOpenSxG.ShowDialog();
            if (Ans == DialogResult.Cancel) return;
            string ResultMess="";
            string srcpath = CommonFunctions.AddSlash(dlgOpenSxG.SelectedPath)
                + CommonFunctions.SxGeoCountry;
            string dstpath = CommonFunctions.SettingsPath +
                CommonFunctions.SxGeoDir + CommonFunctions.SxGeoCountry;

            //ищем файл SxGeoCountry
            IPGeoinfo tmpGeoInfo = new IPGeoinfo(srcpath);
            if (tmpGeoInfo.IsValidSxGeoFile())
            {
                ResultMess = "Файл " + CommonFunctions.SxGeoCountry + " найден. \n"
                    + CommonFunctions.CopyFile(srcpath, dstpath) + "\n";
            }
            else
            {
                ResultMess = "Файл " + CommonFunctions.SxGeoCountry + ": " 
                    + tmpGeoInfo.ErrorMessage + "\n";
            }

            //ищем файл SxGeoCity
            srcpath = CommonFunctions.AddSlash(dlgOpenSxG.SelectedPath)
                + CommonFunctions.SxGeoCity;
            dstpath = CommonFunctions.SettingsPath +
                CommonFunctions.SxGeoDir + CommonFunctions.SxGeoCity;

            tmpGeoInfo = new IPGeoinfo(srcpath);
            if (tmpGeoInfo.IsValidSxGeoFile())
            {
                ResultMess = ResultMess + "Файл " + CommonFunctions.SxGeoCity + " найден. \n"
                    + CommonFunctions.CopyFile(srcpath, dstpath);
            }
            else
            {
                ResultMess = ResultMess + "Файл " + CommonFunctions.SxGeoCity + ": " 
                    + tmpGeoInfo.ErrorMessage;
            }

            MessageBox.Show(ResultMess, "Результат обновления",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            ValidateSxGeo();

        }

        private void btnSxGeoInfo_Click(object sender, EventArgs e)
        {
            SxGeoDB tmpSxDB = new SxGeoDB(SxPath);
            if (!tmpSxDB.OpenDB())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = tmpSxDB.ErrorMessage;
                lblSxGeoStatus.Text = "Ошибка.";
                lblSxGeoStatus.ForeColor = Color.Brown;
                return;
            }
            frmInfo fInfo = new frmInfo();
            fInfo.Header = tmpSxDB.GetHeader();
            fInfo.DBPath = SxPath;
            fInfo.ShowDialog();
            tmpSxDB.CloseDB();
        }

        private void btnBatchFind_Click(object sender, EventArgs e)
        {
            DialogResult Ans = dlgOpen.ShowDialog();
            if (Ans == DialogResult.Cancel) return;
            dlgSaveBatch.FileName = "";
            Ans = dlgSaveBatch.ShowDialog();
            if (Ans == DialogResult.Cancel) return;
            ReportName = dlgSaveBatch.FileName;

            ColumnsData cd = new ColumnsData(CommonFunctions.SettingsPath +
                    CommonFunctions.ColumnsDesrFile);
            cd.LoadData();

            IPBatchFinder Finder = new IPBatchFinder(dlgOpen.FileName,
                ReportName, CSVLoader, cd);
            Finder.AllTorData = settings.Settings.AllTorData;
            Finder.SxPath = SxPath;
            Finder.StatusChange += new IPBatchFinder.OnStatusChange(Finder_StatusChange);
            Finder.Start();
        }

        void Finder_StatusChange(object sender, BatchFinderStatusEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                lblMessage.Text = e.Message;
                switch (e.Status)
                {
                    case BatchFinderStatus.Complete:
                        {
                            lblMessage.ForeColor = Color.Green;
                            pbConnecting.Visible = false;

                            DialogResult Ans = MessageBox.Show("Открыть сохраненный отчет?",
                                "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (Ans == DialogResult.No) return;

                            string tmpMsg = CommonFunctions.OpenFile(ReportName);
                            if (tmpMsg != string.Empty)
                            {
                                lblMessage.ForeColor = Color.Red;
                                lblMessage.Text = tmpMsg;
                            }

                        }; break;
                    case BatchFinderStatus.Working:
                        {
                            if (!pbConnecting.Visible) pbConnecting.Visible = true;
                            lblMessage.ForeColor = Color.Blue;
                        }; break;
                    case BatchFinderStatus.Error:
                        {
                            lblMessage.ForeColor = Color.Red;
                            pbConnecting.Visible = false;
                        }; break;
                }                
            });
        }
    }
}
