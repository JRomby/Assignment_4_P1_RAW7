using System;
using System.Collections.Generic;

namespace DataService
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        private Categories(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private Action<object, string> LazyLoader { get; set; }

        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }

        private ICollection<Products> _products;

        public virtual ICollection<Products> Products { get => LazyLoader.Load(this.Categoryname, ref _products); set => _products = value; }
    }
}
