using EntFrm.Framework.Utility;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class ContextDialog : Form
    {
        public ContextDialog()
        {
            InitializeComponent();
        }

        private void ContextDialog_Load(object sender, EventArgs e)
        { 
            string ShutAtHour = IPublicHelper.Get_ShutAtHour();
            string ShutAtMinute=IPublicHelper.Get_ShutAtMinute();
            dpHours.SelectedItem = ShutAtHour;
            dpMinutes.SelectedItem = ShutAtMinute;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IPublicHelper.Set_ConfigValue("ShutAtHour", dpHours.SelectedItem.ToString());
            IPublicHelper.Set_ConfigValue("ShutAtMinute", dpMinutes.SelectedItem.ToString());

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
        }

        private void btnResetQueue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清零排队吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                string s = IUserContext.OnExecuteCommand_Xp("doClearQueue", null);
                if(bool.Parse(s))
                {
                    MessageBox.Show("排队清零成功！");
                }
                else
                {
                    MessageBox.Show("排队清零失败！");
                }
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统并关闭主机吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                IUserContext.OnExecuteCommand_Xp("doExitService", null);

                Thread.Sleep(5000);

                Application.Exit();
                WindowsHelper.DoExitWindows(WindowsHelper.ExitWindows.ShutDown);
            }
        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统并重启主机吗？", "确认对话框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                IUserContext.OnExecuteCommand_Xp("doExitService", null);

                Thread.Sleep(5000);

                Application.Exit();
                WindowsHelper.DoExitWindows(WindowsHelper.ExitWindows.Reboot);
            }
        }

        private void btnNetwork_Click(object sender, EventArgs e)
        { 
            SettingDialog dlg = new SettingDialog();
            dlg.ShowDialog();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void lbRegInk32_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string str = System.Windows.Forms.Application.StartupPath + "\\HandOns\\reg32.bat";

                string strDirPath = System.IO.Path.GetDirectoryName(str);
                string strFilePath = System.IO.Path.GetFileName(str);

                string targetDir = string.Format(strDirPath);//this is where mybatch.bat lies
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = strFilePath;

                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();


                MessageBox.Show("注册成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册失败，错误原因:" + ex.Message);
            }
        }

        private void lbRegInk64_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string str = System.Windows.Forms.Application.StartupPath + "\\HandOns\\reg64.bat";

                string strDirPath = System.IO.Path.GetDirectoryName(str);
                string strFilePath = System.IO.Path.GetFileName(str);

                string targetDir = string.Format(strDirPath);//this is where mybatch.bat lies
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = strFilePath;

                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();


                MessageBox.Show("注册成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册失败，错误原因:" + ex.Message);
            }
        }
    }
}
