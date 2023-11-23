using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public Manager<Orderlist> orderManager;
        public Manager<Inventory> inventoryManager;
        public OrderController()
        {
            orderManager = new Manager<Orderlist>();
            inventoryManager = new Manager<Inventory>();
        }

        //GET
        [HttpGet]
        public Orderlist ShowOrderList()

        {
            Orderlist OrderList = orderManager.Load();
            return OrderList;
        }

        //PUT
        [HttpPut]
        public void UpdateAnOrder(Guid id, [FromBody] Order order)
        {
            try
            {
                //Loading inventory and orders from json file
                Orderlist OrderList = orderManager.Load();
                Inventory inventory = inventoryManager.Load();

                //Running a check on the list of orders to check weather the required product exists in the list or not

                Order ord = OrderList.Items.FirstOrDefault(o => o.OrderId == id);

                //Releasing product quantity in inventory

                List<AddProduct> temp = ord.OrderedProduct.ToList();

                
                for (int i = 0; i < temp.Count; i++)
                {
                    Product p = inventory.Products.FirstOrDefault(q => q.ID == temp[i].ID);

                    p.Quantity += temp[i].Quantity;
                }

                //Updating the order values

                List<AddProduct> second_temp = order.OrderedProduct.ToList();

                double total = 0;

                try
                {
                    for (int i = 0; i < second_temp.Count; i++)
                    {
                        Product p = inventory.Products.FirstOrDefault(p => p.ID == second_temp[i].ID);
                        
                        if (p.Quantity < second_temp[i].Quantity)
                        {
                            Console.WriteLine("Quantity is limited");
                            return;
                        }

                        p.Quantity -= second_temp[i].Quantity;
                        total += second_temp[i].Quantity * p.Amount;
                    }
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine("Invalid product added in order");
                }

                ord.OrderedProduct = order.OrderedProduct;
                ord.Total_Amount = total;
                orderManager.Save(OrderList);
                inventoryManager.Save(inventory);
            }
            catch(NullReferenceException NRE)
            {
                Console.WriteLine("No such order exists");
            }
        }



        //POST
        [HttpPost]
        public void PlaceAnOrder([FromBody] Order order)
        {
            //Loading inventory and orders from json file

            Inventory inventory = inventoryManager.Load();
            Orderlist OrderList = orderManager.Load();

            //Storing the products of placed order in temp to run a check whether those products actually exist in the inventory or not

            List<AddProduct> temp=order.OrderedProduct.ToList();
            double final_amount = 0;

            try
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    Product p = inventory.Products.FirstOrDefault(p => p.ID == temp[i].ID);
                    
                    if (p.Quantity < temp[i].Quantity)
                    {
                        Console.WriteLine("Quantity is limited");
                        return;
                    }
                    //Assigning values of existing product to the order
                    temp[i].Name = p.Name;
                    temp[i].Amount = p.Amount;
                    temp[i].Description = p.Description;
                    temp[i].IsAvailable = p.IsAvailable;

                    p.Quantity -= temp[i].Quantity;

                    final_amount += temp[i].Quantity * temp[i].Amount;

                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Invalid product added in order");
            }
            
            
            order.Total_Amount = final_amount;
            order.OrderDate = DateTime.Now;
            //Assigning orderid at runtime

            Guid guid = Guid.NewGuid();
            order.OrderId = guid;
            
            OrderList.Items.Add(order);

            //Saving inventory and orders to json file
            inventoryManager.Save(inventory);
            orderManager.Save(OrderList);
        }



        //DELETE
        [HttpDelete]
        public void DeleteAnOrder(Guid id)
        {
            try
            {
                //Loading orders from json file
                Orderlist OrderList = orderManager.Load();
                Inventory inventory = inventoryManager.Load();

                //Searching the reqired order through the list of orders to perform the delete operation
                Order ord = OrderList.Items.FirstOrDefault(o => o.OrderId == id);
      
                List<AddProduct> temp = ord.OrderedProduct.ToList();

                //Updating inventory regarding the order deletion
                for (int i = 0; i < temp.Count; i++)
                {
                    Product p = inventory.Products.FirstOrDefault(p => p.ID == temp[i].ID);

                    p.Quantity -= temp[i].Quantity;
                }

                OrderList.Items.Remove(ord);

                //Updating orders to json file
                orderManager.Save(OrderList);
                inventoryManager.Save(inventory);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No such order exists");
            }
            
        }
    }
}