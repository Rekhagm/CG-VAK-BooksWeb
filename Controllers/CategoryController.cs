using Microsoft.AspNetCore.Mvc;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.Models;
using CGVakBooks.DataAccess.Repository;
using CGVakBooks.DataAccess.Repository.IRepository;

namespace CG_VAK_BooksWeb.Controllers
{
    public class CategoryController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;

        public CategoryController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Category> categoriesList = _unitOfWork.Category.GetAll();
            return View(categoriesList);
        }

        //Get
        public IActionResult Create()
        {
            int lastorderofdisplay = (int)_unitOfWork.Category.GetAll().Max(c => c.OrderOfDisplay);
            Category category = new Category();
            category.OrderOfDisplay = lastorderofdisplay + 1;
            return View(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                var iscategoryexists = _unitOfWork.Category.GetAll().FirstOrDefault(x => x.Name == category.Name || x.OrderOfDisplay == category.OrderOfDisplay);
                if (iscategoryexists != null)
                {
                    TempData["error"] = "Categoty/Order Of Display exists already";
                    return View(category);
                }
                else
                {
                    _unitOfWork.Category.Add(category);
                    _unitOfWork.Category.Save();
                    TempData["success"] = "Catergory Created succesfully";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.OrderOfDisplay.ToString())
            {
                ModelState.AddModelError("customError", "Display name should not match with displayorder");
            }
            var iscategoryexists = _unitOfWork.Category.GetFirstOrDefault(x => x.Name == category.Name && x.OrderOfDisplay == category.OrderOfDisplay);
            if (iscategoryexists != null)
            {
                TempData["info"] = "No Changes are detected";
                return RedirectToAction("Index");
            }

                if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Category.Save();
                TempData["success"] = "Catergory Updated succesfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Category.Save();
                TempData["success"] = "Catergory Deleted succesfully";
            return RedirectToAction("Index");

        }
    }
}

