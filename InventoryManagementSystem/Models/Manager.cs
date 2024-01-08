using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;
using InventoryManagementSystem.Models.Interfaces;

namespace InventoryManagementSystem.Models
{
    public class Manager<T>: Imanager<T>
    {
        public string? filename;

        public async Task Save(T ToBeSaved)
        {
            if (typeof(T).ToString() == "InventoryManagementSystem.Models.Inventory")
            {
                filename = "Inventory.json";
            }
            else
            { 
                filename = "OrderHistory.json";
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(ToBeSaved);
                File.WriteAllText(filename, jsonString);
            }
            catch (Exception)
            {
                Console.WriteLine("Create a file and then save the data");
            }
        }
        public async Task<T> Load()
        {

            if (typeof(T).ToString() == "InventoryManagementSystem.Models.Inventory")
            {
                filename = "Inventory.json";
            }
            else
            {
                filename = "OrderHistory.json";
            }

            string jsonString = File.ReadAllText(filename);
            T ToBeLoaded = JsonSerializer.Deserialize<T>(jsonString)!;

            return ToBeLoaded;

        }
    }
}