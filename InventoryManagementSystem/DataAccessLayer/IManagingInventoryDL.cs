using InventoryManagementSystem.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.DataAccessLayer
{
    public interface IManagingInventoryDL
    {
        public Task<InsertProductResponse> InsertProduct(InsertProductRequest request);
        public Task<GetAllProductsResponse> GetAllProducts();

        public Task<GetProductByIdResponse> GetProductById(string ID);
        public Task<GetProductByNameResponse> GetProductByName(string ID);
        public Task<UpdateProductByIdResponse> UpdateProductById(InsertProductRequest request);
        public Task<UpdateAmountByIdResponse> UpdateAmountById(UpdateAmountByIdRequest request);
        public Task<DeleteProductByIdResponse> DeleteProductById(string ID);
        public Task<DeleteAllProductsResponse> DeleteAllProducts();

    }
}
