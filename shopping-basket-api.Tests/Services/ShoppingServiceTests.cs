using shopping_basket_api.Calculators;
using shopping_basket_api.SQL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shopping_basket_api.Services;
using Xunit;
using AutoFixture;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Tests.Services
{
    public class ShoppingServiceTests
    {
        private readonly Mock<IBasketITemRepository> _basketRepository;
        private readonly Mock<IProcessedBasketItemRepository> _processedBasketItemRepository;
        private readonly Mock<IDiscountRepository> _discountRepository;
        private readonly Mock<IBasketCalculator<IEnumerable<SQL.Models.BasketItem>>> _basketTotalCalculator;

        private readonly IShoppingService _shoppingService;

        private readonly IFixture _fixture;

        public ShoppingServiceTests()
        {
            _fixture = new Fixture();

            _basketRepository = new Mock<IBasketITemRepository>();
            _processedBasketItemRepository = new Mock<IProcessedBasketItemRepository>();
            _discountRepository = new Mock<IDiscountRepository>();
            _basketTotalCalculator = new Mock<IBasketCalculator<IEnumerable<SQL.Models.BasketItem>>>();

            _shoppingService = new ShoppingService(_basketRepository.Object, _processedBasketItemRepository.Object, _discountRepository.Object, _basketTotalCalculator.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddToBasketAsync_NullBasketId_ThrowsArgumentException(string basketId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.AddToBasketAsync(basketId, new Models.AddBasketItemRequest()));

            Assert.Equal(exception.Message, "Missing basket id.");
        }

        [Fact]
        public async Task AddToBasketAsync_RequestIsNull_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _shoppingService.AddToBasketAsync("basket-1", null));
        }

        [Fact]
        public async Task AddToBasketAsync_RequestHasNoItems_ThrowsArgumentException()
        {
            var basketId = "basket-1";
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.AddToBasketAsync(basketId, new Models.AddBasketItemRequest()));

            Assert.Equal(exception.Message, $"No items found for basket {basketId} or it might not exist.");
        }

        [Fact]
        public async Task AddToBasketAsync_RequestHasNullItemAmongstBasketItems_ThrowsArgumentException()
        {
            var items = _fixture.CreateMany<Models.BasketItem>().ToList();

            items.Add(null);

            var request = new Models.AddBasketItemRequest
            {
                Items = items.ToArray()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.AddToBasketAsync("basket-1", request));

            Assert.Equal(exception.Message, "One or more items in the basket is null.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task RemoveItemAsync_NullBasketId_ThrowsArgumentException(string basketId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.RemoveItemAsync(basketId, "item-1"));

            Assert.Equal(exception.Message, "Missing basket id.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task RemoveItemAsync_NullItemId_ThrowsArgumentException(string itemId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.RemoveItemAsync("basket-1", itemId));

            Assert.Equal(exception.Message, "Missing basket item id.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetBasketTotalAsync_NullBasketId_ThrowsArgumentException(string basketId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.GetBasketTotalAsync(basketId, true));

            Assert.Equal(exception.Message, "Missing basket id."); 
        }

        [Fact]
        public async Task GetBasketTotalAsync_BasketDoesNotExist_ThrowsArgumentException()
        {
            var basketId = "basket-1";
            var response = Enumerable.Empty<BasketItem>();

            _basketRepository.Setup(x => x.GetByBasketIdAsync(basketId)).ReturnsAsync(response);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.GetBasketTotalAsync(basketId, true));

            Assert.Equal(exception.Message, $"No items found for basket {basketId} or it might not exist.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task CheckoutAsync_NullBasketId_ThrowsArgumentException(string basketId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.CheckoutAsync(basketId));

            Assert.Equal(exception.Message, "Missing basket id.");
        }

        [Fact]
        public async Task CheckoutAsync_BasketDoesNotExist_ThrowsArgumentException()
        {
            var basketId = "basket-1";
            var response = Enumerable.Empty<BasketItem>();

            _basketRepository.Setup(x => x.GetByBasketIdAsync(basketId)).ReturnsAsync(response);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.CheckoutAsync(basketId));

            Assert.Equal(exception.Message, $"No items found for basket {basketId} or it might not exist.");
        }

        [Fact]
        public async Task CheckoutAsync_RequestIsNull_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _shoppingService.AddDiscountCodeAsync("basket-1", null));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddDiscountCodeAsync_BasketIdIsNull_ThrowsArgumentException(string basketId)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.AddDiscountCodeAsync(basketId, new Models.Discount()));

            Assert.Equal(exception.Message, "Missing basket id.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddDiscountCodeAsync_DiscountCodeIsNull_ThrowsArgumentException(string code)
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _shoppingService.AddDiscountCodeAsync("basket-1", new Models.Discount
            {
                Code = code
            }));

            Assert.Equal(exception.Message, "Missing discount code.");
        }
    }
}
