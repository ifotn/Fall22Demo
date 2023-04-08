namespace Fall22Demo.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set;  } = String.Empty;
        public double Price { get; set; }

        public string? Photo { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
