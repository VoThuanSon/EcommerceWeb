using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IImgRepo
    {
        List<Img> GetImg();
        List<Img> GetImgResId(int res_id);

        Img GetId(int id);

        void Create (Img img);  
        void Update (Img img);
        void Delete (Img img);
    }
}
