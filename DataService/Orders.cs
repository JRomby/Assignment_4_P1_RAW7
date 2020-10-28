using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public partial class Orders
    {
        public Orders()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        private Orders(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private Action<object, string> LazyLoader { get; set; }

        public int Orderid { get; set; }
        public string Customerid { get; set; }
        public int? Employeeid { get; set; }
        public DateTime? Orderdate { get; set; }
        public DateTime? Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public int? Freight { get; set; }
        public string Shipname { get; set; }
        public string Shipaddress { get; set; }
        public string Shipcity { get; set; }
        public string Shippostalcode { get; set; }
        public string Shipcountry { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Employees Employee { get; set; }

        private ICollection<Orderdetails> _Orderdetails;
        public virtual ICollection<Orderdetails> Orderdetails { get => LazyLoader.Load(this, ref _Orderdetails); set => _Orderdetails = value; }
    }
}
