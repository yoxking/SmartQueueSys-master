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
    public partial class WindowDialog : Form
    {
        public WindowDialog()
        {
            InitializeComponent();
        }

        private void WindowDialog_Load(object sender, EventArgs e)
        {
            try
            {
                txtWidth.Text = IPublicHelper.Get_WindowWidth();
                txtHeight.Text = IPublicHelper.Get_WindowHeight();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IPublicHelper.Set_ConfigValue("WindowWidth", txtWidth.Text.Trim());
                IPublicHelper.Set_ConfigValue("WindowHeight", txtHeight.Text.Trim());

                MessageBox.Show("设置成功，请重新启动程序");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
