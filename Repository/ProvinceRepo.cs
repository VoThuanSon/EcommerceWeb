using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class ProvinceRepo : IProvinceRepo
    {
        private readonly ShopContext _shopContext;
        public ProvinceRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(Province province)
        {
            _shopContext.Provinces.Add(province);
            _shopContext.SaveChanges();
        }

        public void Delete(Province province)
        {
            _shopContext.Provinces.Remove(province);
            _shopContext.SaveChanges();
        }

        public Province GetProvinceById(int provinceId)
        {
            return _shopContext.Provinces.FirstOrDefault(p => p.Id == provinceId);
        }

        public List<Province> GetProvinceList()
        {
            return _shopContext.Provinces.ToList();
        }

        public void Update(Province province)
        {
            _shopContext.Provinces.Update(province);
            _shopContext.SaveChanges();
        }
    }
}
