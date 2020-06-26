using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class SettingDialog : Form
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void SettingDialog_Load(object sender, EventArgs e)
        {
            Init_Printer();

            txtIpAddress.Text = IPublicHelper.Get_ServerIp();
            txtPort.Text = IPublicHelper.Get_ServerPort();
            //txtVerifCode.Text = IPublicHelper.Get_VerifCode();
            //txtTermCode.Text = IPublicHelper.Get_TerminalCode();
            dpPrinters.SelectedItem = IPublicHelper.Get_PrinterName();
            rdFloatScreen.Checked = IPublicHelper.Get_ScreenMode().Equals("FloatScreen") ? true : false;
            rdFullScreen.Checked = IPublicHelper.Get_ScreenMode().Equals("FullScreen") ? true : false;
            ckPrintQrcode.Checked = IPublicHelper.Get_QrcodeMode().Equals("True") ? true : false;
            txtQrcodeStr.Text = IPublicHelper.Get_QrcodeText();
        }

        private void Init_Printer()
        {
            List<String> printlist = MyPrinterHelper.GetPrinterList();

            dpPrinters.Items.AddRange(printlist.ToArray());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IPublicHelper.Set_ConfigValue("ServerIp", txtIpAddress.Text.Trim());
                IPublicHelper.Set_ConfigValue("ServerPort", txtPort.Text.Trim());
                //IPublicHelper.Set_ConfigValue("VerifCode", txtVerifCode.Text.Trim());
                //IPublicHelper.Set_ConfigValue("TerminalCode", txtTermCode.Text.Trim());
                IPublicHelper.Set_ConfigValue("PrinterName", dpPrinters.SelectedItem.ToString());
                IPublicHelper.Set_ConfigValue("ScreenMode", rdFullScreen.Checked?"FullScreen":"FloatScreen");

                IPublicHelper.Set_ConfigValue("PrintQrcode",ckPrintQrcode.Checked?"True":"False");
                 IPublicHelper.Set_ConfigValue("QrcodeText", txtQrcodeStr.Text.Trim());

                MessageBox.Show("保存参数信息成功，请重新启动程序!");

                this.DialogResult = DialogResult.OK; //成功
                this.Close(); //关闭登陆窗体
            }
            catch(Exception ex)
            {
                MessageBox.Show("保存参数信息出错！详细信息：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; //失败
            this.Close(); //关闭登陆窗体
        } 
    }
}
