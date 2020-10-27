using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4_P1New
{
    public class Product
    {
        public Product()
        {
            Orderdetails = new HashSet<OrderDetails>();
        }

        [Column("productid")] public int Id { get; set; }

        public string Name { get; set; }
        public int? Supplierid { get; set; }


        public int? Categoryid { get; set; }
        public string QuantityPerUnit { get; set; }
        public int? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetails> Orderdetails { get; set; }
    }
}