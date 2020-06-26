using EntFrm.Framework.Utility;

namespace EntFrm.TicketConsole
{
    partial class SettingDialog
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dpPrinters = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdFullScreen = new System.Windows.Forms.RadioButton();
            this.rdFloatScreen = new System.Windows.Forms.RadioButton();
            this.txtQrcodeStr = new System.Windows.Forms.TextBox();
            this.ckPrintQrcode = new CkGroupBoxEx();
            this.ckPrintQrcode.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(137, 302);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(142, 38);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "主机IP地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(59, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "主机端口:";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIpAddress.Location = new System.Drawing.Point(138, 27);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(304, 25);
            this.txtIpAddress.TabIndex = 2;
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.Location = new System.Drawing.Point(138, 70);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(141, 25);
            this.txtPort.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(299, 302);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 38);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dpPrinters
            // 
            this.dpPrinters.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpPrinters.FormattingEnabled = true;
            this.dpPrinters.Location = new System.Drawing.Point(137, 112);
            this.dpPrinters.Name = "dpPrinters";
            this.dpPrinters.Size = new System.Drawing.Size(304, 23);
            this.dpPrinters.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(41, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "打印机选择:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(55, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "取票模式:";
            // 
            // rdFullScreen
            // 
            this.rdFullScreen.AutoSize = true;
            this.rdFullScreen.Location = new System.Drawing.Point(137, 155);
            this.rdFullScreen.Name = "rdFullScreen";
            this.rdFullScreen.Size = new System.Drawing.Size(88, 19);
            this.rdFullScreen.TabIndex = 4;
            this.rdFullScreen.TabStop = true;
            this.rdFullScreen.Text = "全屏模式";
            this.rdFullScreen.UseVisualStyleBackColor = true;
            // 
            // rdFloatScreen
            // 
            this.rdFloatScreen.AutoSize = true;
            this.rdFloatScreen.Location = new System.Drawing.Point(318, 155);
            this.rdFloatScreen.Name = "rdFloatScreen";
            this.rdFloatScreen.Size = new System.Drawing.Size(88, 19);
            this.rdFloatScreen.TabIndex = 4;
            this.rdFloatScreen.TabStop = true;
            this.rdFloatScreen.Text = "浮动模式";
            this.rdFloatScreen.UseVisualStyleBackColor = true;
            // 
            // txtQrcodeStr
            // 
            this.txtQrcodeStr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQrcodeStr.Location = new System.Drawing.Point(14, 38);
            this.txtQrcodeStr.Name = "txtQrcodeStr";
            this.txtQrcodeStr.Size = new System.Drawing.Size(369, 25);
            this.txtQrcodeStr.TabIndex = 2;
            this.txtQrcodeStr.Text = "欢迎光临";
            // 
            // ckPrintQrcode
            // 
            this.ckPrintQrcode.Controls.Add(this.txtQrcodeStr);
            this.ckPrintQrcode.Location = new System.Drawing.Point(44, 195);
            this.ckPrintQrcode.Name = "ckPrintQrcode";
            this.ckPrintQrcode.Size = new System.Drawing.Size(397, 88);
            this.ckPrintQrcode.TabIndex = 5;
            this.ckPrintQrcode.TabStop = false;
            this.ckPrintQrcode.Text = "打印二维码";
            // 
            // SettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 363);
            this.Controls.Add(this.ckPrintQrcode);
            this.Controls.Add(this.rdFloatScreen);
            this.Controls.Add(this.rdFullScreen);
            this.Controls.Add(this.dpPrinters);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.SettingDialog_Load);
            this.ckPrintQrcode.ResumeLayout(false);
            this.ckPrintQrcode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox dpPrinters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdFullScreen;
        private System.Windows.Forms.RadioButton rdFloatScreen;
        private System.Windows.Forms.TextBox txtQrcodeStr;
        private CkGroupBoxEx ckPrintQrcode;
    }
}