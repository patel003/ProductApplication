using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
      
        public string ProductDescription { get; set; } = null!;
        public string ProductCategory { get; set; }
        public bool IsExpired { get; set; }
        public DateTime DateTime { get; set; }
    }
}
