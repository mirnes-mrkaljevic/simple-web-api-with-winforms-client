using WebAPI.DBAccess;
using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IProductDataAccess dataAccess = new ProductDataAccess();

        public IProductDataAccess DataAccess
        {
            get
            {
                return dataAccess;
            }

            set
            {
                dataAccess = value;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            IList<Product> products = dataAccess.GetAllProducts();
           
            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = dataAccess.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            if (dataAccess.AddProduct(product))
            {
                return Ok(product);
            }
            return Content(HttpStatusCode.BadRequest, "Failed to add product, please try again!");
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            if (dataAccess.DeleteProduct(id))
            {
                return Ok();
            }
            return Content(HttpStatusCode.BadRequest, "Failed to delete product, please try again!");
        }

        [HttpPut]
        public IHttpActionResult EditProduct(int id, Product product)
        {
            if (dataAccess.EditProduct(id, product))
            {
                return Ok();
            }
            return Content(HttpStatusCode.BadRequest, "Failed to edit product, please try again!");
        }
    }
}
