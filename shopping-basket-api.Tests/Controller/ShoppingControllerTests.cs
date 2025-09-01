using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using shopping_basket_api.Controllers;
using shopping_basket_api.Models;
using shopping_basket_api.Services;
using Xunit;

namespace shopping_basket_api.Tests.Controller
{
    public class ShoppingControllerTests
    {
        private readonly Mock<IShoppingService> _shoppingServiceMock;
        private readonly ShoppingController _shoppingController;

        private readonly IFixture _fixture;

        public ShoppingControllerTests()
        {
            _fixture = new Fixture();

            _shoppingServiceMock = new Mock<IShoppingService>();

            _shoppingController = new ShoppingController(_shoppingServiceMock.Object);
        }

        [Fact]
        public async Task AddBasketItems_ProcessedRequest_ReturnsNoContent()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            var result = (await _shoppingController.AddBasketITems(basketId, request)) as NoContentResult;

            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }


        [Fact]
        public async Task AddBasketItems_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.AddToBasketAsync(It.IsAny<string>(), It.IsAny<AddBasketItemRequest>()))
                .ThrowsAsync(new ArgumentException("something happened"));

            var result = (await _shoppingController.AddBasketITems(basketId, request)) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task AddBasketItems_ServiceThrowsUnexpectedException_ReturnsServerError()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.AddToBasketAsync(It.IsAny<string>(), It.IsAny<AddBasketItemRequest>()))
                .ThrowsAsync(new OperationCanceledException("something happened"));

            var result = (await _shoppingController.AddBasketITems(basketId, request)) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task AddDiscountToBasket_ProcessedRequest_ReturnsNoContent()
        {
            var request = _fixture.Create<Discount>();
            var basketId = "basket-1";

            var result = (await _shoppingController.AddDiscountToBasket(basketId, request)) as NoContentResult;

            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }


        [Fact]
        public async Task AddDiscountToBasket_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var request = _fixture.Create<Discount>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.AddDiscountCodeAsync(It.IsAny<string>(), It.IsAny<Discount>()))
                .ThrowsAsync(new ArgumentException("something happened"));

            var result = (await _shoppingController.AddDiscountToBasket(basketId, request)) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task AddDiscountToBasket_ServiceThrowsUnexpectedException_ReturnsServerError()
        {
            var request = _fixture.Create<Discount>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.AddDiscountCodeAsync(It.IsAny<string>(), It.IsAny<Discount>()))
                .ThrowsAsync(new OperationCanceledException("something happened"));

            var result = (await _shoppingController.AddDiscountToBasket(basketId, request)) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task RemoveBasketItem_ProcessedRequest_ReturnsNoContent()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";
            var itemId = "item-1";

            var result = (await _shoppingController.RemoveBasketItem(basketId, itemId)) as NoContentResult;

            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task RemoveBasketItem_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";
            var itemId = "item-1";

            _shoppingServiceMock.Setup(x => x.RemoveItemAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("something happened"));

            var result = (await _shoppingController.RemoveBasketItem(basketId, itemId)) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task RemoveBasketItem_ServiceThrowsUnexpectedException_ReturnsServerError()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";
            var itemId = "item-1";

            _shoppingServiceMock.Setup(x => x.RemoveItemAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new OperationCanceledException("something happened"));

            var result = (await _shoppingController.RemoveBasketItem(basketId, itemId)) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetBasketTotal_ProcessedRequest_ReturnsNoContent()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            var result = (await _shoppingController.GetBasketTotal(basketId, true)) as NoContentResult;

            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GetBasketTotal_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.GetBasketTotalAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ThrowsAsync(new ArgumentException("something happened"));

            var result = (await _shoppingController.GetBasketTotal(basketId, true)) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetBasketTotal_ServiceThrowsUnexpectedException_ReturnsServerError()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.GetBasketTotalAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ThrowsAsync(new OperationCanceledException("something happened"));

            var result = (await _shoppingController.GetBasketTotal(basketId, true)) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Checkout_ProcessedRequest_ReturnsNoContent()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            var result = (await _shoppingController.Checkout(basketId)) as NoContentResult;

            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task Checkout_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.CheckoutAsync(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException("something happened"));

            var result = (await _shoppingController.Checkout(basketId)) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Checkout_ServiceThrowsUnexpectedException_ReturnsServerError()
        {
            var request = _fixture.Create<AddBasketItemRequest>();
            var basketId = "basket-1";

            _shoppingServiceMock.Setup(x => x.CheckoutAsync(It.IsAny<string>()))
                .ThrowsAsync(new OperationCanceledException("something happened"));

            var result = (await _shoppingController.Checkout(basketId)) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
