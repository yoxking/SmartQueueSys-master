using EntFrm.Framework.Utility;

namespace EntFrm.TicketConsole
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
            this.timerDisplay = new EntFrm.Framework.Utility.TimerDisplayEx();
            this.txtTicketStyle = new EntFrm.Framework.Utility.RichTextBoxEx();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.myContainer = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlBottom.SuspendLayout();
            this.myContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerDisplay
            // 
            this.timerDisplay.Location = new System.Drawing.Point(536, 0);
            this.timerDisplay.Name = "timerDisplay";
            this.timerDisplay.Size = new System.Drawing.Size(395, 66);
            this.timerDisplay.TabIndex = 1;
            // 
            // txtTicketStyle
            // 
            this.txtTicketStyle.Location = new System.Drawing.Point(122, 20);
            this.txtTicketStyle.Name = "txtTicketStyle";
            this.txtTicketStyle.Size = new System.Drawing.Size(254, 68);
            this.txtTicketStyle.TabIndex = 0;
            this.txtTicketStyle.Text = "";
            // 
            // myTimer
            // 
            this.myTimer.Interval = 5000;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottom.Controls.Add(this.txtTicketStyle);
            this.pnlBottom.Controls.Add(this.timerDisplay);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 417);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(931, 112);
            this.pnlBottom.TabIndex = 3;
            this.pnlBottom.Click += new System.EventHandler(this.pnlBottom_Click);
            // 
            // myContainer
            // 
            this.myContainer.Controls.Add(this.pnlContainer);
            this.myContainer.Controls.Add(this.pnlBottom);
            this.myContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myContainer.Location = new System.Drawing.Point(0, 0);
            this.myContainer.Name = "myContainer";
            this.myContainer.Size = new System.Drawing.Size(931, 529);
            this.myContainer.TabIndex = 5;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(931, 417);
            this.pnlContainer.TabIndex = 4;
            // 
            // frmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 529);
            this.Controls.Add(this.myContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMainFrame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainFrame_FormClosing);
            this.Load += new System.EventHandler(this.frmMainFrame_Load);
            this.pnlBottom.ResumeLayout(false);
            this.myContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private RichTextBoxEx txtTicketStyle;
        private System.Windows.Forms.Timer myTimer;
        private TimerDisplayEx timerDisplay;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel myContainer;
        private System.Windows.Forms.Panel pnlContainer;
    }
}