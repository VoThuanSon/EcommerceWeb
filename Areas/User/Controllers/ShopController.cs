using Microsoft.AspNetCore.Mvc;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.ViewModels;

namespace WebClothes.Areas.User.Controllers
{
    [Area("User")]

    public class ShopController: Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        public ShopController(IProUnitOfWork proUnitOfWork)
        {
            _proUnitOfWork = proUnitOfWork;
        }
        public IActionResult Index()
        {
            var mode = _proUnitOfWork.Product.GetProducts();
            return View(mode);
        }
        public IActionResult ProDetail(int id)
        {
            var pro = _proUnitOfWork.Product.GetProductById(id);
            var prodetail = new ProductDetail();
            prodetail.ProductsRelate = _proUnitOfWork.Product.GetProductRelate(pro.Id);
            prodetail.Product = pro;
            prodetail.Imgs = _proUnitOfWork.imgRepo.GetImgPro(pro.Id);
            var attrandpor = _proUnitOfWork.AttrAndPro.attrOfPro(pro.Id);
            var listattr = new List<Attribute_Pro>();
            foreach(var i in attrandpor)
            {
                var s = _proUnitOfWork.Attr.Find(i.AtrrId);
                listattr.Add(s);
            }
            prodetail.attribute_Pros = listattr;
            return View(prodetail);
        }
    }
}
