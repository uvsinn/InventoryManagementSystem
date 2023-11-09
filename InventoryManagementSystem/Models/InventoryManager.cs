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
            string jsonString = JsonSerializer.Serialize(inventory);
            File.WriteAllText(filename, jsonString);
        }
        public Inventory LoadInventory()
        {
            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                Inventory inventory = JsonSerializer.Deserialize<Inventory>(jsonString)!;

                return inventory;
            }
            else
            {
                return new Inventory();
            }
        }
    }
}