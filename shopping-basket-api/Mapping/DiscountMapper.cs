namespace shopping_basket_api.Mapping
{
    public static class DiscountMapper
    {
        public static SQL.Models.Discount MapToSQLDiscount(this Models.Discount discount, string basketId)
        {
            if (discount == null) return null;

            return new SQL.Models.Discount
            {
                Id = discount.Code,
                BasketId = basketId,
                DiscountValue = discount.DiscountValue,
            };
        }
    }
}
