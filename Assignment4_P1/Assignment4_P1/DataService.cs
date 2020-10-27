using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Assignment4_P1New
{

    class DataService
    {
        public static northwindContext context = new northwindContext();

        public Order FindOrder(int i)
        {
            return context.Orders.Find(i);
        }

        public Product GetProduct(int i)
        {
            Product product = context.Products.Find(i);
            //Look at doing this in a proper way
            product.Category = context.Categories.Find(product.Categoryid); ;
            return product;
        }

        public List<Product> GetProductByCategory(int i)
        {
            List<Product> products = context.Products.Where(x => x.Categoryid == i).ToList();

            foreach (var x in products)
            {
                x.Category = context.Categories.Find(x.Categoryid);
            }
            return products;
        }

        public List<Product> GetProductByName(string name)
        {
            List<Product> products = context.Products.Where(x => x.Name.Contains(name)).ToList();
            return products;
        }

        public Order GetOrder(int i)
        {
            Order order = context.Orders.Find(i);
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
            List<OrderDetails> orderDetails = context.Orderdetails.Where(x => x.OrderId == i).ToList();

            foreach (var x in orderDetails)
            {
                x.Product = context.Products.Find(x.ProductId);

            }
            return orderDetails;
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int i)
        {
            return context.Orderdetails.Where(x => x.ProductId == i).ToList();
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
            
            Category category = new Category()
            {
                Name = name,
                Description = description,
                Id = context.Categories.ToList().OrderBy(x => x.Id).Last().Id +1
            };
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                Category category = context.Categories.Find(id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            catch (Exception e)
            {
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
                return false;
            }
            return true;
        }
    
    }
}
