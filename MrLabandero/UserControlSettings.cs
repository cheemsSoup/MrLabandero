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
    public partial class UserControlSettings : UserControl
    {
        // =========================
        // CONSTRUCTOR
        // =========================
        public UserControlSettings()
        {
            InitializeComponent();
            LoadAllSettings();

            txtServiceCode.ReadOnly = true;
            txtServiceName.ReadOnly = true;
            txtServiceItemType.ReadOnly = true;
            txtServicePriceType.ReadOnly = true;

            txtAddOnCode.ReadOnly = true;
            txtAddOnName.ReadOnly = true;
            txtAddOnUnit.ReadOnly = true;
        }

        // =============================
        // SECTION 1 - LOAD ALL SETTINGS
        // =============================
        private void LoadAllSettings()
        {
            LoadShopInfo();
            LoadInventoryDefaults();
            LoadServices();
            LoadAddOns();
        }

        // ========================= 
        // SECTION 2 - SHOP INFO TAB
        // =========================
        private void LoadShopInfo()
        {
            txtShopName.Text = DatabaseHelper.GetSetting("ShopName");
            txtShopAddress.Text = DatabaseHelper.GetSetting("ShopAddress");
            txtShopContact.Text = DatabaseHelper.GetSetting("ShopContact");
            txtShopHours.Text = DatabaseHelper.GetSetting("ShopHours");
        }
        private void btnSaveShopInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtShopName.Text))
            {
                MessageBox.Show("Shop name cannot be empty.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper.SetSetting("ShopName", txtShopName.Text.Trim());
            DatabaseHelper.SetSetting("ShopAddress", txtShopAddress.Text.Trim());
            DatabaseHelper.SetSetting("ShopContact", txtShopContact.Text.Trim());
            DatabaseHelper.SetSetting("ShopHours", txtShopHours.Text.Trim());

            MessageBox.Show("Shop info saved successfully!",
                "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================================
        // SECTION 3 - PRICE MANAGEMENT TAB
        // SERVICES AND ADD ONS DGV
        // ================================
        private void LoadServices()
        {
            dgvServices.DataSource = DatabaseHelper.GetAllServices();

            // Hide ID column — not needed for display
            if (dgvServices.Columns["ID"] != null)
                dgvServices.Columns["ID"].Visible = false;
        }
        private void LoadAddOns()
        {
            dgvAddOns.DataSource = DatabaseHelper.GetAllAddOns();

            if (dgvAddOns.Columns["ID"] != null)
                dgvAddOns.Columns["ID"].Visible = false;
        }
        // SERVICES
        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvServices.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID"].Value);

            DatabaseHelper.UpdateService(
                id,
                txtServiceCode.Text.Trim().ToUpper(),
                txtServiceName.Text.Trim(),
                txtServiceItemType.Text.Trim(),
                nudServicePrice.Value,
                txtServicePriceType.Text.Trim());

            LoadServices();

            MessageBox.Show("Service updated!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvServices.Rows[e.RowIndex];
            txtServiceCode.Text = row.Cells["ServiceCode"].Value?.ToString();
            txtServiceName.Text = row.Cells["ServiceName"].Value?.ToString();
            txtServiceItemType.Text = row.Cells["ItemType"].Value?.ToString();
            nudServicePrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
            txtServicePriceType.Text = row.Cells["PriceType"].Value?.ToString();
        }
        // ADD ONS
        private void btnEditAddOn_Click(object sender, EventArgs e)
        {
            if (dgvAddOns.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an add-on to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvAddOns.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID"].Value);

            DatabaseHelper.UpdateAddOn(
                id,
                txtAddOnCode.Text.Trim().ToUpper(),
                txtAddOnName.Text.Trim(),
                nudAddOnPrice.Value,
                txtAddOnUnit.Text.Trim());

            LoadAddOns();

            MessageBox.Show("Add-On updated!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvAddOns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvAddOns.Rows[e.RowIndex];
            txtAddOnCode.Text = row.Cells["AddOnCode"].Value?.ToString();
            txtAddOnName.Text = row.Cells["AddOnName"].Value?.ToString();
            nudAddOnPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
            txtAddOnUnit.Text = row.Cells["Unit"].Value?.ToString();
        }
        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvServices.ReadOnly = true;
            dgvServices.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AllowUserToDeleteRows = false;
        }

        private void dgvAddOns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAddOns.ReadOnly = true;
            dgvAddOns.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvAddOns.AllowUserToAddRows = false;
            dgvAddOns.AllowUserToDeleteRows = false;
        }

        // ==================================
        // SECTION 4 — INVENTORY DEFAULTS TAB
        // ==================================
        private void LoadInventoryDefaults()
        {
            nudDetergentML.Value = Convert.ToDecimal(
               DatabaseHelper.GetSetting("DefaultDetergentML", "60"));
            nudFabconML.Value = Convert.ToDecimal(
                DatabaseHelper.GetSetting("DefaultFabconML", "60"));
        }
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            DatabaseHelper.SetSetting("DefaultDetergentML", nudDetergentML.Value.ToString());
            DatabaseHelper.SetSetting("DefaultFabconML", nudFabconML.Value.ToString());

            MessageBox.Show("Inventory defaults saved!",
                "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // ===================================
        // SECTION 5 — DATABASE MANAGEMENT TAB
        // ===================================
        private void btnClearTransactions_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
             "This will delete ALL transaction records.\n" +
             "Inventory and Settings will NOT be affected.\n\n" +
             "This cannot be undone. Continue?",
             "Confirm Clear Transactions",
             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                DatabaseHelper.ClearTransactions();
                MessageBox.Show("All transactions cleared.",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClearInventory_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
               "This will delete ALL inventory records and logs.\n" +
               "Transactions and Settings will NOT be affected.\n\n" +
               "This cannot be undone. Continue?",
               "Confirm Clear Inventory",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                DatabaseHelper.ClearInventory();
                MessageBox.Show("Inventory cleared.",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnHardReset_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
             "⚠ WARNING: This will delete ALL data including transactions,\n" +
             "inventory, and settings.\n\n" +
             "Default data (services, add-ons, shop info) will be restored.\n\n" +
             "THIS CANNOT BE UNDONE. Are you absolutely sure?",
             "Confirm Hard Reset",
             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // Double confirm for hard reset
                var confirm2 = MessageBox.Show(
                    "Last warning — ALL DATA will be permanently deleted.\nProceed?",
                    "Final Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm2 == DialogResult.Yes)
                {
                    DatabaseHelper.HardReset();
                    LoadAllSettings();
                    MessageBox.Show("Hard reset complete. All data has been cleared.",
                        "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            DatabaseHelper.BackupDatabase();
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            DatabaseHelper.RestoreDatabase();
        }

       
    }
}
