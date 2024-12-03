using System.ComponentModel.DataAnnotations.Schema;

namespace WebClothes.Models
{
    public class Img
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Res_model { get; set; }
        public int Res_Id { get; set; }
 
    }
}
