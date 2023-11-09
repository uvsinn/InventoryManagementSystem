namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }

    }
}