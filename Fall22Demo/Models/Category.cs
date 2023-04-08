using Fall22Demo.Data;
namespace Fall22Demo.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }

}
