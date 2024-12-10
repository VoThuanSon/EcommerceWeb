using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class ImgRepo : IImgRepo
    {
        private readonly ShopContext _shopcontext;
        public ImgRepo(ShopContext shopContext)
        {
            _shopcontext = shopContext;
        }
        public void Create(Img img)
        {
            _shopcontext.imgs.Add(img);
            _shopcontext.SaveChanges();
        }

        public void Delete(Img img)
        {
            _shopcontext.imgs.Remove(img);
            _shopcontext.SaveChanges();
        }

        public Img GetId(int id)
        {
            return _shopcontext.imgs.FirstOrDefault(p => p.Id == id);
        }

        public List<Img> GetImg()
        {
            return _shopcontext.imgs.ToList();
        }

        public List<Img> GetImgResId(int res_id)
        {
            return _shopcontext.imgs.Where(p => p.Res_Id == res_id).ToList();
        }
        public void Update(Img img)
        {
            _shopcontext.imgs.Update(img);
            _shopcontext.SaveChanges();
        }
    }
}
