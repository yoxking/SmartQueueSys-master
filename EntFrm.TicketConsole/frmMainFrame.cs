using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class frmMainFrame : Form
    {
        private int clickTimes ;
        private DateTime lastTime;
        private string parentNo;
        private int gohomeTimes;
        private bool printQrcode;
        private string qrcodeText; 

        #region 
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
         
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED    

                if (this.IsXpOr2003 == true)
                {
                    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED  
                    this.Opacity = 1;
                }

                return cp;

            }

        }  //防止闪烁  

        private Boolean IsXpOr2003
        {
            get
            {
                OperatingSystem os = Environment.OSVersion;
                Version vs = os.Version;

                if (os.Platform == PlatformID.Win32NT)
                    if ((vs.Major == 5) && (vs.Minor != 0))
                        return true;
                    else
                        return false;
                else
                    return false;
            }
        }  

        public frmMainFrame()
        {  
            InitializeComponent();
             
            ////  TODO:  在  InitComponent  调用后添加任何初始化 
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            ////开启双缓冲
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            ////this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void frmMainFrame_Load(object sender, EventArgs e)
        {
            try
            {
                clickTimes = 0;
                gohomeTimes = 1;
                parentNo = "00000000";
                lastTime = DateTime.Now;
                 
                myTimer.Start();
                ScheduleService.CreateInstance().StartSchedule(); 
                
                printQrcode = IPublicHelper.Get_QrcodeMode().Equals("True");
                qrcodeText = IPublicHelper.Get_QrcodeText();

                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                this.txtTicketStyle.Visible = false;
                //this.TopMost = true; 

                Init_BackgroundImage(); 
                Init_ServiceButtons();
                Init_CustomButtons();
                Init_PrintDocument();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                gohomeTimes++;
                if (gohomeTimes > 2)
                {
                    gohomeTimes = 0;

                    if (!parentNo.Equals("00000000"))
                    {
                        parentNo = "00000000";
                        Init_ServiceButtons();
                        Init_CustomButtons();
                    }
                }

                Refresh_ServiceButtons(); 
            }
            catch (Exception ex)
            {

            }
        }

        #region 小票打印
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
        #endregion
        private void Init_BackgroundImage()
        {
            try
            {
                timerDisplay.Location = new Point(this.Width - 500, 0);
                timerDisplay.Width = 500;

                Image bgImage = Image.FromFile(IPublicHelper.BackgroundImage);
                if (IPublicHelper.BgAutoStretch == 1)
                {
                    myContainer.BackgroundImageLayout = ImageLayout.Stretch;
                }
                myContainer.BackgroundImage = bgImage;
            }
            catch(Exception ex)
            { }
        }

        private void Init_ServiceButtons()
        {
            try
            {
                TicketButton btnModel = null;
                TicketButtonEx myButton = null;
                FontStyle fsStyle = FontStyle.Regular;
                this.pnlContainer.Controls.Clear();

                if (IPublicHelper.serviceList != null && IPublicHelper.serviceList.Count > 0)
                {
                    foreach (ServiceInfo info in IPublicHelper.serviceList)
                    {
                        if (info.sParentNo.Equals(parentNo))
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
                                myButton.Size = new Size(btnModel.iButtonWidth, btnModel.iButtonHeight);
                                myButton.Location = new Point(btnModel.iButtonLeft, btnModel.iButtonTop);
                                myButton.BackColor = Color.FromArgb(int.Parse(btnModel.sButtonBgColor));
                                if (!string.IsNullOrEmpty(btnModel.sButtonBgImage1))
                                {
                                    myButton.BackgroundImage = ImageConvert.FromBaseString(IUserContext.OnExecuteCommand_Xp("getImageFrom", new string[] { btnModel.sButtonBgImage1 }));
                                    myButton.BackgroundImageLayout = ImageLayout.Stretch;
                                }

                                myButton.lbTitle.Text = "";
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
                                     
                                    myButton.lbSubtitle.Text = "等待人数：0人";
                                    myButton.lbSubtitle.Font = new Font(btnModel.sSubtitleFtFamily, btnModel.iSubtitleFtSize, fsStyle);
                                    myButton.lbSubtitle.ForeColor = Color.FromArgb(int.Parse(btnModel.sSubtitleFtColor));
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

        //添加自定义的按钮
        private void Init_CustomButtons()
        {
            if (!parentNo.Equals("00000000"))
            {
                //返回首页按钮
                Button btnGohome = new Button();
                btnGohome.Tag = "CustomButton";
                btnGohome.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                btnGohome.ForeColor = System.Drawing.Color.Black;
                btnGohome.BackColor = Color.White;
                btnGohome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnGohome.Location = new System.Drawing.Point(this.Width - 300, 60);
                btnGohome.Name = "btnGohome";
                btnGohome.Size = new System.Drawing.Size(210, 70);
                btnGohome.TabIndex = 0;
                btnGohome.Text = "返回首页";
                btnGohome.UseVisualStyleBackColor = false;
                btnGohome.Click += new System.EventHandler(this.btnGohome_Click);
                this.pnlContainer.Controls.Add(btnGohome);
            }

            //预约扫码报到按钮
            if (IPublicHelper.RegisteModel.Equals("ScanRegiste"))
            {
                //返回首页按钮
                Button btnGohome = new Button();
                btnGohome.Tag = "CustomButton";
                btnGohome.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                btnGohome.ForeColor = System.Drawing.Color.Black;
                btnGohome.BackColor = Color.White;
                btnGohome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnGohome.Location = new System.Drawing.Point(this.Width - 600, 60);
                btnGohome.Name = "ScanRegiste";
                btnGohome.Size = new System.Drawing.Size(210, 70);
                btnGohome.TabIndex = 0;
                btnGohome.Text = "预约刷卡报到";
                btnGohome.UseVisualStyleBackColor = false;
                btnGohome.Click += new System.EventHandler(this.btnRegiste_Click);
                this.pnlContainer.Controls.Add(btnGohome);
            }

        }

        private void Refresh_ServiceButtons()
        {
            //刷新等候人数
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (this.pnlContainer.Controls.Count > 0)
                    {
                        TicketButtonEx btnTicketEx = null;
                        TicketButton btnModel = null;
                        string sCount = "0";

                        foreach (Control myctrl in this.pnlContainer.Controls)
                        {
                            //是否自定义按钮
                            if (myctrl.Tag.Equals("CustomButton"))
                            {
                                continue;
                            }

                            btnTicketEx = (TicketButtonEx)myctrl;
                            if (CommonService.CreateInstance().IsActiveService(btnTicketEx.Tag.ToString()))
                            {
                                btnTicketEx.Enabled = true;
                                ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(btnTicketEx.Tag.ToString()));

                                btnTicketEx.lbTitle.Text = info.sServiceName;
                                btnModel = JsonConvert.DeserializeObject<TicketButton>(info.sTicketButtonFmt);
                                if (info.iHaveChild == 0 && btnModel.bIsShowSubtitle)
                                {
                                    sCount = CommonService.CreateInstance().GetServiceCount(info.sServiceNo);

                                    btnTicketEx.lbSubtitle.Text = "等待人数：" + sCount + "人";
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
                catch (Exception ex)
                { }
            });
            
        }
        
        private void myButton_Click(object sender, EventArgs e)
        {
            try
            {
                Control button = (Control)sender;
                string serviceNo = button.Tag.ToString();

                if (IPublicHelper.serviceList.Exists(p => p.sParentNo.Equals(serviceNo)))
                {
                    parentNo = serviceNo;
                    Init_ServiceButtons();
                    Init_CustomButtons();
                }
                else
                {
                    txtTicketStyle.Clear();

                    string ticketNo = "";
                    string ruserData = "";
                    ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(serviceNo));

                    if (InputDlgService.CreateInstance().GetRUserDataByInput(serviceNo, ref ticketNo,ref ruserData))
                    {
                        string sPFlowNo = IUserContext.OnExecuteCommand_Xp("doAddNewTicket1", new string[] { ticketNo, serviceNo, ruserData, DateTime.Now.ToString("yyyy-MM-dd") });

                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            parentNo = info.sParentNo;
                            Refresh_ServiceButtons();

                            //////////////////////////////////
                            //打印小票
                            string s = CommonService.CreateInstance().GetTicketStyle_FormatStr(info.sTicketStyleNo);
                            if (!string.IsNullOrEmpty(s))
                            {
                                s = IPublicHelper.ReplaceVariables(s, sPFlowNo);
                                txtTicketStyle.Rtf = s;
                            }

                            if (printQrcode)
                            {
                                if (!string.IsNullOrEmpty(qrcodeText))
                                {
                                    string stemp = qrcodeText.Replace("[TicketNo]", sPFlowNo);
                                    Bitmap bt = QrCodeHelper.EncodeQrImage(stemp, 200, 200);
                                    RichtextUtils.InsertImage(this.txtTicketStyle, bt);
                                }
                            }

                            this.pdTicket.PrinterSettings.PrinterName = IPublicHelper.Get_PrinterName();
                            this.pdTicket.Print();
                            //////////////////////////////////

                            PrintingDialog prtDlg = new PrintingDialog();
                            prtDlg.Show();
                        }
                    }
                }

                gohomeTimes = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("操作出错!详细信息：" + ex.Message);
            }
        } 
         
        private void btnGohome_Click(object sender, EventArgs e)
        {
            parentNo = "00000000";
            Init_ServiceButtons();
            Init_CustomButtons();
        }

        private void btnRegiste_Click(object sender, EventArgs e)
        {
            try
            {
                ScanCardDialog dlg = new ScanCardDialog();
                dlg.bInputFlag = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dlg.sStrInput))
                    {
                        IdCardModel card = new IdCardModel(dlg.sStrInput);
                        if (card != null)
                        {
                            string[] result = IUserContext.OnExecuteCommand_Xp("doEnqueueRegUser", new string[] { card.sIdCardNo }).Split(';');

                            string pflowNo = result[0];
                            string serviceNo = result[1];
                            ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(serviceNo));

                            if (info!=null&&!string.IsNullOrEmpty(pflowNo))
                            {
                                //////////////////////////////////
                                //打印小票
                                string s = CommonService.CreateInstance().GetTicketStyle_FormatStr(info.sTicketStyleNo);
                                if (!string.IsNullOrEmpty(s))
                                {
                                    s = IPublicHelper.ReplaceVariables(s, pflowNo);
                                    txtTicketStyle.Rtf = s;
                                }

                                if (printQrcode)
                                {
                                    if (!string.IsNullOrEmpty(qrcodeText))
                                    {
                                        string stemp = qrcodeText.Replace("[TicketNo]", pflowNo);
                                        Bitmap bt = QrCodeHelper.EncodeQrImage(stemp, 200, 200);
                                        RichtextUtils.InsertImage(this.txtTicketStyle, bt);
                                    }
                                }

                                this.pdTicket.PrinterSettings.PrinterName = IPublicHelper.Get_PrinterName();
                                this.pdTicket.Print();
                                //////////////////////////////////

                                PrintingDialog prtDlg = new PrintingDialog();
                                prtDlg.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("刷卡失败!");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("刷卡失败,"+ ex.Message);
            }
        }

        private void pnlBottom_Click(object sender, EventArgs e)
        {
            TimeSpan  clickInterval = DateTime.Now - lastTime;

            if(clickInterval.Seconds<4)
            {
                clickTimes++;
            }
            else
            {
                clickTimes = 0;
            }

            if(clickTimes>2)
            {
                clickTimes = 0;

                 frmLoginForm login = new frmLoginForm();
                 if (login.ShowDialog() == DialogResult.OK)
                 {
                     ContextDialog dlg = new ContextDialog();
                     if(dlg.ShowDialog()==DialogResult.Yes)
                     {
                         this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                     }
                 }
            }

            lastTime = DateTime.Now;

        }
        
        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}
