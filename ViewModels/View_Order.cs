using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime Date_Order { get; set; }
        public string Address { get; set; }


        public int ProvinceId { get; set; }
        public int DistrictId{ get; set; }

        public int WardId { get; set; }
        public string Status { get; set; }
        public List<Product> products { get; set; }
    }
}
