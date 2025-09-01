namespace shopping_basket_api.Calculators
{
    public interface IBasketCalculator<TValue>
    {
        float CalculateResult(TValue value, bool withVAT);
    }
}
