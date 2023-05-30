using System.Text.Json.Serialization;

namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Value { get; set; }

        public int Amount { get; set; }

        public DateTime RegisterDate { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
