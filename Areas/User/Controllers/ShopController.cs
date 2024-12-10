using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.Repository;
using WebClothes.Services.Vnpay;
using WebClothes.ViewModels;

namespace WebClothes.Areas.User.Controllers
{
    [Area("User")]

    public class ShopController : Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IVnPayService _vnPayService;
        public ShopController(IProUnitOfWork proUnitOfWork,UserManager<IdentityUser> userManager, IOrderUnitOfWork orderUnitOfWork,IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
            _userManager = userManager;
            _proUnitOfWork = proUnitOfWork;
            _orderUnitOfWork = orderUnitOfWork;
        }
        public async Task<IActionResult> Shop(int? id, int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                var mode = _proUnitOfWork.Product.GetProductByName(searchString);
                int pageSize = 4;
                return View(await PaginatedList<Product>.CreateAsync(mode.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                int pageSize = 4;
                var mode = _proUnitOfWork.Product.GetProductCategory(id);
                return View(await PaginatedList<Product>.CreateAsync(mode.AsNoTracking(), pageNumber ?? 1, pageSize));
            }


        }
        public IActionResult ProDetail(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            ViewData["UserId"] = userId;
            var pro = _proUnitOfWork.Product.GetProductById(id);
            var prodetail = new ProductDetail();
            prodetail.ProductsRelate = _proUnitOfWork.Product.GetProductRelate(pro.Id);
            prodetail.Product = pro;
            prodetail.Imgs = _proUnitOfWork.imgRepo.GetImgResId(pro.Id);
            var attrandpor = _proUnitOfWork.AttrAndPro.attrOfPro(pro.Id);
            var listattr = new List<Attribute_Pro>();
            foreach (var i in attrandpor)
            {
                var s = _proUnitOfWork.Attr.Find(i.AtrrId);
                listattr.Add(s);
            }
            prodetail.attribute_Pros = listattr;
            return View(prodetail);
        }
        [HttpGet]
        public List<Menu> GetMenu()
        {
                var parent = _proUnitOfWork.Category.GetNoParent();
            var menu = new List<Menu>();
            foreach (var i in parent)
            {
                var men = new Menu();
                var chi = new List<Category>();
                men.Category = i;
                var child = _proUnitOfWork.Category.GetChild(i.Id);
                foreach (var j in child)
                {
                    chi.Add(j);
                }
                men.Categories = chi;
                menu.Add(men);
            }
            return menu;
        }
        [HttpGet]
        public IActionResult Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View();
        }
        [HttpGet]
        public Product GetProCart(int id)
        {
            return _proUnitOfWork.Product.GetProductById(id);
        }
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View();
        }
        [HttpGet]
        public IActionResult GetCusId(View_Customer model)
        {
            var cus = _orderUnitOfWork.CustomerRepo.GetProfileByEmail(model.Email);
            if (cus != null)
            {
                return Ok(cus.Id);
            }
            var cs = new CustomerProfile();
            var prn = _orderUnitOfWork.ProvinceRepo.GetProvinceById(model.ProvinceId);
            var disn = _orderUnitOfWork.districtRepo.GetDistrict(model.DistrictId);
            var warn = _orderUnitOfWork.WardRepo.Get(model.WardId);
            var address = prn.Name + ',' + disn.Name + ',' + warn.Name;
            cs.Address = address;
            cs.Email = model.Email;
            cs.Phone = model.Phone;
            cs.DateofBirth = model.DateofBirth;
            cs.ProvinceId = model.ProvinceId;
            cs.DistrictId = model.DistrictId;
            cs.WardId = model.WardId;
            cs.Name = model.Name;
            _orderUnitOfWork.CustomerRepo.CreateProfile(cs);
            var moi = _orderUnitOfWork.CustomerRepo.GetProfileByEmail(model.Email);
            return Ok(moi.Id);

        }
        [HttpPost]
        public IActionResult Cre_Order(View_Order model)
        {
            if (ModelState.IsValid)
            {
                var customer = _orderUnitOfWork.CustomerRepo.GetProfileById(model.CustomerProfileId);
                var order = new SaleOrder();
                var prn = _orderUnitOfWork.ProvinceRepo.GetProvinceById(model.ProvinceId);
                var disn = _orderUnitOfWork.districtRepo.GetDistrict(model.DistrictId);
                var warn = _orderUnitOfWork.WardRepo.Get(model.WardId);
                var address = prn.Name + ',' + disn.Name + ',' + warn.Name;
                if (customer.Address == model.Address)
                {
                    order.Address = customer.Address;
                    order.ProvinceId = customer.ProvinceId;
                    order.DistrictId = customer.DistrictId;
                    order.WardId = customer.WardId;
                }
                else
                {
                    order.Address = address;
                    order.ProvinceId = model.ProvinceId;
                    order.DistrictId = model.DistrictId;
                    order.WardId = model.WardId;
                }
                order.Date_Order = model.Date_Order;
                order.CustomerId = model.CustomerProfileId;
                order.Status = model.Status;
                order.PaymentMethod = model.PaymentMethod;
                _orderUnitOfWork.SaleOrderRepo.Create(order);
                var or = _orderUnitOfWork.SaleOrderRepo.SaleOrder(order);
                var id = or.Id;
                var result = new
                {
                    success = true,
                    id = id,
                };
                return Ok(result);
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Cre_SaleOrderLine(View_OrderLine model)
        {
            if (ModelState.IsValid)
            {
                var n = new SaleOrderLine();
                n.SaleOrderId = model.SaleOrderId;
                n.ProductId = model.ProductId;
                n.CustomerId = model.CustomerId;
                n.Quantity = model.Quantity;
                n.Variant = model.Variant;
                _orderUnitOfWork.saleOrderLine.CreateSaleOrderLine(n);
                var result = new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Home")
                };
                return Ok(result);
            }

            return Ok();
        }
        [HttpGet]
        public IActionResult TrackingOrder()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> OrderNoLogin(int id)
        {
            var cs = _orderUnitOfWork.CustomerRepo.GetProfileById(id);
            var img = _proUnitOfWork.imgRepo.GetImgResId(cs.Id).FirstOrDefault();
            var orders = _orderUnitOfWork.SaleOrderRepo.SaleOrderUser(cs.Id);
            foreach (var item in orders)
            {
               item.Lines = _orderUnitOfWork.saleOrderLine.OrderLineUser(item.Id,cs.Id);
            }
            var mode = new ViewUserProfile();
            mode.Img = img;
            mode.SaleOrders = orders;
            mode.CustomerProfile = cs;
            return View(mode);
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }


    }
}

