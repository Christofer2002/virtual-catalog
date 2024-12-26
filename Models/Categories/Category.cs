using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Models.Categories
{
    public class Category
    {
        public long Id { get; set; } 
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
