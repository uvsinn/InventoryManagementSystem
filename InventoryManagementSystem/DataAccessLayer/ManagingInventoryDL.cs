using Amazon.Runtime.Internal;
using InventoryManagementSystem.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace InventoryManagementSystem.DataAccessLayer
{
    public class ManagingInventoryDL:IManagingInventoryDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertProductRequest> _mongoCollection;





        public ManagingInventoryDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _mongoDatabase.GetCollection<InsertProductRequest>(_configuration["DatabaseSettings:CollectionName"]);
        }








        public async Task<InsertProductResponse> InsertProduct(InsertProductRequest request)
        {
            InsertProductResponse response = new InsertProductResponse();
            response.IsSuccess = true;
            response.message = "Data Successfully Inserted";

            try
            {
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message= "Exception: "+ ex.Message;
            }
            return response;
        }







        public async Task<GetAllProductsResponse> GetAllProducts()
        {
            GetAllProductsResponse response = new GetAllProductsResponse();
            response.IsSuccess = true;
            response.message = "Data Successfully Fetched";

            try
            {
                response.data = new List<InsertProductRequest>();
                response.data = await _mongoCollection.Find(x => true).ToListAsync();
                if(response.data.Count==0)
                {
                    response.message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }








        public async Task<GetProductByIdResponse> GetProductById(string ID)
        {
            GetProductByIdResponse response = new GetProductByIdResponse();
            response.IsSuccess = true;
            response.message = "Data Fetched by ID Successfully";

            try
            {
                if(ID.Length != 24)
                {
                    throw new Exception("Length of ID is not equal to 24, check and insert again");
                }

                response.data = await _mongoCollection.Find(x => x.ID==ID).FirstOrDefaultAsync();
                if(response.data==null)
                {
                    response.message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }










        public async Task<GetProductByNameResponse> GetProductByName(string Name)
        {
            GetProductByNameResponse response = new GetProductByNameResponse();
            response.IsSuccess = true;
            response.message = "Data Fetched by Name Successfully";

            try
            {
                response.data = new List<InsertProductRequest>(); 
                response.data=await _mongoCollection.Find(x => x.Name == Name).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }









        public async Task<UpdateProductByIdResponse> UpdateProductById(InsertProductRequest request)
        {
            UpdateProductByIdResponse response= new UpdateProductByIdResponse();
            response.IsSuccess = true;
            response.message = "Data Updated Successfully";

            try
            {
                GetProductByIdResponse request1= await GetProductById(request.ID);
                if(request1.data==null)
                {
                    throw new Exception("No record found with the given Product ID");
                }
                var Result = _mongoCollection.ReplaceOneAsync(x => x.ID == request.ID, request);
            }
            catch (Exception ex)
            {
                response.IsSuccess=false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }








        public async Task<UpdateAmountByIdResponse> UpdateAmountById(UpdateAmountByIdRequest request)
        {
            UpdateAmountByIdResponse response = new UpdateAmountByIdResponse();
            response.IsSuccess = true;
            response.message = "Data Updated Successfully";

            try
            {
                var filter = new BsonDocument().Add("Amount", request.Amount);

                var UpdatedAmount = new BsonDocument("$set", filter);

                GetProductByIdResponse request1 = await GetProductById(request.ID);
                if (request1.data == null)
                {
                    throw new Exception("No record found with the given Product ID");
                }
                var Result = _mongoCollection.UpdateOneAsync(x => x.ID == request.ID, UpdatedAmount);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }








        public async Task<DeleteProductByIdResponse> DeleteProductById(string ID)
        {
            DeleteProductByIdResponse response = new DeleteProductByIdResponse();
            response.IsSuccess = true;
            response.message = "Product Deleted Successfully";
            try
            {
                GetProductByIdResponse response1 = await GetProductById(ID);

                if (response1.data == null)
                {
                    throw new Exception("No record found associated to the given Product IDg ");
                }

                var Result= _mongoCollection.DeleteOneAsync(x=> x.ID == ID); 

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }









        public async Task<DeleteAllProductsResponse> DeleteAllProducts()
        {
            DeleteAllProductsResponse response = new DeleteAllProductsResponse();
            response.IsSuccess = true;
            response.message = "All Products Deleted Successfully";
            try
            {
                GetAllProductsResponse response1 = await GetAllProducts();

                if (response1.data.Count == 0)
                {
                    throw new Exception("No records found in the Database");
                }
                var Result = _mongoCollection.DeleteManyAsync(x=> true);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = "Exception: " + ex.Message;
            }
            return response;
        }
    }
}
