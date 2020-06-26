namespace EntFrm.TicketConsole
{
    partial class HandInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandInputDialog));
            this.txtTelphone = new System.Windows.Forms.TextBox();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.axETFHand1 = new AxETFHandProj1.AxETFHand();
            this.btnReset = new MetroFramework.Controls.MetroButton();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnDelete = new MetroFramework.Controls.MetroButton();
            this.btnKeyX = new MetroFramework.Controls.MetroButton();
            this.btnKey8 = new MetroFramework.Controls.MetroButton();
            this.btnKey4 = new MetroFramework.Controls.MetroButton();
            this.btnKeyPoint = new MetroFramework.Controls.MetroButton();
            this.btnKey7 = new MetroFramework.Controls.MetroButton();
            this.btnKey3 = new MetroFramework.Controls.MetroButton();
            this.btnKey0 = new MetroFramework.Controls.MetroButton();
            this.btnKey6 = new MetroFramework.Controls.MetroButton();
            this.btnKey2 = new MetroFramework.Controls.MetroButton();
            this.btnKeyLine = new MetroFramework.Controls.MetroButton();
            this.btnKey9 = new MetroFramework.Controls.MetroButton();
            this.btnKey5 = new MetroFramework.Controls.MetroButton();
            this.btnKey1 = new MetroFramework.Controls.MetroButton();
            this.txtCnName = new System.Windows.Forms.TextBox();
            this.txtIdCard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dpFrom1 = new System.Windows.Forms.ComboBox();
            this.dpSex = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dpFrom2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.axETFHand1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTelphone
            // 
            this.txtTelphone.BackColor = System.Drawing.Color.LightGray;
            this.txtTelphone.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTelphone.ForeColor = System.Drawing.Color.Black;
            this.txtTelphone.Location = new System.Drawing.Point(269, 153);
            this.txtTelphone.Name = "txtTelphone";
            this.txtTelphone.Size = new System.Drawing.Size(643, 42);
            this.txtTelphone.TabIndex = 25;
            this.txtTelphone.Enter += new System.EventHandler(this.txtTelphone_Enter);
            // 
            // inputPanel
            // 
            this.inputPanel.Location = new System.Drawing.Point(852, 424);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(63, 251);
            this.inputPanel.TabIndex = 24;
            // 
            // axETFHand1
            // 
            this.axETFHand1.Location = new System.Drawing.Point(439, 375);
            this.axETFHand1.Name = "axETFHand1";
            this.axETFHand1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axETFHand1.OcxState")));
            this.axETFHand1.Size = new System.Drawing.Size(75, 23);
            this.axETFHand1.TabIndex = 23;
            this.axETFHand1.OnOutText += new System.EventHandler(this.axHand_OnOutText);
            // 
            // btnReset
            // 
            this.btnReset.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnReset.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnReset.Location = new System.Drawing.Point(852, 375);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 42);
            this.btnReset.TabIndex = 22;
            this.btnReset.Text = "重写";
            this.btnReset.UseSelectable = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOK
            // 
            this.btnOK.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnOK.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnOK.Location = new System.Drawing.Point(536, 722);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(138, 50);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnCancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnCancel.Location = new System.Drawing.Point(376, 722);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 50);
            this.btnCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnDelete.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnDelete.Location = new System.Drawing.Point(271, 617);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(138, 60);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseSelectable = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnKeyX
            // 
            this.btnKeyX.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKeyX.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKeyX.Location = new System.Drawing.Point(349, 537);
            this.btnKeyX.Name = "btnKeyX";
            this.btnKeyX.Size = new System.Drawing.Size(60, 60);
            this.btnKeyX.TabIndex = 17;
            this.btnKeyX.Text = "X";
            this.btnKeyX.UseSelectable = true;
            this.btnKeyX.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey8
            // 
            this.btnKey8.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey8.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey8.Location = new System.Drawing.Point(349, 456);
            this.btnKey8.Name = "btnKey8";
            this.btnKey8.Size = new System.Drawing.Size(60, 60);
            this.btnKey8.TabIndex = 16;
            this.btnKey8.Text = "8";
            this.btnKey8.UseSelectable = true;
            this.btnKey8.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey4
            // 
            this.btnKey4.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey4.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey4.Location = new System.Drawing.Point(349, 375);
            this.btnKey4.Name = "btnKey4";
            this.btnKey4.Size = new System.Drawing.Size(60, 60);
            this.btnKey4.TabIndex = 6;
            this.btnKey4.Text = "4";
            this.btnKey4.UseSelectable = true;
            this.btnKey4.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKeyPoint
            // 
            this.btnKeyPoint.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKeyPoint.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKeyPoint.Location = new System.Drawing.Point(271, 537);
            this.btnKeyPoint.Name = "btnKeyPoint";
            this.btnKeyPoint.Size = new System.Drawing.Size(60, 60);
            this.btnKeyPoint.TabIndex = 14;
            this.btnKeyPoint.Text = ".";
            this.btnKeyPoint.UseSelectable = true;
            this.btnKeyPoint.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey7
            // 
            this.btnKey7.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey7.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey7.Location = new System.Drawing.Point(271, 456);
            this.btnKey7.Name = "btnKey7";
            this.btnKey7.Size = new System.Drawing.Size(60, 60);
            this.btnKey7.TabIndex = 13;
            this.btnKey7.Text = "7";
            this.btnKey7.UseSelectable = true;
            this.btnKey7.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey3
            // 
            this.btnKey3.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey3.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey3.Location = new System.Drawing.Point(271, 375);
            this.btnKey3.Name = "btnKey3";
            this.btnKey3.Size = new System.Drawing.Size(60, 60);
            this.btnKey3.TabIndex = 12;
            this.btnKey3.Text = "3";
            this.btnKey3.UseSelectable = true;
            this.btnKey3.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey0
            // 
            this.btnKey0.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey0.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey0.Location = new System.Drawing.Point(193, 537);
            this.btnKey0.Name = "btnKey0";
            this.btnKey0.Size = new System.Drawing.Size(60, 60);
            this.btnKey0.TabIndex = 11;
            this.btnKey0.Text = "0";
            this.btnKey0.UseSelectable = true;
            this.btnKey0.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey6
            // 
            this.btnKey6.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey6.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey6.Location = new System.Drawing.Point(193, 456);
            this.btnKey6.Name = "btnKey6";
            this.btnKey6.Size = new System.Drawing.Size(60, 60);
            this.btnKey6.TabIndex = 10;
            this.btnKey6.Text = "6";
            this.btnKey6.UseSelectable = true;
            this.btnKey6.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey2
            // 
            this.btnKey2.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey2.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey2.Location = new System.Drawing.Point(193, 375);
            this.btnKey2.Name = "btnKey2";
            this.btnKey2.Size = new System.Drawing.Size(60, 60);
            this.btnKey2.TabIndex = 9;
            this.btnKey2.Text = "2";
            this.btnKey2.UseSelectable = true;
            this.btnKey2.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKeyLine
            // 
            this.btnKeyLine.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKeyLine.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKeyLine.Location = new System.Drawing.Point(116, 617);
            this.btnKeyLine.Name = "btnKeyLine";
            this.btnKeyLine.Size = new System.Drawing.Size(60, 60);
            this.btnKeyLine.TabIndex = 8;
            this.btnKeyLine.Text = "-";
            this.btnKeyLine.UseSelectable = true;
            this.btnKeyLine.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey9
            // 
            this.btnKey9.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey9.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey9.Location = new System.Drawing.Point(116, 537);
            this.btnKey9.Name = "btnKey9";
            this.btnKey9.Size = new System.Drawing.Size(60, 60);
            this.btnKey9.TabIndex = 7;
            this.btnKey9.Text = "9";
            this.btnKey9.UseSelectable = true;
            this.btnKey9.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey5
            // 
            this.btnKey5.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey5.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey5.Location = new System.Drawing.Point(116, 456);
            this.btnKey5.Name = "btnKey5";
            this.btnKey5.Size = new System.Drawing.Size(60, 60);
            this.btnKey5.TabIndex = 21;
            this.btnKey5.Text = "5";
            this.btnKey5.UseSelectable = true;
            this.btnKey5.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnKey1
            // 
            this.btnKey1.BackColor = System.Drawing.Color.White;
            this.btnKey1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnKey1.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnKey1.Location = new System.Drawing.Point(116, 375);
            this.btnKey1.Name = "btnKey1";
            this.btnKey1.Size = new System.Drawing.Size(60, 60);
            this.btnKey1.TabIndex = 15;
            this.btnKey1.Text = "1";
            this.btnKey1.UseSelectable = true;
            this.btnKey1.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // txtCnName
            // 
            this.txtCnName.BackColor = System.Drawing.Color.LightGray;
            this.txtCnName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCnName.ForeColor = System.Drawing.Color.Black;
            this.txtCnName.Location = new System.Drawing.Point(268, 88);
            this.txtCnName.Name = "txtCnName";
            this.txtCnName.Size = new System.Drawing.Size(437, 42);
            this.txtCnName.TabIndex = 25;
            this.txtCnName.Enter += new System.EventHandler(this.txtCnName_Enter);
            // 
            // txtIdCard
            // 
            this.txtIdCard.BackColor = System.Drawing.Color.LightGray;
            this.txtIdCard.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIdCard.ForeColor = System.Drawing.Color.Black;
            this.txtIdCard.Location = new System.Drawing.Point(268, 223);
            this.txtIdCard.Name = "txtIdCard";
            this.txtIdCard.Size = new System.Drawing.Size(644, 42);
            this.txtIdCard.TabIndex = 25;
            this.txtIdCard.Enter += new System.EventHandler(this.txtIdCard_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(101, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 27);
            this.label1.TabIndex = 26;
            this.label1.Text = "手机号码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(98, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 27);
            this.label2.TabIndex = 26;
            this.label2.Text = "姓    名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(101, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 27);
            this.label3.TabIndex = 26;
            this.label3.Text = "身份证号：";
            // 
            // dpFrom1
            // 
            this.dpFrom1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dpFrom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpFrom1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpFrom1.FormattingEnabled = true;
            this.dpFrom1.Items.AddRange(new object[] {
            "重庆主城",
            "重庆区县",
            "其他省份",
            "国外"});
            this.dpFrom1.Location = new System.Drawing.Point(268, 294);
            this.dpFrom1.Name = "dpFrom1";
            this.dpFrom1.Size = new System.Drawing.Size(243, 38);
            this.dpFrom1.TabIndex = 27;
            this.dpFrom1.SelectedIndexChanged += new System.EventHandler(this.dpFrom1_SelectedIndexChanged);
            // 
            // dpSex
            // 
            this.dpSex.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dpSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpSex.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpSex.FormattingEnabled = true;
            this.dpSex.Items.AddRange(new object[] {
            "先生",
            "女士"});
            this.dpSex.Location = new System.Drawing.Point(711, 89);
            this.dpSex.Name = "dpSex";
            this.dpSex.Size = new System.Drawing.Size(201, 38);
            this.dpSex.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(103, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 27);
            this.label4.TabIndex = 26;
            this.label4.Text = "来访区域：";
            // 
            // dpFrom2
            // 
            this.dpFrom2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dpFrom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpFrom2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dpFrom2.FormattingEnabled = true;
            this.dpFrom2.Items.AddRange(new object[] {
            "重庆渝中区",
            "重庆江北区",
            "重庆南岸区",
            "重庆九龙坡区",
            "重庆沙坪坝区",
            "重庆大渡口区",
            "重庆北碚区",
            "重庆渝北区",
            "重庆巴南区",
            "重庆区县",
            "其他省份",
            "国外"});
            this.dpFrom2.Location = new System.Drawing.Point(517, 294);
            this.dpFrom2.Name = "dpFrom2";
            this.dpFrom2.Size = new System.Drawing.Size(395, 38);
            this.dpFrom2.TabIndex = 27;
            // 
            // HandInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 800);
            this.Controls.Add(this.dpSex);
            this.Controls.Add(this.dpFrom2);
            this.Controls.Add(this.dpFrom1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdCard);
            this.Controls.Add(this.txtCnName);
            this.Controls.Add(this.txtTelphone);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.axETFHand1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnKeyX);
            this.Controls.Add(this.btnKey8);
            this.Controls.Add(this.btnKey4);
            this.Controls.Add(this.btnKeyPoint);
            this.Controls.Add(this.btnKey7);
            this.Controls.Add(this.btnKey3);
            this.Controls.Add(this.btnKey0);
            this.Controls.Add(this.btnKey6);
            this.Controls.Add(this.btnKey2);
            this.Controls.Add(this.btnKeyLine);
            this.Controls.Add(this.btnKey9);
            this.Controls.Add(this.btnKey5);
            this.Controls.Add(this.btnKey1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HandInputDialog";
            this.Text = "请输入";
            this.Load += new System.EventHandler(this.HandInputDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axETFHand1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTelphone;
        private System.Windows.Forms.Panel inputPanel;
        private AxETFHandProj1.AxETFHand axETFHand1;
        private MetroFramework.Controls.MetroButton btnReset;
        private MetroFramework.Controls.MetroButton btnOK;
        private MetroFramework.Controls.MetroButton btnCancel;
        private MetroFramework.Controls.MetroButton btnDelete;
        private MetroFramework.Controls.MetroButton btnKeyX;
        private MetroFramework.Controls.MetroButton btnKey8;
        private MetroFramework.Controls.MetroButton btnKey4;
        private MetroFramework.Controls.MetroButton btnKeyPoint;
        private MetroFramework.Controls.MetroButton btnKey7;
        private MetroFramework.Controls.MetroButton btnKey3;
        private MetroFramework.Controls.MetroButton btnKey0;
        private MetroFramework.Controls.MetroButton btnKey6;
        private MetroFramework.Controls.MetroButton btnKey2;
        private MetroFramework.Controls.MetroButton btnKeyLine;
        private MetroFramework.Controls.MetroButton btnKey9;
        private MetroFramework.Controls.MetroButton btnKey5;
        private MetroFramework.Controls.MetroButton btnKey1;
        private System.Windows.Forms.TextBox txtCnName;
        private System.Windows.Forms.TextBox txtIdCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dpFrom1;
        private System.Windows.Forms.ComboBox dpSex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dpFrom2;
    }
}