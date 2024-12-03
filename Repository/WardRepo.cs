using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class WardRepo : IWardRepo
    {
        private readonly ShopContext _shopContext;
        public WardRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(Ward ward)
        {
            _shopContext.Wards.Add(ward);
            _shopContext.SaveChanges();
        }

        public void Delete(Ward ward)
        {
            _shopContext.Wards.Remove(ward);
            _shopContext.SaveChanges();
        }

        public Ward Get(int id)
        {
            return _shopContext.Wards.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Ward ward)
        {
            _shopContext.Wards.Update(ward);
            _shopContext.SaveChanges();
        }

        public List<Ward> WardOfDis(int districtId)
        {
            return _shopContext.Wards.Where(w => w.DistrictId == districtId).ToList();
        }

        public List<Ward> Wards()
        {
            return _shopContext.Wards.ToList();  
        }
    }
}
