using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ShopContext _shopContext;
        public CategoryRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(Category category)
        {
            _shopContext.Categories.Add(category);
            _shopContext.SaveChanges();

        }

        public void Delete(Category category)
        {
            _shopContext.Categories.Remove(category);
            _shopContext.SaveChanges();
        }

        public Category Get(int? id)
        {
            return _shopContext.Categories.FirstOrDefault(p => p.Id == id);
        }

        public List<Category> GetCategories()
        {
           return _shopContext.Categories.ToList();
        }

        public List<Category> GetChild(int id)
        {
            return _shopContext.Categories.Where(p => p.ParentId == id).ToList();   
        }

        public List<Category> GetNoParent()
        {
            return _shopContext.Categories.Where(p => p.ParentId  == 0).ToList();
        }

        public void Update(Category category)
        {
            _shopContext.Categories.Update(category);
            _shopContext.SaveChanges();
        }

        
    }
}
