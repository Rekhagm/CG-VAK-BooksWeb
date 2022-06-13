using Microsoft.AspNetCore.Mvc;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.Models;
using CGVakBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System.Linq.Expressions;

namespace CG_VAK_BooksWeb.Controllers
{
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private dynamic CategoryList;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<products> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        //public IActionResult Upsert(int? Id)
        //{
        //    //id = 1;
        //    ProductVM productVM = new()
        //    {

        //        IEnumerable < SelectListItem > CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
        //        {
        //            Text = c.CategoryName,
        //            Value = c.Id.ToString()
        //        });
        //    ViewBag.CategoryList = CategoryList;


        //    if (Id == null || Id == 0)
        //    {

        //        ViewBag.mode = "Create";
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.mode = "Update";
        //        var product = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == Id);
        //        return View(productVM);
        //    }
        //}

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewBag.CoverTypeList = CoverTypeList;

            if (id == null || id == 0)
            {
                ViewBag.mode = "create";
                return View();
            }
            else
            {
                var product = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
                ViewBag.mode = "update";
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult Upsert(products obj, IFormFile file)
        {

            var wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var uploadPath = Path.Combine(wwwRootPath, @"Images\Product");
                if (obj.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.ImageUrl = @"\Images\Product\" + fileName + extension;
            }
            //var product = _db.Products.Find(Mdlproduct.Id);
            if (obj != null)
            {
                if (obj.Id == 0 || obj.Id == null)
                {

                    _unitOfWork.Product.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Added Successfully !";
                    return RedirectToAction("Index");
                }
                else
                {

                    _unitOfWork.Product.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Updated Successfully !";
                    return RedirectToAction("Index");
                }
                //  Console.WriteLine("dbhiq"+Mdlproduct);            
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var productList = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            return View(productList);
        }

        [HttpPost]
        public IActionResult Delete(products products)
        {
            _unitOfWork.Product.Remove(products);
            _unitOfWork.Product.Save();
            TempData["success"] = "Product Deleted succesfully";
            return RedirectToAction("Index");

        }
    }
}
//        #region Implementation for API Calls

//        [HttpGet]

//        public IActionResult GetAll()
//        {
//            var productList = _unitOfWork.Product.GetAll(includeproperties:"Category ,CoverType");
//            return Json(new { status = "success", data = productList });
//        }

//        [HttpDelete]
//        public IActionResult Delete(int? id)
//        {

//            if (id == null)
//            {

//                return Json(new { status = "failed", message = "Deletion of product failed" });
//            }
//            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
//            if (obj == null)
//            {
//                _unitOfWork.Product.Remove(obj);
//                _unitOfWork.Save();
//                return Json(new { status = "Success", message = "Product deleted successfully" });
//            }
//            else
//            {
//                return Json(new { status = "failed", message = "Deletion of product failed ,Product Not found" });
//            }
//        }
//    }
//}



//# endregion