using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4_P1New
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Column("categoryid")] public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}