using MetroFramework.Forms;
using System;

namespace EntFrm.TicketConsole
{
    public partial class frmMssgForm : MetroForm
    {
        private string Title;
        private string Message;
        private bool AutoQuit;

        public frmMssgForm()
        {
            Title = "消息窗口";
            Message = "消息内容";
            InitializeComponent();
        }


        public frmMssgForm(string title,string message,bool autoquit=false)
        {
            Title = title;
            Message = message;
            AutoQuit = autoquit;

            InitializeComponent();
        }

        private void frmMssgForm_Load(object sender, EventArgs e)
        {
            this.Text = Title;

            lbMessage.Text = Message;

            if(AutoQuit)
            {
                myTimer.Start();
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (myTimer != null)
            {
                myTimer.Stop();
            }
            this.Close();
        }

        private void frmMssgForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if(myTimer!=null)
            {
                myTimer.Stop();
            }
        }
    }
}
