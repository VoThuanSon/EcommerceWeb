using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ShopContext _shopContext;
        public ProductRepo(ShopContext context)
        {
            _shopContext = context;
        }

        public void CreateProduct(Product product)
        {
            _shopContext.Products.Add(product);
            _shopContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _shopContext.Products.Remove(product);
            _shopContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _shopContext.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProductByName(string name)
        {
            return _shopContext.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public List<Product> GetProductRelate(int id)
        {
            return _shopContext.Products.Where(p => p.Id != id).ToList();   
        }

        public List<Product> GetProducts()
        {
            return _shopContext.Products.ToList();
        }

        public int LastPro()
        {
            var pro =  _shopContext.Products.OrderBy(p => p.Id).LastOrDefault();
            if(pro != null)
            {
                return pro.Id + 1;
            }
            return 1;
        }

        public void UpdateProduct(Product product)
        {
            _shopContext.Products.Update(product);
            _shopContext.SaveChanges();

        }


    }
}
