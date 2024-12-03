using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class AttrAndProRepo : IAttrAndPro
    {
        private readonly ShopContext _shopContext;
        public AttrAndProRepo(ShopContext context)
        {
            _shopContext = context;

        }
     
        public void Create(AttrAndPro attr)
        {
            _shopContext.AttrAndPro.Add(attr);
            _shopContext.SaveChanges();
        }

        public List<AttrAndPro> attrAndPros()
        {
            return _shopContext.AttrAndPro.ToList();
        }

        public void Update(AttrAndPro attrAndPro)
        {
            _shopContext.AttrAndPro.Update(attrAndPro);
            _shopContext.SaveChanges();
        }

        public void Delete(int proid)
        {
            var s = Get(proid);
            _shopContext.AttrAndPro.Remove(s);
            _shopContext.SaveChanges() ;
            
        }

        public AttrAndPro Get(int proid)
        {
            return _shopContext.AttrAndPro.FirstOrDefault(p => p.Id == proid);
        }

        public List<AttrAndPro> attrOfPro(int id)
        {
            return _shopContext.AttrAndPro.Where(p => p.ProductId == id).ToList();
        }
    }
}
