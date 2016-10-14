using NSubstitute;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.DBAccess;
using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        [Test]
        public void ExpectToCallDataAccessGetAllProductsOnReceivedGetRequest()
        {
            ProductController controller = new ProductController();
            controller.DataAccess = Substitute.For<IProductDataAccess>();
            controller.GetAllProducts();
            controller.DataAccess.Received().GetAllProducts();
        }

        [Test]
        public void ExpectToCallDataAccesAddProductOnReceivedPostRequest()
        {
            ProductController controller = new ProductController();
            controller.DataAccess = Substitute.For<IProductDataAccess>();
            Product product = new Product() { LastUpdated = DateTime.Now, Name = "Test", Price = 2, Photo = null };
            controller.AddProduct(product);
            controller.DataAccess.Received().AddProduct(product);
        }

        [Test]
        public void ExpectToCallDataAccesDeleteProductOnReceivedDeleteRequest()
        {
            ProductController controller = new ProductController();
            controller.DataAccess = Substitute.For<IProductDataAccess>();
            controller.DeleteProduct(2);
            controller.DataAccess.Received().DeleteProduct(2);
        }

        [Test]
        public void ExpectToCallDataAccesEditProductOnReceivedPutRequest()
        {
            ProductController controller = new ProductController();
            controller.DataAccess = Substitute.For<IProductDataAccess>();
            Product product = new Product() { LastUpdated = DateTime.Now, Name = "Test", Price = 2, Photo = null };
            controller.EditProduct(2, product);
            controller.DataAccess.Received().EditProduct(2, product);
        }
    }
}
