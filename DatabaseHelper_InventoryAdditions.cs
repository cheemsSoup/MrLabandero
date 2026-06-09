// ═══════════════════════════════════════════════════════════════════════
// NEW METHODS — Add these inside DatabaseHelper class
// Under SECTION 4 — INVENTORY CRUD
// ═══════════════════════════════════════════════════════════════════════

// Get current stock of an inventory item by name
// Used by UserControlInventory to display current stock
public static decimal GetInventoryStock(string itemName)
{
    using (var conn = new SQLiteConnection(ConnectionString))
    {
        conn.Open();
        string query = @"
            SELECT CurrentQty FROM Inventory
            WHERE ItemName = @name LIMIT 1";

        using (var cmd = new SQLiteCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@name", itemName);
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 0;
        }
    }
}

// Get inventory ID by item name — public version
// Used by UserControlInventory for restock operations
public static int GetInventoryIDByItemName(string itemName)
{
    using (var conn = new SQLiteConnection(ConnectionString))
    {
        conn.Open();
        string query = @"
            SELECT ID FROM Inventory
            WHERE ItemName = @name LIMIT 1";

        using (var cmd = new SQLiteCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@name", itemName);
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }
    }
}
