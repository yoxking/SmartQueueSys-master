namespace EntFrm.SettingConsole
{
    partial class frmEvaluatorBusiness
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtLocPort = new System.Windows.Forms.TextBox();
            this.txtIpAddr = new System.Windows.Forms.TextBox();
            this.txtEvalCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.InfoList = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.EvalorNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaVCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaIpAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaLcPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 596);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetup);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtLocPort);
            this.groupBox1.Controls.Add(this.txtComments);
            this.groupBox1.Controls.Add(this.txtIpAddr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEvalCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(14, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 222);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "评价器设置";
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(426, 174);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(108, 30);
            this.btnSetup.TabIndex = 229;
            this.btnSetup.Text = "评价器设置";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(551, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 30);
            this.btnSave.TabIndex = 228;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtLocPort
            // 
            this.txtLocPort.Location = new System.Drawing.Point(551, 65);
            this.txtLocPort.Name = "txtLocPort";
            this.txtLocPort.Size = new System.Drawing.Size(126, 25);
            this.txtLocPort.TabIndex = 226;
            // 
            // txtIpAddr
            // 
            this.txtIpAddr.Location = new System.Drawing.Point(121, 65);
            this.txtIpAddr.Name = "txtIpAddr";
            this.txtIpAddr.Size = new System.Drawing.Size(332, 25);
            this.txtIpAddr.TabIndex = 226;
            // 
            // txtEvalCode
            // 
            this.txtEvalCode.Location = new System.Drawing.Point(121, 28);
            this.txtEvalCode.Name = "txtEvalCode";
            this.txtEvalCode.Size = new System.Drawing.Size(332, 25);
            this.txtEvalCode.TabIndex = 226;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(54, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 223;
            this.label3.Text = "IP地址:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(489, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 15);
            this.label10.TabIndex = 224;
            this.label10.Text = "端口:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(26, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 223;
            this.label6.Text = "评价器编码:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.InfoList);
            this.groupBox5.Controls.Add(this.btnDel);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Location = new System.Drawing.Point(10, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(707, 313);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "评价器列表";
            // 
            // InfoList
            // 
            this.InfoList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InfoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EvalorNo,
            this.EvaVCode,
            this.EvaIpAddr,
            this.EvaLcPort,
            this.Comments});
            this.InfoList.Location = new System.Drawing.Point(11, 25);
            this.InfoList.Name = "InfoList";
            this.InfoList.RowHeadersVisible = false;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.InfoList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.InfoList.RowTemplate.Height = 27;
            this.InfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoList.Size = new System.Drawing.Size(670, 240);
            this.InfoList.TabIndex = 228;
            this.InfoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoList_CellClick);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(600, 274);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(81, 30);
            this.btnDel.TabIndex = 227;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(520, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 30);
            this.btnAdd.TabIndex = 227;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // EvalorNo
            // 
            this.EvalorNo.DataPropertyName = "sEvalorNo";
            this.EvalorNo.HeaderText = "编号";
            this.EvalorNo.Name = "EvalorNo";
            this.EvalorNo.ReadOnly = true;
            this.EvalorNo.Visible = false;
            // 
            // EvaVCode
            // 
            this.EvaVCode.DataPropertyName = "sEvaVCode";
            this.EvaVCode.HeaderText = "评价器编码";
            this.EvaVCode.Name = "EvaVCode";
            this.EvaVCode.ReadOnly = true;
            this.EvaVCode.Width = 125;
            // 
            // EvaIpAddr
            // 
            this.EvaIpAddr.DataPropertyName = "sEvaIpAddr";
            this.EvaIpAddr.HeaderText = "IP地址";
            this.EvaIpAddr.Name = "EvaIpAddr";
            this.EvaIpAddr.ReadOnly = true;
            // 
            // EvaLcPort
            // 
            this.EvaLcPort.DataPropertyName = "sEvaLcPort";
            this.EvaLcPort.HeaderText = "端口";
            this.EvaLcPort.Name = "EvaLcPort";
            this.EvaLcPort.ReadOnly = true;
            // 
            // Comments
            // 
            this.Comments.DataPropertyName = "sComments";
            this.Comments.HeaderText = "备注";
            this.Comments.Name = "Comments";
            this.Comments.ReadOnly = true;
            this.Comments.Width = 300;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(54, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 223;
            this.label1.Text = "备注:";
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(121, 100);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(556, 54);
            this.txtComments.TabIndex = 226;
            // 
            // frmEvaluatorBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 596);
            this.Controls.Add(this.panel1);
            this.Name = "frmEvaluatorBusiness";
            this.Text = "frmEvaluatorBusiness";
            this.Load += new System.EventHandler(this.frmEvaluatorBusiness_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtLocPort;
        private System.Windows.Forms.TextBox txtIpAddr;
        private System.Windows.Forms.TextBox txtEvalCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView InfoList;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvalorNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaVCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaIpAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaLcPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label1;
    }
}