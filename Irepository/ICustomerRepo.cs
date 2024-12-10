using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ICustomerRepo
    {
        List<CustomerProfile> GetProfiles();
        CustomerProfile GetProfileByEmail(string email);
        CustomerProfile GetProfileById(int id);
        int GetLastId();
        void UpdateProfile(CustomerProfile profile);
        void DeleteProfile(CustomerProfile profile);
        void CreateProfile(CustomerProfile profile);    
    }
}
