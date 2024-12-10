using Microsoft.EntityFrameworkCore;
using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ShopContext _shopContext;
        public CustomerRepo(ShopContext context)
        {
            _shopContext = context;
        }


        public void CreateProfile(CustomerProfile profile)
        {
            _shopContext.CustomerProfiles.Add(profile);
            _shopContext.SaveChanges();
        }

        public void DeleteProfile(CustomerProfile profile)
        {
            _shopContext.CustomerProfiles.Remove(profile);
            _shopContext.SaveChanges();
        }

        public int GetLastId()
        {
            var pro = _shopContext.CustomerProfiles.OrderBy(p => p.Id).LastOrDefault();
            if (pro != null)
            {
                return pro.Id + 1;
            }
            return 1;

        }

        public CustomerProfile GetProfileByEmail(string email)
        {
            return _shopContext.CustomerProfiles.FirstOrDefault(p => p.Email == email);
        }

        public CustomerProfile GetProfileById(int id)
        {
            return _shopContext.CustomerProfiles.FirstOrDefault(p => p.Id  == id);
        }


        public List<CustomerProfile> GetProfiles()
        {
           return _shopContext.CustomerProfiles.ToList();
        }

        public void UpdateProfile(CustomerProfile profile)
        {
            _shopContext.CustomerProfiles.Update(profile);
            _shopContext.SaveChanges();
        }
    }
}
