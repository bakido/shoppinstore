using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shoppinstore.Models;
using shoppinstore.Persistence;
using shoppinstore.Repository;
namespace shoppinstore.Persistence
{
    public class ShippingRepository:Repository<Shipping>,IShippingRepository
    {
        public ShippingRepository(ShoppingStoreEntities2 context): base(context)
        {
        
        }
    }
}