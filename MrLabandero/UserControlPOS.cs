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

            // Disable all NUDs on load
            nudSpin.Enabled = false;
            nudWash.Enabled = false;
            nudRinse.Enabled = false;
            nudDetergent.Enabled = false;
            nudFabcon.Enabled = false;

            // Set NUD minimums to 1
            nudSpin.Minimum = 1;
            nudWash.Minimum = 1;
            nudRinse.Minimum = 1;
            nudDetergent.Minimum = 1;
            nudFabcon.Minimum = 1;

            // Reset subtotal labels
            lblSpinSub.Text = "0.00 Php";
            lblWashSub.Text = "0.00 Php";
            lblRinseSub.Text = "0.00 Php";
            lblDetergentSub.Text = "0.00 Php";
            lblFabconSub.Text = "0.00 Php";
            lblServiceSub.Text = "0.00 Php";
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter customer name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtContactNumber.Text))
            {
                MessageBox.Show("Please enter customer contact number.");
                return false;
            }
            return true;
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
            if (rbRegularWash.Checked)
            {
                panelRegularServices.Visible = true;
                panelFullServices.Visible = false;
                panelRegularServices.BringToFront();
            }
        }

    }
}
