using Microsoft.AspNetCore.Mvc;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.Models;
using CGVakBooks.DataAccess.Repository;
using CGVakBooks.DataAccess.Repository.IRepository;

namespace CG_VAK_BooksWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        //private readonly ICoverTypeRepository _db;

        //public CoverTypeController(ICoverTypeRepository db)
        //{
        //    _db = db;
        //}
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var categories = _db.Categories.ToList();
            //return View(categories);
            IEnumerable<CoverType> covertypeList = _unitOfWork.CoverType.GetAll();
            return View(covertypeList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {
           
            if (ModelState.IsValid)
            {
                var iscovertypeexists = _unitOfWork.CoverType.GetAll().FirstOrDefault(x => x.Name == coverType.Name);
                if (iscovertypeexists != null)
                {
                    TempData["error"] = "Categoty/Order Of Display exists already";
                    return View(coverType);
                }
                else
                {
                    _unitOfWork.CoverType.Add(coverType);
                    _unitOfWork.CoverType.Save();
                    TempData["success"] = "CoverType Created succesfully";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            var covertype = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            return View(covertype);
        }

        [HttpPost]
        public IActionResult Edit(CoverType covertype)
        {
            var iscovertypeexists = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Name == covertype.Name);
            if (iscovertypeexists != null)
            {
                TempData["info"] = "No Changes are detected";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(covertype);
                _unitOfWork.CoverType.Save();
                TempData["success"] = "CoverType Updated succesfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            var covertype = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            return View(covertype);
        }

        [HttpPost]
        public IActionResult Delete(CoverType covertype)
        {
            _unitOfWork.CoverType.Remove(covertype);
            _unitOfWork.CoverType.Save();
            TempData["success"] = "CoverType Deleted succesfully";
            return RedirectToAction("Index");

        }
    
}
}
