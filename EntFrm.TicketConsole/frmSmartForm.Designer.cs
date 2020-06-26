using EntFrm.Framework.Utility;

namespace EntFrm.TicketConsole
{
    partial class frmSmartForm
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.txtTicketStyle = new RichTextBoxEx();
            this.myContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.btnWinSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContainer.SuspendLayout();
            this.myContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Controls.Add(this.txtTicketStyle);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(376, 378);
            this.pnlContainer.TabIndex = 0;
            this.pnlContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlContainer_MouseDown);
            this.pnlContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlContainer_MouseMove);
            this.pnlContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlContainer_MouseUp);
            // 
            // txtTicketStyle
            // 
            this.txtTicketStyle.Location = new System.Drawing.Point(12, 12);
            this.txtTicketStyle.Name = "txtTicketStyle";
            this.txtTicketStyle.Size = new System.Drawing.Size(100, 32);
            this.txtTicketStyle.TabIndex = 1;
            this.txtTicketStyle.Text = "";
            // 
            // myContextMenu
            // 
            this.myContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.myContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWinSetting,
            this.btnSetting,
            this.btnQuit});
            this.myContextMenu.Name = "myContextMenu";
            this.myContextMenu.Size = new System.Drawing.Size(176, 104);
            // 
            // btnSetting
            // 
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(175, 24);
            this.btnSetting.Text = "程序设置";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(175, 24);
            this.btnQuit.Text = "退出程序";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // myTimer
            // 
            this.myTimer.Interval = 5000;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // btnWinSetting
            // 
            this.btnWinSetting.Name = "btnWinSetting";
            this.btnWinSetting.Size = new System.Drawing.Size(175, 24);
            this.btnWinSetting.Text = "窗口设置";
            this.btnWinSetting.Click += new System.EventHandler(this.btnWinSetting_Click);
            // 
            // frmSmartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 378);
            this.Controls.Add(this.pnlContainer);
            this.Name = "frmSmartForm";
            this.Text = "frmSmartForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSmartForm_FormClosing);
            this.Load += new System.EventHandler(this.frmSmartForm_Load);
            this.LocationChanged += new System.EventHandler(this.frmSmartForm_LocationChanged);
            this.pnlContainer.ResumeLayout(false);
            this.myContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private RichTextBoxEx txtTicketStyle;
        private System.Windows.Forms.ContextMenuStrip myContextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnSetting;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.ToolStripMenuItem btnWinSetting;
    }
}