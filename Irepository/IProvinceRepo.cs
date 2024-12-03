using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IProvinceRepo
    {
        List<Province> GetProvinceList();
        Province GetProvinceById(int provinceId);
        void Create(Province province);
        void Update(Province province);
        void Delete(Province province);
    }
}
