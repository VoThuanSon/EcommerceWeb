using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClothes.Irepository;
using WebClothes.Models;
using WebClothes.ViewModels;

namespace WebClothes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImgController : Controller
    {
        private readonly IProUnitOfWork _proUnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImgController(IProUnitOfWork proUnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _proUnitOfWork = proUnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public JsonResult DeleteFile(string ufile)
        {
            string path = _webHostEnvironment.WebRootPath + ufile;
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                    return Json(true);

                }
                catch (Exception ex)
                {
                    return Json(false);
                }


            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UploadFile(IFormFile ufile)
        {
            if (ufile.Length > 0)
            {
                try
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string fullpath = Path.Combine(path, ufile.FileName);
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
        [HttpPost]
        public JsonResult UploadMutiFile(List<IFormFile> files)
        {
            if (files.Count <= 0)
                return Json(false);

            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            foreach (var file in files)
            {
                string filePath = Path.Combine(uploadFolder, file.FileName);
                var check = System.IO.File.Exists(filePath);
                if (!check)
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                        file.CopyTo(fileStream);
            }
            return Json(true);

        }
        public IActionResult ImgView()
        {
            var model = _proUnitOfWork.imgRepo.GetImg();
            return View(model);
        }

        [HttpGet]
        public IActionResult Cre_Img()
        {
            var pro = _proUnitOfWork.Product.GetProducts();
            ViewBag.pro = new SelectList(pro, "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public IActionResult Cre_Img(ViewImg Model)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Model.Files)
                {
                    var s = new Img();
                    var path = "/Images/" + file.FileName;
                    s.Res_model = Model.Res_model;
                    s.Id = Model.Id;
                    s.Res_Id = Model.Res_Id;
                    s.Path = path;
                    _proUnitOfWork.imgRepo.Create(s);
                }
                return RedirectToAction("ImgView");
            }

            return RedirectToAction("Cre_Img");
        }
        [HttpGet]
        public IActionResult EditImg(int id)
        {
            var img = _proUnitOfWork.imgRepo.GetId(id);
            var mod = new ViewImg_One();
            mod.Id = img.Id;
            mod.Path = img.Path;
            mod.Res_model = img.Res_model;
            mod.Res_Id = img.Res_Id;
            var pro = _proUnitOfWork.Product.GetProducts();
            ViewBag.pro = new SelectList(pro, "Id", "Name", img.Res_Id);
            return View(mod);
        }
        [HttpPost]
        public IActionResult EditImg(ViewImg_One Model)
        {
            if (ModelState.IsValid)
            {
                var s = new Img();
                s.Res_model = Model.Res_model;
                s.Id = Model.Id;
                s.Res_Id = Model.Res_Id;
                s.Path = Model.Path;
                _proUnitOfWork.imgRepo.Update(s);
                return RedirectToAction("ImgView");
            }

            return RedirectToAction("EditImg");
        }
        [HttpGet]
        public IActionResult DeleteImg(int id)
        {
            var img = _proUnitOfWork.imgRepo.GetId(id);
            _proUnitOfWork.imgRepo.Delete(img);
            DeleteFile(img.Path);
            return RedirectToAction("ImgView");
        }
    }
}
