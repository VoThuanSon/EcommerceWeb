using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClothes.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UomId { get; set; }
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
        public ProductUom Uom { get; set; }
        public List<SaleOrder> saleOrders { get; set; }
        public List<Attribute_Pro> attribute_Pros {  get; set; }
        public Category Category { get; set; }

    }
}
