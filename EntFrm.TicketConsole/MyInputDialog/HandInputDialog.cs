using EntFrm.Framework.Utility;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class HandInputDialog : MetroForm
    { 

        public string Telphone { get; set; }
        public string CnName { get; set; }
        public string Sex { set; get; }
        public string From { get; set; }
        public string IdCard { get; set; }

        private TextBox currTextBox;

        public HandInputDialog()
        {
            InitializeComponent();
        }

        private void HandInputDialog_Load(object sender, EventArgs e)
        { 
            currTextBox = this.txtTelphone;
            dpFrom1.SelectedIndex = 0;
            dpSex.SelectedIndex = 0;
            onBindFrom2(dpFrom1.SelectedItem.ToString());

            this.axETFHand1.Location = new Point(320, 300);
            this.axETFHand1.Width = 300;
            this.axETFHand1.Height = 240;
            this.axETFHand1.SN = "SCFEF-5DRW2-FE207";
            this.axETFHand1.Picture = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\HandOns\inputbg.jpg");
            this.axETFHand1.Hand = true;
            this.axETFHand1.Interval = 10000;
            this.axETFHand1.PenColor = Color.Black;
            this.axETFHand1.PenWidth = 300;
        }

        private void onBindFrom2(string sfrom1)
        {
            List<ItemObject> regionList = IPublicEntity.getAdminRegion().FindAll(x => x.Name.Equals(sfrom1));

            dpFrom2.DataSource = regionList;
            dpFrom2.ValueMember = "Name";
            dpFrom2.DisplayMember = "Value";

        }

        private void axHand_OnOutText(object sender, EventArgs e)
        {
            MetroButton btn = null;
            this.inputPanel.Controls.Clear();
            string s = this.axETFHand1.FullCACText;

            int count = s.Length > 4 ? 4 : s.Length;

            for (int i = 0; i < count; i++)
            {
                btn = new MetroButton();
                btn.Text = s.Substring(i, 1);
                btn.Location = new Point(0, i * 50);
                btn.Width = 45;
                btn.Height = 45;
                btn.FontSize = MetroFramework.MetroButtonSize.Tall;
                btn.FontWeight = MetroFramework.MetroButtonWeight.Regular;
                btn.Click += new System.EventHandler(btnChar_Click);

                this.inputPanel.Controls.Add(btn);
            }
        }

        private void btnChar_Click(object sender, EventArgs e)
        {
            this.axETFHand1.Hand = false;
            this.axETFHand1.Hand = true;

            MetroButton btn = (MetroButton)sender;

            if (currTextBox != null)
            {
                int cursorPos = currTextBox.SelectionStart;

                currTextBox.Text = currTextBox.Text.Insert(cursorPos, btn.Text);
                currTextBox.Focus();
                currTextBox.SelectionStart = cursorPos + 1;
                currTextBox.SelectionLength = 0;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.axETFHand1.Hand = false;
            this.axETFHand1.Hand = true;
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            MetroButton btn = (MetroButton)sender;

            if (currTextBox != null)
            {
                int cursorPos = currTextBox.SelectionStart;

                currTextBox.Text = currTextBox.Text.Insert(cursorPos, btn.Text);
                currTextBox.Focus();
                currTextBox.SelectionStart = cursorPos + 1;
                currTextBox.SelectionLength = 0;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currTextBox != null)
            {
                int cursorPos = currTextBox.SelectionStart;
                if (cursorPos > 0)
                {
                    currTextBox.Text = currTextBox.Text.Remove(cursorPos - 1, 1);
                    cursorPos--;
                    currTextBox.Focus();
                    currTextBox.SelectionStart = cursorPos;
                    currTextBox.SelectionLength = 0;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string tel = txtTelphone.Text.Trim();
            string name = txtCnName.Text.Trim();
            string idCard = txtIdCard.Text.Trim();
            if (tel.Length==11&&!string.IsNullOrEmpty(name)&& idCard.Length==18)
            {

                Telphone = tel;
                CnName = name;
                Sex = dpSex.SelectedItem.ToString();
                From = dpFrom1.SelectedItem.ToString()+"("+((ItemObject)dpFrom2.SelectedItem).Value+")";
                IdCard = idCard;

                RUserModel ruserModel = new RUserModel();
                ruserModel.UserName = CnName;
                ruserModel.UserSex = Sex;
                ruserModel.UserAge = "";
                ruserModel.Address = From;
                ruserModel.Telephone = Telphone;
                ruserModel.IdcardNo = IdCard;
                ruserModel.RicardNo = "";
                ruserModel.Summary = ""; 

                //IUserContext.OnExecuteCommand_Xp("postIdCard", new string[] { "101", JsonConvert.SerializeObject(userInfo)});

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请正确输入您的姓名、手机号码和身份证号!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtTelphone_Enter(object sender, EventArgs e)
        {
            currTextBox = this.txtTelphone;
        }

        private void txtCnName_Enter(object sender, EventArgs e)
        {
            currTextBox = this.txtCnName;
        }

        private void txtIdCard_Enter(object sender, EventArgs e)
        {
            currTextBox = this.txtIdCard;
        }

        private void dpFrom1_SelectedIndexChanged(object sender, EventArgs e)
        {
            onBindFrom2(dpFrom1.SelectedItem.ToString());
        }
    }
}
