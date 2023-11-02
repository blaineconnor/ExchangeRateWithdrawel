using ExchangeRateWithdrawel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace ExchangeRateWithdrawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyExchangeRateController : ControllerBase
    {
        [HttpPost]
        public ResponseData Run(RequestData requestData)
        {
            ResponseData result = new ResponseData();

            try
            {
                string link = string.Format("http://www.tcmb.gov.tr/kurlar/{0}.xml",
                    (requestData.IsToday) ? "today" : string.Format("{2}{1}/{0}{1}{2}", requestData.Day.ToString().PadLeft(2, '0'), requestData.Month.ToString().PadLeft(2, '0'), requestData.Year)
                    );

                result.Rate = new List<ResponseDataRate>();

                XmlDocument document = new XmlDocument();
                document.Load(link);
                if(document.SelectNodes("Tarih_Date").Count < 1)
                {
                    result.Error = "Exchange Rate Not Found";
                    return result;
                }
                foreach(XmlNode node in document.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    ResponseDataRate rate = new ResponseDataRate();

                    rate.Code = node.Attributes["Kod"].Value;
                    rate.Name = node["Isim"].InnerText;
                    rate.Currency = int.Parse(node["Unit"].InnerText);
                    rate.BuyingRate = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                    rate.SellingRate = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                    rate.EffectiveBuyingRate = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                    rate.EffectiveSellingRate = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));

                    result.Rate.Add(rate);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
