using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class DistrictRepo : IDistrictRepo
    {
        private readonly ShopContext _shopContext;
        public DistrictRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(District district)
        {
            _shopContext.Districts.Add(district);
            _shopContext.SaveChanges();
        }

        public void DeleteDistrict(District district)
        {
            _shopContext.Districts.Remove(district);
            _shopContext.SaveChanges();
        }

        public List<District> GetDisOfPro(int provinceId)
        {
            return _shopContext.Districts.Where(d => d.ProvinceId == provinceId).ToList();
        }

        public District GetDistrict(int id)
        {
            return _shopContext.Districts.FirstOrDefault(p => p.Id == id);
        }

        public List<District> GetDistricts()
        {
            return _shopContext.Districts.ToList();
        }

        public void Update(District district)
        {
            _shopContext.Districts.Update(district);
            _shopContext.SaveChanges();
        }
    }
}
