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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using PdfSharp.Fonts;

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

        // SAVED PDF PATH — USED FOR PRINT OPTION AFTER PDF GENERATION
        private string _lastPdfPath = string.Empty;

        public EventHandler<ProceedEventArgs> OnProceed;

        public class ProceedEventArgs : EventArgs
        {
            public string CustomerName { get; set; }
            public string ContactNumber { get; set; }
            public List<UserControlReceipt.ReceiptOrder> Orders { get; set; }
            public decimal GrandTotal { get; set; }
        }

        // =======================================
        // GROUP 2 — CONSTRUCTOR
        // =======================================
        public UserControlPOS()
        {
            InitializeComponent();

            // HIDE UNNECESSARY PANELS
            panelFullServices.Visible = false;
            panelWashOnly.Visible = false;
            panelAddOns.Visible = false;

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

            // DISABLED UNLESS REQUIREMENTS MET
            panelServiceOptions.Enabled = false;

            ResetSubLabels();
        }

        // =======================================
        // GROUP 3 — INPUT VALIDATION
        // =======================================

        // BLOCKS KEY THAT IS NOT LETTER OR SPACE
        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }
        // BLOCKS ANY INPUT THAT IS NOT DIGIT OR ENHYPEN
        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
                e.Handled = true;
        }
        private void txtFullName_TextChanged_1(object sender, EventArgs e)
        {
            CheckCustomerInfoComplete();
        }
        private void txtContactNumber_TextChanged_1(object sender, EventArgs e)
        {
            CheckCustomerInfoComplete();
        }
        // Enables service options panel only when Full Name is filled AND contact is valid PH number
        private void CheckCustomerInfoComplete()
        {
            bool nameOk = !string.IsNullOrWhiteSpace(txtFullName.Text);
            bool contactOk = Regex.IsMatch(
                txtContactNumber.Text.Trim(),
                @"^(09\d{9}|09\d{2}-\d{3}-\d{4})$");

            panelServiceOptions.Enabled = nameOk && contactOk;

            if (!panelServiceOptions.Enabled)
                ClearServiceSelection();
        }
        // VALIDATES CONTACT NUMBER
        private void txtContactNumber_Leave(object sender, EventArgs e)
        {
            string contact = txtContactNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(contact)) return;

            bool isValid = Regex.IsMatch(contact, @"^(09\d{9}|09\d{2}-\d{3}-\d{4})$");
            if (!isValid)
            {
                MessageBox.Show(
                    "Enter a valid PH mobile number:\n" +
                    "• 09XXXXXXXXX  (11 digits)\n" +
                    "• 09XX-XXX-XXXX",
                    "Invalid Contact Number",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNumber.Focus();
                txtContactNumber.SelectAll();
            }
        }

        // =======================================
        // GROUP 4 — Service Options (Radio Buttons)
        // =======================================

        private void rbFullServices_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbFullServices.Checked)
            {
                panelFullServices.Visible = true;
                panelAddOns.Visible = true;
                panelWashOnly.Visible = false;
                panelFullServices.BringToFront();
                ClearItemSelection();
                UpdateCurrentSubtotal();
            }
        }
        private void rbDryFold_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWash.Checked)
            {
                panelWashOnly.Visible = true;
                panelFullServices.Visible = false;
                panelAddOns.Visible = true;
                //panelRegularServices.BringToFront();
                ClearItemSelection();
                UpdateCurrentSubtotal();
            }
        }
        private void rbWash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWash.Checked)
            {
                panelWashOnly.Visible = true;
                panelFullServices.Visible = false;
                panelAddOns.Visible = true;
                panelWashOnly.BringToFront();
                ClearItemSelection();
                UpdateCurrentSubtotal();
            }
        }

        // =======================================
        // GROUP 5 — Item Types (Radio Buttons)
        // =======================================

        private void rbClothes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurrentSubtotal();
        }
        private void rbTowels_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurrentSubtotal();
        }
        private void rbBeddings_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurrentSubtotal();
        }

        // =======================================
        // GROUP 6 — Add-Ons (Checkboxes)
        // =======================================

        private void ToggleAddOn(CheckBox chk, NumericUpDown nud, Label lbl, decimal price)
        {
            nud.Enabled = chk.Checked;
            lbl.Text = chk.Checked
                ? $"{price * (decimal)nud.Value:0.00} Php"
                : "0.00 Php";
            if (!chk.Checked) nud.Value = 1;
            UpdateCurrentSubtotal();
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
            UpdateCurrentSubtotal();
        }
        private void nudWash_ValueChanged(object sender, EventArgs e)
        {
            if (chkWash.Checked)
                lblWashSub.Text = $"{Prices.AddWash * (decimal)nudWash.Value:0.00} Php";
            UpdateCurrentSubtotal();
        }
        private void nudRinse_ValueChanged(object sender, EventArgs e)
        {
            if (chkRinse.Checked)
                lblRinseSub.Text = $"{Prices.AddRinse * (decimal)nudRinse.Value:0.00} Php";
            UpdateCurrentSubtotal();
        }
        private void nudDetergent_ValueChanged(object sender, EventArgs e)
        {
            if (chkDetergent.Checked)
                lblDetergentSub.Text = $"{Prices.ExtraDetergent * (decimal)nudDetergent.Value:0.00} Php";
            UpdateCurrentSubtotal();
        }
        private void nudFabcon_ValueChanged(object sender, EventArgs e)
        {
            if (chkFabcon.Checked)
                lblFabconSub.Text = $"{Prices.ExtraFabcon * (decimal)nudFabcon.Value:0.00} Php";
            UpdateCurrentSubtotal();
        }

        // =======================================
        // GROUP 7 — COMPUTATIONS
        // =======================================
        private void UpdateCurrentSubtotal()
        {
            decimal baseService = ComputeBaseService();
            decimal addOns = ComputeAddOns();
            decimal currentTotal = baseService + addOns;

            lblServiceSub.Text = $"{baseService:0.00} Php";

            decimal savedTotal = 0;
            foreach (var o in _orders) savedTotal += o.OrderTotal;
            lblGrandTotal.Text = $"{savedTotal + currentTotal:0.00} Php";
        }
        private decimal ComputeBaseService()
        {
            if (rbFullServices.Checked)
            {
                if (rbClothes.Checked) return Prices.FS_Clothes;
                if (rbTowels.Checked) return Prices.FS_Towels;
                if (rbBeddings.Checked) return Prices.FS_Beddings;
            }
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

        // =======================================
        // GROUP  - ADD ORDER
        // =======================================

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateAddOrder()) return;

            var addOnDetails = new List<string>();
            if (chkSpin.Checked)
                addOnDetails.Add($"Additional Spin x{nudSpin.Value} = {Prices.AddSpin * (decimal)nudSpin.Value:0.00} Php");
            if (chkWash.Checked)
                addOnDetails.Add($"Additional Wash x{nudWash.Value} = {Prices.AddWash * (decimal)nudWash.Value:0.00} Php");
            if (chkRinse.Checked)
                addOnDetails.Add($"Additional Rinse x{nudRinse.Value} = {Prices.AddRinse * (decimal)nudRinse.Value:0.00} Php");
            if (chkDetergent.Checked)
                addOnDetails.Add($"Liquid Detergent x{nudDetergent.Value} = {Prices.ExtraDetergent * (decimal)nudDetergent.Value:0.00} Php");
            if (chkFabcon.Checked)
                addOnDetails.Add($"Extra Fabcon x{nudFabcon.Value} = {Prices.ExtraFabcon * (decimal)nudFabcon.Value:0.00} Php");

            string itemType = rbClothes.Checked ? "Clothes (8kg)"
                            : rbTowels.Checked ? "Towels or Curtains (7kg)"
                            : rbBeddings.Checked ? "Beddings (5kg)"
                            : "";

            _orders.Add(new OrderItem
            {
                ServiceType = "Full Service (Wash, Dry, Fold)",
                ItemType = itemType,
                BaseAmount = ComputeBaseService(),
                AddOnDetails = addOnDetails,
                AddOnAmount = ComputeAddOns()
            });
            RefreshOrderSummary();
            ClearServiceSelection();

            MessageBox.Show(
                $"Order #{_orders.Count} added!\nContinue adding or click Generate Receipt.",
                "Order Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshOrderSummary()
        {
            lstOrders.Items.Clear();
            decimal grandTotal = 0;
            int num = 1;

            foreach (var order in _orders)
            {

                lstOrders.Items.Add($"#{num++}  {order.ServiceType} — {order.ItemType}");
                lstOrders.Items.Add($"      Base: {order.BaseAmount:0.00} Php");
                foreach (var detail in order.AddOnDetails)
                    lstOrders.Items.Add($"        + {detail}");
                lstOrders.Items.Add($"      Subtotal: {order.OrderTotal:0.00} Php");
                lstOrders.Items.Add("");
                grandTotal += order.OrderTotal;
            }

            lstOrders.Items.Add("══════════════════════════════");
            lstOrders.Items.Add($"  TOTAL:  {grandTotal:0.00} Php");
            lblGrandTotal.Text = $"{grandTotal:0.00} Php";
        }

        // =======================================
        // GROUP 8 — Proceed Button
        // =======================================
        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (!ValidateProceed()) return;

            var receiptOrders = new List<UserControlReceipt.ReceiptOrder>();
            foreach (var o in _orders)
            {
                receiptOrders.Add(new UserControlReceipt.ReceiptOrder
                {
                    ServiceType = o.ServiceType,
                    ItemType = o.ItemType,
                    BaseAmount = o.BaseAmount,
                    AddOnDetails = o.AddOnDetails,
                    AddOnAmount = o.AddOnAmount
                });
            }

            decimal grandTotal = 0;
            foreach (var o in _orders) grandTotal += o.OrderTotal;

            OnProceed?.Invoke(this, new ProceedEventArgs
            {
                CustomerName = txtFullName.Text.Trim(),
                ContactNumber = txtContactNumber.Text.Trim(),
                Orders = receiptOrders,
                GrandTotal = grandTotal
            });
        }


        // =======================================
        // GROUP — CLEAR BUTTON
        // =======================================
        // CLEARS ALL SERVICES AND ITEMS BUT NOT CUSTOMER INFO
        private void btnClearOptions_Click(object sender, EventArgs e)
        {
            ClearServiceSelection();
        }

        // =======================================
        // GROUP — FORM VALIDATION
        // =======================================
        private bool ValidateAddOrder()
        {
            if (!rbFullServices.Checked && !rbWash.Checked && !rbDryFold.Checked)
            {
                MessageBox.Show("Please select a service type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbFullServices.Checked &&
                !rbClothes.Checked && !rbTowels.Checked && !rbBeddings.Checked)
            {
                MessageBox.Show("Please select an item type (Clothes, Towels, or Beddings).",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private bool ValidateProceed()
        {
            if (_orders.Count == 0)
            {
                MessageBox.Show("Please add at least one order before generating a receipt.",
                    "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            panelWashOnly.Visible = false;
            panelAddOns.Visible = false;


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
        //RESET EVERYTHING AFTER RECEIPT GENERATION
        public void ResetSession()
        {
            txtFullName.Text = "";
            txtContactNumber.Text = "";
            _orders.Clear();
            lstOrders.Items.Clear();
            _lastPdfPath = string.Empty;
            lblGrandTotal.Text = "0.00 Php";
            panelServiceOptions.Enabled = false; // re-disable until new customer info entered
            ClearServiceSelection();
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

        private void rbClothesWash_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
