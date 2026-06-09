using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MrLabandero
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            LoadSummaryStats();
            LoadSalesChart();
            LoadInventoryStatus();
        }

        // =============================================
        // SUMMARY STATS — Today's sales, transactions,
        // avg order value, low stock count
        // =============================================
        private void LoadSummaryStats()
        {
            DateTime today = DateTime.Today;

            // Total sales today
            decimal totalSales = DatabaseHelper.GetTotalSales(today, today);
            lblTotalSales.Text = $"₱{totalSales:N2}";

            // Transaction count + avg order value
            DataTable dt = DatabaseHelper.GetDailySales(today, today);
            if (dt.Rows.Count > 0)
            {
                int txCount = Convert.ToInt32(dt.Rows[0]["Transactions"]);
                lblTransactions.Text = txCount.ToString();
                lblAvgOrder.Text = txCount > 0
                    ? $"₱{(totalSales / txCount):N2}"
                    : "₱0.00";
            }
            else
            {
                lblTransactions.Text = "0";
                lblAvgOrder.Text = "₱0.00";
            }

            // Low stock count — items with CurrentQty <= 500 (ml-based)
            // Adjust threshold per item type as needed
            DataTable inv = DatabaseHelper.GetAllInventory();
            int lowCount = 0;
            foreach (DataRow row in inv.Rows)
            {
                decimal qty = Convert.ToDecimal(row["CurrentQty"]);
                if (qty <= 500) lowCount++;
            }
            lblLowStock.Text = lowCount.ToString();
            lblLowStock.ForeColor = lowCount > 0 ? Color.OrangeRed : Color.SeaGreen;
        }

        // =============================================
        // SALES CHART — Last 7 days line chart
        // Uses System.Windows.Forms.DataVisualization
        // =============================================
        private void LoadSalesChart()
        {
            DateTime today = DateTime.Today;
            DateTime weekAgo = today.AddDays(-6);

            DataTable dt = DatabaseHelper.GetDailySales(weekAgo, today);

            chartSales.Series.Clear();
            chartSales.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartSales.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            chartSales.ChartAreas[0].BackColor = Color.White;
            chartSales.BackColor = Color.White;
            chartSales.BorderlineColor = Color.Transparent;

            var series = new Series("Sales")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.SteelBlue,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                MarkerColor = Color.SteelBlue,
                IsValueShownAsLabel = false,
                XValueType = ChartValueType.Date
            };

            // Build a dict for quick lookup — fill 0 for missing days
            var salesByDate = new System.Collections.Generic.Dictionary<DateTime, decimal>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["Date"]);
                decimal total = Convert.ToDecimal(row["Total Sales (Php)"]);
                salesByDate[date.Date] = total;
            }

            for (int i = 6; i >= 0; i--)
            {
                DateTime d = today.AddDays(-i);
                decimal val = salesByDate.ContainsKey(d.Date) ? salesByDate[d.Date] : 0;
                series.Points.AddXY(d.ToShortDateString(), val);
            }

            chartSales.Series.Add(series);
            chartSales.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd";
            chartSales.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
        }

        // =============================================
        // INVENTORY STATUS — Shows each item with qty
        // Color-coded: Green OK, Orange Low, Red Critical
        // =============================================
        private void LoadInventoryStatus()
        {
            DataTable inv = DatabaseHelper.GetAllInventory();
            panelInventory.Controls.Clear();

            foreach (DataRow row in inv.Rows)
            {
                string itemName = row["ItemName"].ToString();
                decimal qty     = Convert.ToDecimal(row["CurrentQty"]);
                string unit     = row["Unit"].ToString();

                // Build one row panel per item
                var rowPanel = new Panel
                {
                    Height = 32,
                    Dock = DockStyle.Top,
                    Padding = new Padding(0, 4, 0, 4)
                };

                // Item name label
                var lblName = new Label
                {
                    Text = itemName,
                    Width = 130,
                    Font = new Font("Segoe UI", 9f),
                    Location = new Point(0, 8),
                    AutoSize = false
                };

                // Qty label
                var lblQty = new Label
                {
                    Text = $"{qty:N0} {unit}",
                    Width = 80,
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9f),
                    Location = new Point(135, 8),
                    AutoSize = false
                };

                // Status badge label
                string status;
                Color badgeFore, badgeBack;

                if (qty <= 200)
                {
                    status = "Critical"; badgeFore = Color.FromArgb(163, 45, 45);
                    badgeBack = Color.FromArgb(252, 235, 235);
                }
                else if (qty <= 500)
                {
                    status = "Low"; badgeFore = Color.FromArgb(133, 79, 11);
                    badgeBack = Color.FromArgb(250, 238, 218);
                }
                else
                {
                    status = "OK"; badgeFore = Color.FromArgb(59, 109, 17);
                    badgeBack = Color.FromArgb(234, 243, 222);
                }

                var lblStatus = new Label
                {
                    Text = status,
                    Width = 55,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                    ForeColor = badgeFore,
                    BackColor = badgeBack,
                    Location = new Point(220, 6),
                    AutoSize = false
                };

                rowPanel.Controls.Add(lblName);
                rowPanel.Controls.Add(lblQty);
                rowPanel.Controls.Add(lblStatus);

                // Reverse order para Dock.Top mag-stack pababa ng tama
                panelInventory.Controls.Add(rowPanel);
                rowPanel.BringToFront();
            }
        }
    }
}