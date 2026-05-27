using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;


namespace MrLabandero
{
    //HANDLES ALL SQLITE OPERATIONS
    public static class DatabaseHelper
    {
        private static readonly string DbPath = Path.Combine(
              Application.StartupPath, "laundry.db");

        private static string ConnectionString =>
            $"Data Source={DbPath};Version=3;";

        // ==================================================
        // SECTION 1 — INITIALIZE DATABASE
        // CREATES .db AND TABLES ON FIRST RUN
        // ==================================================
        public static void InitializeDatabase()
        {
            // CREATE FILE IF NOT EXIST
            if (!File.Exists(DbPath))
                SQLiteConnection.CreateFile(DbPath);

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string createTables = @"
                    -- Customer info
                    CREATE TABLE IF NOT EXISTS Customers (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName      TEXT NOT NULL,
                        ContactNumber TEXT NOT NULL,
                        DateCreated   DATETIME DEFAULT CURRENT_TIMESTAMP
                    );

                    -- One receipt = one transaction
                    CREATE TABLE IF NOT EXISTS Transactions (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerID    INTEGER NOT NULL REFERENCES Customers(ID),
                        DateCreated   DATETIME DEFAULT CURRENT_TIMESTAMP,
                        GrandTotal    DECIMAL NOT NULL
                    );

                    -- Each order line inside a transaction
                    CREATE TABLE IF NOT EXISTS TransactionItems (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        TransactionID INTEGER NOT NULL REFERENCES Transactions(ID),
                        ServiceType   TEXT NOT NULL,
                        ItemType      TEXT NOT NULL,
                        BaseAmount    DECIMAL NOT NULL,
                        AddOnDetails  TEXT,
                        Subtotal      DECIMAL NOT NULL
                    );

                    -- Inventory stocks
                    CREATE TABLE IF NOT EXISTS Inventory (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName    TEXT NOT NULL,
                        Quantity    DECIMAL NOT NULL DEFAULT 0,
                        Unit        TEXT NOT NULL,
                        DateUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
                    );
                    CREATE TABLE IF NOT EXISTS InventoryLog (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        InventoryID INTEGER NOT NULL REFERENCES Inventory(ID),
                        Action      TEXT NOT NULL,
                        QtyChanged  DECIMAL NOT NULL,
                        QtyAfter    DECIMAL NOT NULL,
                        Remarks     TEXT,
                        DateLogged  DATETIME DEFAULT CURRENT_TIMESTAMP
                    );
                ";

                using (var cmd = new SQLiteCommand(createTables, conn))
                    cmd.ExecuteNonQuery();
            }
        }
        // ==========================================
        // SECTION 2 — POS: SAVE TRANSACTION
        // frmMrLabandero.ShowReceipt()
        // SAVES CUSTOMER, TRANSACTION, AND ALL ITEMS
        // AUTO-DEDUCTS DETERGENT AND FABCON
        // RETURMNS NEW TRANSACTION ID or -1 IF FAILED
        // ==========================================
        public static int SaveTransaction(
            string customerName,
            string contactNumber,
            List<UserControlReceipt.ReceiptOrder> orders,
            decimal grandTotal,
            out int customerID)
        {
            customerID = -1;

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        // STEP 1: SAVE OR REUSE CUSTOMER
                        long custID;
                        string checkCustomer = @"
                            SELECT ID FROM Customers
                            WHERE ContactNumber = @contact LIMIT 1";

                        using (var cmd = new SQLiteCommand(checkCustomer, conn))
                        {
                            cmd.Parameters.AddWithValue("@contact", contactNumber);
                            var result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                custID = Convert.ToInt64(result);
                            }
                            else
                            {
                                string insertCustomer = @"
                                    INSERT INTO Customers (FullName, ContactNumber)
                                    VALUES (@name, @contact);
                                    SELECT last_insert_rowid();";

                                using (var ins = new SQLiteCommand(insertCustomer, conn))
                                {
                                    ins.Parameters.AddWithValue("@name", customerName);
                                    ins.Parameters.AddWithValue("@contact", contactNumber);
                                    custID = Convert.ToInt64(ins.ExecuteScalar());
                                }
                            }
                        }

                        customerID = (int)custID;

                        // STEP 2: SAVE TRANSACTION
                        long transactionID;
                        string insertTransaction = @"
                            INSERT INTO Transactions (CustomerID, GrandTotal)
                            VALUES (@customerID, @grandTotal);
                            SELECT last_insert_rowid();";

                        using (var cmd = new SQLiteCommand(insertTransaction, conn))
                        {
                            cmd.Parameters.AddWithValue("@customerID", custID);
                            cmd.Parameters.AddWithValue("@grandTotal", grandTotal);
                            transactionID = Convert.ToInt64(cmd.ExecuteScalar());
                        }
                            // STEP 3: SAVE EACH ORDER ITEM
                            string insertItem = @"
                            INSERT INTO TransactionItems
                                (TransactionID, ServiceType, ItemType, BaseAmount, AddOnDetails, Subtotal)
                            VALUES
                                (@transID, @serviceType, @itemType, @baseAmount, @addOnDetails, @subtotal)";

