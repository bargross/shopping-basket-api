using AutoFixture;
using shopping_basket_api.Mapping;
using shopping_basket_api.Models;

namespace shopping_basket_api.Tests.Mapping
{
    public class DiscountMapperTests
    {
        private readonly IFixture _fixture;

        public DiscountMapperTests()
        {
            _fixture = new Fixture();
        }

        public void MapToSQLBasketItem_ItemIsNull_ReturnsNull()
        {
            var result = DiscountMapper.MapToSQLDiscount(null, null);

            Assert.True(result == null);
        }

        public void MapToSQLBasketItem_ItemIsValid_ReturnsMappedItem()
        {
            var item = _fixture.Create<Discount>();
            var basketId = "basket-1";

            var result = DiscountMapper.MapToSQLDiscount(item, basketId);

            Assert.True(result != null);
            Assert.Equal(item.Code, result.Id);
            Assert.Equal(item.DiscountValue, result.DiscountValue);
            Assert.Equal(basketId, result.BasketId);
        }
    }
}
