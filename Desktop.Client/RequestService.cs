using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Desktop.Client.Models;
using NLog;

namespace Desktop.Client
{
    public class RequestService : IRequestService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private HttpClient client;

        public string ResponseMessage { get; private set; }

        public RequestService()
        {
            client = new HttpClient();

            string serviceHost = System.Configuration.ConfigurationManager.AppSettings.Get("serviceHost");

            client.BaseAddress = new Uri(serviceHost);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            HttpResponseMessage response = await client.DeleteAsync("api/product/deleteproduct/" + productId);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                ResponseMessage = message;
                logger.Error(message);
                return false;
            }
            return true;
        }

        public async Task<bool> EditProductAsync(Product selectedProduct)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/product/editproduct/" + selectedProduct.Id, selectedProduct);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                ResponseMessage = message;
                logger.Error(message);
                return false; ;
            }
            return true;
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            IList<Product> products = null;
            HttpResponseMessage response = await client.GetAsync("api/product/getallproducts");
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IList<Product>>();
            }
            else
            {
                logger.Error("Cannot get data from service!");
            }

            return products;
        }

        public async Task<bool> AddNewProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/product/addproduct", product);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                ResponseMessage = message;
                return false; ;
            }
            return true;
        }
    }
}
