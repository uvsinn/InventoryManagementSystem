using InventoryManagementSystem.Controllers;
using InventoryManagementSystem.DataAccessLayer;
using InventoryManagementSystem.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Core.Operations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace XunitTestInventory.Tests.Controllers
{
    public class InventoryControllerTest
    {
        [Fact]
        public async Task Get_LoadProductsFromInventory()
        {
            // Arrange

            //Mock<IMongoCollection<InsertProductRequest>> mockManagingInventory = new Mock<IMongoCollection<InsertProductRequest>>();

            var expected = new List<InsertProductRequest>
            {
                new InsertProductRequest{},
                new InsertProductRequest{}
            };

            var fluentMock = new Mock<IFindFluent<InsertProductRequest, InsertProductRequest>>();
            ////fluentMock.Setup(cursor => cursor.ToList()).Returns(Task.FromResult(expected));
            //mockManagingInventory.Setup(x => x.Find(null,null)).Returns(fluentMock.Object);

            var fakeCollection = new Mock<IMongoCollection<InsertProductRequest>>();
            fakeCollection.Setup(x => x.Find(It.IsAny<FilterDefinition<InsertProductRequest>>(), It.IsAny<FindOptions>()))
                .Returns(fluentMock.Object);

            var fakeDatabase = new Mock<IMongoDatabase>();
            fakeDatabase.Setup(x => x.GetCollection<InsertProductRequest>("YourCollectionName", It.IsAny<MongoCollectionSettings>()))
                .Returns(fakeCollection.Object);



            IManagingInventoryDL managingInventory = new ManagingInventoryDL(null);
            var InventoryController = new InventoryController(managingInventory);

            // Act
            var resultTask = InventoryController.GetAllProducts();
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }



        [Fact]
        public async Task Get_GetProductById()
        {
            // Arrange
            string ID = "";
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.GetProductById(ID)).ReturnsAsync(new GetProductByIdResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.GetProductById(ID);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            
            Assert.NotNull(OkResult.Value);

        }




        [Fact]
        public async Task Get_GetProductByName()
        {
            // Arrange
            string Name = "";
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.GetProductByName(Name)).ReturnsAsync(new GetProductByNameResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.GetProductByName(Name);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }



        [Fact]
        
        public async Task Post_InsertProductIntoInventory()
        {
            // Arrange
            InsertProductRequest request = new InsertProductRequest();
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.InsertProduct(request)).ReturnsAsync(new InsertProductResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.InsertProduct(request);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);
        }


        [Fact]
        public async Task Put_UpdateProductById()
        {
            // Arrange
            InsertProductRequest request = new InsertProductRequest();
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.UpdateProductById(request)).ReturnsAsync(new UpdateProductByIdResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.UpdateProductById(request);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }



        [Fact]
        public async Task Put_UpdateAmountById()
        {
            // Arrange
            UpdateAmountByIdRequest request = new UpdateAmountByIdRequest();
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.UpdateAmountById(request)).ReturnsAsync(new UpdateAmountByIdResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.UpdateAmountById(request);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }




        [Fact]
        public async Task Delete_DeleteProductById()
        {
            // Arrange
            string request = "";
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.DeleteProductById(request)).ReturnsAsync(new DeleteProductByIdResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.DeleteProductById(request);
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }



        [Fact]
        public async Task Delete_DeleteAllProducts()
        {
            // Arrange
            Mock<IManagingInventoryDL> mockManagingInventory = new Mock<IManagingInventoryDL>();

            mockManagingInventory.Setup(x => x.DeleteAllProducts()).ReturnsAsync(new DeleteAllProductsResponse());

            var InventoryController = new InventoryController(mockManagingInventory.Object);

            // Act
            var resultTask = InventoryController.DeleteAllProducts();
            var result = await resultTask;
            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(OkResult.Value);

        }
    }
}
