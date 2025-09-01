using AutoFixture;
using shopping_basket_api.Mapping;
using shopping_basket_api.Models;
using Xunit;

namespace shopping_basket_api.Tests.Mapping
{
    public class BasketItemMapperTests
    {
        private readonly IFixture _fixture;

        public BasketItemMapperTests()
        {
            _fixture = new Fixture();
        }

        public void MapToSQLBasketItem_ItemIsNull_ReturnsNull()
        {
            var result = BasketItemMapper.MapToSQLBasketItem(null, null);

            Assert.True(result == null);
        }

        public void MapToSQLBasketItem_ItemIsValid_ReturnsMappedItem()
        {
            var item = _fixture.Create<BasketItem>();
            var result = BasketItemMapper.MapToSQLBasketItem(item, "basket-1");

            Assert.True(result != null);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.Price, result.Price);
            Assert.Equal(item.Discount, result.Discount);
        }

        public void MapToSQLProcessedBasketItem_ItemIsNull_ReturnsNull()
        {
            var result = BasketItemMapper.MapToSQLProcessedBasketItem(null, DateTime.UtcNow);

            Assert.True(result == null);
        }

        public void MapToSQLProcessedBasketItem_ItemIsValid_ReturnsMappedItem()
        {
            var item = _fixture.Create<SQL.Models.BasketItem>();
            var dateProcessed = DateTime.UtcNow;

            var result = BasketItemMapper.MapToSQLProcessedBasketItem(item, dateProcessed);

            Assert.True(result != null);
            Assert.Equal(item.BasketId, result.BasketId);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.Price, result.Price);
            Assert.Equal(item.Discount, result.Discount);
            Assert.Equal(dateProcessed, result.DateProcessed);
        }
    }
}
