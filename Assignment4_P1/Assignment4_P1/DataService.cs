using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment4_P1New
{
    internal class DataService
    {
        public static northwindContext context = new northwindContext();

        public Order FindOrder(int i)
        {
            return context.Orders.Find(i);
        }

        public Product GetProduct(int i)
        {
            var product = context.Products.Find(i);
            //Look at doing this in a proper way
            product.Category = context.Categories.Find(product.Categoryid);
            return product;
        }

        public List<Product> GetProductByCategory(int i)
        {
            var products = context.Products.Where(x => x.Categoryid == i).ToList();

            foreach (var x in products) x.Category = context.Categories.Find(x.Categoryid);
            return products;
        }

        public List<Product> GetProductByName(string name)
        {
            var products = context.Products.Where(x => x.Name.Contains(name)).ToList();
            return products;
        }

        public Order GetOrder(int i)
        {
            var order = context.Orders.Find(i);
            order.OrderDetails = context.Orderdetails.Where(x => x.OrderId == order.Id).ToList();
            foreach (var name in order.OrderDetails)
            {
                name.Product = context.Products.Find(name.ProductId);
                name.Product.Category = context.Categories.Find(name.Product.Categoryid);
            }
            return order;
        }

        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int i)
        {
            var orderDetails = context.Orderdetails.Where(x => x.OrderId == i).ToList();

            foreach (var x in orderDetails)
            {
                x.Product = context.Products.Find(x.ProductId);
                //x.Order = context.Orders.Find(i);
                //x.Order.orderdate = context.Orders.Find(i).orderdate;
            }
            return orderDetails;
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int i)
        {
            var orderDetails = context.Orderdetails.Where(x => x.ProductId == i).ToList();
            foreach (var x in orderDetails)
            {
                x.Product = context.Products.Find(x.ProductId);
                x.Order = context.Orders.Find(x.OrderId);
                x.Order.orderdate = context.Orders.Find(x.OrderId).orderdate;
            }
            return orderDetails;
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int i)
        {
            return context.Categories.Find(i);
        }

        public Category CreateCategory(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description,
                Id = context.Categories.ToList().OrderBy(x => x.Id).Last().Id + 1
            };
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var category = context.Categories.Find(id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            try
            {
                var category = context.Categories.Find(id);
                category.Name = name;
                category.Description = description;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}