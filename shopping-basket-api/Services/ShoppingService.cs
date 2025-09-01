using shopping_basket_api.Calculators;
using shopping_basket_api.Mapping;
using shopping_basket_api.Models;
using shopping_basket_api.SQL.Models;
using shopping_basket_api.SQL.Repositories;

namespace shopping_basket_api.Services
{
    public class ShoppingService(
        IBasketITemRepository basketRepository, 
        IProcessedBasketItemRepository processedBasketItemRepository,
        IDiscountRepository discountRepository,
        IBasketCalculator<IEnumerable<SQL.Models.BasketItem>> basketTotalCalculator): IShoppingService
    {
        public async Task AddToBasketAsync(string basketId, AddBasketItemRequest request)
        {
            if (string.IsNullOrWhiteSpace(basketId)) throw new ArgumentException("Missing basket id.");

            if (request == null) throw new ArgumentNullException(nameof(request));

            if (!request.Items.Any()) throw new ArgumentException($"No items found for basket {basketId} or it might not exist.");

            if (request.Items.Any(x => x == null)) throw new ArgumentException("One or more items in the basket is null."); 

            var mappedItems = request.Items.Select(item => item.MapToSQLBasketItem(basketId));

            await basketRepository.AddManyAsync(mappedItems);
        }

        public async Task AddDiscountCodeAsync(string basketId, Models.Discount discount)
        {
            if (discount == null) throw new ArgumentNullException(nameof(discount));

            if (string.IsNullOrWhiteSpace(basketId)) throw new ArgumentException("Missing basket id.");

            if (string.IsNullOrWhiteSpace(discount.Code)) throw new ArgumentException("Missing discount code.");

            var mappedDiscount = discount.MapToSQLDiscount(basketId);

            await discountRepository.AddManyDiscountsAsync([mappedDiscount]);
        }

        public async Task RemoveItemAsync(string basketId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(basketId)) throw new ArgumentException("Missing basket id.");

            if (string.IsNullOrWhiteSpace(itemId)) throw new ArgumentException("Missing basket item id.");

            await basketRepository.RemoveAsync(basketId, itemId);
        }

        public async Task<float> GetBasketTotalAsync(string basketId, bool withVAT)
        {
            if (string.IsNullOrWhiteSpace(basketId)) throw new ArgumentException("BasketId is null.");
            
            var shoppingItems = await basketRepository.GetByBasketIdAsync(basketId);

            if (!shoppingItems.Any()) throw new ArgumentException($"No items found for basket {basketId} or it might not exist.");
        
            return basketTotalCalculator.CalculateResult(shoppingItems, withVAT);
        }

        public async Task CheckoutAsync(string basketId)
        {
            if (string.IsNullOrWhiteSpace(basketId)) throw new ArgumentException("BasketId is null.");

            var shoppingItems = await basketRepository.GetByBasketIdAsync(basketId);
            if (!shoppingItems.Any()) throw new ArgumentException($"No items found for basket {basketId} or it might not exist.");
            
            var dateProcessed = DateTime.UtcNow;
            var processedItems = shoppingItems.Select(st => st.MapToSQLProcessedBasketItem(dateProcessed)) as IEnumerable<ProcessedBasketItem>;

            // remove from the active baskets
            await basketRepository.RemoveManyAsync(basketId, processedItems.Select(x => x.Id));

            // add to processed baskets
            await processedBasketItemRepository.AddManyAsync(processedItems);
        }
    }
}
