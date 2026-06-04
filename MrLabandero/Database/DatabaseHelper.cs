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
                    CREATE TABLE IF NOT EXISTS Customers (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName      TEXT NOT NULL,
                        ContactNumber TEXT NOT NULL,
                        DateCreated   DATETIME DEFAULT CURRENT_TIMESTAMP
                    );

                    CREATE TABLE IF NOT EXISTS Transactions (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerID  INTEGER NOT NULL REFERENCES Customers(ID),
                        DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
                        GrandTotal  DECIMAL NOT NULL
                    );

                    -- Services reference table — pre-populated
                    CREATE TABLE IF NOT EXISTS Services (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        ServiceCode TEXT NOT NULL UNIQUE,
                        ServiceName TEXT NOT NULL,
                        ItemType    TEXT NOT NULL,
                        Price       DECIMAL NOT NULL,
                        PriceType   TEXT NOT NULL
                    );

                    -- AddOns reference table — pre-populated
                    CREATE TABLE IF NOT EXISTS AddOns (
                        ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                        AddOnCode TEXT NOT NULL UNIQUE,
                        AddOnName TEXT NOT NULL,
                        Price     DECIMAL NOT NULL,
                        Unit      TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS TransactionItems (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        TransactionID INTEGER NOT NULL REFERENCES Transactions(ID),
                        ServiceCode   TEXT NOT NULL,
                        ItemType      TEXT NOT NULL,
                        Quantity      DECIMAL NOT NULL DEFAULT 1,
                        BaseAmount    DECIMAL NOT NULL,
                        Subtotal      DECIMAL NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS TransactionAddOns (
                        ID                INTEGER PRIMARY KEY AUTOINCREMENT,
                        TransactionItemID INTEGER NOT NULL REFERENCES TransactionItems(ID),
                        AddOnCode         TEXT NOT NULL,
                        Quantity          DECIMAL NOT NULL DEFAULT 1,
                        Subtotal          DECIMAL NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Inventory (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName    TEXT NOT NULL,
                        CurrentQty  DECIMAL NOT NULL DEFAULT 0,
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

                SeedServices(conn);
                SeedAddOns(conn);
            }
        }

        //  =======================================
        // SEED SERVICE TABLE BASED ON PRICE
        // ONLY INSERT IF TABLE IS EMPTY
        // =======================================
        private static void SeedServices(SQLiteConnection conn)
        {
            string check = "SELECT COUNT(*) FROM Services";
            using (var cmd = new SQLiteCommand(check, conn))
            {
                long count = Convert.ToInt64(cmd.ExecuteScalar());
                if (count > 0) return; // ALREADY SEEDED
            }

            string insert = @"
            INSERT INTO Services (ServiceCode, ServiceName, ItemType, Price, PriceType) VALUES
            ('FULL01', 'Full Service (Wash, Dry, Fold)', 'Clothes',            180, 'per load'),
            ('FULL02', 'Full Service (Wash, Dry, Fold)', 'Towels or Curtains', 190, 'per load'),
            ('FULL03', 'Full Service (Wash, Dry, Fold)', 'Beddings',           210, 'per load'),
            ('WASH01', 'Regular - Wash Only',            'Clothes',             35, 'per kg'),
            ('WASH02', 'Regular - Wash Only',            'Towels or Curtains',  45, 'per kg'),
            ('WASH03', 'Regular - Wash Only',            'Beddings',            65, 'per kg'),
            ('DFLD01', 'Regular - Dry and Fold',         'Clothes',            100, 'per basket'),
            ('DFLD02', 'Regular - Dry and Fold',         'Towels or Curtains', 110, 'per basket'),
            ('DFLD03', 'Regular - Dry and Fold',         'Beddings',           130, 'per basket')";

            using (var cmd = new SQLiteCommand(insert, conn))
                cmd.ExecuteNonQuery();
        }

        //  =======================================
        // SEED ADDS ON TABLE IF EMPTY
        // ONLY INSERT TABLE IF EMPTY
        //  =======================================
        private static void SeedAddOns(SQLiteConnection conn)
        {
            string check = "SELECT COUNT(*) FROM AddOns";
            using (var cmd = new SQLiteCommand(check, conn))
            {
                long count = Convert.ToInt64(cmd.ExecuteScalar());
                if (count > 0) return; // ALREADY SEEDED
            }
            string insert = @"
            INSERT INTO AddOns (AddOnCode, AddOnName, Price, Unit) VALUES
            ('ADDSPN', 'Additional Spin',  30, 'per 10 mins'),
            ('ADDWSH', 'Additional Wash',  30, 'per 7 mins'),
            ('ADDRNS', 'Additional Rinse', 30, 'per 5 mins'),
            ('ADDDET', 'Liquid Detergent', 25, 'per 60ml'),
            ('ADDFBC', 'Extra Fabcon',     15, 'per 60ml')";

            using (var cmd = new SQLiteCommand(insert, conn))
                cmd.ExecuteNonQuery();
        }

        // =======================================
        // SECTION 2 — LOOKUP HELPERS
        // Get Service ID and AddOn ID by name — used in SaveTransaction
        //  =======================================

        // Get ServiceCode by ServiceName + ItemType
        private static string GetServiceCode(SQLiteConnection conn,
            string serviceName, string itemType)
        {
            string query = @"
                SELECT ServiceCode FROM Services
                WHERE ServiceName = @name AND ItemType = @item
                LIMIT 1";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", serviceName);
                cmd.Parameters.AddWithValue("@item", itemType);
                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "UNKNOWN";
            }
        }

        // Get AddOnCode by AddOnName
        private static string GetAddOnCode(SQLiteConnection conn, string addOnName)
        {
            string query = "SELECT AddOnCode FROM AddOns WHERE AddOnName = @name LIMIT 1";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", addOnName);
                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "UNKNOWN";
            }
        }

        // Get AddOn Price by AddOnCode
        private static decimal GetAddOnPrice(SQLiteConnection conn, string addOnCode)
        {
            string query = "SELECT Price FROM AddOns WHERE AddOnCode = @code LIMIT 1";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@code", addOnCode);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        // ==========================================
        // SECTION 3 — POS: SAVE TRANSACTION
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
                        string insertCustomer = "INSERT INTO Customers (Fullname, ContactNumber) VALUES (@name, @contact)";

                                using (var ins = new SQLiteCommand(insertCustomer, conn))
                                    {   
                                    ins.Parameters.AddWithValue("@name", customerName.Trim());
                                    ins.Parameters.AddWithValue("@contact", contactNumber.Trim());
                                    ins.ExecuteNonQuery();
                                    }

                                string getID = "SELECT last_insert_rowid()";
                                using (var getId = new SQLiteCommand(getID, conn))
                                { 
                                    custID = Convert.ToInt64(getId.ExecuteScalar());
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
                        // STEP 3: SAVE ORDER AND ADD ONS
                        // ── Step 3: Save each Order Item ──
                        foreach (var order in orders)
                        {
                            // Strip weight/basket info for lookup
                            // "Clothes (2.5kg)" → "Clothes"
                            string cleanItemType = order.ItemType.Split('(')[0].Trim();

                            // Get ServiceCode from Services table
                            string serviceCode = GetServiceCode(conn, order.ServiceType, cleanItemType);

                            // Parse quantity — "Clothes (2.5kg)" → 2.5
                            decimal quantity = ParseQuantityFromItemType(order.ItemType);

                            // Insert TransactionItem with ServiceCode directly
                            long transItemID;
                            string insertItem = @"
                                INSERT INTO TransactionItems
                                    (TransactionID, ServiceCode, ItemType, Quantity, BaseAmount, Subtotal)
                                VALUES
                                    (@transID, @serviceCode, @itemType, @qty, @baseAmount, @subtotal)";

                            using (var cmd = new SQLiteCommand(insertItem, conn))
                            {
                                cmd.Parameters.AddWithValue("@transID", transactionID);
                                cmd.Parameters.AddWithValue("@serviceCode", serviceCode);
                                cmd.Parameters.AddWithValue("@itemType", order.ItemType);
                                cmd.Parameters.AddWithValue("@qty", quantity);
                                cmd.Parameters.AddWithValue("@baseAmount", order.BaseAmount);
                                cmd.Parameters.AddWithValue("@subtotal", order.OrderTotal);
                                cmd.ExecuteNonQuery();
                            }

                            using (var getId = new SQLiteCommand("SELECT last_insert_rowid()", conn))
                                transItemID = Convert.ToInt64(getId.ExecuteScalar());

                            // ── Step 4: Save Add-ons per Transaction Item ──
                            foreach (var detail in order.AddOnDetails)
                            {
                                string addOnName = ParseAddOnName(detail);
                                int addOnQty = ParseQtyFromDetail(detail);

                                // Get AddOnCode from AddOns table
                                string addOnCode = GetAddOnCode(conn, addOnName);
                                if (addOnCode == "UNKNOWN") continue;

                                decimal addOnPrice = GetAddOnPrice(conn, addOnCode);
                                decimal addOnSubtotal = addOnPrice * addOnQty;

                                // Insert TransactionAddOn with AddOnCode directly
                                string insertAddOn = @"
                                    INSERT INTO TransactionAddOns
                                        (TransactionItemID, AddOnCode, Quantity, Subtotal)
                                    VALUES
                                        (@transItemID, @addOnCode, @qty, @subtotal)";

                                using (var cmd = new SQLiteCommand(insertAddOn, conn))
                                {
                                    cmd.Parameters.AddWithValue("@transItemID", transItemID);
                                    cmd.Parameters.AddWithValue("@addOnCode", addOnCode);
                                    cmd.Parameters.AddWithValue("@qty", addOnQty);
                                    cmd.Parameters.AddWithValue("@subtotal", addOnSubtotal);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // ── Step 5: Auto-deduct Inventory ──
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

        // ===========================
        // SECTION 3 — INVENTORY CRUD
        // ===========================

        // GET ALL INVENTORY
        public static DataTable GetAllInventory()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT ID, ItemName, CurrentQty, Unit, DateUpdated
                    FROM Inventory
                    ORDER BY ItemName";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // ADD NEW INVENTORY ITEM + LOG ACTION
        public static void AddInventoryItem(string itemName, decimal quantity,
            string unit, string remarks = "")
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        string insertItem = @"
                            INSERT INTO Inventory (ItemName, CurrentQty, Unit, DateUpdated)
                            VALUES (@name, @qty, @unit, CURRENT_TIMESTAMP);
                            SELECT last_insert_rowid();";

                        long newID;
                        using (var cmd = new SQLiteCommand(insertItem, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", itemName);
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@unit", unit);
                            newID = Convert.ToInt64(cmd.ExecuteScalar());
                        }

                        LogInventoryAction(conn, newID, "Add", quantity, quantity, remarks);

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        MessageBox.Show($"Error adding item:\n{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // UPDATE ITEM INVENTORY AND THEN LOGS ACTION
        public static void UpdateInventoryItem(int id, string itemName,
            decimal newQty, string unit, string remarks = "")
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        // GET CURRENT QUANTITY FOR LOG
                        decimal oldQty = 0;
                        string getQty = "SELECT CurrentQty FROM Inventory WHERE ID = @id";
                        using (var cmd = new SQLiteCommand(getQty, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            oldQty = Convert.ToDecimal(cmd.ExecuteScalar());
                        }

                        string update = @"
                            UPDATE Inventory
                            SET ItemName = @name, CurrentQty = @qty,
                                Unit = @unit, DateUpdated = CURRENT_TIMESTAMP
                            WHERE ID = @id";

                        using (var cmd = new SQLiteCommand(update, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@name", itemName);
                            cmd.Parameters.AddWithValue("@qty", newQty);
                            cmd.Parameters.AddWithValue("@unit", unit);
                            cmd.ExecuteNonQuery();
                        }

                        decimal qtyChanged = newQty - oldQty;
                        LogInventoryAction(conn, id, "Edit", qtyChanged, newQty, remarks);

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        MessageBox.Show($"Error updating item:\n{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // RESTOCK - ADD QUANTITY ON TOP OF STOCK
        public static void RestockInventoryItem(int id, decimal addQty, string remarks = "")
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        decimal newQty = 0;
                        string restock = @"
                            UPDATE Inventory
                            SET CurrentQty = CurrentQty + @addQty, DateUpdated = CURRENT_TIMESTAMP
                            WHERE ID = @id;
                            SELECT CurrentQty FROM Inventory WHERE ID = @id;";

                        using (var cmd = new SQLiteCommand(restock, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@addQty", addQty);
                            newQty = Convert.ToDecimal(cmd.ExecuteScalar());
                        }

                        LogInventoryAction(conn, id, "Restock", addQty, newQty, remarks);

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        MessageBox.Show($"Error restocking item:\n{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // DELETE ITEM IN INVENTORY AND LOG ACTION
        public static void DeleteInventoryItem(int id)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (var dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        string deleteLogs = "DELETE FROM InventoryLog WHERE InventoryID = @id";
                        using (var cmd = new SQLiteCommand(deleteLogs, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteItem = "DELETE FROM Inventory WHERE ID = @id";
                        using (var cmd = new SQLiteCommand(deleteItem, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        MessageBox.Show($"Error deleting item:\n{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // GET INVENTORY LOG FOR ITEM
        public static DataTable GetInventoryLog(int inventoryID)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT
                        Action      AS 'Action',
                        QtyChanged  AS 'Qty Changed',
                        QtyAfter    AS 'Qty After',
                        Remarks     AS 'Remarks',
                        DateLogged  AS 'Date'
                    FROM InventoryLog
                    WHERE InventoryID = @id
                    ORDER BY DateLogged DESC";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@id", inventoryID);
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // =========================
        // SECTION 4 — SALES REPORT
        // =========================

        // All transactions with customer info — filterable by date range
        public static DataTable GetSalesReport(DateTime from, DateTime to)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT
                        t.ID            AS 'Trans #',
                        c.ID            AS 'Customer ID',
                        c.FullName      AS 'Customer',
                        c.ContactNumber AS 'Contact',
                        t.DateCreated   AS 'Date',
                        t.GrandTotal    AS 'Total (Php)'
                    FROM Transactions t
                    INNER JOIN Customers c ON c.ID = t.CustomerID
                    WHERE DATE(t.DateCreated) BETWEEN @from AND @to
                    ORDER BY t.DateCreated DESC";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@from", from.ToString("yyyy-MM-dd"));
                    adapter.SelectCommand.Parameters.AddWithValue("@to", to.ToString("yyyy-MM-dd"));
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // Daily sales — grouped by day
        public static DataTable GetDailySales(DateTime from, DateTime to)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT
                        DATE(DateCreated)   AS 'Date',
                        COUNT(*)            AS 'Transactions',
                        SUM(GrandTotal)     AS 'Total Sales (Php)'
                    FROM Transactions
                    WHERE DATE(DateCreated) BETWEEN @from AND @to
                    GROUP BY DATE(DateCreated)
                    ORDER BY DATE(DateCreated) DESC";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@from", from.ToString("yyyy-MM-dd"));
                    adapter.SelectCommand.Parameters.AddWithValue("@to", to.ToString("yyyy-MM-dd"));
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // Weekly sales — grouped by week number
        public static DataTable GetWeeklySales(int year)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT
                        'Week ' || strftime('%W', DateCreated)  AS 'Week',
                        COUNT(*)                                AS 'Transactions',
                        SUM(GrandTotal)                         AS 'Total Sales (Php)'
                    FROM Transactions
                    WHERE strftime('%Y', DateCreated) = @year
                    GROUP BY strftime('%W', DateCreated)
                    ORDER BY strftime('%W', DateCreated) DESC";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@year", year.ToString());
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // Monthly sales — grouped by month
        public static DataTable GetMonthlySales(int year)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT
                        strftime('%Y-%m', DateCreated)  AS 'Month',
                        COUNT(*)                        AS 'Transactions',
                        SUM(GrandTotal)                 AS 'Total Sales (Php)'
                    FROM Transactions
                    WHERE strftime('%Y', DateCreated) = @year
                    GROUP BY strftime('%Y-%m', DateCreated)
                    ORDER BY strftime('%Y-%m', DateCreated) DESC";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@year", year.ToString());
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // Total sales for a date range — for summary label
        public static decimal GetTotalSales(DateTime from, DateTime to)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT IFNULL(SUM(GrandTotal), 0)
                    FROM Transactions
                    WHERE DATE(DateCreated) BETWEEN @from AND @to";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@from", from.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@to", to.ToString("yyyy-MM-dd"));
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        // ===========================
        // SECTION 5 — PRIVATE HELPERS
        // ==========================

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
        // Parse quantity from detail string — "Additional Spin x2 = 60.00 Php" → 2
        private static int ParseQtyFromDetail(string detail)
        {
            try
            {
                int xIndex = detail.IndexOf('x');
                int spaceIndex = detail.IndexOf(' ', xIndex);
                return int.Parse(detail.Substring(xIndex + 1, spaceIndex - xIndex - 1));
            }
            catch { return 1; }
        }


        private static decimal GetAddOnPrice(SQLiteConnection conn, long addOnID)
        {
            string query = "SELECT Price FROM AddOns WHERE ID = @id";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", addOnID);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        // Parse add-on name from detail — "Additional Spin x2 = 60.00 Php" → "Additional Spin"
        private static string ParseAddOnName(string detail)
        {
            try
            {
                int xIndex = detail.IndexOf(" x");
                return xIndex > 0 ? detail.Substring(0, xIndex).Trim() : detail;
            }
            catch { return detail; }
        }

        // Parse quantity from ItemType — "Clothes (2.5kg)" → 2.5, "Clothes (2 Basket)" → 2
        private static decimal ParseQuantityFromItemType(string itemType)
        {
            try
            {
                int start = itemType.IndexOf('(');
                int end = itemType.IndexOf(')');
                if (start < 0 || end < 0) return 1;

                string inner = itemType.Substring(start + 1, end - start - 1);
                string numStr = "";
                foreach (char c in inner)
                {
                    if (char.IsDigit(c) || c == '.' || c == ',') numStr += c;
                    else break;
                }

                return decimal.Parse(numStr.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
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
