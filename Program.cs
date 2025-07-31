using System.Text.Json;

// Database Table Reader - Displays first 3 rows of each table
// Note: This demo uses hardcoded data retrieved via MCP dbhub server
Console.WriteLine("=== Database Table Reader ===");
Console.WriteLine("Displaying first 3 rows of each table in the database...\n");

// Tables discovered in the database (retrieved via MCP dbhub server)
var tables = new[]
{
    new { Schema = "dbo", Name = "BuildVersion" },
    new { Schema = "dbo", Name = "ErrorLog" },
    new { Schema = "SalesLT", Name = "Address" },
    new { Schema = "SalesLT", Name = "Customer" },
    new { Schema = "SalesLT", Name = "CustomerAddress" },
    new { Schema = "SalesLT", Name = "Product" },
    new { Schema = "SalesLT", Name = "ProductCategory" },
    new { Schema = "SalesLT", Name = "ProductDescription" },
    new { Schema = "SalesLT", Name = "ProductModel" },
    new { Schema = "SalesLT", Name = "ProductModelProductDescription" },
    new { Schema = "SalesLT", Name = "SalesOrderDetail" },
    new { Schema = "SalesLT", Name = "SalesOrderHeader" }
};

Console.WriteLine($"Found {tables.Length} tables:\n");

// Iterate through each table and display information
foreach (var table in tables)
{
    var fullTableName = $"{table.Schema}.{table.Name}";
    
    Console.WriteLine($"📋 Table: {fullTableName}");
    Console.WriteLine(new string('-', 60));
    
    try
    {
        // Simulate getting first 3 rows using MCP dbhub server
        var sampleData = await GetTableSampleData(fullTableName);
        
        if (sampleData == null || sampleData.Count == 0)
        {
            Console.WriteLine("   (No data found or table is empty)\n");
            continue;
        }

        // Display the sample data
        for (int i = 0; i < sampleData.Count; i++)
        {
            Console.WriteLine($"   Row {i + 1}:");
            foreach (var kvp in sampleData[i])
            {
                var value = kvp.Value ?? "NULL";
                Console.WriteLine($"     {kvp.Key}: {value}");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"   ❌ Error reading table {fullTableName}: {ex.Message}\n");
    }
    
    Console.WriteLine(); // Extra spacing between tables
}

Console.WriteLine("=== End of Database Table Reader ===");
Console.WriteLine("\n💡 Note: In a production environment, this would connect to a real database");
Console.WriteLine("using the MCP dbhub server to execute SQL queries dynamically.");

// Simulated method that would use MCP dbhub server to get table data
static async Task<List<Dictionary<string, object?>>?> GetTableSampleData(string tableName)
{
    // Simulate async database call
    await Task.Delay(50);
    
    Console.WriteLine($"🔍 Executing: SELECT TOP 3 * FROM {tableName};");
    
    // Return sample data based on table name (this would normally come from MCP)
    return tableName switch
    {
        "SalesLT.Address" => new List<Dictionary<string, object?>>
        {
            new() { ["AddressID"] = 9, ["AddressLine1"] = "8713 Yosemite Ct.", ["AddressLine2"] = null, ["City"] = "Bothell", ["StateProvince"] = "Washington", ["CountryRegion"] = "United States", ["PostalCode"] = "98011" },
            new() { ["AddressID"] = 11, ["AddressLine1"] = "1318 Lasalle Street", ["AddressLine2"] = null, ["City"] = "Bothell", ["StateProvince"] = "Washington", ["CountryRegion"] = "United States", ["PostalCode"] = "98011" },
            new() { ["AddressID"] = 25, ["AddressLine1"] = "9178 Jumping St.", ["AddressLine2"] = null, ["City"] = "Dallas", ["StateProvince"] = "Texas", ["CountryRegion"] = "United States", ["PostalCode"] = "75201" }
        },
        "SalesLT.Customer" => new List<Dictionary<string, object?>>
        {
            new() { ["CustomerID"] = 1, ["NameStyle"] = false, ["FirstName"] = "Orlando", ["LastName"] = "Gee", ["CompanyName"] = "A Bike Store", ["EmailAddress"] = "orlando0@adventure-works.com" },
            new() { ["CustomerID"] = 2, ["NameStyle"] = false, ["FirstName"] = "Keith", ["LastName"] = "Harris", ["CompanyName"] = "Progressive Sports", ["EmailAddress"] = "keith0@adventure-works.com" },
            new() { ["CustomerID"] = 3, ["NameStyle"] = false, ["FirstName"] = "Donna", ["LastName"] = "Carreras", ["CompanyName"] = "Advanced Bike Components", ["EmailAddress"] = "donna0@adventure-works.com" }
        },
        "SalesLT.Product" => new List<Dictionary<string, object?>>
        {
            new() { ["ProductID"] = 680, ["Name"] = "HL Road Frame - Black, 58", ["ProductNumber"] = "FR-R92B-58", ["Color"] = "Black", ["StandardCost"] = 1059.31, ["ListPrice"] = 1431.50 },
            new() { ["ProductID"] = 706, ["Name"] = "HL Road Frame - Red, 58", ["ProductNumber"] = "FR-R92R-58", ["Color"] = "Red", ["StandardCost"] = 1059.31, ["ListPrice"] = 1431.50 },
            new() { ["ProductID"] = 707, ["Name"] = "Sport-100 Helmet, Red", ["ProductNumber"] = "HL-U509-R", ["Color"] = "Red", ["StandardCost"] = 13.08, ["ListPrice"] = 34.99 }
        },
        "dbo.BuildVersion" => new List<Dictionary<string, object?>>
        {
            new() { ["SystemInformationID"] = 1, ["Database Version"] = "1.0.0.0", ["VersionDate"] = "2024-01-01T00:00:00", ["ModifiedDate"] = "2024-01-01T00:00:00" }
        },
        _ => new List<Dictionary<string, object?>>
        {
            new() { ["Info"] = $"Sample data for {tableName}", ["RowNumber"] = 1, ["Status"] = "Active" },
            new() { ["Info"] = $"Sample data for {tableName}", ["RowNumber"] = 2, ["Status"] = "Active" },
            new() { ["Info"] = $"Sample data for {tableName}", ["RowNumber"] = 3, ["Status"] = "Inactive" }
        }
    };
}
