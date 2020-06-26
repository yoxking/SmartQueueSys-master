namespace EntFrm.TicketConsole
{
    partial class ScanCardDialog
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnInputDlg = new System.Windows.Forms.Button();
            this.lbTimeStr = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnInputDlg);
            this.pnlContainer.Controls.Add(this.lbTimeStr);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(582, 353);
            this.pnlContainer.TabIndex = 6;
            // 
            // btnInputDlg
            // 
            this.btnInputDlg.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInputDlg.Location = new System.Drawing.Point(207, 294);
            this.btnInputDlg.Name = "btnInputDlg";
            this.btnInputDlg.Size = new System.Drawing.Size(182, 50);
            this.btnInputDlg.TabIndex = 7;
            this.btnInputDlg.Text = "手动输入";
            this.btnInputDlg.UseVisualStyleBackColor = true;
            this.btnInputDlg.Click += new System.EventHandler(this.btnInputDlg_Click);
            // 
            // lbTimeStr
            // 
            this.lbTimeStr.AutoSize = true;
            this.lbTimeStr.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeStr.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTimeStr.ForeColor = System.Drawing.Color.Red;
            this.lbTimeStr.Location = new System.Drawing.Point(259, 255);
            this.lbTimeStr.Name = "lbTimeStr";
            this.lbTimeStr.Size = new System.Drawing.Size(73, 30);
            this.lbTimeStr.TabIndex = 5;
            this.lbTimeStr.Text = "15秒";
            // 
            // ScanCardDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.pnlContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanCardDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请刷身份证";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanCardDialog_FormClosing);
            this.Load += new System.EventHandler(this.ScanCardDialog_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTimeStr;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnInputDlg;
    }
}