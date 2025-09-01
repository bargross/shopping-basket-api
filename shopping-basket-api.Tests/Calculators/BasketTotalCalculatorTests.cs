using AutoFixture;
using Xunit;
using shopping_basket_api.Calculators;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Tests.Calculators
{
    public class BasketTotalCalculatorTests
    {
        private readonly BasketTotalCalculator _basketTotalCalculator;
        private readonly BasketItem[] _basketItems;
        private readonly IFixture _fixture;

        public BasketTotalCalculatorTests()
        {
            _fixture = new Fixture();

            _basketTotalCalculator = new BasketTotalCalculator(new BasketItemDiscountCalculator());
            _basketItems = _fixture.CreateMany<BasketItem>().ToArray();
        }

        [Fact]
        public void CalculateResult_WithVAT_CalculatesCorrectResult()
        {
            var calculatedResult = _basketItems.Select(x => CalculatePrice(x, true)).Sum();
            var calculatorResult = _basketTotalCalculator.CalculateResult(_basketItems, true);

            Assert.True(calculatorResult == calculatedResult);
        }

        [Fact]
        public void CalculateResult_WithoutVAT_CalculatesCorrectResult()
        {
            var calculatedResult = _basketItems.Select(x => CalculatePrice(x, false)).Sum();
            var calculatorResult = _basketTotalCalculator.CalculateResult(_basketItems, false);

            Assert.True(calculatorResult == calculatedResult);
        }

        private float CalculatePrice(BasketItem item, bool withVAT)
        {
            var discountedPrice = item.Discount != default ? item.Price - (item.Price * item.Discount) : item.Price;

            return withVAT ? discountedPrice + (discountedPrice * (float)0.2) : discountedPrice;
        }
    }
}
