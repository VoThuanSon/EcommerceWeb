using System.Text.Json.Serialization;
using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class View_Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UomId { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<Attribute_Pro> Attr {  get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string DesShort { get; set; }
        public string DesLong { get; set; }

        public string SKU { get; set; }
        public float Sale { get; set; }
        public double Price { get; set; }
        public float CapitalPrice { get; set; }
        public float SellPrice { get; set; }
        public string Barcode { get; set; }
    }
}
