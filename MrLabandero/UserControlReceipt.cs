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
    public partial class UserControlReceipt : UserControl
    {
        // =======================================
        // RECEIPT DATA - DATA FROM UserControlPOS via LoadReceipt()
        // =======================================
        private string _customerName = "";
        private string _contactNumber = "";
        private List<ReceiptOrder> _orders = new List<ReceiptOrder>();
        private decimal _grandTotal = 0;
        // =======================================
        // ORDER RECEIPT - MIRRORS OrderItem from UserControlPOS
        // =======================================
        public class ReceiptOrder
        {
            public string ServiceType { get; set; }
            public string ItemType { get; set; }
            public decimal BaseAmount { get; set; }
            public List<string> AddOnDetails { get; set; } = new List<string>();
            public decimal AddOnAmount { get; set; }
            public decimal OrderTotal => BaseAmount + AddOnAmount;
        }

        // =======================================
        // EVENTS - RAISED TO NOTIFY PARENT FORM TO SWITCH USERCONTROL
        // =======================================
        // Raised when user clicks Return — go back to POS with data intact
        public event EventHandler OnReturn;
        // Raised when user clicks New Order — reset everything
        public event EventHandler OnNewOrder;

        // =======================================
        // CONSTRUCTOR
        // =======================================
        public UserControlReceipt()
        {
            InitializeComponent();

            panelReceiptContent.Height = this.Height - panelServiceButtons.Height - 10;
            panelServiceButtons.Dock = DockStyle.Bottom;
            panelReceiptContent.Dock = DockStyle.Fill;
            panelReceiptContent.AutoScroll = true;
        }
        // =======================================
        // GROUP 1 - LOAD RECEIPT
        // =======================================
        private int _customerID = 0;
        private int _transactionID = 0;
        public void LoadReceipt(string customerName, string contactNumber,
          List<ReceiptOrder> orders, decimal grandTotal, int transactionID, int customerID)
        {
            _customerName = customerName;
            _contactNumber = contactNumber;
            _orders = orders;
            _grandTotal = grandTotal;
            _transactionID = transactionID;  // ← add this
            _customerID = customerID;

            BuildReceiptDisplay();
        }

        // =======================================
        // GROUP 3 - RECEIPT
        // =======================================
        private void BuildReceiptDisplay()
        {
            panelReceiptContent.Controls.Clear();
            panelReceiptContent.AutoScrollPosition = new System.Drawing.Point(0, 0);

            int y = 10;
            int fullWidth = panelReceiptContent.Width - 40;

            // ── Shop Header ──
            AddLabel("MR. LABANDERO LAUNDRY SHOP", ref y, bold: true, size: 12, center: true);
            AddLabel("Provincial Rd., Borol 1st, Balagtas, Bulacan", ref y, bold: false, size: 8, center: true);
            AddLabel("0947-860-4797  |  Mon-Sun 7:00AM - 7:00PM", ref y, bold: false, size: 8, center: true);

            AddDivider(ref y);

            AddLabel("RECEIPT", ref y, bold: true, size: 11, center: true);

            // ── Customer Info ──
            AddLabel($"Date     : {DateTime.Now:MMMM dd, yyyy  hh:mm tt}", ref y, bold: false, size: 9, center: false);
            AddLabel($"Transaction #: {_transactionID}", ref y, bold: false, size: 9, center: false);
            AddLabel($"Customer : {_customerName}", ref y, bold: false, size: 9, center: false);
            AddLabel($"Contact  : {_contactNumber}", ref y, bold: false, size: 9, center: false);

            AddDivider(ref y);

            // ── Order Details ──
            AddLabel("ORDER DETAILS", ref y, bold: true, size: 9, center: false);

            int orderNum = 1;
            foreach (var order in _orders)
            {
                // Order header
                AddLabel($"#{orderNum++}  {order.ServiceType}", ref y, bold: true, size: 9, center: false);
                AddLabel($"     Item : {order.ItemType}", ref y, bold: false, size: 8, center: false);

                // Base amount — right aligned
                AddLineItem($"     Base Service", $"{order.BaseAmount:0.00} Php", ref y);

                // Add-on lines
                foreach (var detail in order.AddOnDetails)
                {
                    var parts = detail.Split('=');
                    string lbl = parts.Length > 0 ? "     + " + parts[0].Trim() : detail;
                    string amt = parts.Length > 1 ? parts[1].Trim() : "";
                    AddLineItem(lbl, amt, ref y);
                }

                // Subtotal
                AddLineItem($"     Subtotal", $"{order.OrderTotal:0.00} Php", ref y, bold: true);
                y += 5; // small spacer between orders
            }

            AddDivider(ref y);

            // ── Grand Total ──
            AddLineItem("TOTAL", $"{_grandTotal:0.00} Php", ref y, bold: true, size: 11);

            AddDivider(ref y);

            // ── Footer ──
            AddLabel("Thank you for choosing Mr. Labandero!", ref y, bold: false, size: 8, center: true);
            AddLabel("Available for Pick-up and Delivery (with minimal fee)", ref y, bold: false, size: 8, center: true);

            // Expand panel height if content overflows
            panelReceiptContent.AutoScroll = true;
            panelReceiptContent.AutoScrollMinSize = new System.Drawing.Size(panelReceiptContent.Width, y + 20);
        }

        // =======================================
        // GROUP 3 - BUTTONS
        // =======================================
        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            OnReturn?.Invoke(this, EventArgs.Empty);
        }

        private void btnNewOrder_Click_1(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
               "Start a new order? This will clear all current orders.",
               "New Order",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
                OnNewOrder?.Invoke(this, EventArgs.Empty);
        }

        // =======================================
        // GROUP 3 - HELPERS
        // =======================================
        // Adds a simple label to the receipt panel
        private void AddLabel(string text, ref int y,
            bool bold, float size, bool center)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Arial", size, bold ? FontStyle.Bold : FontStyle.Regular);
            lbl.AutoSize = false;
            lbl.Width = panelReceiptContent.Width - 40;
            lbl.Height = (int)(size * 2.5f);
            lbl.Location = new Point(20, y);
            lbl.TextAlign = center
                ? ContentAlignment.MiddleCenter
                : ContentAlignment.MiddleLeft;
            panelReceiptContent.Controls.Add(lbl);
            y += lbl.Height + 2;
        }

        // Adds a label+amount pair (left label, right-aligned amount) on same line
        private void AddLineItem(string label, string amount,
            ref int y, bool bold = false, float size = 8)
        {
            int lineHeight = (int)(size * 2.5f);

            // Left label
            var lblLeft = new Label();
            lblLeft.Text = label;
            lblLeft.Font = new Font("Arial", size, bold ? FontStyle.Bold : FontStyle.Regular);
            lblLeft.AutoSize = false;
            lblLeft.Width = panelReceiptContent.Width - 100;
            lblLeft.Height = lineHeight;
            lblLeft.Location = new Point(20, y);
            lblLeft.TextAlign = ContentAlignment.MiddleLeft;
            panelReceiptContent.Controls.Add(lblLeft);

            // Right amount
            var lblRight = new Label();
            lblRight.Text = amount;
            lblRight.Font = new Font("Arial", size, bold ? FontStyle.Bold : FontStyle.Regular);
            lblRight.AutoSize = false;
            lblRight.Width = 80;
            lblRight.Height = lineHeight;
            lblRight.Location = new Point(panelReceiptContent.Width - 100, y);
            lblRight.TextAlign = ContentAlignment.MiddleRight;
            panelReceiptContent.Controls.Add(lblRight);

            y += lineHeight + 2;
        }

        private void AddDivider(ref int y)
        {
            var line = new Label();
            line.BorderStyle = BorderStyle.Fixed3D;
            line.AutoSize = false;
            line.Height = 2;
            line.Width = panelReceiptContent.Width - 40;
            line.Location = new Point(20, y);
            panelReceiptContent.Controls.Add(line);
            y += 10;
        }

        private void panelReceiptContent_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
