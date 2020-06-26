using System;
using System.Windows.Forms;

namespace MyFormDesinger
{
    public partial class frmMainFrame : Form
    {
        public MyFormDesigner myDesigner;
        public MyFormSettings mySettings;

        public frmMainFrame()
        {
            InitializeComponent();
        }

        private void frmMainFrame_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.dockPanel1.DefaultFloatWindowSize = new System.Drawing.Size(260,680);

            myDesigner = new MyFormDesigner();
            myDesigner.MdiParent = this;
            myDesigner.Show(this.dockPanel1);
            myDesigner.DockTo(this.dockPanel1, DockStyle.Fill);

            mySettings = new MyFormSettings();
            mySettings.MdiParent = this;
            mySettings.Show(this.dockPanel1);
            mySettings.DockTo(this.dockPanel1, DockStyle.Right);
        }
    }
}
