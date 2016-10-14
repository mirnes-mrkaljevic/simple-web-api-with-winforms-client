using Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client
{
    public interface IRequestService
    {
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> EditProductAsync(Product selectedProduct);
        Task<IList<Product>> GetAllProductsAsync();
        Task<bool> AddNewProductAsync(Product product);

        string ResponseMessage { get; }
    }
}
