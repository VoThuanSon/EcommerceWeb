using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class ViewUserProfile
    {
        public Img Img { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }
}
