namespace InventoryManagementSystem.Models.DatabaseModels
{
    public class GetAllProductsResponse
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
        public List<InsertProductRequest> data { get; set; }
    }
}
