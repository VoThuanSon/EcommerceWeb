using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class Menu
    {
        public Category Category { get; set; }
        public List<Category> ChildCategory { get; set;}
    }
}
