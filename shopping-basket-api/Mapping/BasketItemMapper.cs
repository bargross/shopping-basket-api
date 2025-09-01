namespace shopping_basket_api.Mapping
{
    public static class BasketItemMapper
    {
        public static SQL.Models.BasketItem? MapToSQLBasketItem(this Models.BasketItem item, string basketId)
        {
            if (item == null) return null;

            return new SQL.Models.BasketItem
            {
                BasketId = basketId,
                Discount = item.Discount ?? 0,
                Id = Guid.NewGuid().ToString(),
                Name = item.Name,
                Price = item.Price
            };
        }

        public static SQL.Models.ProcessedBasketItem? MapToSQLProcessedBasketItem(this SQL.Models.BasketItem item, DateTime dateProcessed)
        {
            if (item == null) return null;

            return new SQL.Models.ProcessedBasketItem
            {
                BasketId = item.BasketId,
                Discount = item.Discount,
                Id = Guid.NewGuid().ToString(),
                Name = item.Name,
                Price = item.Price,
                DateProcessed = dateProcessed
            };
        }
    }
}
