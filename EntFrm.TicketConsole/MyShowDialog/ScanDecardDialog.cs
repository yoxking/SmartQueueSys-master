using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ScanDecardDialog : Form
    {
        #region  T6接口函数
        [DllImport("dcic32.dll")]        //初始化端口
        public static extern int IC_InitComm(int Port);

        [DllImport("dcic32.dll")]        //关闭端口
        public static extern short IC_ExitComm(int icdev);

        [DllImport("dcic32.dll")]        //卡下电(要重新审核密码才能继续操作)
        public static extern short IC_Down(int icdev);

        [DllImport("dcic32.dll")]        //初始化卡型
        public static extern short IC_InitType(int icdev, int cardType);

        [DllImport("dcic32.dll")]        //判断连接是否成功,<0 ,连接不成功.0可以读写,1连接成功,但是没插卡.
        public static extern short IC_Status(int icdev);
        [DllImport("dcic32.dll")]        //检查原始密码(小于0为不正确)
        public static extern short IC_CheckPass_4442hex(int icdev, string Password);
        [DllImport("dcic32.dll")]        //更改密码(0为更改成功)
        public static extern short IC_ChangePass_4442hex(int icdev, string Password);
        [DllImport("dcic32.dll")]        //在固定的位置写入固定长度的数据
        public static extern short IC_Write(int icdev, int offset, int Length, string Databuffer);
        [DllImport("dcic32.dll")]        //在固定的位置读出固定长度的数据
        public static extern short IC_Read(int icdev, int offset, int l, byte[] Databuffer);
        [DllImport("dcic32.dll")]        //发出声音
        public static extern short IC_DevBeep(int icdev, int intime);
        //cpu卡函数
        ////[DllImport("./dcic32.dll")] //cpu卡上电复位
        ////public static extern short IC_CpuReset(int icdev, ref short rlen, StringBuilder rbuff);
        ////[DllImport("./dcic32.dll")] //cpu卡上电复位
        ////public static extern short IC_CpuReset_Hex(int icdev, ref short rlen, StringBuilder rbuff);

        ////[DllImport("./dcic32.dll")]
        ////public static extern short IC_CpuReset(int icdev, [Out] byte[] rlen, [Out] byte[] rbuff);
        ////[DllImport("./dcic32.dll")]
        ////public static extern short IC_CpuReset_Hex(int icdev, [Out] byte[] rlen, [Out] byte[] rbuff);
        [DllImport("dcic32.dll")]
        public static extern short IC_CpuReset(int icdev, ref byte rlen, ref char rbuff);
        [DllImport("dcic32.dll")]
        public static extern short IC_CpuReset_Hex(int icdev, ref byte rlen, StringBuilder rbuff);


        [DllImport("dcic32.dll")]//设置cpu参数
        public static extern short IC_SetCpuPara(int icdev, short cputype, short cpupro, short cpuetu);
        [DllImport("dcic32.dll")] //cpu卡信息交换协议
        public static extern short IC_CpuApdu(int icdev, short slen, string sbuff, ref short rlen, ref string rbuff);
        [DllImport("dcic32.dll")] //cpu卡信息交换协议
        public static extern short IC_CpuApdu_Hex(int icdev, short slen, string sbuff, ref short rlen, StringBuilder rbuff);

        #endregion

        private BarcodeHook BarCode = new BarcodeHook();
        private bool bResult = false;
        private int clockTime;
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private string StrInput = "";
         
        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        private int icdev=-1;

        public ScanDecardDialog()
        {
            InitializeComponent();
        }

        private void ScanDecardDialog_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.pnlContainer.BackgroundImage = EntFrm.TicketConsole.Properties.Resources.ResScanDecard;
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
            try
            {
                icdev = IC_InitComm(100); //初始化usb 
            }
            catch (Exception ex)
            {
                icdev = -1;
            }

            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }


        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            lbTimeStr.Text = clockTime + "秒";
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = bResult ? DialogResult.OK : DialogResult.Cancel;

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
                     
                    if (icdev >-1)
                    {
                        if (IC_Status(icdev) == 0)
                        {
                            IC_DevBeep(icdev, 10);
                            bResult = true;
                        }
                    } 

                    Thread.Sleep(1000);
                }
            }

            return -1;
        } 
    }
}
