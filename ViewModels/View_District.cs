using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public List<Ward> Wards { get; set; }
    }
}
