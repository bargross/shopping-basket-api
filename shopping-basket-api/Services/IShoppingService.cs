using shopping_basket_api.Models;

namespace shopping_basket_api.Services
{
    public interface IShoppingService
    {
        Task AddToBasketAsync(string basketId, AddBasketItemRequest request);
        Task AddDiscountCodeAsync(string basketId, Discount discount);
        Task RemoveItemAsync(string basketId, string itemId);
        Task<float> GetBasketTotalAsync(string basketId, bool withVAT);
        Task CheckoutAsync(string basketId);
    }
}
