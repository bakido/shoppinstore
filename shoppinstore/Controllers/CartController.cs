using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using shoppinstore.Persistence;
using shoppinstore.Models;
namespace shoppinstore.Controllers
{
    public class CartController : Controller
    { 
        UnitOfWork unitOfWork = new UnitOfWork(new ShoppingStoreEntities2());
        public static List<Order> OrderList = new List<Order>();
        private static int ProductID;

        public ActionResult AddItemsToShoppingCart(int id)
        {
            ProductID = id;
            Product product=unitOfWork.Products.Get(id);
            unitOfWork.Order.AddProductToCart(product,1);
            ViewBag.Cost = unitOfWork.Order.CalculateCost((int?)product.price);
            return View("ShowCartItems", unitOfWork.Order.ShowCartItems());
        }

        public ActionResult showProducts()
        {           
            return View(unitOfWork.Products.GetAll());
        }

        public ActionResult ShowCartItems()
        {           
            return View(unitOfWork.Order.ShowCartItems());
        }

        public ActionResult RemoveProductFromList(int id)
        {
            unitOfWork.Order.RemoveProductFromList(id);
           Product product= unitOfWork.Products.Get(id);
            
           ViewBag.Cost = unitOfWork.Order.ReduceCartCostIfProductRemoved((int?)product.price);
           return View("ShowCartItems", unitOfWork.Order.ShowCartItems());
        }

        public ActionResult clearShoppingCart()
        {
            unitOfWork.Order.clearShoppingCart();
            return View("ShowCartItems", unitOfWork.Order.ShowCartItems());
        }
   
        [HttpGet]
        [Authorize]
        public ActionResult RequestShippingDetails()
        {
            return View();
        }
        [Authorize]
        public ActionResult RequestShippingDetails(FormCollection addShippingDetails)
        {
                Shipping userShippingDetails = new Shipping();            
                userShippingDetails.FirstName = addShippingDetails["FirstName"];
                userShippingDetails.LastName = addShippingDetails["LastName"];
                userShippingDetails.Address = addShippingDetails["Address"];
                userShippingDetails.Country = addShippingDetails["Country"];
                userShippingDetails.Province = addShippingDetails["Province"];
                userShippingDetails.ContactNumber = Convert.ToInt32(addShippingDetails["ContactNumber"]);
                userShippingDetails.ShippingDate = DateTime.Now;

                int b = ProductID;
          
                unitOfWork.Shipping.add(userShippingDetails);
                var UserID = User.Identity.GetUserId();
                OrderList = unitOfWork.Order.OrderList(ProductID, UserID, userShippingDetails.ShippingID);                     
                unitOfWork.Order.AddRange(OrderList);
                unitOfWork.Order.clearShoppingCart();
                unitOfWork.Complete();
                OrderList.Clear();
                ViewBag.Message = "Thank you for shopping at blahh blah store";
                return View("ShowCartItems",unitOfWork.Order.ShowCartItems());
        }
        
    }
}