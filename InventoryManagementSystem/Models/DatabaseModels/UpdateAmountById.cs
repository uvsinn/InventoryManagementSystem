using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DatabaseModels
{
    public class UpdateAmountByIdRequest
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public int Amount { get; set; }
    }
    public class UpdateAmountByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
    }
}
