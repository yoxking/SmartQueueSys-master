namespace EntFrm.TicketConsole
{
    partial class ScanBcodeDialog
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
            this.lbTimeStr = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTimeStr
            // 
            this.lbTimeStr.AutoSize = true;
            this.lbTimeStr.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeStr.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTimeStr.ForeColor = System.Drawing.Color.Red;
            this.lbTimeStr.Location = new System.Drawing.Point(238, 290);
            this.lbTimeStr.Name = "lbTimeStr";
            this.lbTimeStr.Size = new System.Drawing.Size(73, 30);
            this.lbTimeStr.TabIndex = 5;
            this.lbTimeStr.Text = "15秒";
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.lbTimeStr);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(582, 353);
            this.pnlContainer.TabIndex = 7;
            // 
            // ScanBcodeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.pnlContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanBcodeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请刷条码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanBcodeDialog_FormClosing);
            this.Load += new System.EventHandler(this.ScanBcodeDialog_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTimeStr;
        private System.Windows.Forms.Panel pnlContainer;
    }
}