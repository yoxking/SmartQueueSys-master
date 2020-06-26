using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class PrintingDialog : Form
    {
        public PrintingDialog()
        {
            InitializeComponent();
        }

        private void PrintingDialog_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.pnlContainer.BackgroundImage = EntFrm.TicketConsole.Properties.Resources.ResPrintTicket;
            this.pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;

            myTimer.Start();
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
