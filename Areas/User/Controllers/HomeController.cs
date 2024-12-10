using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.ViewModels;

namespace WebClothes.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController : Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IProUnitOfWork proUnitOfWork, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IOrderUnitOfWork orderUnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _proUnitOfWork = proUnitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _orderUnitOfWork = orderUnitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var mode = _proUnitOfWork.Product.GetProducts();
            return View(mode);
        }
        [HttpGet]
        public List<Attribute_Value> GetAttr_Va(int id)
        {
            return _proUnitOfWork.Attr_Va.GetAttribute_Values(id);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    } // Assign the "User" role to the new user
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return Ok(user);
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetImgUser(string email)
        {
            var cs = _orderUnitOfWork.CustomerRepo.GetProfileByEmail(email);
            if (cs != null)
            {
                var img = _proUnitOfWork.imgRepo.GetImgResId(cs.Id).FirstOrDefault();
                return Ok(img.Path);
            }
            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var mode = new ViewUserProfile();
            var cs = _orderUnitOfWork.CustomerRepo.GetProfileByEmail(email);
            if (cs != null)
            {
                var orders = _orderUnitOfWork.SaleOrderRepo.SaleOrderUser(cs.Id);

                mode.SaleOrders = orders;
                foreach (var item in orders)
                {
                    item.Lines = _orderUnitOfWork.saleOrderLine.OrderLineUser(item.Id, cs.Id);
                }

                mode.Img = _proUnitOfWork.imgRepo.GetImgResId(cs.Id).FirstOrDefault();
                mode.CustomerProfile = cs;
                return View(mode);
            }
            ViewData["email"] = email;
            return View("Cre_Profile");
           
        }
        [HttpPost]
        public JsonResult UploadFile(IFormFile ufile, int id)
        {
            if (ufile.Length > 0)
            {
                try
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/User");

                    string fullpath = Path.Combine(path, ufile.FileName);
                    var s = new Img();
                    var pt = "/Images/User/" + ufile.FileName;
                    s.Res_model = "User";
                    s.Res_Id = id;
                    s.Path = pt;
                    _proUnitOfWork.imgRepo.Create(s);
                    using (var fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        ufile.CopyTo(fileStream);
                    }
                    return Json(true);
                }
                catch (Exception ex)
                {
                    return Json(false);
                }

            }
            return Json(false);

        }
    }
}
