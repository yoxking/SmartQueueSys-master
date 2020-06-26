using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.CallerConsole
{
    public partial class frmMainFrame : Form
    {
        /*
         * 
         处理状态
         0;//等待办理中...
         1;//正在办理中...
         2;//完成流程...，终止流程...
         3;//顾客未到...
         4;//票号滞后...
         5;//窗口转移...
         * **/

        public delegate void SetWaitnumCallback(int waitingNum, bool windowShow);
        public delegate void SetMessageCallback(string ticketNo, string statusMssg);
        public delegate void SetTicketsCallback(List<ItemObject> ticketList); 
        public static SetWaitnumCallback RefreshWaitnum;
        public static SetMessageCallback RefreshMessage;
        public static SetTicketsCallback RefreshTickets; 
        private ViewTicketFlows ticket = null;
        private bool pausingFlag = false;   

        #region   
        internal AnchorStyles StopAanhor = AnchorStyles.None;
        private Point mPoint = new Point();
        #endregion

        public frmMainFrame()
        {
            InitializeComponent();
        }


        //这篇文章是在互联网搜索到的，但是很多文章都没有给 WM_QUERYENDSESSION赋值这句话，所以重新整理了一下
        /// <summary>
        /// 窗口过程的回调函数
        /// </summary>
        ///<param name="m">
        private const int WM_QUERYENDSESSION = 0x0011;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //此消息在OnFormClosing之前
                case WM_QUERYENDSESSION:

                    IUserContext.OnExecuteCommand_Xp("doSignOut", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    this.Dispose();
                    Application.Exit();

                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        private void frmMainFrame_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            RefreshWaitnum += doRefreshWaitnum;
            RefreshMessage += doRefreshMessage;
            RefreshTickets += doRefreshTickets;

            myTimer.Start();

            InitializeForm();
            DoBindTransfer();
            DoRefreshForm();
            UpdateTickets();
        }

        private void InitializeForm()
        {
            System.Windows.Forms.Timer StopRectTimer = new System.Windows.Forms.Timer();
            StopRectTimer.Tick += new EventHandler(myTimerTick);
            StopRectTimer.Interval = 200;
            StopRectTimer.Enabled = true;
            //this.TopMost = true;
            StopRectTimer.Start();

            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width - 20;
            int y = Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height - 50;
            this.SetDesktopLocation(x, y);

            lbStafferName.Text = ILoginHelper.CurrentUser.sStafferName;
            this.Text = ILoginHelper.CurrentUser.sCounterName;
             
        }

        private void DoBindTransfer()
        { 
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("getTransferList", new string[] { "" });

                if (!string.IsNullOrEmpty(sResult))
                {
                    List<ItemObject> itemList = JsonConvert.DeserializeObject<List<ItemObject>>(sResult);

                    dpTransfer.DataSource = itemList;
                    dpTransfer.ValueMember = "Name";
                    dpTransfer.DisplayMember = "Value";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void DoRefreshForm()
        {
            btnCallRepeat.Enabled = false;
            //btnCallGoto.Enabled = true;

            btnPause.Enabled = false;
            btnWelcome.Enabled = false;
            btnNotCome.Enabled = false;
            btnFinishFlow.Enabled = false;
            btnTransfer.Enabled = false;
            dpTransfer.Enabled = false;
            btnAbortFlow.Enabled = false;
            btnDelay.Enabled = false;

            if (ticket == null)
            {
                //btnCallGoto.Enabled = true;
                btnPause.Enabled = true;
            }
            else if (ticket != null)
            {
                btnCallRepeat.Enabled = true;

                btnWelcome.Enabled = true;
                btnNotCome.Enabled = true;
                btnFinishFlow.Enabled = true;
                btnTransfer.Enabled = true;
                dpTransfer.Enabled = true;
                btnAbortFlow.Enabled = true;
                btnDelay.Enabled = true;
            }
        }

        private void UpdateTickets()
        { 
            //刷新等候病人信息
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("getWaitingListByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo, "100" });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        List<ItemObject> ticketList = JsonConvert.DeserializeObject<List<ItemObject>>(sResult);

                        frmMainFrame.RefreshTickets(ticketList);
                    }
                    else
                    {
                        frmMainFrame.RefreshTickets(null);
                    }
                }
                catch (Exception ex)
                {
                    frmMainFrame.RefreshMessage("", ex.Message);
                }

            });

        }
  
        private void doRefreshWaitnum(int waitingNum, bool windowShow)
        { 
            if (this.lbWaiterNum.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.lbWaiterNum.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.lbWaiterNum.Disposing || this.lbWaiterNum.IsDisposed)
                        return;
                }

                SetWaitnumCallback d = new SetWaitnumCallback(doRefreshWaitnum);
                this.lbWaiterNum.Invoke(d, new object[] { waitingNum, windowShow });
            }
            else
            {                
                this.lbWaiterNum.Text = waitingNum.ToString(); 

                if (windowShow)
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        this.Activate();
                    }
                }
            }
        }
         
        private void doRefreshMessage(string ticketNo, string statusMssg)
        { 
            if (this.lbTicketNo.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.lbTicketNo.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.lbTicketNo.Disposing || this.lbTicketNo.IsDisposed)
                        return;
                }

                SetMessageCallback d = new SetMessageCallback(doRefreshMessage);
                this.lbTicketNo.Invoke(d, new object[] { ticketNo, statusMssg });
            }
            else
            {
                this.lbTicketNo.Text = ticketNo; 
                this.lbMessage.Text = statusMssg; 
            }
        }

        private void doRefreshTickets(List<ItemObject> ticketList)
        {
            if (this.dpTicketList.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.dpTicketList.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.dpTicketList.Disposing || this.dpTicketList.IsDisposed)
                        return;
                }

                SetTicketsCallback d = new SetTicketsCallback(doRefreshTickets);
                this.dpTicketList.Invoke(d, new object[] { ticketList });
            }
            else
            {
                dpTicketList.DataSource = ticketList;
                dpTicketList.ValueMember = "Value";
                dpTicketList.DisplayMember = "Name";
            }
        }
         
        private void myTimerTick(object sender, EventArgs e)
        {
            if (this.Bounds.Contains(Cursor.Position))
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, 0);
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point(0, this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - this.Height);
                        break;
                }
            }
            else
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, (this.Height - 8) * (-1));
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point((-1) * (this.Width - 8), this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 8, this.Location.Y);
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, (Screen.PrimaryScreen.Bounds.Height - 8));
                        break;
                }
            }

        }

        private void mStopAnhor()
        {
            if (this.Top <= 0 && this.Left <= 0)
            {
                StopAanhor = AnchorStyles.None;
            }
            else if (this.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (this.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else if (this.Top >= Screen.PrimaryScreen.Bounds.Height - this.Height)
            {
                StopAanhor = AnchorStyles.Bottom;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }
 
        private void frmMainFrame_LocationChanged(object sender, EventArgs e)
        {
            this.mStopAnhor();
        }

        private void frmMainFrame_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void frmMainFrame_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        private void frmMainFrame_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                myContextMenu.Show(this, e.Location);
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

        private void frmMainFrame_SizeChanged(object sender, EventArgs e)
        { 
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.myNotifyIcon.Visible = true;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void onExitSystem()
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IUserContext.OnExecuteCommand_Xp("doSignOut", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                myNotifyIcon.Visible = false;
                 
                this.Dispose();
                Application.Exit();
            }
        }

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        { 
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingDialog dlg = new SettingDialog();
            dlg.ShowDialog();
        }

        private void btnQuitCaller_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void btnCallNext_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("doCallNextTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (!string.IsNullOrEmpty(sResult))
                {
                    sResult = IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { sResult });
                    ticket = JsonConvert.DeserializeObject<ViewTicketFlows>(sResult);

                    if (ticket != null)
                    {
                        lbTicketNo.Text = ticket.sTicketNo;
                        lbMessage.Text = "呼叫下一位操作成功！";
                    }
                }
                else
                {
                    ticket = null;
                    lbTicketNo.Text = "";
                    lbMessage.Text = "暂无办理业务的顾客!";
                }
                DoRefreshForm();
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnCallRepeat_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doRecallTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        lbMessage.Text = "重复呼叫操作成功!";
                        return;
                    }
                }
                lbMessage.Text = "重复呼叫操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnCallGoto_Click(object sender, EventArgs e)
        {
            try
            {
                string sNewTicketNo = "";
                if (dpTicketList.SelectedIndex >= 0)
                {
                    sNewTicketNo = ((ItemObject)dpTicketList.SelectedItem).Value.ToString();
                } 

                if (!string.IsNullOrEmpty(sNewTicketNo))
                {

                    string sResult = IUserContext.OnExecuteCommand_Xp("doSpecialTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo, sNewTicketNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        sResult = IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { sResult });
                        ticket = JsonConvert.DeserializeObject<ViewTicketFlows>(sResult);

                        if (ticket != null)
                        {
                            lbTicketNo.Text = ticket.sTicketNo;
                            lbMessage.Text = "指定呼叫操作成功！";
                        }
                    }
                    else
                    {
                        ticket = null;
                        lbMessage.Text = "指定呼叫操作失败!";
                    }

                    DoRefreshForm();
                }
                else
                {
                    lbMessage.Text = "指定呼叫操作失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnWelcome_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doStartTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        lbMessage.Text = "开始办理操作成功!";
                        return;
                    }
                }
                lbMessage.Text = "开始办理操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnNotCome_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doNotcomeTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "顾客未到操作成功！";

                        DoRefreshForm(); 
                        return;
                    }
                }
                lbMessage.Text = "顾客未到操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnFinishFlow_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doFinishTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "办理完成操作成功！";

                        DoRefreshForm();
                        return;
                    }
                }
                lbMessage.Text = "办理完成操作失败!";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnCallHelper_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("doSeekHelp", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (bool.Parse(sResult))
                {
                    lbMessage.Text = "窗口求助成功！"; 
                }
                else
                {
                    lbMessage.Text = "窗口求助失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!pausingFlag)
            {
                btnCallHelper.Enabled = false;

                btnCallNext.Enabled = false;
                btnCallRepeat.Enabled = false;
                btnCallGoto.Enabled = false;

                btnWelcome.Enabled = false; 
                btnNotCome.Enabled = false; 
                btnFinishFlow.Enabled = false;

                dpTicketList.Enabled = false;
                dpTransfer.Enabled = false;

                btnPause.Text = "恢复";
                pausingFlag = true;

                IUserContext.OnExecuteCommand_Xp("doOutService", new string[] { ILoginHelper.CurrentUser.sCounterNo });
            }
            else
            {
                btnCallNext.Enabled = true;
                btnCallHelper.Enabled = true;
                btnCallGoto.Enabled = true;

                dpTicketList.Enabled = true;
                dpTransfer.Enabled = true;

                btnPause.Text = "暂停";
                pausingFlag = false;
                DoRefreshForm();

                IUserContext.OnExecuteCommand_Xp("doInService", new string[] { ILoginHelper.CurrentUser.sCounterNo });
            }
        }
          
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ticket != null)
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("doTransferTicket", new string[] { dpTransfer.SelectedValue.ToString(), ticket.sPFlowNo });

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        ticket = null;
                        lbTicketNo.Text = "";
                        lbMessage.Text = "业务转移成功！";
                    }
                    else
                    {
                        lbMessage.Text = "业务转移失败!";
                    }
                    DoRefreshForm();
                } 
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        } 
          
        private void btnDelay_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = IUserContext.OnExecuteCommand_Xp("doDelayTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (!string.IsNullOrEmpty(sResult))
                {
                    ticket = null;
                    lbTicketNo.Text = "";
                    lbMessage.Text = "滞后操作成功！";

                    DoRefreshForm();
                }
                else
                {
                    lbMessage.Text = "滞后操作失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void btnAbortFlow_Click(object sender, EventArgs e)
        {
            try
            { 
                string sResult = IUserContext.OnExecuteCommand_Xp("doAbortTicket", new string[] { ILoginHelper.CurrentUser.sCounterNo });

                if (!string.IsNullOrEmpty(sResult))
                {
                    ticket = null;
                    lbTicketNo.Text = "";
                    lbMessage.Text = "中止业务操作成功！";

                    DoRefreshForm();
                }
                else
                {
                    lbMessage.Text = "中止业务操作失败!";
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            //刷新等候人数
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string sResult = IUserContext.OnExecuteCommand_Xp("getWaitingCountByCounterNo", new string[] { ILoginHelper.CurrentUser.sCounterNo });
                    
                    frmMainFrame.RefreshWaitnum(int.Parse(sResult), sResult.Equals("0"));
                }
                catch (Exception ex)
                {
                    frmMainFrame.RefreshMessage("", ex.Message);
                }
            });
        } 

        private void dpTicketList_DropDown(object sender, EventArgs e)
        {
            UpdateTickets(); 
        }
    }
}
