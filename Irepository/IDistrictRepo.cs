using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IDistrictRepo
    {
        List<District> GetDistricts();
        List<District> GetDisOfPro(int provinceId);
        District GetDistrict(int id);
        void DeleteDistrict(District district);
        void Create(District district);
        void Update(District district);
    }
}
