using System;

namespace DataService
{
    public partial class Orderdetails
    {

        public Orderdetails()
        {

        }

        public int Orderid { get; set; }
        public int Productid { get; set; }
        public int Unitprice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        public virtual Orders Order { get; set; }

        public virtual Products Product { get;set; }
    }
}
