using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; }
    }
}
