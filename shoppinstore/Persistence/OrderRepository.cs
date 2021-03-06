﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shoppinstore.Models;
using shoppinstore.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;
using Microsoft.Owin.Security;
namespace shoppinstore.Persistence
{
    public class OrderRepository:Repository<Order>,IOrderRepository
    {

        public OrderRepository(ShoppingStoreEntities2 context) : base(context)
        {
        
        }
        private static List<CartItems> Cartlist = new List<CartItems>();
        public void AddProductToCart(Product product,int quantity)
        {
            CartItems ProductInList = Cartlist.Where(p => p.product.ProductId == product.ProductId).FirstOrDefault();
           if(ProductInList == null)
            {
                Cartlist.Add(new CartItems { product = product, quantity = quantity });
            }
           else
            {
                ProductInList.quantity += quantity;
            }          
        }
        public List<CartItems> ShowCartItems()
        {
            return Cartlist;
        }
        public void RemoveProductFromList(int  id)
        {
            CartItems productToRemoveFromList = Cartlist.Find(p=>p.product.ProductId==id);
            int quantity = productToRemoveFromList.quantity;
            if (quantity > 0)
            {
                productToRemoveFromList.quantity--;
               
          
            }
            if (quantity == 1)
            {
                Cartlist.Remove(productToRemoveFromList);
            }
        }
        public void clearShoppingCart()
        {
            Cartlist.Clear();
        }
        public void AddRanges(IEnumerable<Order> entities)
        {
            AddRange(entities);       
        }
        public static List<Order> makeorder = new List<Order>();
        private static int?  total = 0;
        
        public  int? CalculateCost(int? price)
        {
            int? cost = 0;
            cost += price;
            total += cost;
            return total;           
        }
      
        public  List<Order> OrderList(int productId,string userID,int shippingID)
        {        
            
            CartItems OrderItem = Cartlist.SingleOrDefault(p => p.product.ProductId == productId);
            int quantity = OrderItem.quantity;          
            Order item = makeorder.SingleOrDefault(p => p.ProductId == productId);
            if (item == null)
            {
                makeorder.Add(new Order { ProductId = productId, quantity = quantity, UserId = userID,TotalCost= total});
            }
            else
            {
                item.quantity++;
            }                
            return makeorder;
        }

        public int? ReduceCartCostIfProductRemoved(int? price)
        {
            total -= price;
            return total;
        }
    }
    public class CartItems
    {
        public  Product product { get; set; }
        public int quantity { get; set; }
    }
}