using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ICategoryRepo   {
        List<Category> GetCategories();
        List<Category> GetNoParent();
        List<Category> GetChild(int id);    
        void Create(Category category);
        void Delete(Category category);
        void Update(Category category);
        Category Get(int? id);
    }
}
