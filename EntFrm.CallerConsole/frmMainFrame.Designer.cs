namespace EntFrm.CallerConsole
{
    partial class frmMainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainFrame));
            this.myNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.myContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dpTicketList = new System.Windows.Forms.ComboBox();
            this.dpTransfer = new System.Windows.Forms.ComboBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnQuitCaller = new System.Windows.Forms.Button();
            this.btnCallHelper = new System.Windows.Forms.Button();
            this.btnFinishFlow = new System.Windows.Forms.Button();
            this.btnAbortFlow = new System.Windows.Forms.Button();
            this.btnDelay = new System.Windows.Forms.Button();
            this.btnNotCome = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnWelcome = new System.Windows.Forms.Button();
            this.btnCallGoto = new System.Windows.Forms.Button();
            this.btnCallRepeat = new System.Windows.Forms.Button();
            this.btnCallNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbWaiterNum = new System.Windows.Forms.Label();
            this.lbStafferName = new System.Windows.Forms.Label();
            this.lbTicketNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.myContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myNotifyIcon
            // 
            this.myNotifyIcon.ContextMenuStrip = this.myContextMenu;
            this.myNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myNotifyIcon.Icon")));
            this.myNotifyIcon.Text = "虚拟呼叫器";
            this.myNotifyIcon.Visible = true;
            this.myNotifyIcon.DoubleClick += new System.EventHandler(this.myNotifyIcon_DoubleClick);
            // 
            // myContextMenu
            // 
            this.myContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetting,
            this.toolStripSeparator1,
            this.btnShow,
            this.btnQuit});
            this.myContextMenu.Name = "myContextMenu";
            this.myContextMenu.Size = new System.Drawing.Size(154, 82);
            // 
            // btnSetting
            // 
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(153, 24);
            this.btnSetting.Text = "系统设置";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // btnShow
            // 
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(153, 24);
            this.btnShow.Text = "打开呼叫器";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(153, 24);
            this.btnQuit.Text = "退出呼叫器";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dpTicketList);
            this.panel1.Controls.Add(this.dpTransfer);
            this.panel1.Controls.Add(this.btnTransfer);
            this.panel1.Controls.Add(this.lbMessage);
            this.panel1.Controls.Add(this.btnQuitCaller);
            this.panel1.Controls.Add(this.btnCallHelper);
            this.panel1.Controls.Add(this.btnFinishFlow);
            this.panel1.Controls.Add(this.btnAbortFlow);
            this.panel1.Controls.Add(this.btnDelay);
            this.panel1.Controls.Add(this.btnNotCome);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnWelcome);
            this.panel1.Controls.Add(this.btnCallGoto);
            this.panel1.Controls.Add(this.btnCallRepeat);
            this.panel1.Controls.Add(this.btnCallNext);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbWaiterNum);
            this.panel1.Controls.Add(this.lbStafferName);
            this.panel1.Controls.Add(this.lbTicketNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 606);
            this.panel1.TabIndex = 53;
            // 
            // dpTicketList
            // 
            this.dpTicketList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpTicketList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpTicketList.FormattingEnabled = true;
            this.dpTicketList.Items.AddRange(new object[] {
            " "});
            this.dpTicketList.Location = new System.Drawing.Point(29, 171);
            this.dpTicketList.Name = "dpTicketList";
            this.dpTicketList.Size = new System.Drawing.Size(138, 25);
            this.dpTicketList.TabIndex = 30;
            this.dpTicketList.DropDown += new System.EventHandler(this.dpTicketList_DropDown);
            // 
            // dpTransfer
            // 
            this.dpTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpTransfer.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpTransfer.FormattingEnabled = true;
            this.dpTransfer.Items.AddRange(new object[] {
            "呼叫护士站",
            "器材已用完",
            "自定义消息"});
            this.dpTransfer.Location = new System.Drawing.Point(29, 217);
            this.dpTransfer.Name = "dpTransfer";
            this.dpTransfer.Size = new System.Drawing.Size(138, 25);
            this.dpTransfer.TabIndex = 30;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(179, 212);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(112, 35);
            this.btnTransfer.TabIndex = 29;
            this.btnTransfer.Text = "业务转移";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.ForeColor = System.Drawing.Color.Green;
            this.lbMessage.Location = new System.Drawing.Point(30, 575);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(106, 15);
            this.lbMessage.TabIndex = 27;
            this.lbMessage.Text = "等待呼叫中...";
            // 
            // btnQuitCaller
            // 
            this.btnQuitCaller.Location = new System.Drawing.Point(33, 525);
            this.btnQuitCaller.Name = "btnQuitCaller";
            this.btnQuitCaller.Size = new System.Drawing.Size(263, 33);
            this.btnQuitCaller.TabIndex = 24;
            this.btnQuitCaller.Text = "退出呼叫器";
            this.btnQuitCaller.UseVisualStyleBackColor = true;
            this.btnQuitCaller.Click += new System.EventHandler(this.btnQuitCaller_Click);
            // 
            // btnCallHelper
            // 
            this.btnCallHelper.Location = new System.Drawing.Point(30, 482);
            this.btnCallHelper.Name = "btnCallHelper";
            this.btnCallHelper.Size = new System.Drawing.Size(263, 33);
            this.btnCallHelper.TabIndex = 24;
            this.btnCallHelper.Text = "窗口求助";
            this.btnCallHelper.UseVisualStyleBackColor = true;
            this.btnCallHelper.Click += new System.EventHandler(this.btnCallHelper_Click);
            // 
            // btnFinishFlow
            // 
            this.btnFinishFlow.Location = new System.Drawing.Point(30, 439);
            this.btnFinishFlow.Name = "btnFinishFlow";
            this.btnFinishFlow.Size = new System.Drawing.Size(263, 33);
            this.btnFinishFlow.TabIndex = 23;
            this.btnFinishFlow.Text = "办理完成";
            this.btnFinishFlow.UseVisualStyleBackColor = true;
            this.btnFinishFlow.Click += new System.EventHandler(this.btnFinishFlow_Click);
            // 
            // btnAbortFlow
            // 
            this.btnAbortFlow.Location = new System.Drawing.Point(31, 395);
            this.btnAbortFlow.Name = "btnAbortFlow";
            this.btnAbortFlow.Size = new System.Drawing.Size(260, 33);
            this.btnAbortFlow.TabIndex = 21;
            this.btnAbortFlow.Text = "终止流程";
            this.btnAbortFlow.UseVisualStyleBackColor = true;
            this.btnAbortFlow.Click += new System.EventHandler(this.btnAbortFlow_Click);
            // 
            // btnDelay
            // 
            this.btnDelay.Location = new System.Drawing.Point(31, 308);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(260, 33);
            this.btnDelay.TabIndex = 21;
            this.btnDelay.Text = "票号滞后";
            this.btnDelay.UseVisualStyleBackColor = true;
            this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
            // 
            // btnNotCome
            // 
            this.btnNotCome.Location = new System.Drawing.Point(30, 353);
            this.btnNotCome.Name = "btnNotCome";
            this.btnNotCome.Size = new System.Drawing.Size(260, 33);
            this.btnNotCome.TabIndex = 21;
            this.btnNotCome.Text = "顾客未到";
            this.btnNotCome.UseVisualStyleBackColor = true;
            this.btnNotCome.Click += new System.EventHandler(this.btnNotCome_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(179, 259);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(112, 33);
            this.btnPause.TabIndex = 20;
            this.btnPause.Text = "暂停办理";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnWelcome
            // 
            this.btnWelcome.Location = new System.Drawing.Point(29, 259);
            this.btnWelcome.Name = "btnWelcome";
            this.btnWelcome.Size = new System.Drawing.Size(139, 33);
            this.btnWelcome.TabIndex = 20;
            this.btnWelcome.Text = "欢迎光临";
            this.btnWelcome.UseVisualStyleBackColor = true;
            this.btnWelcome.Click += new System.EventHandler(this.btnWelcome_Click);
            // 
            // btnCallGoto
            // 
            this.btnCallGoto.Location = new System.Drawing.Point(182, 166);
            this.btnCallGoto.Name = "btnCallGoto";
            this.btnCallGoto.Size = new System.Drawing.Size(112, 35);
            this.btnCallGoto.TabIndex = 15;
            this.btnCallGoto.Text = "指定呼叫";
            this.btnCallGoto.UseVisualStyleBackColor = true;
            this.btnCallGoto.Click += new System.EventHandler(this.btnCallGoto_Click);
            // 
            // btnCallRepeat
            // 
            this.btnCallRepeat.Location = new System.Drawing.Point(182, 119);
            this.btnCallRepeat.Name = "btnCallRepeat";
            this.btnCallRepeat.Size = new System.Drawing.Size(112, 35);
            this.btnCallRepeat.TabIndex = 14;
            this.btnCallRepeat.Text = "重复呼叫";
            this.btnCallRepeat.UseVisualStyleBackColor = true;
            this.btnCallRepeat.Click += new System.EventHandler(this.btnCallRepeat_Click);
            // 
            // btnCallNext
            // 
            this.btnCallNext.Location = new System.Drawing.Point(29, 119);
            this.btnCallNext.Name = "btnCallNext";
            this.btnCallNext.Size = new System.Drawing.Size(139, 35);
            this.btnCallNext.TabIndex = 13;
            this.btnCallNext.Text = "呼叫下一位";
            this.btnCallNext.UseVisualStyleBackColor = true;
            this.btnCallNext.Click += new System.EventHandler(this.btnCallNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "等候人数:";
            // 
            // lbWaiterNum
            // 
            this.lbWaiterNum.AutoSize = true;
            this.lbWaiterNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWaiterNum.ForeColor = System.Drawing.Color.Red;
            this.lbWaiterNum.Location = new System.Drawing.Point(114, 89);
            this.lbWaiterNum.Name = "lbWaiterNum";
            this.lbWaiterNum.Size = new System.Drawing.Size(16, 15);
            this.lbWaiterNum.TabIndex = 10;
            this.lbWaiterNum.Text = "0";
            // 
            // lbStafferName
            // 
            this.lbStafferName.AutoSize = true;
            this.lbStafferName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStafferName.ForeColor = System.Drawing.Color.Green;
            this.lbStafferName.Location = new System.Drawing.Point(114, 24);
            this.lbStafferName.Name = "lbStafferName";
            this.lbStafferName.Size = new System.Drawing.Size(34, 15);
            this.lbStafferName.TabIndex = 9;
            this.lbStafferName.Text = "N/A";
            // 
            // lbTicketNo
            // 
            this.lbTicketNo.AutoSize = true;
            this.lbTicketNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketNo.ForeColor = System.Drawing.Color.Red;
            this.lbTicketNo.Location = new System.Drawing.Point(114, 57);
            this.lbTicketNo.Name = "lbTicketNo";
            this.lbTicketNo.Size = new System.Drawing.Size(34, 15);
            this.lbTicketNo.TabIndex = 8;
            this.lbTicketNo.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前员工:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "当前票号:";
            // 
            // myTimer
            // 
            this.myTimer.Interval = 5000;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // frmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 606);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "虚拟呼叫器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainFrame_FormClosing);
            this.Load += new System.EventHandler(this.frmMainFrame_Load);
            this.LocationChanged += new System.EventHandler(this.frmMainFrame_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.frmMainFrame_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMainFrame_MouseUp);
            this.myContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon myNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip myContextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnShow;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnSetting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnCallHelper;
        private System.Windows.Forms.Button btnFinishFlow;
        private System.Windows.Forms.Button btnNotCome;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnWelcome;
        private System.Windows.Forms.Button btnCallGoto;
        private System.Windows.Forms.Button btnCallRepeat;
        private System.Windows.Forms.Button btnCallNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbWaiterNum;
        private System.Windows.Forms.Label lbStafferName;
        private System.Windows.Forms.Label lbTicketNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuitCaller;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.ComboBox dpTransfer;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnAbortFlow;
        private System.Windows.Forms.Button btnDelay;
        private System.Windows.Forms.ComboBox dpTicketList;
    }
}