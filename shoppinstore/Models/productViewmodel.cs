using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace shoppinstore.Models
{
    public class productViewmodel
    {
        public IPagedList<Product> pageDlist { get; set; }
    }
}