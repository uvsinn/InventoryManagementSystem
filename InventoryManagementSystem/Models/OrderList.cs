namespace InventoryManagementSystem.Models
{
    public class Orderlist
    {
        public List<Order> Items { get; set; }
        public Orderlist()
        {
            Items = new List<Order>();
        }

    }
}