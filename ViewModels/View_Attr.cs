using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public enum Displaytype
    {
        Radio, Checkbox
    }
    public class View_Attr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DisplayType displayType { get; set; }

    }
}
