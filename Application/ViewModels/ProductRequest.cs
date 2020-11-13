using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ProductRequest
    {
        [MinLength(3, ErrorMessage = "Name is too small"), MaxLength(50, ErrorMessage = "Name is too big!")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public int Amount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Unit price should be greater than R$ 0,00")]
        public double UnitPrice { get; set; }
    }
}