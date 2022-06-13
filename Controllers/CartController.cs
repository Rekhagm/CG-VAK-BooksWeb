using Microsoft.AspNetCore.Mvc;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.Models;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using CGVakBooks.Utilities;
using Stripe.Checkout;

namespace CG_VAK_BooksWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var shoppingCartVM = new ShoppingCartVM()
            {

                ListCart = _unitOfWork.ShoppingCart.GetAll(includeproperties: "Product"),
                OrderHeader = new()

            };
            double Total = 0;
            foreach (var cart in shoppingCartVM.ListCart)
            {

                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                Total = (cart.Price * cart.Count);
            }
            ViewBag.Total = Total;
            return View(shoppingCartVM);

        }

       public IActionResult Summary()
        {
            var shoppingCartVM = new ShoppingCartVM()
            {

                ListCart = _unitOfWork.ShoppingCart.GetAll(includeproperties: "Product"),
                OrderHeader = new()

            };

            shoppingCartVM.OrderHeader.Name = "Rekha";
            shoppingCartVM.OrderHeader.PhoneNumber = "9898765678";
            shoppingCartVM.OrderHeader.StreetAddress = "Outer Ring Road";
            shoppingCartVM.OrderHeader.City = "Bangalore";
            shoppingCartVM.OrderHeader.State = "Karnataka";
            shoppingCartVM.OrderHeader.PostalCode = "566005";

            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }



        [HttpPost]
        public IActionResult SummaryPOST(ShoppingCartVM shoppingCartVM)
        {
            shoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(includeproperties: "Product");
            shoppingCartVM.OrderHeader = new();
            shoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            shoppingCartVM.OrderHeader.PaymentStatus = AppConstants.PaymentStatusPending;
            shoppingCartVM.OrderHeader.OrderStatus = AppConstants.StatusPending;

            _unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in shoppingCartVM.ListCart)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = shoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count,

                };
                _unitOfWork.OrderDetails.Add(orderDetails);
                _unitOfWork.Save();
            }

            var domailUrl = "https://localhost:44343/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domailUrl + $"Cart/OrderConfirmation?id={shoppingCartVM.OrderHeader.Id}",
                CancelUrl = domailUrl + $"Cart/Index",
            };

            foreach (var item in shoppingCartVM.ListCart)
            {

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title

                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentId(shoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }


        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            if (orderHeader.PaymentStatus != AppConstants.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {

                    _unitOfWork.OrderHeader.UpdateStatus(id, AppConstants.PaymentStatusApproved, AppConstants.StatusApproved);
                    _unitOfWork.Save();
                }
            }

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll().ToList();
            _unitOfWork.ShoppingCart.RemoveChange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);
        }
        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}


        

