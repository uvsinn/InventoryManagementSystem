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

        public Inventory ShowInventoryProducts()
        {
            Inventory inventory = manager.Load();
            
            return inventory;

        }

        //POST: api/Product
        [HttpPost]
        public void AddProductToInventory(Product product)
        {
            try
            {
                Inventory inventory = manager.Load();
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
        public void UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                Inventory inventory = manager.Load();
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
        public void DeleteProductFromInventory(int id)
        {
            try
            {
                Inventory inventory = manager.Load();
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