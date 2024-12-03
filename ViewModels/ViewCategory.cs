using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class ViewCategory
    {
        [Key]
        [Column(Order = 1)]

        public int Id { get; set; }

        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
