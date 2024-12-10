using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int SaleOrderId { get; set; }
        public string Variant { get; set; }
        public int Quantity { get; set; }
    }
}
