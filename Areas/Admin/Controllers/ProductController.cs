using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Newtonsoft.Json.Linq;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.Repository;
using WebClothes.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace WebClothes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProUnitOfWork proUnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _proUnitOfWork = proUnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
      
        [HttpPost]
        public List<Product> GetPro()
        {
            return _proUnitOfWork.Product.GetProducts();
        }
        public IActionResult Product()
        {
            var model = _proUnitOfWork.Product.GetProducts();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Pro()
        {
            var s = _proUnitOfWork.Attr.GetAttribute();
            var model = new View_Product();
            model.Attr = s;
            var oum = _proUnitOfWork.proUomRepo.GetProOum();
            ViewBag.oum = new SelectList(oum, "Id", "Name", null);
            var cat = _proUnitOfWork.Category.GetCategories();
            ViewBag.cat = new SelectList(cat, "Id", "Name", null);
            ViewBag.attr = new SelectList(s, "Id", "Name", null);
            ViewBag.Id = _proUnitOfWork.Product.LastPro();

            return View(model);
        }
        [HttpPost]
        public IActionResult Cre_Pro(View_Product model)
        {
            var Id = _proUnitOfWork.Product.LastPro();
            if (ModelState.IsValid)
            {
                var pro = new Product();
                pro.Id = model.Id;
                pro.Name = model.Name;
                pro.Img = model.Img;
                pro.SKU = model.SKU;
                pro.Price = model.Price;
                pro.Quantity = model.Quantity;
                pro.Barcode = model.Barcode;
                pro.CategoryId = model.CategoryId;
                pro.DesShort = model.DesShort;
                pro.DesLong = model.DesLong;
                pro.Sale = model.Sale;
                pro.Price = model.Price;
                pro.SellPrice = model.SellPrice;
                pro.CapitalPrice = model.CapitalPrice;
                pro.UomId = model.UomId;
                _proUnitOfWork.Product.CreateProduct(pro);
                var imgs = new Img();
                foreach (var file in model.Files)
                {
                    var s = new Img();
                    var prId = _proUnitOfWork.Product.LastPro();
                    var path = "/Images/" + file.FileName;
                    s.Res_model = "Product";
                    s.Res_Id = Id;
                    s.Path = path;
                    _proUnitOfWork.imgRepo.Create(s);
                }

                return RedirectToAction("Product");

            }
            return RedirectToAction("Cre_Pro");
        }
        [HttpGet]
        public IActionResult EditPro(int id)
        {
            var s = _proUnitOfWork.Product.GetProductById(id);
            var model = new View_Product_Edit();
            model.Id = s.Id;
            model.UomId = s.UomId;
            model.SKU = s.SKU;
            model.Img = s.Img;
            model.Price = s.Price;
            model.Quantity = s.Quantity;
            model.CategoryId = s.CategoryId;
            model.Barcode = s.Barcode;
            model.DesLong = s.DesLong;
            model.DesShort = s.DesShort;
            model.SellPrice = s.SellPrice;
            model.CapitalPrice = s.CapitalPrice;
            model.Name = s.Name;
            model.Sale = s.Sale;
            var oum = _proUnitOfWork.proUomRepo.GetProOum();
            ViewBag.oum = new SelectList(oum, "Id", "Name", model.UomId);
            var cat = _proUnitOfWork.Category.GetCategories();
            ViewBag.cat = new SelectList(cat, "Id", "Name", model.CategoryId);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditPro(View_Product_Edit model)
        {
            if (ModelState.IsValid)
            {
                var pro = new Product();
                pro.Id = model.Id;
                pro.Name = model.Name;
                pro.Img = model.Img;
                pro.SKU = model.SKU;
                pro.Quantity = model.Quantity;
                pro.Price = model.Price;
                pro.Barcode = model.Barcode;
                pro.CategoryId = model.CategoryId;
                pro.DesShort = model.DesShort;
                pro.DesLong = model.DesLong;
                pro.Price = model.Price;
                pro.Sale = model.Sale;
                pro.SellPrice = model.SellPrice;
                pro.CapitalPrice = model.CapitalPrice;
                pro.UomId = model.UomId;
                _proUnitOfWork.Product.UpdateProduct(pro);
                return RedirectToAction("Product");
            }
            return RedirectToAction("EditPro");
        }
        [HttpGet]
        public IActionResult DeletePro(int id)
        {
            _proUnitOfWork.Product.DeleteProduct(_proUnitOfWork.Product.GetProductById(id));
            return RedirectToAction("Product");
        }
        public IActionResult Attributes()
        {
            var model = _proUnitOfWork.Attr.GetAttribute();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Attribute()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Attribute(View_Attr model)
        {

            if (ModelState.IsValid)
            {
                var mod = new Attribute_Pro();
                mod.DisplayType = model.displayType;
                mod.Name = model.Name;
                _proUnitOfWork.Attr.Create(mod);
                return RedirectToAction("Attributes");
            }
            return RedirectToAction("Cre_Attribute");
        }
        [HttpGet]
        public IActionResult EditAttr(int id)
        {
            var model = _proUnitOfWork.Attr.Find(id);
            var s = new View_Attr();
            s.displayType = model.DisplayType;
            s.Name = model.Name;
            s.Id = model.Id;
            return View(s);
        }
        [HttpPost]
        public IActionResult EditAttr(View_Attr model)
        {
            if (ModelState.IsValid)
            {
                var s = new Attribute_Pro();
                s.DisplayType = model.displayType;
                s.Name = model.Name;
                s.Id = model.Id;
                _proUnitOfWork.Attr.Update(s);
                return RedirectToAction("Attributes");
            }
            return RedirectToAction("EditAttr");
        }
        [HttpGet]
        public IActionResult DeleteAttr(int id)
        {
            _proUnitOfWork.Attr.Delete(_proUnitOfWork.Attr.Find(id));
            return RedirectToAction("Attributes");
        }
        public IActionResult ProOumView()
        {
            var model = _proUnitOfWork.proUomRepo.GetProOum();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_ProOum()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cre_ProOum(ViewPro_Oum model)
        {
            if (ModelState.IsValid)
            {
                var pro = new ProductUom();
                pro.Id = model.Id;
                pro.Name = model.Name;
                _proUnitOfWork.proUomRepo.Create(pro);
                return RedirectToAction("ProOumView");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditProOum(int? id)
        {
            var oum = _proUnitOfWork.proUomRepo.FindIdOum(id);
            return View(oum);
        }
        [HttpGet]
        public IActionResult DeleteProOum(int? id)
        {
            _proUnitOfWork.proUomRepo.Delete(id);
            return RedirectToAction("ProOumView");
        }
        [HttpPost]
        public IActionResult EditProOum(ProductUom model)
        {
            if (ModelState.IsValid)
            {
                _proUnitOfWork.proUomRepo.Update(model);
                return RedirectToAction("ProOumView");
            }
            return View(model);
        }

        public IActionResult Category()
        {
            var model = _proUnitOfWork.Category.GetCategories();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Category()
        {
            var query = _proUnitOfWork.Category.GetCategories();
            ViewBag.cat = new SelectList(query, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Category(ViewCategory model)
        {
            if (model.Name != null)
            {
                var cat = new Category();
                cat.Name = model.Name;
                cat.ParentId = model.ParentId;
                _proUnitOfWork.Category.Create(cat);
                return RedirectToAction("Category");

            }
            return RedirectToAction("Cre_Category");

        }
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            var model = _proUnitOfWork.Category.Get(id);
            var query = _proUnitOfWork.Category.GetCategories();

            ViewBag.cat = new SelectList(query, "Id", "Name", model.Id);
            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteCat(int? id)
        {
            _proUnitOfWork.Category.Delete(_proUnitOfWork.Category.Get(id));
            return RedirectToAction("Category");
        }
        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            if (model.Name != null)
            {
                _proUnitOfWork.Category.Update(model);
                return RedirectToAction("Category");
            }
            return RedirectToAction("EditCategory");
        }
        public IActionResult Attr_Val()
        {
            var model = _proUnitOfWork.Attr_Va.Get();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Attribute_Val()
        {
            var query = _proUnitOfWork.Attr.GetAttribute();
            ViewBag.Attr = new SelectList(query, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Attribute_Val(View_Attr_Val model)
        {
            if (ModelState.IsValid)
            {
                var s = new Attribute_Value();
                s.Attribute_Id = model.Attr_Id;
                s.Fixed_Price = model.Fixed_Price;
                s.Name = model.Name;
                _proUnitOfWork.Attr_Va.Create(s);
                return RedirectToAction("Attr_Val");

            }
            return RedirectToAction("Cre_Attribute_Val");
        }
        [HttpGet]
        public IActionResult Edit_AttrVal(int id)
        {
            var model = _proUnitOfWork.Attr_Va.GetID(id);
            var s = new View_Attr_Val();
            s.Attr_Id = model.Attribute_Id;
            s.Id = model.Id;
            s.Fixed_Price = model.Fixed_Price;
            s.Name = model.Name;
            var query = _proUnitOfWork.Attr.GetAttribute();
            ViewBag.Attr = new SelectList(query, "Id", "Name", s.Attr_Id);
            return View(s);
        }
        [HttpPost]
        public IActionResult Edit_AttrVal(View_Attr_Val model)
        {
            if (ModelState.IsValid)
            {
                var s = new Attribute_Value();
                s.Attribute_Id = model.Attr_Id;
                s.Fixed_Price = model.Fixed_Price;
                s.Name = model.Name;
                s.Id = model.Id;
                _proUnitOfWork.Attr_Va.Update(s);
                return RedirectToAction("Attr_Val");
            }
            return RedirectToAction("Edit_AttrVal");
        }
        [HttpGet]
        public IActionResult Delete_AttrVal(int id)
        {
            _proUnitOfWork.Attr_Va.Delete(_proUnitOfWork.Attr_Va.GetID(id));
            return RedirectToAction("Attr_Val");
        }


        [HttpGet]
        public IActionResult AttrAndPro()
        {
            var model = _proUnitOfWork.AttrAndPro.attrAndPros();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_AttrAndPro()
        {
            var pro = _proUnitOfWork.Product.GetProducts();
            var attr = _proUnitOfWork.Attr.GetAttribute();
            ViewBag.attr = new SelectList(attr, "Id", "Name", null);
            ViewBag.pro = new SelectList(pro, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Cre_AttrAndPro(ViewAttrAndPro model)
        {
            if (ModelState.IsValid)
            {
                var at = new AttrAndPro();
                at.Id = model.Id;
                at.AtrrId = model.AtrrId;
                at.ProductId = model.ProductId;
                _proUnitOfWork.AttrAndPro.Create(at);
                return RedirectToAction("AttrAndPro");
            }
            return RedirectToAction("Cre_AttrAndPro");
        }
        [HttpGet]
        public IActionResult EditVari(int id)
        {
            var s = _proUnitOfWork.AttrAndPro.Get(id);
            var pro = _proUnitOfWork.Product.GetProducts();
            var attr = _proUnitOfWork.Attr.GetAttribute();
            ViewBag.attr = new SelectList(attr, "Id", "Name", s.AtrrId);
            ViewBag.pro = new SelectList(pro, "Id", "Name", s.ProductId);
            return View();
        }
        [HttpPost]
        public IActionResult EditVari(ViewAttrAndPro model)
        {
            if (ModelState.IsValid)
            {
                var at = new AttrAndPro();
                at.Id = model.Id;
                at.AtrrId = model.AtrrId;
                at.ProductId = model.ProductId;
                _proUnitOfWork.AttrAndPro.Update(at);
                return RedirectToAction("AttrAndPro");
            }
            return RedirectToAction("EditVari");
        }
        [HttpGet]
        public IActionResult DeleteVari(int id)
        {
            _proUnitOfWork.AttrAndPro.Delete(id);
            return RedirectToAction("AttrAndPro");
        }


    }
}
