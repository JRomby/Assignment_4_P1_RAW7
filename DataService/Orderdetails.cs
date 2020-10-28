using System;

namespace DataService
{
    public partial class Orderdetails
    {
        private Products _product;
        private Orders _order;
        private Orderdetails(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public Orderdetails()
        {

        }

        private Action<object, string> LazyLoader { get; set; }
        public int Orderid { get; set; }
        public int Productid { get; set; }
        public int Unitprice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        public virtual Orders Order { get => LazyLoader.Load(this, ref _order); set => _order = value; }

        public virtual Products Product { get => LazyLoader.Load(this, ref _product); set => _product = value; }
    }
}
