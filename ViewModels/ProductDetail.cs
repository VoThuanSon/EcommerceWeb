using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class ProductDetail
    {
        public Product Product { get; set; }
        public List<Img> Imgs { get; set; }

        public List<Attribute_Pro> attribute_Pros { get; set; }
        public List<Product> ProductsRelate { get; set; }
    }
}
