using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IAttr_VaRepo
    {
        List<Attribute_Value> Get();
        List<Attribute_Value> GetAttribute_Values(int id);
        void Create(Attribute_Value attributeValue);
        void Update(Attribute_Value attributeValue);
        void Delete(Attribute_Value attributeValue);
        Attribute_Value GetID(int id);
    }
}
