using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class IDialogModel : Form
    {
        private string StrInput;

        public string sStrInput
        {
            get { return StrInput; }
            set { StrInput = value; }
        }

        public IDialogModel()
        {
            InitializeComponent();
        }
    }
}
