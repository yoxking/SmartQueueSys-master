using EntFrm.Framework.Utility;
using EntFrm.MainService.Dialogs;
using EntFrm.MainService.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.MainService
{
    public partial class MainFrame : Form
    {
        public delegate void SpeechTextCallback(string text, string voice, int volume, int rate); 
        public delegate void SetMessageCallback(string message); 
        public delegate void DoExitMainService();
        public static SpeechTextCallback DoSpeechText;
        public static SetMessageCallback PrintMessage; 
        public static DoExitMainService ExitService;

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            PrintMessage += doPrintMessage;
            ExitService += doExitSerivce;
            DoSpeechText += doSpeechText;

            OnInit_Form();
            OnStart_QueueService();

            myTimer.Start();
            //try
            //{
            //    //this.btnRegister.Visible = false;
            //    string activeDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    int ret = IUserContext.doCheck_EncryptDogStatus(ref activeDate);
            //    if (ret == 0)
            //    {
            //        PrintMessage += doPrintMessage; 
            //        ExitService += doExitSerivce;

            //        OnStart_QueueService();

            //        myTimer.Start();

            //        DateTime dt = DateTime.Parse(activeDate);

            //        if (dt > DateTime.Parse("2029-12-31"))
            //        {
            //            this.lbRegInfo.Text = "产品授权：正式版本";
            //            this.btnRegister.Text = "更改注册码";
            //            //this.btnRegister.Visible = false;
            //        }
            //        else
            //        {
            //            this.lbRegInfo.Text = "产品授权：授权至" + activeDate;
            //            this.btnRegister.Visible = true;
            //        }

            //        this.WindowState = FormWindowState.Minimized;
            //        this.Hide();
            //    }
            //    else
            //    {
            //        ret = IUserContext.doCheck_TrialRegistStatus();
            //        if (ret > 0)
            //        {
            //            if (ret < 3)
            //            {
            //                MessageBox.Show("当前软件授权为试用版本，请联系管理员注册正式版本！");
            //            }

            //            PrintMessage += doPrintMessage;
            //            ExitService += doExitSerivce;

            //            OnStart_QueueService();

            //            myTimer.Start();

            //            this.lbRegInfo.Text = "产品授权：试用版本（限用" + ret + "天）";
            //            this.btnRegister.Visible = true;

            //            this.WindowState = FormWindowState.Minimized;
            //            this.Hide();
            //        }
            //        else
            //        {
            //            if (MessageBox.Show("软件授权试用限期已过，是否注册软件？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //            {
            //                RegSoft dlg = new RegSoft();
            //                dlg.ShowDialog();
            //            }

            //            this.Close();
            //            this.Dispose();
            //            Application.Exit();
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出错提示：" + ex.Message);

            //    this.Close();
            //    this.Dispose();
            //    Application.Exit();
            //}
        }

        private void OnInit_Form()
        {

            List<ItemObject> varsList = IPublicConsts.GetRegisteModel();

            dpRegisteList.DataSource = varsList;
            dpRegisteList.ValueMember = "Value";
            dpRegisteList.DisplayMember = "Name";


            string serverurl = IUserContext.GetConfigValue("MqServerurl");
            string serverip = IUserContext.GetConfigValue("MqServerip");
            string username = IUserContext.GetConfigValue("MqUsername");
            string password = IUserContext.GetConfigValue("MqPassword");
            string mqenable = IUserContext.GetConfigValue("MqEnable");            
            string regmodel = IUserContext.GetParamValue(IPublicConsts.DEF_REGISTEMODE, "Other");

            txtMqServerUrl.Text = serverurl;
            txtMqServerIp.Text = serverip;
            txtMqUsername.Text = username;
            txtMqPassword.Text = password;
            ckMqEnable.Checked = bool.Parse(mqenable);
            dpRegisteList.SelectedValue = regmodel;

        }

        private void OnStart_QueueService()
        {
            try
            {

                //初始化主机服务
                Thread thread1 = new Thread(NetHostService.CreateInstance().OpenService);
                thread1.Start();

                //初始化硬件呼叫监听                
                Thread thread2 = new Thread(HdcallerService.CreateInstance().doInitiaCaller);
                thread2.Start();

                //初始化消息服务
                Thread thread3 = new Thread(IRabbitMqService.CreateInstance().StartMqService);
                thread3.Start();

                //预约注册服务
                Thread thread7 = new Thread(IRegistService.CreateInstance().StartRegistFlows);
                thread7.Start();

                //初始化综合屏显示
                //Eq2013LedMatrix eq2008 = new Eq2008LedMatrix();
                //eq2008.doStart_LedMatrix();

                //初始化LED屏信息
                Thread thread4 = new Thread(DisplayService.CreateInstance().doInitialLedTip);
                thread4.Start();

                //清空员工登录
                Thread thread5 = new Thread(ITicketService.CreateInstance().doSignoutAll);
                thread5.Start();

                //历史数据迁移操作
                Thread thread6 = new Thread(DbaseService.CreateInstance().doMigrateData);
                thread6.Start();
            }
            catch (Exception ex)
            {
                NetHostService.CreateInstance().CloseService();
                IRabbitMqService.CreateInstance().StopMqService();
                IRegistService.CreateInstance().StopRegistFlows();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "服务启动失败...");
            }
        }

        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void doPrintMessage(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.txtResult.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.txtResult.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.txtResult.Disposing || this.txtResult.IsDisposed)
                        return;
                }

                SetMessageCallback d = new SetMessageCallback(doPrintMessage);
                this.txtResult.Invoke(d, new object[] { text });
            }
            else
            {
                txtResult.AppendText(text + Environment.NewLine);
            }
        }

        private void doSpeechText(string text, string voice, int volume, int rate)
        {
            BeginInvoke(new Action(() =>
            {
                SpeechHelper cs = new SpeechHelper();
                cs.SpeakText(text, voice, volume, rate);
            }));
        }

        private void doExitSerivce()
        {
            try
            {
                NetHostService.CreateInstance().CloseService();
                IRabbitMqService.CreateInstance().StopMqService();
                IRegistService.CreateInstance().StopRegistFlows();
            }
            catch(Exception ex) { }

            BeginInvoke(new Action(() =>
            { 
                this.Close();
                this.Dispose();
                Application.Exit();

            }));
        }

        private void frmMainFrame_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.myNotifyIcon.Visible = true;
            }
        }

        private void myNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void onExitSystem()
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                myNotifyIcon.Visible = false;

                try
                {
                    NetHostService.CreateInstance().CloseService();
                    IRabbitMqService.CreateInstance().StopMqService();
                    IRegistService.CreateInstance().StopRegistFlows();
                }
                catch (Exception ex) { }

                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            this.txtResult.SelectionStart = this.txtResult.Text.Length;
            this.txtResult.SelectionLength = 0;
            this.txtResult.ScrollToCaret();
        }
        
        private void btnSysetting_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "EntFrm.SettingConsole.exe";
            process.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            // process.WaitForExit();//无限制的等待，看自己情况要不要加

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnDbsetting_Click(object sender, EventArgs e)
        {
            Setting dlg = new Setting();
            dlg.ShowDialog();
        }

        private void btnClearlog_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            //LogClass info=JsonConvert.DeserializeObject<LogClass>(txtResult.Text);

            //MessageBox.Show(info.sClassName);
        }
          
        private void btnBrowselog_Click(object sender, EventArgs e)
        {
            string logFile = System.Windows.Forms.Application.StartupPath + "\\AppLogs";
            System.Diagnostics.Process.Start("Explorer.exe", logFile);
        } 
        private void myTimer_Tick(object sender, EventArgs e)
        {

            //Thread thread2 = new Thread(DisplayService.CreateInstance().doRefreshLedTip);
            //thread2.Start();
        } 
         
        private void btnClearAllData_Click(object sender, EventArgs e)
        {
            DbaseService.CreateInstance().doClearAllData();
        }

        private void btnClearFlowsData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doClearQueueData);
            thread.Start();
        }

        private void btnBakupDb_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doBakupData);
            thread.Start();
        }

        private void btnRecoverDb_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DbaseService.CreateInstance().doRecoverData);
            thread.Start();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnMqSave_Click(object sender, EventArgs e)
        {
            try
            {
                string serverUrl = txtMqServerUrl.Text.Trim();
                string serverIp = txtMqServerIp.Text.Trim();
                string username = txtMqUsername.Text.Trim();
                string password = txtMqPassword.Text.Trim();
                string mqenable = ckMqEnable.Checked.ToString();
                string regmodel = dpRegisteList.SelectedValue.ToString();

                IUserContext.SetConfigValue("MqServerurl", serverUrl);
                IUserContext.SetConfigValue("MqServerip", serverIp);
                IUserContext.SetConfigValue("MqUsername", username);
                IUserContext.SetConfigValue("MqPassword", password);
                IUserContext.SetConfigValue("MqEnable", mqenable);
                IUserContext.SetParamValue(IPublicConsts.DEF_REGISTEMODE, regmodel,"Other");

                MessageBox.Show("保存成功!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("出错提示：" + ex.Message);
            }
        }
    }
}
