namespace HotelReservation
{
    public static class PriceCalculator
    {

        public static decimal GetTotalPrice(
            decimal pricePerday,
            int days,
            Season season,
            Discount discount = Discount.None)

        {
            int multiplier = (int)season;
            decimal discountMultiplier = (decimal)discount / 100;

            decimal priceBeforeDiscount = pricePerday * days * multiplier;
            decimal discountAmount = priceBeforeDiscount * discountMultiplier;

            decimal finalPrice = priceBeforeDiscount - discountAmount;

            return finalPrice;
        }
    }
}
