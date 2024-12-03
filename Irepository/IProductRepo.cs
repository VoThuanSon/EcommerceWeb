using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IProductRepo
    {
        List<Product> GetProducts();
        List<Product> GetProductRelate(int id);
        Product GetProductById(int id);
        Product GetProductByName(string name);
        int LastPro();
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void CreateProduct(Product product);
    }
}
