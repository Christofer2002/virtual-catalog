using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Models.Products
{
    public class Product
    {
        public long Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal Price { get; set; } 
        public int Stock { get; set; }
        public long CategoryId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Category? Category { get; set; }
    }

}
