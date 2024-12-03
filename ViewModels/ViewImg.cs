using System.ComponentModel.DataAnnotations.Schema;
using WebClothes.Models;

namespace WebClothes.ViewModels
{
    public class ViewImg
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public List<IFormFile> Files { get; set; }
        public string Res_model { get; set; }
        public int Res_Id { get; set; }
    }
}
