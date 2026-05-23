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

        public frmMrLabandero()
        {
            InitializeComponent();
        }

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

            mainPanel.Controls.Add(ucPOS);
            ucPOS.Visible = true;
        }

        // Proceed clicked — switch to receipt
        private void ShowReceipt(object sender, UserControlPOS.ProceedEventArgs e)
        {
            ucReceipt.LoadReceipt(
                e.CustomerName,
                e.ContactNumber,
                e.Orders,
                e.GrandTotal);

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
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(ucPOS);
            ucPOS.Visible = true;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
