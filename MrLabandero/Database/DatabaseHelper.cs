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
                    -- Customer per transaction record
                    CREATE TABLE IF NOT EXISTS Customers (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName      TEXT NOT NULL,
                        ContactNumber TEXT NOT NULL,
                        DateCreated   DATETIME DEFAULT CURRENT_TIMESTAMP
                    );

                    -- One receipt = one transaction
                    CREATE TABLE IF NOT EXISTS Transactions (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerID  INTEGER NOT NULL REFERENCES Customers(ID),
                        DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
                        GrandTotal  DECIMAL NOT NULL
                    );

                    -- Service types with prices — pre-populated
                    CREATE TABLE IF NOT EXISTS Services (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        ServiceName TEXT NOT NULL,
                        ItemType    TEXT NOT NULL,
                        Price       DECIMAL NOT NULL,
                        PriceType   TEXT NOT NULL
                    );

                    -- Add-on types with prices — pre-populated
                    CREATE TABLE IF NOT EXISTS AddOns (
                        ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                        AddOnName TEXT NOT NULL,
                        Price     DECIMAL NOT NULL,
                        Unit      TEXT NOT NULL
                    );

                    -- Each order line inside a transaction
                    CREATE TABLE IF NOT EXISTS TransactionItems (
                        ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                        TransactionID INTEGER NOT NULL REFERENCES Transactions(ID),
                        ServiceID     INTEGER NOT NULL REFERENCES Services(ID),
                        Quantity      DECIMAL NOT NULL DEFAULT 1,
                        BaseAmount    DECIMAL NOT NULL,
                        Subtotal      DECIMAL NOT NULL
                    );

                    -- Add-ons per transaction item
                    CREATE TABLE IF NOT EXISTS TransactionAddOns (
                        ID                INTEGER PRIMARY KEY AUTOINCREMENT,
                        TransactionItemID INTEGER NOT NULL REFERENCES TransactionItems(ID),
                        AddOnID           INTEGER NOT NULL REFERENCES AddOns(ID),
                        Quantity          INTEGER NOT NULL DEFAULT 1,
                        Subtotal          DECIMAL NOT NULL
                    );

                    -- Inventory stocks
                    CREATE TABLE IF NOT EXISTS Inventory (
                        ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName    TEXT NOT NULL,
                        CurrentQty  DECIMAL NOT NULL DEFAULT 0,
                        Unit        TEXT NOT NULL,
                        DateUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
                    );

                    -- Inventory movement history
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
                INSERT INTO Services (ServiceName, ItemType, Price, PriceType) VALUES
                -- Full Service (per load)
                ('Full Service (Wash, Dry, Fold)', 'Clothes',              180, 'per load'),
                ('Full Service (Wash, Dry, Fold)', 'Towels or Curtains',   190, 'per load'),
                ('Full Service (Wash, Dry, Fold)', 'Beddings',             210, 'per load'),
                -- Regular Wash Only (per kg)
                ('Regular - Wash Only',            'Clothes',               35, 'per kg'),
                ('Regular - Wash Only',            'Towels or Curtains',    45, 'per kg'),
                ('Regular - Wash Only',            'Beddings',              65, 'per kg'),
                -- Regular Dry and Fold (per basket)
                ('Regular - Dry and Fold',         'Clothes',              100, 'per basket'),
                ('Regular - Dry and Fold',         'Towels or Curtains',   110, 'per basket'),
                ('Regular - Dry and Fold',         'Beddings',             130, 'per basket')";

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
                INSERT INTO AddOns (AddOnName, Price, Unit) VALUES
                ('Additional Spin',    30, 'per 10 mins'),
                ('Additional Wash',    30, 'per 7 mins'),
                ('Additional Rinse',   30, 'per 5 mins'),
                ('Liquid Detergent',   25, 'per 60ml'),
                ('Extra Fabcon',       15, 'per 60ml')";

            using (var cmd = new SQLiteCommand(insert, conn))
                cmd.ExecuteNonQuery();
        }

        // =======================================
        // SECTION 2 — LOOKUP HELPERS
        // Get Service ID and AddOn ID by name — used in SaveTransaction
        //  =======================================

        // Get Service ID by ServiceName + ItemType
        public static long GetServiceID(SQLiteConnection conn,
            string serviceName, string itemType)
        {
            string query = @"
                SELECT ID FROM Services
                WHERE ServiceName = @name AND ItemType = @item
                LIMIT 1";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", serviceName);
                cmd.Parameters.AddWithValue("@item", itemType);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : -1;
            }
        }

        // GET ADD ON ID BY ADD ON NAME
        public static long GetAddOnID(SQLiteConnection conn, string addOnName)
        {
            string query = "SELECT ID FROM AddOns WHERE AddOnName = @name LIMIT 1";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", addOnName);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : -1;
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
                        foreach (var order in orders)
                        {
                            // Parse item type — strip weight/basket info for lookup
                            // e.g. "Clothes (2.5kg)" → "Clothes"
                            string cleanItemType = order.ItemType.Split('(')[0].Trim();

                            // Get ServiceID from Services table
                            long serviceID = GetServiceID(conn, order.ServiceType, cleanItemType);

                            // Parse quantity from ItemType
                            // e.g. "Clothes (2.5kg)" → 2.5, "Clothes (2 Basket)" → 2
                            decimal quantity = ParseQuantityFromItemType(order.ItemType);

                            // Insert TransactionItem
                            long transItemID;
                            string insertItem = @"
                                INSERT INTO TransactionItems
                                    (TransactionID, ServiceID, Quantity, BaseAmount, Subtotal)
                                VALUES
                                    (@transID, @serviceID, @qty, @baseAmount, @subtotal)";

                            using (var cmd = new SQLiteCommand(insertItem, conn))
                            {
                                cmd.Parameters.AddWithValue("@transID", transactionID);
                                cmd.Parameters.AddWithValue("@serviceID", serviceID);
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
                                // Parse AddOnName and Quantity from detail string
                                // Format: "Additional Spin x2 = 60.00 Php"
                                string addOnName = ParseAddOnName(detail);
                                int addOnQty = ParseQtyFromDetail(detail);
                                long addOnID = GetAddOnID(conn, addOnName);

                                if (addOnID == -1) continue; // skip if not found

                                // Compute subtotal from AddOns table price
                                decimal addOnPrice = GetAddOnPrice(conn, addOnID);
                                decimal addOnSubtotal = addOnPrice * addOnQty;

                                string insertAddOn = @"
                                    INSERT INTO TransactionAddOns
                                        (TransactionItemID, AddOnID, Quantity, Subtotal)
                                    VALUES
                                        (@transItemID, @addOnID, @qty, @subtotal)";

                                using (var cmd = new SQLiteCommand(insertAddOn, conn))
                                {
                                    cmd.Parameters.AddWithValue("@transItemID", transItemID);
                                    cmd.Parameters.AddWithValue("@addOnID", addOnID);
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
