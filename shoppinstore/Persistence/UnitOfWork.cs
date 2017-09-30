using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shoppinstore.Repository;
using shoppinstore.Models;
namespace shoppinstore.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        ShoppingStoreEntities2 _context;
        public UnitOfWork(ShoppingStoreEntities2 context)
        {
            _context = context;
        }
        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);
                return _products;
            }
        }
        private IProductRepository _products { get; set; }

        public IOrderRepository Order
        {
            get
            {
                if (_orders == null)
                    _orders = new OrderRepository(_context);

                return _orders;
            }
        }
        public IShippingRepository Shipping
        {
            get
            {
                if (_Shippings == null)
                    _Shippings = new ShippingRepository(_context);

                return _Shippings;

            }
        }


        public ShippingRepository  _Shippings { get; set; }
        private IOrderRepository _orders { get; set; }        
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
