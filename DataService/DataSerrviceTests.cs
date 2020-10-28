using System.Globalization;
using System.Linq;
using Xunit;

namespace DataService
{
    public class DataServiceTests
    {
        /* Categories */

        [Fact]
        public void Category_Object_HasIdNameAndDescription()
        {
            var category = new Categories();
            Assert.Equal(0, category.Categoryid);
            Assert.Null(category.Categoryname);
            Assert.Null(category.Description);
        }

        [Fact]
        public void GetAllCategories_NoArgument_ReturnsAllCategories()
        {
            var service = new DataService();
            var categories = service.GetCategories();
            Assert.Equal(8, categories.Count);
            Assert.Equal("Beverages", categories.First().Categoryname);
        }

        [Fact]
        public void GetCategory_ValidId_ReturnsCategoryObject()
        {
            var service = new DataService();
            var category = service.GetCategory(1);
            Assert.Equal("Beverages", category.Categoryname);
        }

        [Fact]
        public void CreateCategory_ValidData_CreteCategoryAndRetunsNewObject()
        {
            var service = new DataService();
            var category = service.CreateCategory("Test", "CreateCategory_ValidData_CreteCategoryAndRetunsNewObject");
            Assert.True(category.Categoryid > 0);
            Assert.Equal("Test", category.Categoryname);
            Assert.Equal("CreateCategory_ValidData_CreteCategoryAndRetunsNewObject", category.Description);

            // cleanup
            service.DeleteCategory(category.Categoryid);
        }

        [Fact]
        public void DeleteCategory_ValidId_RemoveTheCategory()
        {
            var service = new DataService();
            var category = service.CreateCategory("Test", "DeleteCategory_ValidId_RemoveTheCategory");
            var result = service.DeleteCategory(category.Categoryid);
            Assert.True(result);
            category = service.GetCategory(category.Categoryid);
            Assert.Null(category);
        }

        [Fact]
        public void DeleteCategory_InvalidId_ReturnsFalse()
        {
            var service = new DataService();
            var result = service.DeleteCategory(-1);
            Assert.False(result);
        }

        [Fact]
        public void UpdateCategory_NewNameAndDescription_UpdateWithNewValues()
        {
            var service = new DataService();
            var category = service.CreateCategory("TestingUpdate",
                "UpdateCategory_NewNameAndDescription_UpdateWithNewValues");

            var result = service.UpdateCategory(category.Categoryid, "UpdatedName", "UpdatedDescription");
            Assert.True(result);

            category = service.GetCategory(category.Categoryid);

            Assert.Equal("UpdatedName", category.Categoryname);
            Assert.Equal("UpdatedDescription", category.Description);

            // cleanup
            service.DeleteCategory(category.Categoryid);
        }

        [Fact]
        public void UpdateCategory_InvalidID_ReturnsFalse()
        {
            var service = new DataService();
            var result = service.UpdateCategory(-1, "UpdatedName", "UpdatedDescription");
            Assert.False(result);
        }


        /* products */

        [Fact]
        public void Product_Object_HasIdNameUnitPriceQuantityPerUnitAndUnitsInStock()
        {
            var product = new Products();
            Assert.Equal(0, product.Productid);
            Assert.Null(product.Productname);
            Assert.Null(product.Unitprice);
            Assert.Null(product.Quantityperunit);
            Assert.Null(product.Unitsinstock);
        }

        [Fact]
        public void GetProduct_ValidId_ReturnsProductWithCategory()
        {
            var service = new DataService();
            var product = service.GetProduct(1);
            Assert.Equal("Chai", product.Productname);
            Assert.Equal("Beverages", product.Category.Categoryname);
        }

        [Fact]
        public void GetProductsByCategory_ValidId_ReturnsProductWithCategory()
        {
            var service = new DataService();
            var products = service.GetProductByCategory(1);
            Assert.Equal(12, products.Count);
            Assert.Equal("Chai", products.First().Productname);
            Assert.Equal("Beverages", products.First().Category.Categoryname);
            Assert.Equal("Lakkalikööri", products.Last().Productname);
        }

        [Fact]
        public void GetProduct_NameSubString_ReturnsProductsThatMachesTheSubString()
        {
            var service = new DataService();
            var products = service.GetProductByName("em");
            Assert.Equal(4, products.Count);
            Assert.Equal("NuNuCa Nuß-Nougat-Creme", products.First().Productname);
            Assert.Equal("Flotemysost", products.Last().Productname);
        }

        /* orders */
        [Fact]
        public void Order_Object_HasIdDatesAndOrderDetails()
        {
            var order = new Orders();
            Assert.Equal(0, order.Orderid);
            Assert.Null(order.Orderdate);
            Assert.Null(order.Requireddate);
            Assert.Equal(0, order.Orderdetails.Count);
            Assert.Null(order.Shipname);
            Assert.Null(order.Shipcity);
        }

        [Fact]
        public void GetOrder_ValidId_ReturnsCompleteOrder()
        {
            var service = new DataService();
            var order = service.GetOrder(10248);
            Assert.Equal(3, order.Orderdetails.Count);
            Assert.Equal("Queso Cabrales", order.Orderdetails.First().Product.Productname);
            Assert.Equal("Dairy Products", order.Orderdetails.First().Product.Category.Categoryname);
        }

        [Fact]
        public void GetOrders()
        {
            var service = new DataService();
            var orders = service.GetOrders();
            Assert.Equal(830, orders.Count);
        }


        /* orderdetails */
        [Fact]
        public void OrderDetails_Object_HasOrderProductUnitPriceQuantityAndDiscount()
        {
            var orderDetails = new Orderdetails();
            Assert.Equal(0, orderDetails.Orderid);
            Assert.Null(orderDetails.Order);
            Assert.Equal(0, orderDetails.Productid);
            Assert.Null(orderDetails.Product);
            Assert.Equal(0.0, orderDetails.Unitprice);
            Assert.Equal(0.0, orderDetails.Quantity);
            Assert.Equal(0.0, orderDetails.Discount);
        }

        [Fact]
        public void GetOrderDetailByOrderId_ValidId_ReturnsProductNameUnitPriceAndQuantity()
        {
            var service = new DataService();
            var orderDetails = service.GetOrderDetailsByOrderId(10248);
            Assert.Equal(3, orderDetails.Count);
            Assert.Equal("Queso Cabrales", orderDetails.First().Product.Productname);
            Assert.Equal(14, orderDetails.First().Unitprice);
            Assert.Equal(12, orderDetails.First().Quantity);
        }

        [Fact]
        public void GetOrderDetailByProductId_ValidId_ReturnsOrderDateUnitPriceAndQuantity()
        {
            var service = new DataService();
            var orderDetails = service.GetOrderDetailsByProductId(11);
            Assert.Equal(38, orderDetails.Count);
            Assert.Equal("1997-12-09",
                orderDetails.First().Order.Orderdate.Value
                    .ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("de-DE")));
            Assert.Equal(21, orderDetails.First().Unitprice);
            Assert.Equal(15, orderDetails.First().Quantity);
        }
    }
}