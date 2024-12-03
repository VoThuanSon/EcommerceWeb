using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClothes.Models
{
    public class CustomerProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Phone { get; set; }
        public int ProvinceId {  get; set; }

        public int DistrictId {  get; set; }

        public int WardId {  get; set; }

        public string Address { get; set; }

        public List<SaleOrder> Orders { get; set; }
    }
}

