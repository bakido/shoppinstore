using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shoppinstore.Repository;
using shoppinstore.Models;
namespace shoppinstore.Persistence

{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(ShoppingStoreEntities2 context):base(context)
        {             
        }
      
    }
}