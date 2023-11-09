
namespace InventoryManagementSystem.Models
{
    
    public class Order
    {
        public Guid OrderId{ get; set; }
        public List<AddProduct>? OrderedProduct { get; set; }
        public double Total_Amount { get; set; }
        public string description { get; set; }
        public DateTime OrderDate { get; set; }
        public bool isProcessed { get; set;}

        public Order()
        {
            OrderedProduct = new List<AddProduct>();
            isProcessed = true;
        }

    }

}