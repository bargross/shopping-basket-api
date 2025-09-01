using AutoFixture;
using Xunit;
using shopping_basket_api.Calculators;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Tests.Calculators
{
    public class BaskettemDiscountCalculatorTests
    {
        private readonly BasketItemDiscountCalculator _basketTotalCalculator;
        private readonly BasketItem _basketItem;
        private readonly IFixture _fixture;

        public BaskettemDiscountCalculatorTests()
        {
            _fixture = new Fixture();

            _basketTotalCalculator = new BasketItemDiscountCalculator();
            _basketItem = _fixture.Create<BasketItem>();
        }

        [Fact]
        public void CalculateResult_WithVAT_CalculatesCorrectResult()
        {
            var calculatedResult = CalculatePrice(_basketItem, true);
            var calculatorResult = _basketTotalCalculator.CalculateResult(_basketItem, true);

            Assert.True(calculatorResult == calculatedResult);
        }

        [Fact]
        public void CalculateResult_WithoutVAT_CalculatesCorrectResult()
        {
            var calculatedResult = CalculatePrice(_basketItem, false);
            var calculatorResult = _basketTotalCalculator.CalculateResult(_basketItem, false);

            Assert.True(calculatorResult == calculatedResult);
        }

        private float CalculatePrice(BasketItem item, bool withVAT)
        {
            var discountedPrice = item.Discount != default ? item.Price - (item.Price * item.Discount) : item.Price;

            return withVAT ? discountedPrice + (discountedPrice * (float)0.2) : discountedPrice;
        }
    }
}
