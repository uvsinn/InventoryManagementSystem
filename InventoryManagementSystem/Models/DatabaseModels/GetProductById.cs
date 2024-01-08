namespace InventoryManagementSystem.Models.DatabaseModels
{
    public class GetProductByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }

        public InsertProductRequest data {  get; set; }
    }
}
