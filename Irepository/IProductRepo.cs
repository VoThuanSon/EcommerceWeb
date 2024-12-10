using WebClothes.Models;
using WebClothes.ViewModels;

namespace WebClothes.Irepository
{
    public interface IProductRepo
    {
        List<Product> GetProducts();
        List<Product> GetProductRelate(int id);
        IQueryable<Product> GetProductCategory(int? id);
        Product GetProductById(int id);
        int LastPro();
        IQueryable<Product> GetProductByName(string name);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void CreateProduct(Product product);
    }
}
