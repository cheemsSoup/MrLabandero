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
    public partial class UserControl_Inventory : UserControl
    {
        private int _fabconID = -1;
        private int _detergentID = -1;
        public UserControl_Inventory()
        {
            InitializeComponent();
            RefreshInventory();

            nudFabconAdd.Minimum = 0;
            nudDetergentAdd.Minimum = 0;
            nudFabconAdd.Maximum = 999999;
            nudDetergentAdd.Maximum = 999999;
            nudFabconAdd.Enabled = true;
            nudDetergentAdd.Enabled = true;

            dgvInventory.ReadOnly = true;
            dgvInventory.AllowUserToAddRows = false;
            dgvInventory.AllowUserToDeleteRows = false;
            dgvInventory.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            RefreshInventory();
        }
        // =======================================
        // SECTION 1 — REFRESH / LOAD
        // CALLS LOAD EVERY RESTOCK
        // ========================================
        public void RefreshInventory()
        {
            dgvInventory.DataSource = DatabaseHelper.GetAllInventory();

            if (dgvInventory.Columns["ID"] != null)
                dgvInventory.Columns["ID"].Visible = false;

            decimal defaultFabconML = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultFabconML", "60"));
            decimal defaultDetergentML = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultDetergentML", "60"));

            lblFabconDefault.Text = $"Default: {defaultFabconML:0}ml / order";
            lblDetergentDefault.Text = $"Default: {defaultDetergentML:0}ml / order";

            decimal fabconStock = DatabaseHelper.GetInventoryStock("Fabcon");
            decimal detergentStock = DatabaseHelper.GetInventoryStock("Liquid Detergent");

            _fabconID = DatabaseHelper.GetInventoryIDByItemName("Fabcon");
            _detergentID = DatabaseHelper.GetInventoryIDByItemName("Liquid Detergent");

            lblFabconStock.Text = $"Stock: {fabconStock:0.##} ml";
            UpdateServingsLabel(lblFabconServings, fabconStock, defaultFabconML);

            lblDetergentStock.Text = $"Stock: {detergentStock:0.##} ml";
            UpdateServingsLabel(lblDetergentServings, detergentStock, defaultDetergentML);
        }

        // ===========================
        // SECTION 2 - RESTOCK BUTTONS
        // ===========================
        private void btnAddFabcon_Click(object sender, EventArgs e)
        {
            if ( nudFabconAdd.Value == 0 )
            {
                MessageBox.Show(
                    "Invalid value! Please input minimum of 1 ml.", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_fabconID == -1)
            {
                MessageBox.Show(
                    "'Fabcon' not found in Inventory.\n" +
                    "Please add it first via Settings → Inventory.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string remarks = string.IsNullOrWhiteSpace(txtRemarks.Text)
                ? "Manual restock"
                : txtRemarks.Text.Trim();

            DatabaseHelper.RestockInventoryItem(
                _fabconID, nudFabconAdd.Value, remarks);

            MessageBox.Show(
                $"Fabcon restocked by {nudFabconAdd.Value} ml.",
            "Restocked", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtRemarks.Clear();
            nudFabconAdd.Value = 0;
            RefreshInventory();
        }
        private void btnAddDetergent_Click(object sender, EventArgs e)
        {
            if (nudDetergentAdd.Value == 0)
            {
                MessageBox.Show(
                    "Invalid value! Please input minimum of 1 ml.", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_detergentID == -1)
            {
                MessageBox.Show(
                    "'Liquid Detergent' not found in Inventory.\n" +
                    "Please add it first via Settings → Inventory.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string remarks = string.IsNullOrWhiteSpace(txtRemarks.Text)
                ? "Manual restock"
                : txtRemarks.Text.Trim();

            DatabaseHelper.RestockInventoryItem(
                _detergentID, nudDetergentAdd.Value, remarks);

            MessageBox.Show(
             $"Liquid Detergent restocked by {nudDetergentAdd.Value} ml.",
             "Restocked", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtRemarks.Clear();
            nudDetergentAdd.Value = 0;
            RefreshInventory();
        }

        // ======================
        // SECTION 3 - NUD VALUES
        // ======================
        private void nudFabconAdd_ValueChanged(object sender, EventArgs e)
        {
            decimal defaultML = Convert.ToDecimal(
               DatabaseHelper.GetSetting("DefaultFabconML", "60"));
            decimal currentStock = DatabaseHelper.GetInventoryStock("Fabcon");
            decimal afterRestock = currentStock + nudFabconAdd.Value;

            UpdateServingsLabel(lblFabconServings, afterRestock, defaultML);
        }
        private void nudDetergentAdd_ValueChanged(object sender, EventArgs e)
        {
            decimal defaultML = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultDetergentML", "60"));
            decimal currentStock = DatabaseHelper.GetInventoryStock("Liquid Detergent");
            decimal afterRestock = currentStock + nudDetergentAdd.Value;

            UpdateServingsLabel(lblDetergentServings, afterRestock, defaultML);
        }

        // ===================
        // SECTION 4 - HELPERS
        // ===================
        // UPDATE SERVINGS WITH COLOR INDICATOR
        // Green ≥ 30, Orange 10–29, Red < 10
        private void UpdateServingsLabel(Label lbl, decimal stock, decimal defaultML)
        {
            if (defaultML <= 0)
            {
                lbl.Text = "Servings: N/A";
                lbl.ForeColor = SystemColors.ControlText;
                return;
            }

            decimal servings = Math.Floor(stock / defaultML);
            lbl.Text = $"Servings left: {servings}";

            // COLOR CODING BASED ON SERVINGS REMAINING
            if (servings >= 30)
                lbl.ForeColor = Color.Green;
            else if (servings >= 10)
                lbl.ForeColor = Color.Orange;
            else
                lbl.ForeColor = Color.Red;

        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
