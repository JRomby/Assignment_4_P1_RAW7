using System;
using System.Collections.Generic;

namespace Assignment4_P1New
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string Customerid { get; set; }
        public int? Employeeid { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? Required { get; set; }
        public DateTime? Shippeddate { get; set; }
        public int? Freight { get; set; }
        public string ShipName { get; set; }
        public string Shipaddress { get; set; }
        public string ShipCity { get; set; }
        public string Shippostalcode { get; set; }
        public string Shipcountry { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}