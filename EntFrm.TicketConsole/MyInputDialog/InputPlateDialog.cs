using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class FullBoardDialog : MetroForm
    {
        private string StrInput;

        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public FullBoardDialog()
        {
            InitializeComponent();
        }

        private void FullBoardDialog_Load(object sender, EventArgs e)
        {
            dpWord.SelectedIndex = 0;
            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);

            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim().Length > 0)
            {
                StrInput = dpWord.SelectedItem.ToString() + txtInput.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请正确输入文本内容!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string s = txtInput.Text.Trim();
            if (s.Length > 0)
            {
                txtInput.Text = s.Substring(0, s.Length - 1);
            }
            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string s = btn.Text;

            s = txtInput.Text.Trim() + s;
            txtInput.Text = s;

            txtInput.Focus();
            txtInput.Select(txtInput.Text.Length, 0);
        }

        private void btnTByOrder_Click(object sender, EventArgs e)
        {
            StrInput = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
