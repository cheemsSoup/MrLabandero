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
    public partial class frmMrLabandero : Form
    {
        private UserControlPOS ucPOS;
        private UserControlReceipt ucReceipt;
        private UserControlSalesReport ucSalesReport;
        private UserControlSettings uSettings;

        public frmMrLabandero()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
        }
        // HOME
        private void btnHome_Click(object sender, EventArgs e)
        {

        }
        // POS
        private void btnPOS_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            if (ucPOS == null)
            {
                ucPOS = new UserControlPOS();
                ucPOS.Dock = DockStyle.Fill;
                 
                ucPOS.OnProceed += ShowReceipt;
            }

            if (ucReceipt == null)
            {
                ucReceipt = new UserControlReceipt();
                ucReceipt.Dock = DockStyle.Fill;

                ucReceipt.OnReturn += ReturnToPOS;
                ucReceipt.OnNewOrder += StartNewOrder;
            }

            ucPOS.ReloadPrices();
            mainPanel.Controls.Add(ucPOS);
            ucPOS.Visible = true;
        }
        private bool _transactionSaved = false;
        // Proceed clicked — switch to receipt
        private void ShowReceipt(object sender, UserControlPOS.ProceedEventArgs e)
        {
            if (!_transactionSaved)
            {
                int customerID;
                int transactionID = DatabaseHelper.SaveTransaction(
                    e.CustomerName, e.ContactNumber,
                    e.Orders, e.GrandTotal, out customerID);

                ucReceipt.LoadReceipt(
                    e.CustomerName, e.ContactNumber,
                    e.Orders, e.GrandTotal,
                    transactionID, customerID);

                _transactionSaved = true;
            }

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucReceipt);
            ucReceipt.Visible = true;
        }
        private void ReturnToPOS(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucPOS);
            ucPOS.Visible = true;
        }
        // New Order — reset POS then switch back
        private void StartNewOrder(object sender, EventArgs e)
        {
            ucPOS.ResetSession();
            _transactionSaved = false;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucPOS);
            ucPOS.Visible = true;
        }
        // INVENTORY
        private void btnInventory_Click(object sender, EventArgs e)
        {

        }
        // SALES REPORT
        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            if (ucSalesReport == null)
            {
                ucSalesReport = new UserControlSalesReport();
                ucSalesReport.Dock = DockStyle.Fill;
            }

            mainPanel.Controls.Add(ucSalesReport);
            ucSalesReport.Visible = true;
        }
        // SETTINGS
        private void btnSettings_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            if (uSettings == null)
            {
                uSettings = new UserControlSettings();
                uSettings.Dock = DockStyle.Fill;
            }

            mainPanel.Controls.Add(uSettings);
            uSettings.Visible = true;
        }
    }
}
