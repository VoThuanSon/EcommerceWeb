namespace WebClothes.Models
{
    public class SaleOrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int SaleOrderId { get; set; }
        public Product Product { get; set; }
        public string Variant { get; set; }
        public int Quantity {  get; set; }
    }
}
