using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrLabandero
{
    public partial class UserControlPOS : UserControl
    {
        public UserControlPOS()
        {
            InitializeComponent();

            panelFullServices.Visible = false;
            panelRegularServices.Visible = false;
        }

        private void rbFullServices_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbFullServices.Checked)
            {
                panelFullServices.Visible = true;
                panelRegularServices.Visible = false;
                panelFullServices.BringToFront();
            }
        }

        private void rbRegularServices_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRegularServices.Checked)
            {
                panelRegularServices.Visible = true;
                panelFullServices.Visible = false;
                panelRegularServices.BringToFront();
            }
        }
    }
}
