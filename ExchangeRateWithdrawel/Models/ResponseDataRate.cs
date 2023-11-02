namespace ExchangeRateWithdrawel.Models
{
    public class ResponseDataRate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Currency { get; set; }
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public decimal EffectiveBuyingRate { get; set; }
        public decimal EffectiveSellingRate { get; set; }
    }
}
