using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private Manager<Inventory> manager;
        public InventoryController()
        {
            manager = new Manager<Inventory>();
        }

        [HttpGet]
        
        //GET: api/Product/1001

        public async Task<Inventory> ShowInventoryProducts()
        {
             Inventory inventory = await manager.Load();
            
            return inventory;

        }

        //POST: api/Product
        [HttpPost]
        public async Task AddProductToInventory(Product product)
        {
            try
            {
                Inventory inventory = await manager.Load();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == product.ID);

                inventory.Products.Add(product);
                manager.Save(inventory);
            }
            catch(NullReferenceException NRE)
            {
                Console.WriteLine("A Product with same ID already exists in inventory");
            }
        }

        //PUT: api/Product/1001
        [HttpPut]
        public async Task UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                Inventory inventory = await manager.Load();
                Product? prd = inventory.Products.FirstOrDefault(p => p.ID == id);

                prd.ID = product.ID;
                prd.Name = product.Name;
                prd.Amount = product.Amount;
                prd.Description = product.Description;
                prd.IsAvailable = product.IsAvailable;
                prd.Quantity = product.Quantity;
                manager.Save(inventory);
            }
            catch (NullReferenceException NRE)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            }
        }
        //DELETE: api/Product/1001
        [HttpDelete]
        public async Task DeleteProductFromInventory(int id)
        {
            try
            {
                Inventory inventory = await manager.Load();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);
                
                inventory.Products.Remove(prd);
                manager.Save(inventory);
            }
            catch(NullReferenceException NRE)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            } 
        }
    }
}