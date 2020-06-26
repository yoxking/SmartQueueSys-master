using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ScanCardDialog : Form
    {
        #region API声明
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_OpenPort(int iPort);
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_StartFindIDCard(int iPort, byte[] pucManaInfo, int iIfOpen);
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_SelectIDCard(int iPort, byte[] pucManaMsg, int iIfOpen);
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_ReadBaseMsg(int iPort, byte[] pucCHMsg, ref UInt32 puiCHMsgLen, byte[] pucPHMsg, ref UInt32 puiPHMsgLen, int iIfOpen);
        #endregion

        private bool bResult = false;
        private int clockTime;
        private Thread thread;
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private string StrInput;
        private bool InputFlag = false;

        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public bool bInputFlag { get => InputFlag; set => InputFlag = value; }

        public ScanCardDialog()
        {
            InitializeComponent();
        }

        private void ScanCardDialog_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.pnlContainer.BackgroundImage = EntFrm.TicketConsole.Properties.Resources.ResScanCard;
            this.pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;

            this.lbTimeStr.Location = new Point((this.pnlContainer.Width - lbTimeStr.Width) / 2, this.lbTimeStr.Location.Y);
            this.btnInputDlg.Location = new Point((this.pnlContainer.Width - btnInputDlg.Width) / 2, this.btnInputDlg.Location.Y);

            btnInputDlg.Visible = InputFlag;
            clockTime = 15;

            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            bkWorker.RunWorkerAsync();
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }


        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            lbTimeStr.Text = clockTime + "秒";
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bResult)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            thread.Abort();
            this.Close();
        }

        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            //判断是否请求了取消后台操作
            if (bkWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                thread = new Thread(ReadIdMessage);
                thread.Start();

                while (true)
                {
                    clockTime--;

                    if (clockTime < 1 || bResult)
                    {
                        break;
                    }
                    bkWorker.ReportProgress(clockTime);

                    Thread.Sleep(1000);
                }
            }

            return -1;
        }


        private void ReadIdMessage()
        {
            try
            {
                //变量声明
                byte[] CardPUCIIN = new byte[255];
                byte[] pucManaMsg = new byte[255];
                byte[] pucCHMsg = new byte[255];
                byte[] pucPHMsg = new byte[3024];
                UInt32 puiCHMsgLen = 0;
                UInt32 puiPHMsgLen = 0;
                int st = 0;

                int iport = 1001;

                //读卡操作
                do
                {
                    st = SDT_StartFindIDCard(iport, CardPUCIIN, 1);

                    Thread.Sleep(100);

                }
                while (st != 0x9f);


                st = SDT_SelectIDCard(iport, pucManaMsg, 1);
                if (st != 0x90) return;
                st = SDT_ReadBaseMsg(iport, pucCHMsg, ref puiCHMsgLen, pucPHMsg, ref puiPHMsgLen, 1);
                if (st != 0x90) return;
                //显示结果
                sStrInput = System.Text.ASCIIEncoding.Unicode.GetString(pucCHMsg);

                bResult = true;
            }
            catch (Exception ex)
            {
                clockTime = 0;
                bResult = false;
            }
        }

        private void ScanCardDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        } 

        private void btnInputDlg_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            thread.Abort();
            this.Close();
        } 
    }
}