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
    public partial class UserControlSalesReport : UserControl
    {
        private bool _isDetailedView = false;
        public UserControlSalesReport()
        {
            InitializeComponent();

            btnGenerate.Visible = false;
            panelDaily.Visible = false;
            panelYearlyMonthly.Visible = false;

            panelSearchID.Visible = false;
            panelExactTransaction.Visible = false;

            btnSummary.Visible = false;
            btnDetailed.Visible = false;

            nudYear.Minimum = 2026;
            nudYear.Maximum = 2030;
            nudYear.Value = DateTime.Today.Year;

            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;

            dgvSalesReport.Visible = false;
            dgvSalesReport.ReadOnly = true;
            dgvSalesReport.AllowUserToAddRows = false;
            dgvSalesReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalesReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblTotalSales.Text = "Total Sales: ₱0.00";
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            _isDetailedView = false;
            SetToggleState(isSummary: true);

            if (rbDaily.Checked)
                LoadDailyReport();
        }
        private void btnDetailed_Click(object sender, EventArgs e)
        {
            _isDetailedView = true;
            SetToggleState(isSummary: false);

            if (rbDaily.Checked)
                LoadDailyReport();
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {

            if (rbDaily.Checked)
            {
                if (dtpFrom.Value.Date > dtpTo.Value.Date)
                {
                    MessageBox.Show("'From' date cannot be later than 'To' date.",
                        "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSummary.Visible = true;
                btnDetailed.Visible = true;
                _isDetailedView = false;
                SetToggleState(isSummary: true);

                LoadDailyReport();
            }
            else if (rbWeekly.Checked)
            {
                dgvSalesReport.DataSource = DatabaseHelper.GetWeeklySales((int)nudYear.Value);

                decimal total = DatabaseHelper.GetTotalSales(
                    new DateTime((int)nudYear.Value, 1, 1),
                    new DateTime((int)nudYear.Value, 12, 31));

                lblTotalSales.Text = $"Total Sales for {(int)nudYear.Value}: ₱{total:0.00}";
            }
            else if (rbMonthly.Checked)
            {
                dgvSalesReport.DataSource = DatabaseHelper.GetMonthlySales((int)nudYear.Value);

                decimal total = DatabaseHelper.GetTotalSales(
                    new DateTime((int)nudYear.Value, 1, 1),
                    new DateTime((int)nudYear.Value, 12, 31));

                lblTotalSales.Text = $"Total Sales for {(int)nudYear.Value}: ₱{total:0.00}";
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool hasTransID = !string.IsNullOrWhiteSpace(txtTransactionID.Text);
            bool hasCustID = !string.IsNullOrWhiteSpace(txtCustomerID.Text);

            // AT LEAST ONE INPUT
            if (!hasTransID && !hasCustID)
            {
                MessageBox.Show("Please enter at least a Transaction ID or Customer ID.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int transID = 0;
            int custID = 0;

            if (hasTransID) int.TryParse(txtTransactionID.Text.Trim(), out transID);
            if (hasCustID) int.TryParse(txtCustomerID.Text.Trim(), out custID);

            // BOTH ID PRESENTED VALIDATED
            if (hasTransID && hasCustID)
            {
                bool isValid = DatabaseHelper.ValidateTransactionCustomer(transID, custID);
                if (!isValid)
                {
                    MessageBox.Show(
                        $"Transaction #{transID} does not belong to Customer #{custID}.",
                        "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ShowExactTransaction(transID);
            }
            // TRANSACTION ID RETURN SPECIFIC TRANSACTION
            else if (hasTransID)
            {
                ShowExactTransaction(transID);
            }
            // CUSTOMER ID RETURNS ALL TRANSACTION
            else
            {
                dgvSalesReport.Visible = true;
                panelExactTransaction.Visible = false;

                var data = DatabaseHelper.GetTransactionsByCustomerID(custID);

                if (data.Rows.Count == 0)
                {
                    MessageBox.Show($"No transactions found for Customer #{custID}.",
                        "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearReport();
                    return;
                }

                dgvSalesReport.DataSource = data;

                // COMPUTE TOTAL CUSTOMER TRANSACTION
                decimal total = 0;
                foreach (DataRow row in data.Rows)
                    total += Convert.ToDecimal(row["Grand Total (Php)"]);

                lblTotalSales.Text =
                    $"Customer #{custID} — {data.Rows.Count} Transaction/s — " +
                    $"Total: ₱{total:0.00}";
            }
        }

        private void rbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaily.Checked)
            {
                if (dtpFrom.Value.Date > dtpTo.Value.Date)
                {
                    MessageBox.Show("'From' date cannot be later than 'To' date.",
                        "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                panelDaily.Visible = true;
                panelYearlyMonthly.Visible = false;
                panelSearchID.Visible = false;

                dgvSalesReport.Visible = true;
                panelExactTransaction.Visible = false;

                btnGenerate.Visible = true;
                btnSummary.Visible = false;
                btnDetailed.Visible = false;
                _isDetailedView = false;

                ClearReport();
            }
        }
        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeekly.Checked)
            {
                panelDaily.Visible = false;
                panelYearlyMonthly.Visible = true;
                panelSearchID.Visible = false;

                dgvSalesReport.Visible = true;
                panelExactTransaction.Visible = false;

                btnGenerate.Visible = true;
                btnSummary.Visible = false;
                btnDetailed.Visible = false;

                ClearReport();
            }
        }
        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked)
            {
                panelDaily.Visible = false;
                panelYearlyMonthly.Visible = true;
                panelSearchID.Visible = false;

                dgvSalesReport.Visible = true;
                panelExactTransaction.Visible = false;

                btnGenerate.Visible = true;
                btnSummary.Visible = false;
                btnDetailed.Visible = false;

                ClearReport();
            }
        }
        private void rbSearchID_CheckedChanged(object sender, EventArgs e)
        {
            panelDaily.Visible = false;
            panelYearlyMonthly.Visible = false;
            panelSearchID.Visible = true;

            dgvSalesReport.Visible = false;
            btnGenerate.Visible = false;
            btnSummary.Visible = false;
            btnDetailed.Visible = false;

            ClearReport();
            ClearExactTransaction();

            lblTotalSales.Text = "";
        }

        private void SetToggleState(bool isSummary)
        {
            btnSummary.BackColor = isSummary
                ? System.Drawing.Color.FromArgb(63, 81, 181)
                : System.Drawing.Color.FromArgb(200, 208, 246);

            btnDetailed.BackColor = !isSummary
                ? System.Drawing.Color.FromArgb(63, 81, 181)
                : System.Drawing.Color.FromArgb(200, 208, 246);

            btnSummary.ForeColor = isSummary
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(63, 81, 181);

            btnDetailed.ForeColor = !isSummary
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(63, 81, 181);
        }
        private void LoadDailyReport()
        {
            decimal total = DatabaseHelper.GetTotalSales(
                dtpFrom.Value.Date,
                dtpTo.Value.Date);

            if (_isDetailedView)
            {
                dgvSalesReport.DataSource = DatabaseHelper.GetDetailedDailySales(
                    dtpFrom.Value.Date,
                    dtpTo.Value.Date);

                lblTotalSales.Text = $"Total Sales: ₱{total:0.00}  " +
                                     $"({dtpFrom.Value:MMM dd} – {dtpTo.Value:MMM dd, yyyy})  " +
                                     $"[Detailed View]";
            }
            else
            {
                dgvSalesReport.DataSource = DatabaseHelper.GetDailySales(
                    dtpFrom.Value.Date,
                    dtpTo.Value.Date);

                lblTotalSales.Text = $"Total Sales: ₱{total:0.00}  " +
                                     $"({dtpFrom.Value:MMM dd} – {dtpTo.Value:MMM dd, yyyy})  " +
                                     $"[Summary View]";
            }
        }
        private void ClearReport()
        {
            dgvSalesReport.DataSource = null;
            dgvSalesReport.Rows.Clear();
            dgvSalesReport.Columns.Clear();
            lblTotalSales.Text = "Total Sales: ₱0.00";
        }
        private void txtTransactionID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }
        private void txtCustomerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }
        private void ClearExactTransaction()
        {
            panelExactTransaction.Controls.Clear();
            panelExactTransaction.Visible = false;
        }
        // Populates panelExactTransaction dynamically with labels
        private void ShowExactTransaction(int transactionID)
        {
            var data = DatabaseHelper.GetTransactionByID(transactionID);

            if (data.Rows.Count == 0)
            {
                MessageBox.Show($"Transaction #{transactionID} not found.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hide DataGrid, show Exact Transaction panel
            dgvSalesReport.Visible = false;
            panelExactTransaction.Visible = true;
            panelExactTransaction.Controls.Clear();
            panelExactTransaction.AutoScroll = true;

            int y = 10;
            int x = 15;
            int fullWidth = panelExactTransaction.Width - 30;

            // Get header info from first row
            DataRow first = data.Rows[0];

            // ── Transaction Header ──
            AddLabel("TRANSACTION DETAILS", ref y, bold: true, size: 11, center: true, fullWidth: fullWidth, x: x);
            AddDivider(ref y, x, fullWidth);
            AddLabel($"Trans #     : {first["Trans #"]}", ref y, bold: false, size: 9, center: false, fullWidth: fullWidth, x: x);
            AddLabel($"Customer ID : {first["Customer ID"]}", ref y, bold: false, size: 9, center: false, fullWidth: fullWidth, x: x);
            AddLabel($"Customer    : {first["Customer"]}", ref y, bold: false, size: 9, center: false, fullWidth: fullWidth, x: x);
            AddLabel($"Contact     : {first["Contact"]}", ref y, bold: false, size: 9, center: false, fullWidth: fullWidth, x: x);
            AddLabel($"Date        : {first["Date"]}  {first["Time"]}", ref y, bold: false, size: 9, center: false, fullWidth: fullWidth, x: x);

            AddDivider(ref y, x, fullWidth);
            AddLabel("ORDER BREAKDOWN", ref y, bold: true, size: 9, center: false, fullWidth: fullWidth, x: x);

            // ── Order Items and Add-Ons ──
            int itemNum = 1;
            foreach (DataRow row in data.Rows)
            {
                string type = row["Type"].ToString();
                string code = row["Code"].ToString();
                string desc = row["Description"].ToString();
                decimal qty = row["Qty"] != DBNull.Value ? Convert.ToDecimal(row["Qty"]) : 0;
                decimal amt = row["Amount (Php)"] != DBNull.Value ? Convert.ToDecimal(row["Amount (Php)"]) : 0;

                if (type == "Item")
                {
                    y += 4; // spacer between items
                    AddLabel($"#{itemNum++}  {code} — {desc}", ref y, bold: true, size: 9, center: false, fullWidth: fullWidth, x: x);
                    AddLineItem($"     Qty: {qty}  Base", $"₱{amt:0.00}", ref y, bold: false, size: 9, fullWidth: fullWidth, x: x);
                }
                else // Add-On
                {
                    AddLineItem($"     + {code} ({desc}) x{qty}", $"₱{amt:0.00}", ref y, bold: false, size: 9, fullWidth: fullWidth, x: x);
                }
            }

            AddDivider(ref y, x, fullWidth);

            // ── Grand Total ──
            decimal grandTotal = first["Grand Total (Php)"] != DBNull.Value
                ? Convert.ToDecimal(first["Grand Total (Php)"])
                : 0;

            AddLineItem("GRAND TOTAL", $"₱{grandTotal:0.00}", ref y, bold: true, size: 11, fullWidth: fullWidth, x: x);

            lblTotalSales.Text = $"Transaction #{transactionID} — Grand Total: ₱{grandTotal:0.00}";

            // Expand panel height to fit content
            panelExactTransaction.AutoScrollMinSize =
                new System.Drawing.Size(0, y + 20);
        }
        // ── Dynamic label helpers for panelExactTransaction ──
        private void AddLabel(string text, ref int y,
            bool bold, float size, bool center, int fullWidth, int x)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Arial", size,
                bold ? FontStyle.Bold : FontStyle.Regular);
            lbl.AutoSize = false;
            lbl.Width = fullWidth;
            lbl.Height = (int)(size * 2.5f);
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.TextAlign = center
                ? ContentAlignment.MiddleCenter
                : ContentAlignment.MiddleLeft;
            panelExactTransaction.Controls.Add(lbl);
            y += lbl.Height + 2;
        }
        private void AddLineItem(string label, string amount, ref int y,
            bool bold, float size, int fullWidth, int x)
        {
            int lineHeight = (int)(size * 2.5f);

            var lblLeft = new Label();
            lblLeft.Text = label;
            lblLeft.Font = new Font("Arial", size,
                bold ? FontStyle.Bold : FontStyle.Regular);
            lblLeft.AutoSize = false;
            lblLeft.Width = fullWidth - 80;
            lblLeft.Height = lineHeight;
            lblLeft.Location = new System.Drawing.Point(x, y);
            lblLeft.TextAlign = ContentAlignment.MiddleLeft;
            panelExactTransaction.Controls.Add(lblLeft);

            var lblRight = new Label();
            lblRight.Text = amount;
            lblRight.Font = new Font("Arial", size,
                bold ? FontStyle.Bold : FontStyle.Regular);
            lblRight.AutoSize = false;
            lblRight.Width = 80;
            lblRight.Height = lineHeight;
            lblRight.Location = new System.Drawing.Point(x + fullWidth - 80, y);
            lblRight.TextAlign = ContentAlignment.MiddleRight;
            panelExactTransaction.Controls.Add(lblRight);

            y += lineHeight + 2;
        }
        private void AddDivider(ref int y, int x, int fullWidth)
        {
            var line = new Label();
            line.BorderStyle = BorderStyle.Fixed3D;
            line.AutoSize = false;
            line.Height = 2;
            line.Width = fullWidth;
            line.Location = new System.Drawing.Point(x, y);
            panelExactTransaction.Controls.Add(line);
            y += 10;
        }
    }
}
