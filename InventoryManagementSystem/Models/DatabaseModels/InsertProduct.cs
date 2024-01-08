using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.DatabaseModels
{
    public class InsertProductRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [Required]
        [BsonElement("Name")]
        public string? Name { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
    public class InsertProductResponse
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
    }
}
