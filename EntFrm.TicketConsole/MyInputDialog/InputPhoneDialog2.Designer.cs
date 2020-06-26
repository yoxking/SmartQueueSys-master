namespace EntFrm.TicketConsole
{
    partial class MyIdCardDialog
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
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtTelphone = new MetroFramework.Controls.MetroTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNum7 = new MetroFramework.Controls.MetroButton();
            this.btnBackspace = new MetroFramework.Controls.MetroButton();
            this.btnNum6 = new MetroFramework.Controls.MetroButton();
            this.btnNum0 = new MetroFramework.Controls.MetroButton();
            this.btnNum4 = new MetroFramework.Controls.MetroButton();
            this.btnNum8 = new MetroFramework.Controls.MetroButton();
            this.btnNumX = new MetroFramework.Controls.MetroButton();
            this.btnNum2 = new MetroFramework.Controls.MetroButton();
            this.btnNum9 = new MetroFramework.Controls.MetroButton();
            this.btnNum5 = new MetroFramework.Controls.MetroButton();
            this.btnNum3 = new MetroFramework.Controls.MetroButton();
            this.btnNum1 = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUName = new MetroFramework.Controls.MetroTextBox();
            this.txtIdCard = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.metroButton1.Location = new System.Drawing.Point(226, 13);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(106, 46);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "确定";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroButton2);
            this.panel1.Controls.Add(this.metroButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 443);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 72);
            this.panel1.TabIndex = 9;
            // 
            // metroButton2
            // 
            this.metroButton2.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.metroButton2.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroButton2.Location = new System.Drawing.Point(354, 13);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(106, 46);
            this.metroButton2.TabIndex = 1;
            this.metroButton2.Text = "取消";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(37, 170);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(164, 25);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "请输入电话号码：";
            // 
            // txtTelphone
            // 
            this.txtTelphone.BackColor = System.Drawing.Color.White;
            this.txtTelphone.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtTelphone.Lines = new string[0];
            this.txtTelphone.Location = new System.Drawing.Point(255, 165);
            this.txtTelphone.MaxLength = 32767;
            this.txtTelphone.Name = "txtTelphone";
            this.txtTelphone.PasswordChar = '\0';
            this.txtTelphone.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelphone.SelectedText = "";
            this.txtTelphone.Size = new System.Drawing.Size(328, 40);
            this.txtTelphone.TabIndex = 3;
            this.txtTelphone.UseSelectable = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.btnNum7);
            this.panel3.Controls.Add(this.btnBackspace);
            this.panel3.Controls.Add(this.btnNum6);
            this.panel3.Controls.Add(this.btnNum0);
            this.panel3.Controls.Add(this.btnNum4);
            this.panel3.Controls.Add(this.btnNum8);
            this.panel3.Controls.Add(this.btnNumX);
            this.panel3.Controls.Add(this.btnNum2);
            this.panel3.Controls.Add(this.btnNum9);
            this.panel3.Controls.Add(this.btnNum5);
            this.panel3.Controls.Add(this.btnNum3);
            this.panel3.Controls.Add(this.btnNum1);
            this.panel3.Controls.Add(this.metroLabel1);
            this.panel3.Controls.Add(this.txtTelphone);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(20, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(630, 455);
            this.panel3.TabIndex = 10;
            // 
            // btnNum7
            // 
            this.btnNum7.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum7.Location = new System.Drawing.Point(532, 250);
            this.btnNum7.Name = "btnNum7";
            this.btnNum7.Size = new System.Drawing.Size(60, 46);
            this.btnNum7.TabIndex = 18;
            this.btnNum7.Text = "7";
            this.btnNum7.UseSelectable = true;
            this.btnNum7.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnBackspace
            // 
            this.btnBackspace.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnBackspace.Location = new System.Drawing.Point(454, 314);
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.Size = new System.Drawing.Size(60, 46);
            this.btnBackspace.TabIndex = 19;
            this.btnBackspace.Text = "退格";
            this.btnBackspace.UseSelectable = true;
            this.btnBackspace.Click += new System.EventHandler(this.btnBackspace_Click);
            // 
            // btnNum6
            // 
            this.btnNum6.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum6.Location = new System.Drawing.Point(454, 250);
            this.btnNum6.Name = "btnNum6";
            this.btnNum6.Size = new System.Drawing.Size(60, 46);
            this.btnNum6.TabIndex = 20;
            this.btnNum6.Text = "6";
            this.btnNum6.UseSelectable = true;
            this.btnNum6.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum0
            // 
            this.btnNum0.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum0.Location = new System.Drawing.Point(284, 314);
            this.btnNum0.Name = "btnNum0";
            this.btnNum0.Size = new System.Drawing.Size(60, 46);
            this.btnNum0.TabIndex = 21;
            this.btnNum0.Text = "0";
            this.btnNum0.UseSelectable = true;
            this.btnNum0.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum4
            // 
            this.btnNum4.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum4.Location = new System.Drawing.Point(284, 250);
            this.btnNum4.Name = "btnNum4";
            this.btnNum4.Size = new System.Drawing.Size(60, 46);
            this.btnNum4.TabIndex = 22;
            this.btnNum4.Text = "4";
            this.btnNum4.UseSelectable = true;
            this.btnNum4.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum8
            // 
            this.btnNum8.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum8.Location = new System.Drawing.Point(116, 314);
            this.btnNum8.Name = "btnNum8";
            this.btnNum8.Size = new System.Drawing.Size(60, 46);
            this.btnNum8.TabIndex = 23;
            this.btnNum8.Text = "8";
            this.btnNum8.UseSelectable = true;
            this.btnNum8.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNumX
            // 
            this.btnNumX.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNumX.Location = new System.Drawing.Point(372, 314);
            this.btnNumX.Name = "btnNumX";
            this.btnNumX.Size = new System.Drawing.Size(60, 46);
            this.btnNumX.TabIndex = 24;
            this.btnNumX.Text = "X";
            this.btnNumX.UseSelectable = true;
            this.btnNumX.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum2
            // 
            this.btnNum2.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum2.Location = new System.Drawing.Point(116, 250);
            this.btnNum2.Name = "btnNum2";
            this.btnNum2.Size = new System.Drawing.Size(60, 46);
            this.btnNum2.TabIndex = 25;
            this.btnNum2.Text = "2";
            this.btnNum2.UseSelectable = true;
            this.btnNum2.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum9
            // 
            this.btnNum9.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum9.Location = new System.Drawing.Point(202, 314);
            this.btnNum9.Name = "btnNum9";
            this.btnNum9.Size = new System.Drawing.Size(60, 46);
            this.btnNum9.TabIndex = 26;
            this.btnNum9.Text = "9";
            this.btnNum9.UseSelectable = true;
            this.btnNum9.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum5
            // 
            this.btnNum5.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum5.Location = new System.Drawing.Point(372, 250);
            this.btnNum5.Name = "btnNum5";
            this.btnNum5.Size = new System.Drawing.Size(60, 46);
            this.btnNum5.TabIndex = 27;
            this.btnNum5.Text = "5";
            this.btnNum5.UseSelectable = true;
            this.btnNum5.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum3
            // 
            this.btnNum3.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum3.Location = new System.Drawing.Point(202, 250);
            this.btnNum3.Name = "btnNum3";
            this.btnNum3.Size = new System.Drawing.Size(60, 46);
            this.btnNum3.TabIndex = 28;
            this.btnNum3.Text = "3";
            this.btnNum3.UseSelectable = true;
            this.btnNum3.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnNum1
            // 
            this.btnNum1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNum1.Location = new System.Drawing.Point(34, 250);
            this.btnNum1.Name = "btnNum1";
            this.btnNum1.Size = new System.Drawing.Size(60, 46);
            this.btnNum1.TabIndex = 29;
            this.btnNum1.Text = "1";
            this.btnNum1.UseSelectable = true;
            this.btnNum1.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(34, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(549, 1);
            this.label1.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtUName);
            this.panel4.Controls.Add(this.txtIdCard);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(630, 140);
            this.panel4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(41, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "姓      名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(41, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "请刷身份证";
            // 
            // txtUName
            // 
            this.txtUName.BackColor = System.Drawing.Color.White;
            this.txtUName.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUName.Lines = new string[0];
            this.txtUName.Location = new System.Drawing.Point(255, 80);
            this.txtUName.MaxLength = 32767;
            this.txtUName.Name = "txtUName";
            this.txtUName.PasswordChar = '\0';
            this.txtUName.ReadOnly = true;
            this.txtUName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUName.SelectedText = "";
            this.txtUName.Size = new System.Drawing.Size(328, 40);
            this.txtUName.TabIndex = 3;
            this.txtUName.UseSelectable = true;
            // 
            // txtIdCard
            // 
            this.txtIdCard.BackColor = System.Drawing.Color.White;
            this.txtIdCard.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtIdCard.Lines = new string[0];
            this.txtIdCard.Location = new System.Drawing.Point(255, 21);
            this.txtIdCard.MaxLength = 32767;
            this.txtIdCard.Name = "txtIdCard";
            this.txtIdCard.PasswordChar = '\0';
            this.txtIdCard.ReadOnly = true;
            this.txtIdCard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIdCard.SelectedText = "";
            this.txtIdCard.Size = new System.Drawing.Size(328, 40);
            this.txtIdCard.TabIndex = 3;
            this.txtIdCard.UseSelectable = true;
            // 
            // MyIdCardDialog
            // 
            this.ClientSize = new System.Drawing.Size(670, 535);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyIdCardDialog";
            this.Text = "请刷身份证";
            this.Load += new System.EventHandler(this.MyIdCardDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtTelphone;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtIdCard;
        private MetroFramework.Controls.MetroButton btnNum7;
        private MetroFramework.Controls.MetroButton btnBackspace;
        private MetroFramework.Controls.MetroButton btnNum6;
        private MetroFramework.Controls.MetroButton btnNum0;
        private MetroFramework.Controls.MetroButton btnNum4;
        private MetroFramework.Controls.MetroButton btnNum8;
        private MetroFramework.Controls.MetroButton btnNumX;
        private MetroFramework.Controls.MetroButton btnNum2;
        private MetroFramework.Controls.MetroButton btnNum9;
        private MetroFramework.Controls.MetroButton btnNum5;
        private MetroFramework.Controls.MetroButton btnNum3;
        private MetroFramework.Controls.MetroButton btnNum1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroTextBox txtUName;
    }
}