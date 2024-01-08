using DeepEqual.Syntax;
using InventoryManagementSystem.Controllers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace InventoryManagement.Tests.Controllers
{
    [TestFixture]
    public class InventoryControllerTests
    {
        //private InventoryController? _controller;
        //private Inventory? _inventory;
        //private Imanager<Inventory>? _manager;
        //Product? product1;
        //Product? product2;


        //[SetUp]
        //public void Setup()
        //{
        //    Imanager<Inventory> _InventoryManager = new Manager<Inventory>();
        //    _controller = new InventoryController(_InventoryManager);

        //    _inventory = new Inventory();

        //    Imanager<Inventory> _inventoryManager = new Manager<Inventory>();
        //    _manager = _inventoryManager;

        //    //for POST
        //    product2 = new Product
        //    {
        //        ID = 1001,
        //        Name = "TestProduct",
        //        Amount = 10.0,
        //        Description = "Test Description",
        //        IsAvailable = true,
        //        Quantity = 5
        //    };

        //    //for PUT

        //    product1 = new Product
        //    {
        //        ID = 1001,
        //        Name = "phone",
        //        Amount = 10000,
        //        Description = "iphone",
        //        IsAvailable = true,
        //        Quantity = 80
        //    };
        //}

        ////[Test]
        ////public async Task Get_LoadProductsFromInventory()
        ////{
        ////    // Arrange
        ////    _inventory = await _manager.Load();
        ////    // Act
        ////    Inventory TestInventory = await _controller.ShowInventoryProducts();

        ////    // Assert
        ////    Assert.IsNotNull(TestInventory);
        ////    _inventory.ShouldDeepEqual(TestInventory);
        ////}
        ////[Test]
        ////public async Task Post_AddProductToInventory()
        ////{
        ////    // Arrange

        ////    // Act
        ////    _controller.AddProductToInventory(product2);

        ////    // Assert
        ////    _inventory = await _manager.Load();
        ////    Assert.IsNotNull(_inventory);
        ////    var testproduct = _inventory.Products.FirstOrDefault(p => p.ID == product2.ID);
        ////    Assert.IsNotNull(testproduct, "Product added successfully");
        ////}

        ////[Test]
        ////[TestCase(1001)]

        ////public async Task Put_UpdatesProductInInventory(int productId)
        ////{
        ////    // Arrange

        ////    // Act
        ////    _controller.UpdateProduct(productId, product1);

        ////    // Assert
        ////    _inventory = await _manager.Load();
        ////    Assert.IsNotNull(_inventory);

        ////    var TestProduct = _inventory.Products.FirstOrDefault(p => p.ID == product1.ID);

        ////    Assert.IsNotNull(TestProduct, "Product updated successfully");

        ////    Assert.AreEqual(product1.Name, TestProduct.Name);
        ////    Assert.AreEqual(product1.Amount, TestProduct.Amount);
        ////    Assert.AreEqual(product1.Description, TestProduct.Description);
        ////    Assert.AreEqual(product1.IsAvailable, TestProduct.IsAvailable);
        ////    Assert.AreEqual(product1.Quantity, TestProduct.Quantity);
        ////}

        //[TestCase(4)]
        //public async Task Delete_RemovesProductFromInventory(int productId)
        //{
        //    // Arrange

        //    // Act
        //    _controller.DeleteProductFromInventory(productId);

        //    // Assert
        //    _inventory = await _manager.Load();
        //    Assert.IsNotNull(_inventory, "Inventory ins not NULL");

        //    var testProduct = _inventory.Products.Find(p => p.ID == productId);
        //    Assert.IsNull(testProduct, "Product Successfully Deleted");
        //}

    }
}
