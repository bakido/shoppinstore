﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shoppinstore.Models;
using shoppinstore.Persistence;
namespace shoppinstore.Repository
{

    public interface IOrderRepository:IRepository<Order>
    {
        void AddRanges(IEnumerable<CartItems> entities);
        void AddProductToCart(Product product, int quantity);
        List<CartItems> ShowCartItems();
        void RemoveProductFromList(int id);
        void clearShoppingCart();
         List<Order> OrderList(int id,string userID,int ShippingID);

    }
}
