using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IWardRepo 
    {
        List<Ward> Wards();
        List<Ward> WardOfDis(int districtId);
        Ward Get(int id);
        void Create(Ward ward);
        void Update(Ward ward);
        void Delete(Ward ward);
    }
}
