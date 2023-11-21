using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public OrderManager orderManager;
        public InventoryManager inventoryManager;
        public OrderController()
        {
            orderManager = new OrderManager();
            inventoryManager = new InventoryManager();
        }


        //GET
        [HttpGet]
        public Orderlist GetOrder()

        {
            Orderlist OrderList = orderManager.LoadOrders();
            return OrderList;
        }



        //PUT
        [HttpPut]
        public void PutOrder(Guid id, [FromBody] Order order)
        {
            //Loading inventory and orders from json file
            Orderlist OrderList = orderManager.LoadOrders();
            Inventory inventory = inventoryManager.LoadInventory();

            //Running a check on the list of orders to check weather the required product exists in the list or not

            Order ord = OrderList.Items.FirstOrDefault(o => o.OrderId == id);

            if (ord == null)
            {
                Console.WriteLine("No such order exists");
                return;
            }

            //Releasing product quantity in inventory

            List<AddProduct> temp= ord.OrderedProduct.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                Product p = inventory.Products.FirstOrDefault(q => q.ID == temp[i].ID);

                p.Quantity+= temp[i].Quantity;
            }

            //Updating the order values

            List<AddProduct> second_temp = order.OrderedProduct.ToList();

            double total = 0;
            for (int i = 0; i < second_temp.Count; i++)
            {
                Product p = inventory.Products.FirstOrDefault(p => p.ID == second_temp[i].ID);
                if (p == null)
                {
                    Console.WriteLine("Invalid product added in order");
                    return;
                }
                else if (p.Quantity < second_temp[i].Quantity)
                {
                    Console.WriteLine("Quantity is limited");
                    return;
                }

                p.Quantity -= second_temp[i].Quantity;
                total += second_temp[i].Quantity *p.Amount;
            }

            ord.OrderedProduct = order.OrderedProduct;
            ord.Total_Amount = total;
            orderManager.SaveOrders(OrderList);
            inventoryManager.SaveInventory(inventory);

        }



        //POST
        [HttpPost]
        public void PostOrder([FromBody] Order order)
        {
            //Loading inventory and orders from json file

            Inventory inventory = inventoryManager.LoadInventory();
            Orderlist OrderList = orderManager.LoadOrders();

            //Storing the products of placed order in temp to run a check whether those products actually exist in the inventory or not

            List<AddProduct> temp=order.OrderedProduct.ToList();
            double final_amount = 0;

            for (int i = 0; i < temp.Count; i++)
            {
                Product p=inventory.Products.FirstOrDefault(p => p.ID == temp[i].ID);
                if(p == null)
                {
                    Console.WriteLine("Invalid product added in order");
                    return;
                }
                else if (p.Quantity < temp[i].Quantity)
                {
                    Console.WriteLine("Quantity is limited");
                    return;
                }
                //Assigning values of existing product to the order
                temp[i].Name= p.Name;
                temp[i].Amount= p.Amount;
                temp[i].Description = p.Description;
                temp[i].IsAvailable = p.IsAvailable;

                p.Quantity -= temp[i].Quantity;

                final_amount += temp[i].Quantity * temp[i].Amount;
                
            }
            
            order.Total_Amount = final_amount;
            order.OrderDate = DateTime.Now;
            //Assigning orderid at runtime
            Guid guid = Guid.NewGuid();
            order.OrderId = guid;
            
            OrderList.Items.Add(order);

            //Saving inventory and orders to json file
            inventoryManager.SaveInventory(inventory);
            orderManager.SaveOrders(OrderList);
        }



        //DELETE
        [HttpDelete]
        public void DeleteOrder(Guid id)
        {
            //Loading orders from json file
            Orderlist OrderList = orderManager.LoadOrders();
            Inventory inventory = inventoryManager.LoadInventory();

            //Searching the reqired order through the list of orders to perform the delete operation
            Order ord = OrderList.Items.FirstOrDefault(o => o.OrderId == id);

            if (ord == null)
            {
                Console.WriteLine("No such order exists");
                return;
            }
            List<AddProduct> temp = ord.OrderedProduct.ToList();

            //Updating inventory regarding the order deletion
            for (int i = 0; i < temp.Count; i++)
            {
                Product p = inventory.Products.FirstOrDefault(p => p.ID == temp[i].ID);
                
                p.Quantity -= temp[i].Quantity;               
            }

            OrderList.Items.Remove(ord);

            //Updating orders to json file
            orderManager.SaveOrders(OrderList);
            inventoryManager.SaveInventory(inventory);
        }
    }
}