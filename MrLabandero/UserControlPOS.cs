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

        // HELPER METHOD
        private void ClearItemSelection()
        {
            rbClothes.Checked = false;
            rbTowels.Checked = false;
            rbBeddings.Checked = false;
            lblServiceSub.Text = "0.00 Php";
        }
        // VALIDATION METHOD
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (!rbFullServices.Checked && !rbWash.Checked && !rbDryFold.Checked)
            {
                MessageBox.Show("Please select a service type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbFullServices.Checked)
            {
                if (!rbClothes.Checked && !rbTowels.Checked && !rbBeddings.Checked)
                {
                    MessageBox.Show("Please select an item type.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        //FULL SERVICE RB POS
        private void rbFullServices_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbFullServices.Checked)
            {
                panelFullServices.Visible = true;
                panelRegularServices.Visible = false;
                panelFullServices.BringToFront();
                ClearItemSelection();
                UpdateGrandTotal();
            }
        }
        //WASH RB POS
        private void rbRegularServices_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWash.Checked)
            {
                panelRegularServices.Visible = true;
                panelFullServices.Visible = false;
                panelRegularServices.BringToFront();
            }
        }

        //FULL SERVICE RB
        private void rbClothes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrandTotal();
        }
        private void rbTowels_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrandTotal();
        }
        private void rbBeddings_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrandTotal();
        }
        // ADD ONS - CHECK BAKSES VALUES
        private void chkSpin_CheckedChanged(object sender, EventArgs e)
        {
            nudSpin.Enabled = chkSpin.Checked;
            if (!chkSpin.Checked) { nudSpin.Value = 1; lblSpinSub.Text = "0.00 Php"; }
            UpdateGrandTotal();
        }
        private void chkWash_CheckedChanged(object sender, EventArgs e)
        {
            nudWash.Enabled = chkWash.Checked;
            if (!chkWash.Checked) { nudWash.Value = 1; lblWashSub.Text = "0.00 Php"; }
            UpdateGrandTotal();
        }
        private void chkRinse_CheckedChanged(object sender, EventArgs e)
        {
            nudRinse.Enabled = chkRinse.Checked;
            if (!chkRinse.Checked) { nudRinse.Value = 1; lblRinseSub.Text = "0.00 Php"; }
            UpdateGrandTotal();
        }
        private void chkDetergent_CheckedChanged(object sender, EventArgs e)
        {
            nudDetergent.Enabled = chkDetergent.Checked;
            if (!chkDetergent.Checked) { nudDetergent.Value = 1; lblDetergentSub.Text = "0.00 Php"; }
            UpdateGrandTotal();
        }
        private void chkFabcon_CheckedChanged(object sender, EventArgs e)
        {
            nudFabcon.Enabled = chkFabcon.Checked;
            if (!chkFabcon.Checked) { nudFabcon.Value = 1; lblFabconSub.Text = "0.00 Php"; }
            UpdateGrandTotal();
        }

        // ADD ONS - NUD VALUES
        private void nudSpin_ValueChanged(object sender, EventArgs e)
        {
            if (chkSpin.Checked)
                lblSpinSub.Text = $"{Prices.AddSpin * (decimal)nudSpin.Value:0.00} Php";
            UpdateGrandTotal();
        }
        private void nudWash_ValueChanged(object sender, EventArgs e)
        {
            if (chkWash.Checked)
                lblWashSub.Text = $"{Prices.AddWash * (decimal)nudWash.Value:0.00} Php";
            UpdateGrandTotal();
        }
        private void nudRinse_ValueChanged(object sender, EventArgs e)
        {
            if (chkRinse.Checked)
                lblRinseSub.Text = $"{Prices.AddRinse * (decimal)nudRinse.Value:0.00} Php";
            UpdateGrandTotal();
        }
        private void nudDetergent_ValueChanged(object sender, EventArgs e)
        {
            if (chkDetergent.Checked)
                lblDetergentSub.Text = $"{Prices.ExtraDetergent * (decimal)nudDetergent.Value:0.00} Php";
            UpdateGrandTotal();
        }
        private void nudFabcon_ValueChanged(object sender, EventArgs e)
        {
            if (chkFabcon.Checked)
                lblFabconSub.Text = $"{Prices.ExtraFabcon * (decimal)nudFabcon.Value:0.00} Php";
            UpdateGrandTotal();
        }

        //CLEAR OPTIONS BTN
        private void btnClearOptions_Click(object sender, EventArgs e)
        {
            // Clear service selection
            rbFullServices.Checked = false;
            rbWash.Checked = false;
            rbDryFold.Checked = false;

            // Hide panels
            panelFullServices.Visible = false;
            panelRegularServices.Visible = false;

            // Clear item type
            ClearItemSelection();

            // Uncheck and reset all add-ons
            chkSpin.Checked = false;
            chkWash.Checked = false;
            chkRinse.Checked = false;
            chkDetergent.Checked = false;
            chkFabcon.Checked = false;

            nudSpin.Value = 1;
            nudWash.Value = 1;
            nudRinse.Value = 1;
            nudDetergent.Value = 1;
            nudFabcon.Value = 1;

            // Reset all labels
            lblSpinSub.Text = "0.00 Php";
            lblWashSub.Text = "0.00 Php";
            lblRinseSub.Text = "0.00 Php";
            lblDetergentSub.Text = "0.00 Php";
            lblFabconSub.Text = "0.00 Php";
            lblServiceSub.Text = "0.00 Php";
            lblGrandTotal.Text = "0.00 Php";
        }
        //PROCEED BUTON
        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            decimal total = ComputeBaseService() + ComputeAddOns();

            MessageBox.Show(
                $"Customer: {txtFullName.Text}\n" +
                $"Contact:  {txtContactNumber.Text}\n" +
                $"─────────────────────\n" +
                $"Base Service: {ComputeBaseService():0.00} Php\n" +
                $"Add-Ons:      {ComputeAddOns():0.00} Php\n" +
                $"─────────────────────\n" +
                $"TOTAL:        {total:0.00} Php",
                "Order Summary",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        //COMPUUTATIONS
        private decimal ComputeBaseService()
        {
            if (!rbFullServices.Checked) return 0;

            if (rbClothes.Checked) return Prices.FS_Clothes;
            if (rbTowels.Checked) return Prices.FS_Towels;
            if (rbBeddings.Checked) return Prices.FS_Beddings;

            return 0;
        }
        private decimal ComputeAddOns()
        {
            decimal total = 0;

            if (chkSpin.Checked) total += Prices.AddSpin * (decimal)nudSpin.Value;
            if (chkWash.Checked) total += Prices.AddWash * (decimal)nudWash.Value;
            if (chkRinse.Checked) total += Prices.AddRinse * (decimal)nudRinse.Value;
            if (chkDetergent.Checked) total += Prices.ExtraDetergent * (decimal)nudDetergent.Value;
            if (chkFabcon.Checked) total += Prices.ExtraFabcon * (decimal)nudFabcon.Value;

            return total;
        }
        private void UpdateGrandTotal()
        {
            decimal baseService = ComputeBaseService();
            decimal addOns = ComputeAddOns();
            decimal grand = baseService + addOns;

            lblServiceSub.Text = $"{baseService:0.00} Php";
            lblGrandTotal.Text = $"{grand:0.00} Php";
        }

       
    }
}
