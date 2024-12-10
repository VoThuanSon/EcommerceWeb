using Microsoft.AspNetCore.Mvc;
using WebClothes.Models.Vnpay;
using WebClothes.Services.Vnpay;

namespace WebClothes.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

    }
}
