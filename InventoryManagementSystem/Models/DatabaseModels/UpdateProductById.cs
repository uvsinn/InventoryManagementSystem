using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DatabaseModels
{
    public class UpdateProductByIdRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public InsertProductRequest data { get; set; }
    }
    public class UpdateProductByIdResponse
    {
        public bool IsSuccess { get; set; } 
        public string message {  get; set; }
    }
}














