using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class frmSmartForm : Form
    {
        private bool printQrcode;
        private string QrcodeText;

        #region  
        internal AnchorStyles StopAanhor = AnchorStyles.None;
        private Point mPoint = new Point();
        private StringReader streamToPrint = null;
        //private StreamReader streamToPrint = null;
        private Font printFont;
        private int iCheckPrint;
        //private int iTicketPrintCount = 1;
        private bool bUse80Printer = true;
        private PrintDocument pdTicket = new PrintDocument();
        private PageSetupDialog psdTicket = new PageSetupDialog();
        private int iPageWidth58 = 228;
        private int iPageWidth80 = 314;
        private int iPageHeight = 1169;
        #endregion  

        public frmSmartForm()
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

                    this.Close();
                    this.Dispose();
                    Application.Exit();

                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        private void frmSmartForm_Load(object sender, EventArgs e)
        {
            try
            {
                int width = int.Parse(IPublicHelper.Get_WindowWidth());
                int height = int.Parse(IPublicHelper.Get_WindowHeight());

                this.Size = new Size(width, height);

                myTimer.Start(); 

                System.Windows.Forms.Timer StopRectTimer = new System.Windows.Forms.Timer();
                StopRectTimer.Tick += new EventHandler(myTimerTick);
                StopRectTimer.Interval = 200;
                StopRectTimer.Enabled = true;
                this.TopMost = true;
                StopRectTimer.Start();

                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.txtTicketStyle.Visible = false;

                Init_BackgroundImage();
                Init_ServiceButtons();
                Init_PrintDocument();

                printQrcode = IPublicHelper.Get_QrcodeMode().Equals("True");
                QrcodeText = IPublicHelper.Get_QrcodeText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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


        private void Init_PrintDocument()
        {
            this.pdTicket.PrintController = new StandardPrintController();
            this.pdTicket.BeginPrint += new PrintEventHandler(this.pdTicket_BeginPrint);
            this.pdTicket.PrintPage += new PrintPageEventHandler(this.pdTicket_PrintPage);
            this.pdTicket.EndPrint += new PrintEventHandler(this.pdTicket_EndPrint);
            this.psdTicket.Document = this.pdTicket;
            this.psdTicket.PageSettings.Margins = new Margins(10, 10, 0, 0);
            this.psdTicket.PageSettings.PaperSize = new PaperSize("paper", this.bUse80Printer ? this.iPageWidth80 : this.iPageWidth58, this.iPageHeight);

        } 
          
        private bool IsActiveService(string sServiceNo)
        {
            bool bResult = true;
            string sCount = "0";
            string sCondition = "";

            ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(sServiceNo));

            if (info != null)
            {
                DateTime dtCurrent = DateTime.Now;
                DateTime dtMiddle = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 13:00:00");
                DateTime dAMStartTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dAMStartTime.ToString("HH:mm:ss"));
                DateTime dAMEndTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dAMEndTime.ToString("HH:mm:ss"));
                DateTime dPMStartTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dPMStartTime.ToString("HH:mm:ss"));
                DateTime dPMEndTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dPMEndTime.ToString("HH:mm:ss"));


                if (info.iWeekLimit == 1)
                {
                    string weekDay = ((int)dtCurrent.DayOfWeek).ToString();
                    bResult = info.sWeekDays.IndexOf(weekDay) >= 0 ? true : false;
                }

                if (bResult)
                {
                    if (info.iAMLimit == 1 && dtCurrent < dtMiddle)
                    {
                        if (dtCurrent >= dAMStartTime && dtCurrent <= dAMEndTime)
                        {
                            bResult = true;

                            sCondition = "ServiceNo= '" + sServiceNo + "' And   QueueTime Between '" + info.dAMStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + info.dAMEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                            sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sCondition });
                            if (info.iAMTotal < int.Parse(sCount))
                            {
                                bResult = false;
                            }
                        }
                        else
                        {
                            bResult = false;
                        }
                    }

                    if (info.iPMLimit == 1 && dtCurrent > dtMiddle)
                    {
                        if (dtCurrent >= dPMStartTime && dtCurrent <= dPMEndTime)
                        {
                            bResult = true;

                            sCondition = "ServiceNo= '" + sServiceNo + "' And   QueueTime Between '" + info.dPMStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + info.dPMEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                            sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sCondition });
                            if (info.iPMTotal < int.Parse(sCount))
                            {
                                bResult = false;
                            }
                        }
                        else
                        {
                            bResult = false;
                        }
                    }
                }

            }

            return bResult;
        }
        
        private void Init_BackgroundImage()
        {
            try
            {
                Image bgImage = Image.FromFile(IPublicHelper.BackgroundImage);
                //if (IPublicHelper.BgAutoStretch == 1)
                //{
                //    pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;
                //}
                pnlContainer.BackgroundImage = bgImage;
            }
            catch (Exception ex)
            { }
        }

        private void Init_ServiceButtons()
        {
            try
            {  
                TicketButton btnModel = null;
                TicketButtonEx myButton = null;
                FontStyle fsStyle = FontStyle.Regular; 

                if (IPublicHelper.serviceList != null && IPublicHelper.serviceList.Count > 0)
                {
                    foreach (ServiceInfo info in IPublicHelper.serviceList)
                    {
                        if (info.sParentNo.Equals("00000000"))
                        {
                            btnModel = JsonConvert.DeserializeObject<TicketButton>(info.sTicketButtonFmt);

                            if (btnModel != null)
                            {
                                myButton = new TicketButtonEx();
                                myButton.Tag = info.sServiceNo;
                                myButton.Click += new EventHandler(myButton_Click);
                                myButton.lbTitle.Tag = info.sServiceNo;
                                myButton.lbTitle.Click += new EventHandler(myButton_Click);
                                myButton.lbSubtitle.Tag = info.sServiceNo;
                                myButton.lbSubtitle.Click += new EventHandler(myButton_Click);
                                myButton.Size = new System.Drawing.Size(btnModel.iButtonWidth, btnModel.iButtonHeight);
                                myButton.Location = new Point(btnModel.iButtonLeft, btnModel.iButtonTop);
                                myButton.BackColor = Color.FromArgb(int.Parse(btnModel.sButtonBgColor));
                                if (!string.IsNullOrEmpty(btnModel.sButtonBgImage1))
                                {
                                    myButton.BackgroundImage = ImageConvert.FromBaseString(IUserContext.OnExecuteCommand_Xp("getImageFrom", new string[] { btnModel.sButtonBgImage1 }));
                                    myButton.BackgroundImageLayout = ImageLayout.Stretch;
                                }

                                if (btnModel.bIsShowTitle)
                                {
                                    if (!string.IsNullOrEmpty(btnModel.sTitleFtStyle))
                                    {
                                        fsStyle = (FontStyle)Enum.Parse(typeof(FontStyle), btnModel.sTitleFtStyle);
                                    }
                                    myButton.lbTitle.Text = info.sServiceName;
                                    myButton.lbTitle.Font = new Font(btnModel.sTitleFtFamily, btnModel.iTitleFtSize, fsStyle);
                                    myButton.lbTitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sTitleFtColor));
                                }

                                myButton.lbSubtitle.Text = "";

                                if (info.iHaveChild == 0 && btnModel.bIsShowSubtitle)
                                {
                                    if (!string.IsNullOrEmpty(btnModel.sSubtitleFtStyle))
                                    {
                                        fsStyle = (FontStyle)Enum.Parse(typeof(FontStyle), btnModel.sSubtitleFtStyle);
                                    }
                                    string sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByServiceNo", new string[] { info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0" });

                                    myButton.lbSubtitle.Text = "等待人数：" + sCount + "人";
                                    myButton.lbSubtitle.Font = new Font(btnModel.sSubtitleFtFamily, btnModel.iSubtitleFtSize, fsStyle);
                                    myButton.lbSubtitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sSubtitleFtColor));
                                }


                                if (!IsActiveService(info.sServiceNo))
                                {
                                    myButton.lbTitle.Text = "暂停服务";
                                    myButton.lbSubtitle.Text = "";
                                    //myButton.BackgroundImage = ImageConvert.FromBaseString(IUserContext.OnExecuteCommand_Xp("getImageFrom", new string[] { btnModel.sButtonBgImage2 }));
                                    myButton.Enabled = false;
                                }

                                this.pnlContainer.Controls.Add(myButton);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh_ServiceButtons()
        {
            try
            {
                if (this.pnlContainer.Controls.Count > 0)
                {
                    TicketButtonEx btnTicketEx = null;
                    TicketButton btnModel = null;
                    FontStyle fsStyle = FontStyle.Regular;
                    string sCount = "0";

                    foreach (Control myctrl in this.pnlContainer.Controls)
                    {
                        if (myctrl is TicketButtonEx)
                        {
                            btnTicketEx = (TicketButtonEx)myctrl;
                            if (IsActiveService(btnTicketEx.Tag.ToString()))
                            {
                                btnTicketEx.Enabled = true;
                                ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(btnTicketEx.Tag.ToString()));

                                btnTicketEx.lbTitle.Text = info.sServiceName;
                                btnModel = JsonConvert.DeserializeObject<TicketButton>(info.sTicketButtonFmt);
                                if (info.iHaveChild == 0 && btnModel.bIsShowSubtitle)
                                {
                                    if (!string.IsNullOrEmpty(btnModel.sSubtitleFtStyle))
                                    {
                                        fsStyle = (FontStyle)Enum.Parse(typeof(FontStyle), btnModel.sSubtitleFtStyle);
                                    }
                                    sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByServiceNo", new string[] { info.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0" });

                                    btnTicketEx.lbSubtitle.Text = "等待人数：" + sCount + "人";
                                    btnTicketEx.lbSubtitle.Font = new Font(btnModel.sSubtitleFtFamily, btnModel.iSubtitleFtSize, fsStyle);
                                    btnTicketEx.lbSubtitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sSubtitleFtColor));
                                }
                                else
                                {
                                    btnTicketEx.lbSubtitle.Text = "";
                                }
                            }
                            else
                            {
                                btnTicketEx.lbTitle.Text = "暂停服务";
                                btnTicketEx.lbSubtitle.Text = "";
                                //btnTicketEx.BackgroundImage = ImageConvert.FromBaseString(IUserContext.OnExecuteCommand_Xp("getImageFrom", new string[] { btnModel.sButtonBgImage2 }));
                                btnTicketEx.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void myButton_Click(object sender, EventArgs e)
        {
            try
            {
                Control button = (Control)sender;
                string serviceNo = button.Tag.ToString();

                txtTicketStyle.Clear();

                string newTicketNo = "";
                string parameters = "";
                string name = "";
                string phone = "";
                string cardNo = "";
                string strResult = "";
                ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(serviceNo));

                string sPFlowNo = IUserContext.OnExecuteCommand_Xp("doAddNewTicket", new string[] { serviceNo, newTicketNo, parameters, name, phone, cardNo, strResult, DateTime.Now.ToString("yyyy-MM-dd") });

                if (!string.IsNullOrEmpty(sPFlowNo))
                {

                    string s = getTicketStyle_FormatStr(info.sTicketStyleNo);
                    if (!string.IsNullOrEmpty(s))
                    {
                        s = IPublicHelper.ReplaceVariables(s, sPFlowNo);
                        txtTicketStyle.Rtf = s;
                    }


                    if (printQrcode)
                    {
                        if (!string.IsNullOrEmpty(QrcodeText))
                        {
                            string stemp = QrcodeText.Replace("[TicketNo]", sPFlowNo);
                            Bitmap bt = QrCodeHelper.EncodeQrImage(stemp, 200, 200);
                            RichtextUtils.InsertImage(this.txtTicketStyle, bt);
                        }
                    }

                    this.pdTicket.PrinterSettings.PrinterName = IPublicHelper.Get_PrinterName();
                    this.pdTicket.Print();

                    //PrintingDialog prtDlg = new PrintingDialog();
                    //prtDlg.Show(); 
                    Refresh_ServiceButtons();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("操作出错!详细信息：" + ex.Message);
            }
        }

        private string getTicketStyle_FormatStr(string sStyleNo)
        {
            string sResult = "";
            string s = IUserContext.OnExecuteCommand_Xp("getTicketStyle", new string[] { sStyleNo });
            TicketStyle info = JsonConvert.DeserializeObject<TicketStyle>(s);

            if (info != null)
            {
                //bResult = System.Text.Encoding.Default.GetBytes(info.sTicketFormat);
                sResult = info.sTicketFormat;
            }

            return sResult;
        }
         
        private void pdTicket_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            iCheckPrint = txtTicketStyle.Print(iCheckPrint, txtTicketStyle.TextLength, e);
            //Check for more pages
            if (iCheckPrint < txtTicketStyle.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void pdTicket_BeginPrint(object sender, PrintEventArgs e)
        {
            this.iCheckPrint = 0;
            printFont = txtTicketStyle.Font;//打印使用的字体 
            streamToPrint = new StringReader(txtTicketStyle.Text);//打印richTextBox1.Text 

            //如预览文件改为：
            //streamToPrint=new StreamReader("d:\\new.rtf");
        }

        private void pdTicket_EndPrint(object sender, PrintEventArgs e)
        {
            if (streamToPrint != null) streamToPrint.Close();//释放不用的资源
        }

        private void frmSmartForm_LocationChanged(object sender, EventArgs e)
        {
            this.mStopAnhor();
        }

        private void pnlContainer_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void pnlContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        private void pnlContainer_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                myContextMenu.Show(this, e.Location);
            }
        }

        private void frmSmartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            { 
                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmLoginForm login = new frmLoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                ContextDialog dlg = new ContextDialog();
                if (dlg.ShowDialog() == DialogResult.Yes)
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                }
            }
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Refresh_ServiceButtons();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnWinSetting_Click(object sender, EventArgs e)
        {
            WindowDialog dlg = new WindowDialog();
            dlg.Show();
        }
    }
}