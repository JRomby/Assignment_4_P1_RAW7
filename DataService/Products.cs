using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public partial class Products
    {
        public Products()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        private Products(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private Action<object, string> LazyLoader { get; set; }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Supplierid { get; set; }
        public int? Categoryid { get; set; }
        public string Quantityperunit { get; set; }
        public int? Unitprice { get; set; }
        public int? Unitsinstock { get; set; }

        private Categories _category;
        public virtual Categories Category { get => LazyLoader.Load(this, ref _category
        ); set => _category = value; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
    }
}
