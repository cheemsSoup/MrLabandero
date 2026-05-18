using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MrLabandero
{
    public partial class UserControlPOS : UserControl
    {
        // =======================================
        // GROUP 1 — ORDER ITEM CLASS
        // =======================================
        private class OrderItem
        {
            public string ServiceType { get; set; }
            public string ItemType { get; set; }
            public decimal BaseAmount { get; set; }
            public List<string> AddOnDetails { get; set; } = new List<string>();
            public decimal AddOnAmount { get; set; }
            public decimal OrderTotal => BaseAmount + AddOnAmount;
        }

        // HOLDS ALL CUSTOMER ORDER FOR CURRENT SESSION
        private List<OrderItem> _orders = new List<OrderItem>();

        // Saved PDF path — used for the Print option after PDF is generated
        private string _lastPdfPath = string.Empty;

        // =======================================
        // GROUP 2 — CONSTRUCTOR
        // =======================================
        public UserControlPOS()
        {
            InitializeComponent();

            // HIDE UNNECESSARY PANELS
            panelFullServices.Visible = false;
            panelRegularServices.Visible = false;

            // DISABLE NUD ON LOAD
            nudSpin.Enabled = false;
            nudWash.Enabled = false;
            nudRinse.Enabled = false;
            nudDetergent.Enabled = false;
            nudFabcon.Enabled = false;

            // SET NUD VALUE MINIMUM TO 1
            nudSpin.Minimum = 1;
            nudWash.Minimum = 1;
            nudRinse.Minimum = 1;
            nudDetergent.Minimum = 1;
            nudFabcon.Minimum = 1;

            // RESET SUBTEXT TOTALS
            lblSpinSub.Text = "0.00 Php";
            lblWashSub.Text = "0.00 Php";
            lblRinseSub.Text = "0.00 Php";
            lblDetergentSub.Text = "0.00 Php";
            lblFabconSub.Text = "0.00 Php";
            lblServiceSub.Text = "0.00 Php";
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
        private void ToggleAddOn(CheckBox chk, NumericUpDown nud, Label lbl, decimal price)
        {
            nud.Enabled = chk.Checked;
            lbl.Text = chk.Checked
                ? $"{price * (decimal)nud.Value:0.00} Php"
                : "0.00 Php";
            if (!chk.Checked) nud.Value = 1;
            UpdateGrandTotal();
        }
        private void chkSpin_CheckedChanged(object sender, EventArgs e)
           => ToggleAddOn(chkSpin, nudSpin, lblSpinSub, Prices.AddSpin);
        private void chkWash_CheckedChanged(object sender, EventArgs e)
            => ToggleAddOn(chkWash, nudWash, lblWashSub, Prices.AddWash);
        private void chkRinse_CheckedChanged(object sender, EventArgs e)
            => ToggleAddOn(chkRinse, nudRinse, lblRinseSub, Prices.AddRinse);
        private void chkDetergent_CheckedChanged(object sender, EventArgs e)
            => ToggleAddOn(chkDetergent, nudDetergent, lblDetergentSub, Prices.ExtraDetergent);
        private void chkFabcon_CheckedChanged(object sender, EventArgs e)
            => ToggleAddOn(chkFabcon, nudFabcon, lblFabconSub, Prices.ExtraFabcon);


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

        // =======================================
        // GROUP — FORM VALIDATION
        // =======================================
        // CLEARS ALL SERVICES AND ITEMS BUT NOT CUSTOMER INFO
        private void btnClearOptions_Click(object sender, EventArgs e)
        {
            ClearServiceSelection();
        }

        // =======================================
        // GROUP — FORM VALIDATION
        // =======================================

        private bool ValidateForm()
        {
            // CHECKS FULL NAME IF BLANK
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter the customer's full name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // CONTACT NUMBER FORMAT VALIDATION AND BLANK CHECK
            string contact = txtContactNumber.Text.Trim();
            if (!string.IsNullOrWhiteSpace(contact))
            {
                bool validContact = Regex.IsMatch(contact, @"^(09\d{9}|09\d{2}-\d{3}-\d{4})$");
                if (!validContact)
                {
                    MessageBox.Show(
                        "Enter a valid PH mobile number:\n• 09XXXXXXXXX\n• 09XX-XXX-XXXX",
                        "Invalid Contact Number",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContactNumber.Focus();
                    return false;
                }
            }

            // CHECK IF SERVICE IS SELECTED
            if (!rbFullServices.Checked && !rbWash.Checked && !rbDryFold.Checked)
            {
                MessageBox.Show("Please select a service type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // CHECK ITEM SELECTED UNDER FULL SERVICE
            if (rbFullServices.Checked &&
                !rbClothes.Checked && !rbTowels.Checked && !rbBeddings.Checked)
            {
                MessageBox.Show("Please select an item type (Clothes, Towels, or Beddings).",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // =======================================
        // GROUP  - HELPER METHODS
        // =======================================

        //CLEARS EVERYTHING EXCEPT FULL NAME AND CONTACT NUMBER
        private void ClearServiceSelection()
        {
            rbFullServices.Checked = false;
            rbWash.Checked = false;
            rbDryFold.Checked = false;

            panelFullServices.Visible = false;
            panelRegularServices.Visible = false;

            ClearItemSelection();

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

            ResetSubLabels();
        }
        //RESETS RB VALUE TO UNCHECKED AND SERVICE SUBTEXT TO ZERO
        private void ClearItemSelection()
        {
            rbClothes.Checked = false;
            rbTowels.Checked = false;
            rbBeddings.Checked = false;
            lblServiceSub.Text = "0.00 Php";
        }
        //RESETS SUBTEXT VALUE TO ZERO
        private void ResetSubLabels()
        {
            lblSpinSub.Text = "0.00 Php";
            lblWashSub.Text = "0.00 Php";
            lblRinseSub.Text = "0.00 Php";
            lblDetergentSub.Text = "0.00 Php";
            lblFabconSub.Text = "0.00 Php";
            lblServiceSub.Text = "0.00 Php";
            lblGrandTotal.Text = "0.00 Php";
        }
    }
}
