namespace EntFrm.TicketConsole
{
    partial class frmLoadForm
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
            this.myProgress = new System.Windows.Forms.ProgressBar();
            this.lbMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // myProgress
            // 
            this.myProgress.Location = new System.Drawing.Point(45, 61);
            this.myProgress.Name = "myProgress";
            this.myProgress.Size = new System.Drawing.Size(517, 23);
            this.myProgress.TabIndex = 0;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMessage.Location = new System.Drawing.Point(46, 110);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(298, 24);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.Text = "正在连接主机服务中......";
            this.lbMessage.Click += new System.EventHandler(this.lbMessage_Click);
            // 
            // frmLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 189);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.myProgress);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在加载中...";
            this.Load += new System.EventHandler(this.frmLoadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar myProgress;
        private System.Windows.Forms.Label lbMessage;
    }
}