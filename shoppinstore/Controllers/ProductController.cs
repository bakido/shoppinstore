using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoppinstore.Persistence;
using shoppinstore.Models;
using PagedList.Mvc;
using PagedList;

namespace shoppinstore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        ShoppingStoreEntities2 dd;
        UnitOfWork unitOfWork;

        public ProductController()
        {
            dd = new ShoppingStoreEntities2();
            unitOfWork = new UnitOfWork(dd);
        }

        [HttpGet]
        public ActionResult AddNewProductToTheStore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProductToTheStore(FormCollection NewProductDetails, int? page)
        {
            productViewmodel model = new productViewmodel();
            Product p = new Product()
            {
                productName = NewProductDetails["productName"],
                Description = NewProductDetails["Description"],
                price =Convert.ToDecimal(NewProductDetails["price"]),
                Category = NewProductDetails["Category"],
                DateAdded=DateTime.Now
            };
                       
            unitOfWork.Products.add(p);       
            unitOfWork.Complete();
            model.pageDlist = unitOfWork.Products.GetAll().ToPagedList(page ?? 1, 2);
            return View("DisplayAllProductsInStore", model);
        }

        public ActionResult DisplayAllProductsInStore(int? page)
        {
            productViewmodel model = new productViewmodel();
            var products = unitOfWork.Products.GetAll();
            model.pageDlist = products.ToPagedList(page ?? 1, 2);
            //to be ordered by date ascending remember
            return View (model);
        }
       
        [HttpGet]        
        public ActionResult EditProductInformation(int id)
        {
            Product EditedProduct = new Product();
            EditedProduct = unitOfWork.Products.Get(id); 
                 
           return View(EditedProduct);
        }
        [HttpPost]
        public ActionResult EditProductInformation(FormCollection NewProductDetails, int? page)
        {
            int ProductID = Convert.ToInt32(NewProductDetails["ProductId"]);
            var EditedProduct = unitOfWork.Products.Get(ProductID);
            EditedProduct.Category = NewProductDetails["Category"];
            EditedProduct.price =Convert.ToDecimal(NewProductDetails["price"]);
            EditedProduct.Description = NewProductDetails["Description"];
            EditedProduct.productName = NewProductDetails["productName"];
            EditedProduct.DateAdded =DateTime.Now;
            unitOfWork.Complete();
            productViewmodel model = new productViewmodel();
            model.pageDlist = unitOfWork.Products.GetAll().ToPagedList(page ?? 1, 4);
            return View("DisplayAllProductsInStore",model );
        }
        public ActionResult RemoveProductFromStock(int id, int? page)
        {
            Product ProductToDelete= unitOfWork.Products.Get(id);
            unitOfWork.Products.Remove(ProductToDelete);
            unitOfWork.Complete();
            return View("DisplayAllProductsInStore", unitOfWork.Products.GetAll().ToPagedList(page ?? 1, 6));
        }
    }
}
