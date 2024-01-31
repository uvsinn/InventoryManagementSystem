using System.Text.Json;
using System.Xml;

namespace InventoryManagementSystem.Models
{
    public class OrderManager
    {
        public void SaveOrders(Orderlist OrderList)
        {
            try
            {
                string OrderFileName = "OrderHistory.json";
                string jsonString = JsonSerializer.Serialize(OrderList, new JsonSerializerOptions
                {
                    WriteIndented = true,

                });
                File.WriteAllText(OrderFileName, jsonString);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Create a file and then save the data");
            }
        }
        public Orderlist LoadOrders()
        {
            try
            {
                string OrderFileName = "OrderHistory.json";

                string jsonString = File.ReadAllText(OrderFileName);
                Orderlist OrderList = JsonSerializer.Deserialize<Orderlist>(jsonString);
                return OrderList;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Create a file and then Load the data");
                return new Orderlist();
            }
        }
    }
}