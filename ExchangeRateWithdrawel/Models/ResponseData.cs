namespace ExchangeRateWithdrawel.Models
{
    public class ResponseData
    {
        public List<ResponseDataRate> Rate { get; set; }
        public string Error { get; set; }
    }
}
