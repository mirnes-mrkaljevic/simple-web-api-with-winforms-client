using NSubstitute;
using NUnit.Framework;
using Desktop.Client.Models;
using Desktop.Client.Presenters;
using Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client.Tests.Presenters
{
    [TestFixture]
    public class ProductPresenterTests
    {
        [Test]
        public void ExpectToCallAddNewProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IRequestService requestService = Substitute.For<IRequestService>();
            ProductPresenter presenter = new ProductPresenter(view, requestService);
           
            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = "Test",
                PriceText = "2"
            };
           
            view.AddNewProduct += Raise.Event<EventHandler<ProductViewModel>>(view, viewModel);
            Received.InOrder(async () =>
            {
                await requestService.Received().AddNewProductAsync(Arg.Is<Product>(x=>x.Price == 2 && x.Name == "Test"));
            });

        }

        [Test]
        public void ExpectToCallEditProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IRequestService requestService = Substitute.For<IRequestService>();
            ProductPresenter presenter = new ProductPresenter(view, requestService);

            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = "Test",
                PriceText = "2"
            };

            view.ModifyProduct += Raise.Event<EventHandler<ProductViewModel>>(view, viewModel);
            Received.InOrder(async () =>
            {
                await requestService.Received().EditProductAsync(Arg.Is<Product>(x => x.Price == 2 && x.Name == "Test"));
            });

        }

        [Test]
        public void ExpectToCallDeleteProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IRequestService requestService = Substitute.For<IRequestService>();
            ProductPresenter presenter = new ProductPresenter(view, requestService);

  

            view.DeleteProduct += Raise.Event<EventHandler<int>>(view, 2);
            Received.InOrder(async () =>
            {
                await requestService.Received().DeleteProductAsync(2);
            });
        }

        [Test]
        public void ExpectToCallGetAllProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IRequestService requestService = Substitute.For<IRequestService>();
            ProductPresenter presenter = new ProductPresenter(view, requestService);

            view.ViewLoad += Raise.EventWith(view, new EventArgs());
            Received.InOrder(async () =>
            {
                await requestService.Received().GetAllProductsAsync();
            });
        }



    }
}
