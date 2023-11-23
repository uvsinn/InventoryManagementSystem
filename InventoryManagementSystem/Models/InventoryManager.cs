using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;

namespace InventoryManagementSystem.Models
{
    public class InventoryManager
    {
        public string filename = "Inventory.json";

        public void SaveInventory(Inventory inventory)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(inventory);
                File.WriteAllText(filename, jsonString);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Create a file and then save the data");
            }
        }
        public Inventory LoadInventory()
        {
            try
            {
                string jsonString = File.ReadAllText(filename);
                Inventory inventory = JsonSerializer.Deserialize<Inventory>(jsonString)!;

                return inventory;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Create a file and then Load the data");
                return new Inventory();
            }
            
        }
    }
}