namespace MyFormDesinger
{
    partial class MyFormDesigner
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.MyDesigner = new MyFormDesinger.DesignerControl();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MyDesigner);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 673);
            this.panel2.TabIndex = 5;
            // 
            // MyDesigner
            // 
            this.MyDesigner.BackColor = System.Drawing.Color.White;
            this.MyDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyDesigner.Location = new System.Drawing.Point(0, 0);
            this.MyDesigner.Margin = new System.Windows.Forms.Padding(5);
            this.MyDesigner.Name = "MyDesigner";
            this.MyDesigner.Size = new System.Drawing.Size(813, 673);
            this.MyDesigner.TabIndex = 3;
            // 
            // MyFormDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 673);
            this.CloseButton = false;
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyFormDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "界面设计器";
            this.Text = "界面设计器";
            this.Load += new System.EventHandler(this.MyFormDesigner_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DesignerControl MyDesigner;
        private System.Windows.Forms.Panel panel2;
    }
}

