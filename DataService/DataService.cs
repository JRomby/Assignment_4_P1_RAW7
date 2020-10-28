using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataService
{
    public class DataService
    {
        northwind2Context context = new northwind2Context();
         public async Task<List<Categories>>  GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Categories> GetCategory(int categoryId)
        {
            return await context.Categories.FindAsync(categoryId);
        }

        public async Task<Categories> CreateCategory(string name, string description)
        {
            Categories category = new Categories
            {
                Categoryname = name, Description = description, Categoryid = context.Categories.Count() + 1
            };
            await AddCategory(category);
            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = context.Categories.FindAsync(id);
            if (await category == null)
                return false;
            RemoveCategory(await category);
            return true;
        }

        public async Task<bool> UpdateCategory(int id, string newName, string newDescription)
        {
            var category = context.Categories.FindAsync(id);
            if (await category == null)
                return false;
            ChangeCategory(await category, newName, newDescription);
            return true;
        }

        public async Task<Products> GetProduct(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Products>> GetProductByCategory(int id)
        {
            return await context.Products.Where(x => x.Categoryid == id).ToListAsync();
        }

        public async Task<List<Products>> GetProductByName(string subName)
        {
            return await context.Products.Where(x => x.Productname.Contains(subName)).ToListAsync();
        }

        public async Task<Orders> GetOrder(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<List<Orders>> GetOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<List<Orderdetails>> GetOrderDetailsByOrderId(int id)
        {
            return await context.Orderdetails.Where(x => x.Orderid == id).ToListAsync();
        }

        public async Task<List<Orderdetails>> GetOrderDetailsByProductId(int id)
        {
            return await context.Orderdetails.Where(x => x.Productid == id).ToListAsync();
        }

        public async Task AddCategory(Categories category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async void RemoveCategory(Categories category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async void ChangeCategory(Categories category, string name, string description)
        {
            category.Categoryname = name;
            category.Description = description;
            await context.SaveChangesAsync();
        }
    }
}
