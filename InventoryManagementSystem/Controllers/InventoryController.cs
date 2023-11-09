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

        // GET: InventoryManagement
        [HttpGet]
        //public List<Product> Get()
        //{
        //    Inventory inventory = inventoryManager.LoadInventory();

        //    return inventory.Products;
        //}

        //GET: api/Product/1001

        public Inventory Get()
        {
            Inventory inventory = inventoryManager.LoadInventory();
            
            return inventory;

        }

        //POST: api/Product
        [HttpPost]
        public void Post(Product product)
        {
            Inventory inventory = inventoryManager.LoadInventory();
            Product prd = inventory.Products.FirstOrDefault(p => p.ID == product.ID);
            if (prd != null)
            {
                Console.WriteLine("A product with same id already exist");
                return;
            }
            inventory.Products.Add(product);
            inventoryManager.SaveInventory(inventory);
        }

        //PUT: api/Product/1001
        [HttpPut]
        public void Put(int id, [FromBody] Product product)
        {
            Inventory inventory = inventoryManager.LoadInventory();
            Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);
            if (prd != null)
            {
                prd.Name = product.Name;
                prd.Amount = product.Amount;
                prd.Description = product.Description;
                prd.IsAvailable = product.IsAvailable;
                prd.Quantity = product.Quantity;
            }
            inventoryManager.SaveInventory(inventory);
        }
        //DELETE: api/Product/1001
        [HttpDelete]
        public void Delete(int id)
        {
            Inventory inventory = inventoryManager.LoadInventory();
            Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);
            if (prd == null)
            {
                Console.WriteLine("NO such product exists");
            }
            inventory.Products.Remove(prd);
            inventoryManager.SaveInventory(inventory);
        }
    }
}