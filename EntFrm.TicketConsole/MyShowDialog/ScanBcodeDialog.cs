using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ScanBcodeDialog : Form
    {
        private BarcodeHook BarCode = new BarcodeHook();
        private bool bResult = false;
        private int clockTime; 
        private BackgroundWorker bkWorker = new BackgroundWorker(); 
        private string StrInput=""; 
         
        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public ScanBcodeDialog()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BarcodeHook.BarCodeDelegate(BarCode_BarCodeEvent);
        }

        private void ScanBcodeDialog_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.pnlContainer.BackgroundImage = EntFrm.TicketConsole.Properties.Resources.ResScanBarcode;
            this.pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;

            this.lbTimeStr.Location = new Point((this.pnlContainer.Width - lbTimeStr.Width) / 2, this.lbTimeStr.Location.Y);
             
            clockTime = 15;
            BarCode.Start();

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
            DialogResult = bResult?DialogResult.OK:DialogResult.Cancel;
            
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
         
        private void ScanBcodeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        public void BarCode_BarCodeEvent(BarcodeHook.BarCodes barCode)
        {
            try
            {
                if (barCode.IsValid)
                {
                    StrInput = barCode.BarCode;
                    bResult = true;
                    BarCode.Stop();
                }
            }
            catch(Exception ex) { }
        }
    }
}