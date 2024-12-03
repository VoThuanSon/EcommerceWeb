using Microsoft.AspNetCore.Mvc;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.ViewModels;

namespace WebClothes.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController:Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        public HomeController(IProUnitOfWork proUnitOfWork)
        {
            _proUnitOfWork = proUnitOfWork;
        }
        public IActionResult Index()
        {
            var mode =_proUnitOfWork.Product.GetProducts();
            return View(mode);
        }
        [HttpGet]
        public List<Attribute_Value> GetAttr_Va(int id)
        {
            return _proUnitOfWork.Attr_Va.GetAttribute_Values(id);
        }
        public IActionResult Menu()
        {
            var category_no_parent = _proUnitOfWork.Category.GetNoParent();
            var menu = new List<Menu>();
            var categoryChild = new List<Category>();
            foreach(var i in category_no_parent)
            {
                var me = new Menu();
                me.Category = i;
                var child = _proUnitOfWork.Category.GetChild(i.Id);
                if(child != null) 
                {
                    me.ChildCategory = child;
                }               
                menu.Add(me);
            }
            return PartialView("Menu",menu);
        }
    }
}
