using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private InventoryManager inventoryManager;
        public InventoryController()
        {
            inventoryManager = new InventoryManager();
        }

        [HttpGet]
        
        //GET: api/Product/1001

        public Inventory ShowInventoryProducts()
        {
            Inventory inventory = inventoryManager.LoadInventory();
            
            return inventory;

        }

        //POST: api/Product
        [HttpPost]
        public void AddProductToInventory(Product product)
        {
            try
            {
                Inventory inventory = inventoryManager.LoadInventory();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == product.ID);

                inventory.Products.Add(product);
                inventoryManager.SaveInventory(inventory);
            }
            catch(NullReferenceException NRE)
            {
                Console.WriteLine("A Product with same ID already exists in inventory");
            }
        }

        //PUT: api/Product/1001
        [HttpPut]
        public void UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                Inventory inventory = inventoryManager.LoadInventory();
                Product? prd = inventory.Products.FirstOrDefault(p => p.ID == id);

                prd.ID = product.ID;
                prd.Name = product.Name;
                prd.Amount = product.Amount;
                prd.Description = product.Description;
                prd.IsAvailable = product.IsAvailable;
                prd.Quantity = product.Quantity;
                inventoryManager.SaveInventory(inventory);
            }
            catch (NullReferenceException NRE)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            }
        }
        //DELETE: api/Product/1001
        [HttpDelete]
        public void DeleteProductFromInventory(int id)
        {
            try
            {
                Inventory inventory = inventoryManager.LoadInventory();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);
                
                inventory.Products.Remove(prd);
                inventoryManager.SaveInventory(inventory);
            }
            catch(NullReferenceException NRE)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            } 
        }
    }
}