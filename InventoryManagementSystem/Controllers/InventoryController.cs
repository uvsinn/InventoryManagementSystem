using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Interfaces;
using InventoryManagementSystem.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private Imanager<Inventory> InventoryManager;
        public InventoryController(Imanager<Inventory> _InventoryManager)
        {
            InventoryManager = _InventoryManager;
        }

        [HttpGet]
        [BasicAuthentication]
        [MyAuthorize(Roles = "Customer,Admin")]
        //GET: api/Product/1001

        public async Task<Inventory> ShowInventoryProducts()
        {
            Inventory inventory = await InventoryManager.Load();
            
            return inventory;

        }

        //POST: api/Product
        [HttpPost]
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        public async Task AddProductToInventory(Product product)
        {
            try
            {
                Inventory inventory = await InventoryManager.Load();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == product.ID);

                inventory.Products.Add(product);
                InventoryManager.Save(inventory);
            }
            catch(NullReferenceException )
            {
                Console.WriteLine("A Product with same ID already exists in inventory");
            }
        }

        //PUT: api/Product/1001
        [HttpPut]
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        public async Task UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                Inventory inventory = await InventoryManager.Load();
                Product? prd = inventory.Products.FirstOrDefault(p => p.ID == id);

                prd.ID = product.ID;
                prd.Name = product.Name;
                prd.Amount = product.Amount;
                prd.Description = product.Description;
                prd.IsAvailable = product.IsAvailable;
                prd.Quantity = product.Quantity;
                InventoryManager.Save(inventory);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            }
        }
        //DELETE: api/Product/1001
        [HttpDelete]
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        public async Task DeleteProductFromInventory(int id)
        {
            try
            {
                Inventory inventory = await InventoryManager.Load();
                Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);
                
                inventory.Products.Remove(prd);
                InventoryManager.Save(inventory);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("No such Product Exists in Inventory");
            } 
        }
    }
}