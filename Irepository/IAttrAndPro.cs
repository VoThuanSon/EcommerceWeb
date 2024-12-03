using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IAttrAndPro
    {
        List<AttrAndPro> attrAndPros();
        List<AttrAndPro> attrOfPro(int id);
         void Create(AttrAndPro attr);
        void Update(AttrAndPro attrAndPro);
        void Delete(int proid);
        AttrAndPro Get(int proid);
    }
}
