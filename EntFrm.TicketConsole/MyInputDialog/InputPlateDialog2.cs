using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class PlateInputDialog : MetroForm
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
        private bool bScanFlag = true;
        private Thread thread;
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private string StrInput;

        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public PlateInputDialog()
        {
            InitializeComponent();
        }

        private void PlateInputDialog_Load(object sender, EventArgs e)
        {
            dpWord.SelectedIndex = 0;
            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);

            //dpServiceList.SelectedIndex = 0;

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
                    if (!bScanFlag)
                    {
                        break;
                    }

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

                bScanFlag = false;
                bResult = true;
            }
            catch (Exception ex)
            {
                bScanFlag = true;
                bResult = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int i = txtInput.Text.Trim().Length;
            if (i > 0 && i < 8)
            {
                StrInput = dpWord.SelectedItem.ToString() + txtInput.Text.Trim();
                //StrInput = txtInput.Text.Trim();
                bResult = true;
                bScanFlag = false;
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            else
            {
                MessageBox.Show("请正确输入车牌号码!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bResult = false;
            bScanFlag = false; 
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string s = txtInput.Text.Trim();
            if (s.Length > 0)
            {
                txtInput.Text = s.Substring(0, s.Length - 1);
            }
            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string s = btn.Text;

            s = txtInput.Text.Trim() + s;
            txtInput.Text = s;

            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);
        }

        private void btnTByTime_Click(object sender, EventArgs e)
        {
            StrInput = "";
            bResult = true;
            bScanFlag = false;
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }
    }
}