                            foreach (var order in orders)
                            {
                                string addOns = string.Join(" | ", order.AddOnDetails);

                                using (var cmd = new SQLiteCommand(insertItem, conn))
                                {
                                    cmd.Parameters.AddWithValue("@transID", transactionID);
                                    cmd.Parameters.AddWithValue("@serviceType", order.ServiceType);
                                    cmd.Parameters.AddWithValue("@itemType", order.ItemType);
                                    cmd.Parameters.AddWithValue("@baseAmount", order.BaseAmount);
                                    cmd.Parameters.AddWithValue("@addOnDetails", addOns);
                                    cmd.Parameters.AddWithValue("@subtotal", order.OrderTotal);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // STEP 4: DEDUCT FROM INNVENTORY
                            DeductInventory(conn, orders);

                            dbTrans.Commit();
                            return (int)transactionID;
                        }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        MessageBox.Show($"Error saving transaction:\n{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }



        // ═════════════════════════════════════════════════════════
        // SECTION 5 — PRIVATE HELPERS
        // ═════════════════════════════════════════════════════════

        // AUTO-DEDUCT PER TRANSACTION
        // Full Service + Wash Only: 60ml Detergent + 60ml Fabcon (default)
        // Dry and Fold: 60ml Fabcon only (default)
        // Extra add-ons multiply the deduction
        private static void DeductInventory(SQLiteConnection conn,
            List<UserControlReceipt.ReceiptOrder> orders)
        {
            // GET ID AND NAME FOR RELIABILITY
            long detergentID = GetInventoryIDByName(conn, "Liquid Detergent");
            long fabconID = GetInventoryIDByName(conn, "Fabcon");

            // WANR IF NOT FOUND - OWNER NEED TO ADD INVENTORY
            if (detergentID == -1)
                MessageBox.Show(
                    "'Liquid Detergent' not found in Inventory.\nPlease add it first.",
                    "Inventory Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (fabconID == -1)
                MessageBox.Show(
                    "'Fabcon' not found in Inventory.\nPlease add it first.",
                    "Inventory Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            foreach (var order in orders)
            {
                decimal detergentDeduct = 0;
                decimal fabconDeduct = 60; // DEFAULT 60ml FOR ALL SERVICE TYPES

                // DETERGENT - FOR FFULL SERVIC AND WASH ONLY
                if (order.ServiceType == "Full Service (Wash, Dry, Fold)" ||
                    order.ServiceType == "Regular - Wash Only")
                    detergentDeduct = 60; // DEFAULT 60ml

                // EXTRA ADD-ONS 
                foreach (var detail in order.AddOnDetails)
                {
                    if (detail.StartsWith("Liquid Detergent"))
                    {
                        int qty = ParseQtyFromDetail(detail);
                        detergentDeduct += qty * 60;
                    }
                    if (detail.StartsWith("Extra Fabcon"))
                    {
                        int qty = ParseQtyFromDetail(detail);
                        fabconDeduct += qty * 60;
                    }
                }

                string remarks = $"Auto-deduct: {order.ServiceType} — {order.ItemType}";

                // DEDUCT DETERGENT
                if (detergentID != -1 && detergentDeduct > 0)
                    DeductFromInventory(conn, detergentID, detergentDeduct, remarks);

                // DEDUCT FABCON
                if (fabconID != -1 && fabconDeduct > 0)
                    DeductFromInventory(conn, fabconID, fabconDeduct, remarks);
            }
        }

        // GET INVENTORY ID FOR ITEM NAME — used for auto-deduct lookup
        private static long GetInventoryIDByName(SQLiteConnection conn, string itemName)
        {
            string query = "SELECT ID FROM Inventory WHERE ItemName = @name LIMIT 1";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", itemName);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : -1;
            }
        }

        // DEDUCT FROM INVENTORY AND LOG ACTION
        // WARNS OWNER LOW INVENTORY BUT BYPASSES THROOUGH
        private static void DeductFromInventory(SQLiteConnection conn,
            long inventoryID, decimal deductQty, string remarks)
        {
            decimal currentQty = 0;
            string getQty = "SELECT CurrentQty FROM Inventory WHERE ID = @id";
            using (var cmd = new SQLiteCommand(getQty, conn))
            {
                cmd.Parameters.AddWithValue("@id", inventoryID);
                currentQty = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            decimal newQty = currentQty - deductQty;

            // WARN LOW STOCK BUT CONTINUES
            if (newQty < 0)
                MessageBox.Show(
                    $"Warning: Stock is below zero after deduction.\nPlease restock soon.",
                    "Low Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            string update = @"
                UPDATE Inventory
                SET CurrentQty = @newQty, DateUpdated = CURRENT_TIMESTAMP
                WHERE ID = @id";

            using (var cmd = new SQLiteCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@id", inventoryID);
                cmd.Parameters.AddWithValue("@newQty", newQty);
                cmd.ExecuteNonQuery();
            }

            LogInventoryAction(conn, inventoryID, "Deduct", -deductQty, newQty, remarks);
        }

        // PARSE QUANTITY FROM ADD-ON DETAIL STRING
        // Format: "Liquid Detergent x2 = 50.00 Php"
        private static int ParseQtyFromDetail(string detail)
        {
            try
            {
                int xIndex = detail.IndexOf('x');
                int spaceIndex = detail.IndexOf(' ', xIndex);
                string qtyStr = detail.Substring(xIndex + 1, spaceIndex - xIndex - 1);
                return int.Parse(qtyStr);
            }
            catch { return 1; }
        }

        // LOGS ALL INVENTORY ACTIONS - CALLED BY CRUD
        private static void LogInventoryAction(SQLiteConnection conn,
            long inventoryID, string action, decimal qtyChanged,
            decimal qtyAfter, string remarks)
        {
            string log = @"
                INSERT INTO InventoryLog (InventoryID, Action, QtyChanged, QtyAfter, Remarks)
                VALUES (@invID, @action, @qtyChanged, @qtyAfter, @remarks)";

            using (var cmd = new SQLiteCommand(log, conn))
            {
                cmd.Parameters.AddWithValue("@invID", inventoryID);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@qtyChanged", qtyChanged);
                cmd.Parameters.AddWithValue("@qtyAfter", qtyAfter);
                cmd.Parameters.AddWithValue("@remarks", remarks ?? "");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
