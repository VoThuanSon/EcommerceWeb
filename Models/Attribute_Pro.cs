using System.ComponentModel.DataAnnotations;

namespace WebClothes.Models
{
    public enum DisplayType
    {
        Radio, Checkbox
    }
    public class Attribute_Pro
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DisplayType DisplayType { get; set; }

        public List<Attribute_Value> Values { get; set; }
    }
}
