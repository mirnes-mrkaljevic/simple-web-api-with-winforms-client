using Desktop.Client.Models;
using Desktop.Client.View;
using Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client.Presenters
{
    public class ProductPresenter
    {
        private IProductView view;
        private IRequestService requestService;

        private Product selectedProduct = new Product();

        public ProductPresenter(IProductView view, IRequestService requestService)
        {
            this.view = view;
            this.requestService = requestService;
            SubsribeToViewEvents();
        }

        private void SubsribeToViewEvents()
        {
            view.ViewLoad += View_Load;
            view.AddNewProduct += View_AddNewProduct;
            view.ProductSelected += View_ProductSelected;
            view.ModifyProduct += View_ModifyProduct;
            view.DeleteProduct += View_DeleteProduct;
        }

        private async void View_DeleteProduct(object sender, int productId)
        {

            if(!await requestService.DeleteProductAsync(productId))
            {
                view.ShowMessage(requestService.ResponseMessage);
                return;
            }
           
            ReloadData();
        }

        private async void View_ModifyProduct(object sender, ProductViewModel viewModel)
        {
            Validator validator = new Validator();
            string validationMessage = string.Empty;
            if (!validator.ValidateProductName(viewModel.NameText, out validationMessage) || !validator.ValidateProductPrice(viewModel.PriceText, out validationMessage))
            {
                view.ShowMessage(validationMessage);
                return;
            }

            PopulateProductFromViewModel(selectedProduct, viewModel);
           
            if (!await requestService.EditProductAsync(selectedProduct))
            {
                view.ShowMessage(requestService.ResponseMessage);
                return;
            }

            ReloadData();
            view.ClearInputControls();
        }

        private void View_ProductSelected(object sender, int selectedProductId)
        {
            selectedProduct.Id = selectedProductId;
        }

        private void View_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private async void ReloadData()
        {
            IList<Product> products = await requestService.GetAllProductsAsync();

            if (products == null)
            {
                view.ShowMessage(requestService.ResponseMessage);
                return;
            }

            view.PopulateDataGridView(products);
        }

        private void View_AddNewProduct(object sender, ProductViewModel productViewModel)
        {
            Validator validator = new Validator();
            string validationMessage = string.Empty;
            if (!validator.ValidateProductName(productViewModel.NameText, out validationMessage) || !validator.ValidateProductPrice(productViewModel.PriceText, out validationMessage))
            {
                view.ShowMessage(validationMessage);
                return;
            }

            Product product = new Product();
            PopulateProductFromViewModel(product, productViewModel);

            AddNewProductAsync(product);
          
        }

        private async void AddNewProductAsync(Product product)
        {
            if (!await requestService.AddNewProductAsync(product))
            {
                view.ShowMessage(requestService.ResponseMessage);
                return;
            }
            ReloadData();
            view.ClearInputControls();
        }

        private void PopulateProductFromViewModel(Product product, ProductViewModel productViewModel)
        {
            product.Name = productViewModel.NameText;
            product.Price = float.Parse(productViewModel.PriceText);
            string photoPath = productViewModel.PhotoPathText;

            if (!string.IsNullOrEmpty(photoPath))
            {
                product.Photo = File.ReadAllBytes(photoPath);
            }
        }

    }
}
