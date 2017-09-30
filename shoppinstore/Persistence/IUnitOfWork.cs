using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shoppinstore.Repository;
namespace shoppinstore.Persistence
{
    public interface IUnitOfWork
    {
      IProductRepository Products { get; }
      IOrderRepository Order { get; }
      int Complete();
    }
}
