using System.Text.Json;
using System.Xml;

namespace InventoryManagementSystem.Models
{
    public class OrderManager
    {
        public void SaveOrders(Orderlist OrderList)
        {
            string OrderFileName = "OrderHistory.json";
            string jsonString = JsonSerializer.Serialize(OrderList, new JsonSerializerOptions
            {
                WriteIndented = true,
                
            });
            File.WriteAllText(OrderFileName, jsonString);
        }
        public Orderlist LoadOrders()
        {
            string OrderFileName = "OrderHistory.json";

            string jsonString = File.ReadAllText(OrderFileName);
            Orderlist OrderList = JsonSerializer.Deserialize<Orderlist>(jsonString);

            return OrderList;
        }
    }
}