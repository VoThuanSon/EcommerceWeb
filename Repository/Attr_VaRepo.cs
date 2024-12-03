using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class Attr_VaRepo : IAttr_VaRepo
    {
        private readonly ShopContext _shopContext;
        public Attr_VaRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(Attribute_Value attributeValue)
        {
            _shopContext.Attribute_Values.Add(attributeValue);
            _shopContext.SaveChanges();
        }

        public void Delete(Attribute_Value attributeValue)
        {
            _shopContext.Attribute_Values.Remove(attributeValue);
            _shopContext.SaveChanges();
        }

        public List<Attribute_Value> Get()
        {
            
            return _shopContext.Attribute_Values.ToList();
        }

        public List<Attribute_Value> GetAttribute_Values(int id)
        {
            return _shopContext.Attribute_Values.Where(p => p.Attribute_Id == id).ToList();
        }

        public Attribute_Value GetID(int id)
        {
            return _shopContext.Attribute_Values.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Attribute_Value attributeValue)
        {
            _shopContext.Attribute_Values.Update(attributeValue);
            _shopContext.SaveChanges();
        }
    }
}
