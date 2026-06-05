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

            btnSummary.Visible = false;
            btnDetailed.Visible = false;

            nudYear.Minimum = 2026;
            nudYear.Maximum = 2030;
            nudYear.Value = DateTime.Today.Year;

            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;

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
                btnSummary.Visible = false;
                btnDetailed.Visible = false;

                btnGenerate.Visible = true;
                panelDaily.Visible = false;
                panelYearlyMonthly.Visible = true;
                ClearReport();
            }
        }

        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked)
            {
                btnSummary.Visible = false;
                btnDetailed.Visible = false;

                btnGenerate.Visible = true;
                panelDaily.Visible = false;
                panelYearlyMonthly.Visible = true;
                ClearReport();
            }
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

       
    }
}
