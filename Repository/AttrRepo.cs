using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class AttrRepo : IAttrRepo
    {
        private readonly ShopContext _shopContext;
        public AttrRepo(ShopContext context) { 
            _shopContext = context;
        }

        public void Create(Attribute_Pro attr)
        {
            _shopContext.Attributes.Add(attr);
            _shopContext.SaveChanges();
        }

        public void Delete(Attribute_Pro attribute)
        {
            _shopContext.Attributes.Remove(attribute);
            _shopContext.SaveChanges();
        }

        public Attribute_Pro Find(int id)
        {
            return _shopContext.Attributes.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Attribute_Pro attribute)
        {
            _shopContext.Attributes.Update(attribute);
            _shopContext.SaveChanges();
        }

        List<Attribute_Pro> IAttrRepo.GetAttribute()
        {
           return _shopContext.Attributes.ToList();
        }
    }
}
