using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public class DataService
    {
        northwind2Context context = new northwind2Context();
        public List<Categories> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Categories GetCategory(int categoryId)
        {
            return context.Categories.Find(categoryId);
        }

        public Categories CreateCategory(string name, string description)
        {
            Categories category = new Categories
            {
                Categoryname = name, Description = description, Categoryid = context.Categories.Count() + 1
            };
            AddCategory(category);
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
                return false;
            RemoveCategory(category);
            return true;
        }

        public bool UpdateCategory(int id, string newName, string newDescription)
        {
            var category = context.Categories.Find(id);
            if (category == null)
                return false;
            category.Categoryname = newName;
            category.Description = newDescription;
            context.SaveChanges();
            return true;
        }

        public Products GetProduct(int id)
        {
            return context.Products.Find(id);
        }

        public List<Products> GetProductByCategory(int id)
        {
            return context.Products.Where(x => x.Categoryid == id).ToList();
        }

        public List<Products> GetProductByName(string subName)
        {
            return context.Products.Where(x => x.Productname.Contains(subName)).ToList();
        }

        public Orders GetOrder(int id)
        {
            return context.Orders.Find(id);
        }

        public List<Orders> GetOrders()
        {
            return context.Orders.ToList();
        }

        public List<Orderdetails> GetOrderDetailsByOrderId(int id)
        {
            return context.Orderdetails.Where(x => x.Orderid == id).ToList();
        }

        public List<Orderdetails> GetOrderDetailsByProductId(int id)
        {
            return context.Orderdetails.Where(x => x.Productid == id).ToList();
        }

        public void AddCategory(Categories category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void RemoveCategory(Categories category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
