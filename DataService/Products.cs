using System;
using System.Collections.Generic;

namespace DataService
{
    public partial class Products
    {
        public Products()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Supplierid { get; set; }
        public int? Categoryid { get; set; }
        public string Quantityperunit { get; set; }
        public int? Unitprice { get; set; }
        public int? Unitsinstock { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
    }
}
