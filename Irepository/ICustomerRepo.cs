using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ICustomerRepo
    {
        List<CustomerProfile> GetProfiles();
        CustomerProfile GetProfileById(int id);
        void UpdateProfile(CustomerProfile profile);
        void DeleteProfile(CustomerProfile profile);
        void CreateProfile(CustomerProfile profile);    
    }
}
