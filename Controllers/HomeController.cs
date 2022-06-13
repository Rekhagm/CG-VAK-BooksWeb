using CGVakBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.Models;
using System.Linq.Expressions;

namespace CG_VAK_BooksWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
 

        public HomeController(ILogger<HomeController> logger ,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<products> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            ShoppingCart ShoppingCart = new()
            {
                ProductId = Id,
                Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == Id),
                Count = 1
            };
            return View(ShoppingCart);
        }


        [HttpPost]

        public IActionResult Details(ShoppingCart shoppingCart)
        {
            ShoppingCart  cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(p => p.ProductId == shoppingCart.ProductId);

            if(cart == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cart,shoppingCart.Count);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}