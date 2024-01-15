using InventoryManagementSystem.DataAccessLayer;
using InventoryManagementSystem.Models.DatabaseModels;
using InventoryManagementSystem.Models.Interfaces;
using InventoryManagementSystem.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //private Imanager<Inventory> InventoryManager;
        private readonly IManagingInventoryDL _managingInventory;





        //public InventoryController(Imanager<Inventory> _InventoryManager)
        //{
        //    InventoryManager = _InventoryManager;
        //}
        public InventoryController(IManagingInventoryDL managingInventory)
        {
            _managingInventory = managingInventory;
        }







        //GET: api/Product/1001
        [HttpGet]
        //[BasicAuthentication]
        //[MyAuthorize(Roles = "Customer,Admin")]

        //public async Task<Inventory> ShowInventoryProducts()
        //{
        //    Inventory inventory = await InventoryManager.Load();

        //    return inventory;

        //}
        public async Task<IActionResult> GetAllProducts()
        {
            GetAllProductsResponse response = new GetAllProductsResponse();
            try
            {
                //Connection between controller and data access layer
                response = await _managingInventory.GetAllProducts();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(string ID)
        {
            GetProductByIdResponse response = new GetProductByIdResponse();
            try
            {
                //Connection between controller and data access layer
                response = await _managingInventory.GetProductById(ID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            GetProductByNameResponse response = new GetProductByNameResponse();
            try
            {
                //Connection between controller and data access layer
                response = await _managingInventory.GetProductByName(Name);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }








        //POST: api/Product
        [HttpPost]
        //[BasicAuthentication]
        //[MyAuthorize(Roles = "Admin")]
        //public async Task AddProductToInventory(Product product)
        //{
        //    try
        //    {
        //        Inventory inventory = await InventoryManager.Load();
        //        Product prd = inventory.Products.FirstOrDefault(p => p.ID == product.ID);

        //        inventory.Products.Add(product);
        //        InventoryManager.Save(inventory);
        //    }
        //    catch(NullReferenceException )
        //    {
        //        Console.WriteLine("A Product with same ID already exists in inventory");
        //    }
        //}
        public async Task<IActionResult> InsertProduct(InsertProductRequest request)
        {
            InsertProductResponse response = new InsertProductResponse();
            try
            {
                if(request.Amount<0)
                {
                    throw new Exception("Amount cannot be smaller than zero");
                }
                if (request.Quantity < 0)
                {
                    throw new Exception("Quantity cannot be smaller than zero");
                }
                //Connection between controller and data access layer
                response = await _managingInventory.InsertProduct(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }










        //PUT: api/Product/1001
        [HttpPut]
        //[BasicAuthentication]
        //[MyAuthorize(Roles = "Admin")]
        //public async Task UpdateProduct(int id, [FromBody] Product product)
        //{
        //    try
        //    {
        //        Inventory inventory = await InventoryManager.Load();
        //        Product? prd = inventory.Products.FirstOrDefault(p => p.ID == id);

        //        prd.ID = product.ID;
        //        prd.Name = product.Name;
        //        prd.Amount = product.Amount;
        //        prd.Description = product.Description;
        //        prd.IsAvailable = product.IsAvailable;
        //        prd.Quantity = product.Quantity;
        //        InventoryManager.Save(inventory);
        //    }
        //    catch (NullReferenceException)
        //    {
        //        Console.WriteLine("No such Product Exists in Inventory");
        //    }
        //}

        public async Task<IActionResult> UpdateProductById(InsertProductRequest request)
        {
            UpdateProductByIdResponse response = new UpdateProductByIdResponse();
            try
            {
                if(request.Amount<0)
                {
                    throw new Exception("Amount cannot be smaller than zero");
                }
                if (request.Quantity < 0)
                {
                    throw new Exception("Quantity cannot be smaller than zero");
                }
                response = await _managingInventory.UpdateProductById(request);
            }
            catch(Exception ex)
            {
                response.IsSuccess=false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAmountById(UpdateAmountByIdRequest request)
        {
            UpdateAmountByIdResponse response = new UpdateAmountByIdResponse();
            try
            {
                if (request.Amount < 0)
                {
                    throw new Exception("Amount cannot be smaller than zero");
                }
                response = await _managingInventory.UpdateAmountById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return Ok(response);
        }









        //DELETE: api/Product/1001
        [HttpDelete]
        //[BasicAuthentication]
        //[MyAuthorize(Roles = "Admin")]
        //public async Task DeleteProductFromInventory(int id)
        //{
        //    try
        //    {
        //        Inventory inventory = await InventoryManager.Load();
        //        Product prd = inventory.Products.FirstOrDefault(p => p.ID == id);

        //        inventory.Products.Remove(prd);
        //        InventoryManager.Save(inventory);
        //    }
        //    catch(NullReferenceException)
        //    {
        //        Console.WriteLine("No such Product Exists in Inventory");
        //    } 
        //}

        public async Task<IActionResult> DeleteProductById(string ID)
        {
            DeleteProductByIdResponse response= new DeleteProductByIdResponse();
            try
            {
                response = await _managingInventory.DeleteProductById(ID);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: "+ex.Message;
            }
            return Ok(response);
        }


        [HttpDelete]

        public async Task<IActionResult> DeleteAllProducts()
        {
            DeleteAllProductsResponse response= new DeleteAllProductsResponse();
            try
            {
                response =  await _managingInventory.DeleteAllProducts();
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: "+ex.Message;
            }
            return Ok(response);
        }
    }
    
}