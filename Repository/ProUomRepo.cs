using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class ProUomRepo : IProUomRepo
    {
        private readonly ShopContext _shopContext;
        public ProUomRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(ProductUom product)
        {
            _shopContext.ProductUom.Add(product);
            _shopContext.SaveChanges();
        }

        public void Delete(int? id)
        {
            var product = _shopContext.ProductUom.FirstOrDefault(p => p.Id == id);
            _shopContext.ProductUom.Remove(product);
            _shopContext.SaveChanges();
        }

        public ProductUom FindIdOum(int? id)
        {
            return _shopContext.ProductUom.FirstOrDefault(p => p.Id == id);

        }

        public List<ProductUom> GetProOum()
        {
            return _shopContext.ProductUom.ToList();
        }

        public void Update(ProductUom product)
        {
            _shopContext.ProductUom.Update(product);
            _shopContext.SaveChanges();
        }
    }
}
