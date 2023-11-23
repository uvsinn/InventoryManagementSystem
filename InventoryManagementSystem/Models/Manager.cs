﻿using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;

namespace InventoryManagementSystem.Models
{
    public class Manager<T>
    {
        public string? filename;

        public void Save(T ToBeSaved)
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
            catch (FileNotFoundException)
            {
                Console.WriteLine("Create a file and then save the data");
            }
        }
        public T Load()
        {
            T temp;

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