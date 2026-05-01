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
        public frmMrLabandero()
        {
            InitializeComponent();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            UserControlPOS myPOS = new UserControlPOS();
            myPOS.Dock = DockStyle.Fill;

            mainPanel.Controls.Add(myPOS);
        }
    }
}
