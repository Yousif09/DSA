using Spectre.Console;  
/* NOTE: a products Sku identifies it, so this version makes Sku init-only. The other values may change when a user
 updates a product. The required modifier makes callers provide the essential string values when creating an object*/
public sealed class Product
{
    public required string Name { get; set; }
    public required string Sku { get; init; }
    public decimal Price { get; set; }
    public required string Department { get; set; }
    public int StockQuantity { get; set; }
}