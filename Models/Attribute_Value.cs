namespace WebClothes.Models
{
    public class Attribute_Value
    {
        public int Id {  get; set; }
        public string Name {  get; set; }

        public int Attribute_Id { get; set; }
        public float Fixed_Price {  get; set; }
    }
}
