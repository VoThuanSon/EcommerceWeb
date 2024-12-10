using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.Repository;
using WebClothes.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;

namespace WebClothes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class OrderController : Controller
    {
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IProUnitOfWork _proUnitOfWork;

        public OrderController(IOrderUnitOfWork orderUnitOfWork, HttpClient httpClient, IProUnitOfWork proUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
            _proUnitOfWork = proUnitOfWork;
        }
        [HttpGet]
        public ActionResult<List<CustomerProfile>> GetCustomerProfiles()
        {
            var customerProfiles = _orderUnitOfWork.CustomerRepo.GetProfiles();
            return Ok(customerProfiles);
        }
        public IActionResult Customer()
        {
            var model = _orderUnitOfWork.CustomerRepo.GetProfiles();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Customer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Customer(View_Customer model)
        {
            if (ModelState.IsValid)
            {
                var cs = new CustomerProfile();
                var prn = _orderUnitOfWork.ProvinceRepo.GetProvinceById(model.ProvinceId);
                var disn = _orderUnitOfWork.districtRepo.GetDistrict(model.DistrictId);
                var warn = _orderUnitOfWork.WardRepo.Get(model.WardId);
                var address = prn.Name + ',' + disn.Name + ',' + warn.Name;
                cs.Id = model.Id;
                cs.Address = address;
                cs.Email = model.Email;
                cs.Phone = model.Phone;
                cs.ProvinceId = model.ProvinceId;
                cs.DistrictId = model.DistrictId;
                cs.WardId = model.WardId;
                cs.Name = model.Name;
                cs.DateofBirth = model.DateofBirth;
                _orderUnitOfWork.CustomerRepo.CreateProfile(cs);
                var result = new
                {
                    success = true,
                    redirectUrl = Url.Action("Customer", "Order")
                };
                return Ok(result);
            }
            return RedirectToAction("Cre_Customer");
        }
        [HttpGet]
        public IActionResult Edit_Customer(int id)
        {
            var mode = _orderUnitOfWork.CustomerRepo.GetProfileById(id);
            var mod = new View_Customer();
            mod.Id = id;
            mod.Name = mode.Name;
            mod.Address = mode.Address;
            mod.ProvinceId = mode.ProvinceId;
            mod.DistrictId = mode.DistrictId;
            mod.Email = mode.Email;
            mod.Phone = mode.Phone;
            mod.WardId = mode.WardId;
            mod.DateofBirth = mode.DateofBirth;
            return View(mod);
        }
        [HttpPost]
        public IActionResult Edit_Customer(View_Customer model)
        {
            if (ModelState.IsValid)
            {
                var cs = _orderUnitOfWork.CustomerRepo.GetProfileById(model.Id);
                var prn = _orderUnitOfWork.ProvinceRepo.GetProvinceById(model.ProvinceId);
                var disn = _orderUnitOfWork.districtRepo.GetDistrict(model.DistrictId);
                var warn = _orderUnitOfWork.WardRepo.Get(model.WardId);
                cs.Address = prn.Name + ',' + disn.Name + ',' + warn.Name;
                cs.ProvinceId = model.ProvinceId;
                cs.DistrictId = model.DistrictId;
                cs.WardId = model.WardId;
                cs.Name = model.Name;
                cs.DateofBirth = model.DateofBirth;
                _orderUnitOfWork.CustomerRepo.UpdateProfile(cs);
                var result = new
                {
                    success = true,
                    redirectUrl = Url.Action("Customer", "Order")
                };
                return Ok(result);
            }
            return RedirectToAction("Edit_Customer");
        }
        [HttpGet]
        public IActionResult Delete_Customer(int id)
        {
            var pr = _orderUnitOfWork.CustomerRepo.GetProfileById(id);
            _orderUnitOfWork.CustomerRepo.DeleteProfile(pr);
            return RedirectToAction("Customer");
        }

        public IActionResult SaleOrder()
        {
            var mod = _orderUnitOfWork.SaleOrderRepo.SaleOrders();
            foreach (var i in mod)
            {
                var listPro = new List<Product>();
                var listOrder = _orderUnitOfWork.saleOrderLine.GetSaleOrders(i.Id);
                foreach (var ii in listOrder)
                {
                    var pr = new Product();
                    pr = _proUnitOfWork.Product.GetProductById(ii.ProductId);
                    listPro.Add(pr);
                }
                i.products = listPro;
            }
            return View(mod);
        }
        [HttpGet]
        public IActionResult Delete_Order(int id)
        {
            var s = _orderUnitOfWork.SaleOrderRepo.GetSaleOrder(id);
            _orderUnitOfWork.SaleOrderRepo.Delete(s);
            return RedirectToAction("SaleOrder");
        }
        [HttpGet]
        public IActionResult Cre_Order()
        {
            return View();
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
                order.PaymentMethod = model.PaymentMethod;
                order.CustomerId = model.CustomerProfileId;
                order.Status = model.Status;
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
            return RedirectToAction("Cre_Order");
        }
        [HttpGet]
        public IActionResult Edit_Order(int id)
        {
            var mode = _orderUnitOfWork.SaleOrderRepo.GetSaleOrder(id);
            var ct = _orderUnitOfWork.CustomerRepo.GetProfileById(mode.CustomerId);
            var mod = new View_Order();
            if (mode.Address == ct.Address)
            {
                mod.Address = mode.Address;
            }
            else
            {
                mod.Address = null;
            }
            mod.Id = id;
            mod.WardId = mode.WardId;
            mod.ProvinceId = mode.ProvinceId;
            mod.DistrictId = mode.DistrictId;
            mod.products = mode.products;
            mod.PaymentMethod = mode.PaymentMethod;
            mod.CustomerProfileId = mode.CustomerId;
            mod.Status = mode.Status;
            mod.Date_Order = mode.Date_Order;
            return View(mod);
        }
        [HttpPost]
        public IActionResult Edit_Order(View_Order model)
        {
            if (ModelState.IsValid)
            {
                var customer = _orderUnitOfWork.CustomerRepo.GetProfileById(model.CustomerProfileId);
                var order = _orderUnitOfWork.SaleOrderRepo.GetSaleOrder(model.Id);
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
                _orderUnitOfWork.SaleOrderRepo.Update(order);
                var result = new
                {
                    success = true,
                    redirectUrl = Url.Action("SaleOrder", "Order")
                };
                return Ok(result);
            }
            return RedirectToAction("Edit_Order");
        }
        public IActionResult Province()
        {
            var mod = _orderUnitOfWork.ProvinceRepo.GetProvinceList();
            return View(mod);
        }
        [HttpGet]
        public IActionResult Cre_Province()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Province(View_Province model)
        {
            if (ModelState.IsValid)
            {
                var pro = new Province();
                pro.Id = model.Id;
                pro.Name = model.Name;
                _orderUnitOfWork.ProvinceRepo.Create(pro);
                return RedirectToAction("Province");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit_Province(int id)
        {
            var mod = _orderUnitOfWork.ProvinceRepo.GetProvinceById(id);
            var model = new View_Province();
            model.Id = id;
            model.Name = mod.Name;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit_Province(View_Province model)
        {
            if (ModelState.IsValid)
            {
                var pro = new Province();
                pro.Id = model.Id;
                pro.Name = model.Name;
                _orderUnitOfWork.ProvinceRepo.Update(pro);
                return RedirectToAction("Province");
            }
            return View(model);
        }
        public IActionResult Delete_Province(int id)
        {
            var pr = _orderUnitOfWork.ProvinceRepo.GetProvinceById(id);
            if (pr != null)
            {
                _orderUnitOfWork.ProvinceRepo.Delete(pr);
                return RedirectToAction("Province");
            }
            return RedirectToAction("Province");
        }
        public IActionResult District()
        {
            var model = _orderUnitOfWork.districtRepo.GetDistricts();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_District()
        {
            var pro = _orderUnitOfWork.ProvinceRepo.GetProvinceList();
            ViewBag.pro = new SelectList(pro, "Id", "Name", null);
            return View();
        }
        [HttpPost]

        public IActionResult Cre_District(View_District model)
        {
            if (ModelState.IsValid)
            {
                var dt = new District();
                dt.Id = model.Id;
                dt.Name = model.Name;
                dt.ProvinceId = model.ProvinceId;
                _orderUnitOfWork.districtRepo.Create(dt);
                return RedirectToAction("District");

            }
            return RedirectToAction("Cre_District");
        }
        [HttpGet]
        public IActionResult Edit_District(int id)
        {
            var pro = _orderUnitOfWork.ProvinceRepo.GetProvinceList();
            ViewBag.pro = new SelectList(pro, "Id", "Name", id);
            var m = _orderUnitOfWork.districtRepo.GetDistrict(id);
            var model = new View_District();
            model.Id = id;
            model.Name = m.Name;
            model.ProvinceId = m.ProvinceId;
            return View(model);
        }
        [HttpPost]

        public IActionResult Edit_District(View_District model)
        {
            if (ModelState.IsValid)
            {
                var dt = new District();
                dt.Id = model.Id;
                dt.Name = model.Name;
                dt.ProvinceId = model.ProvinceId;
                _orderUnitOfWork.districtRepo.Update(dt);
                return RedirectToAction("District");
            }
            return RedirectToAction("Edit_District");
        }
        public IActionResult Delete_District(int id)
        {
            var rm = _orderUnitOfWork.districtRepo.GetDistrict(id);
            if (rm != null)
            {
                _orderUnitOfWork.districtRepo.DeleteDistrict(rm);
                return RedirectToAction("District");
            }
            return RedirectToAction("District");
        }

        public IActionResult Ward()
        {
            var model = _orderUnitOfWork.WardRepo.Wards();
            return View(model);
        }
        [HttpGet]
        public IActionResult Cre_Ward()
        {
            var dis = _orderUnitOfWork.districtRepo.GetDistricts();
            ViewBag.dis = new SelectList(dis, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Ward(View_Ward model)
        {
            if (ModelState.IsValid)
            {
                var wa = new Ward();
                wa.Id = model.Id;
                wa.Name = model.Name;
                wa.DistrictId = model.DistrictId;
                _orderUnitOfWork.WardRepo.Create(wa);
                return RedirectToAction("Ward");
            }
            return RedirectToAction("Cre_Ward");
        }
        [HttpGet]
        public IActionResult Edit_Ward(int id)
        {
            var dis = _orderUnitOfWork.districtRepo.GetDistricts();
            ViewBag.dis = new SelectList(dis, "Id", "Name", id);
            var m = _orderUnitOfWork.WardRepo.Get(id);
            var model = new View_Ward();
            model.Id = id;
            model.Name = m.Name;
            model.DistrictId = m.DistrictId;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit_Ward(View_Ward model)
        {
            if (ModelState.IsValid)
            {
                var wa = new Ward();
                wa.Id = model.Id;
                wa.Name = model.Name;
                wa.DistrictId = model.DistrictId;
                _orderUnitOfWork.WardRepo.Update(wa);
                return RedirectToAction("Ward");
            }
            return RedirectToAction("Cre_Ward");
        }
        public IActionResult Delete_Ward(int id)
        {
            var w = _orderUnitOfWork.WardRepo.Get(id);
            if (w != null)
            {
                _orderUnitOfWork.WardRepo.Delete(w);
                return RedirectToAction("Ward");
            }
            return RedirectToAction("Ward");
        }
        public IActionResult SaleOrderLine()
        {
            var mod = _orderUnitOfWork.saleOrderLine.SaleOrders();
            return View(mod);
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
                    redirectUrl = Url.Action("SaleOrder", "Order")
                };
                return Ok(result);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Cre_SaleOrderLine()
        {
            var ct = _orderUnitOfWork.CustomerRepo.GetProfiles();
            var or = _orderUnitOfWork.SaleOrderRepo.SaleOrders();
            var pr = _proUnitOfWork.Product.GetProducts();
            ViewBag.ct = new SelectList(ct, "Id", "Name", null);
            ViewBag.or = new SelectList(or, "Id", "Id", null);
            ViewBag.pr = new SelectList(pr, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Edit_SaleOrderLine(View_OrderLine model)
        {
            if (ModelState.IsValid)
            {
                var n = new SaleOrderLine();
                n.SaleOrderId = model.SaleOrderId;
                n.ProductId = model.ProductId;
                n.CustomerId = model.CustomerId;
                n.Quantity = model.Quantity;
                n.Variant = model.Variant;
                _orderUnitOfWork.saleOrderLine.UpdateSaleOrderLine(n);
                return RedirectToAction("SaleOrderLine");
            }

            return RedirectToAction("Edit_SaleOrderLine");
        }

        [HttpGet]
        public IActionResult Edit_SaleOrderLine(int id)
        {
            var sale = _orderUnitOfWork.saleOrderLine.GetSaleOrderLine(id);
            var s = new View_OrderLine();
            s.Quantity = sale.Quantity;
            s.Variant = sale.Variant;
            s.SaleOrderId = sale.SaleOrderId;
            s.ProductId = sale.ProductId;
            s.CustomerId = sale.CustomerId;
            var ct = _orderUnitOfWork.CustomerRepo.GetProfiles();
            var or = _orderUnitOfWork.SaleOrderRepo.SaleOrders();
            var pr = _proUnitOfWork.Product.GetProducts();
            ViewBag.ct = new SelectList(ct, "Id", "Name", sale.CustomerId);
            ViewBag.or = new SelectList(or, "Id", "Id", sale.SaleOrderId);
            ViewBag.pr = new SelectList(pr, "Id", "Name", sale.ProductId);
            return View(s);
        }
        [HttpGet]
        public IActionResult Delete_SaleOrderLine(int id)
        {
            var s = _orderUnitOfWork.saleOrderLine.GetSaleOrderLine(id);
            _orderUnitOfWork.saleOrderLine.DeleteSaleOrderLine(s);
            return RedirectToAction("SaleOrderLine");
        }
    }
}
