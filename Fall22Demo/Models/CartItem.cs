using System.ComponentModel.DataAnnotations;

namespace Fall22Demo.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string? CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
