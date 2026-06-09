using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrLabandero
{
    public partial class UserControlInventory : UserControl
    {
        // ─────────────────────────────────────────────────────────
        // Inventory IDs — loaded once, used for restock operations
        // ─────────────────────────────────────────────────────────
        private int _fabconID    = -1;
        private int _detergentID = -1;

        // ═════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═════════════════════════════════════════════════════════
        public UserControlInventory()
        {
            InitializeComponent();

            // NUD minimum = 1 — can't add 0 or negative stock
            nudFabconAdd.Minimum    = 1;
            nudDetergentAdd.Minimum = 1;
            nudFabconAdd.Maximum    = 999999;
            nudDetergentAdd.Maximum = 999999;

            // DataGridView settings
            dgvInventory.ReadOnly               = true;
            dgvInventory.AllowUserToAddRows     = false;
            dgvInventory.AllowUserToDeleteRows  = false;
            dgvInventory.EditMode               = DataGridViewEditMode.EditProgrammatically;
            dgvInventory.SelectionMode          = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.AutoSizeColumnsMode    = DataGridViewAutoSizeColumnsMode.Fill;

            // Load everything on start
            RefreshInventory();
        }

        // ═════════════════════════════════════════════════════════
        // SECTION 1 — REFRESH / LOAD
        // Called on load and after every restock
        // ═════════════════════════════════════════════════════════

        public void RefreshInventory()
        {
            // ── Load DataGridView ──
            dgvInventory.DataSource = DatabaseHelper.GetAllInventory();

            // Hide ID column — not needed for display
            if (dgvInventory.Columns["ID"] != null)
                dgvInventory.Columns["ID"].Visible = false;

            // ── Load default ml values from Settings ──
            decimal defaultFabconML    = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultFabconML",    "60"));
            decimal defaultDetergentML = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultDetergentML", "60"));

            // ── Get current stocks from DB ──
            decimal fabconStock    = DatabaseHelper.GetInventoryStock("Fabcon");
            decimal detergentStock = DatabaseHelper.GetInventoryStock("Liquid Detergent");

            // ── Get IDs for restock operations ──
            _fabconID    = DatabaseHelper.GetInventoryIDByItemName("Fabcon");
            _detergentID = DatabaseHelper.GetInventoryIDByItemName("Liquid Detergent");

            // ── Update Fabcon panel ──
            lblFabconStock.Text    = $"Stock: {fabconStock:0.##} ml";
            UpdateServingsLabel(lblFabconServings, fabconStock, defaultFabconML);

            // ── Update Detergent panel ──
            lblDetergentStock.Text = $"Stock: {detergentStock:0.##} ml";
            UpdateServingsLabel(lblDetergentServings, detergentStock, defaultDetergentML);
        }

        // ═════════════════════════════════════════════════════════
        // SECTION 2 — RESTOCK BUTTONS
        // ═════════════════════════════════════════════════════════

        // Add Fabcon stock
        private void btnAddFabcon_Click(object sender, EventArgs e)
        {
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

            txtRemarks.Clear();
            nudFabconAdd.Value = 1;

            RefreshInventory();

            MessageBox.Show(
                $"Fabcon restocked by {nudFabconAdd.Minimum} ml.",
                "Restocked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Add Detergent stock
        private void btnAddDetergent_Click(object sender, EventArgs e)
        {
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

            txtRemarks.Clear();
            nudDetergentAdd.Value = 1;

            RefreshInventory();

            MessageBox.Show(
                $"Liquid Detergent restocked.",
                "Restocked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ═════════════════════════════════════════════════════════
        // SECTION 3 — NUD VALUE CHANGED
        // Live preview of servings after adding stock
        // ═════════════════════════════════════════════════════════

        private void nudFabconAdd_ValueChanged(object sender, EventArgs e)
        {
            // Preview — show what servings will be after restock
            decimal defaultML   = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultFabconML", "60"));
            decimal currentStock = DatabaseHelper.GetInventoryStock("Fabcon");
            decimal afterRestock = currentStock + nudFabconAdd.Value;

            UpdateServingsLabel(lblFabconServings, afterRestock, defaultML);
        }

        private void nudDetergentAdd_ValueChanged(object sender, EventArgs e)
        {
            decimal defaultML    = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultDetergentML", "60"));
            decimal currentStock = DatabaseHelper.GetInventoryStock("Liquid Detergent");
            decimal afterRestock = currentStock + nudDetergentAdd.Value;

            UpdateServingsLabel(lblDetergentServings, afterRestock, defaultML);
        }

        // ═════════════════════════════════════════════════════════
        // SECTION 4 — HELPER METHODS
        // ═════════════════════════════════════════════════════════

        // Updates servings label with color indicator
        // Green ≥ 30, Orange 10–29, Red < 10
        private void UpdateServingsLabel(Label lbl, decimal stock, decimal defaultML)
        {
            if (defaultML <= 0)
            {
                lbl.Text      = "Servings: N/A";
                lbl.ForeColor = SystemColors.ControlText;
                return;
            }

            decimal servings = Math.Floor(stock / defaultML);
            lbl.Text = $"Servings left: {servings}";

            // Color coding based on servings remaining
            if (servings >= 30)
                lbl.ForeColor = Color.Green;
            else if (servings >= 10)
                lbl.ForeColor = Color.Orange;
            else
                lbl.ForeColor = Color.Red;
        }
    }
}
