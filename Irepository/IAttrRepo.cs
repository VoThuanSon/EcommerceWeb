using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IAttrRepo
    {
        List<Attribute_Pro> GetAttribute();
        void Create(Attribute_Pro attribute);
        void Update(Attribute_Pro attribute);
        void Delete(Attribute_Pro attribute);
        Attribute_Pro Find(int id);

    }
}
