using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClothes.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int ParentId { get; set; }

    }
}
