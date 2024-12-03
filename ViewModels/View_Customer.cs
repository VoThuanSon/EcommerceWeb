using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string modelName { get; set; } = "Customer_customer";
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string Address { get; set; }

    }
}
